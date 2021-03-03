using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes.AutoAPI
{
    class Context
    {
        private static Dictionary<string, string> m_context = null;

        public static async Task Initialize(bool force = false)
        {
            Logging.Info("Context Initialize - starting");
            if (!force && m_context != null && m_context.Count > 0)
            {
                Logging.Info("Context Initialize - Already initialized");
                return;
            }
            m_context = new Dictionary<string, string>();

            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage())
                {
                    httpRequest.Method = HttpMethod.Post;
                    httpRequest.RequestUri = new Uri(Constants.REMOTE_CONTEXT);
                    httpRequest.Headers.Add("x-try-ignore-session", "true");

                    using (var httpResponse = await httpClient.SendAsync(httpRequest))
                    {
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            Logging.Info(string.Format("Context Initialize - response bad status {0}", (int)httpResponse.StatusCode));
                            throw new Exception(string.Format("Bad response status for AutoAPI context retreival. Status: {0}", (int)httpResponse.StatusCode));
                        }

                        // ensuring content type was sent
                        if (httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                        {
                            Logging.Info(string.Format("Context Initialize - response bad content type {0}", httpResponse.Content.Headers.ContentType.MediaType));
                            throw new Exception(string.Format("Unexpected content type, {0}", httpResponse.Content.Headers.ContentType.MediaType));
                        }

                        // Buffering resposne into JObject
                        JObject responseParsed = JObject.Parse(
                            await httpResponse.Content.ReadAsStringAsync());

                        // adding to context
                        foreach (var index in responseParsed)
                        {
                            Classes.Logging.Info(string.Format("Context {0}: {1}", index.Key, index.Value.ToString()));
                            m_context.Add(index.Key, index.Value.ToString());
                        }
                    }
                }
            }
        }

        public static async Task<string> Get(string key)
        {
            await Context.Initialize();

            string value = string.Empty;
            if(m_context.TryGetValue(key, out value))
            {
                return value;
            }

            throw new KeyNotFoundException();
        }
    }
}
