using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace fried_fame_client_windows.Classes.AutoAPI
{
    class Node
    {
        public class VPNNode
        {
            public int id;
            public string country;
            public string city;
            public string ip;
            public string hostname;
            public bool ovpn;
            public bool pptp;

            private int ping = -1;

            /// <summary>
            /// Alias of Node.CanConnect
            /// </summary>
            /// <returns></returns>
            public async Task<VPNNodeConnectable> CanConnect()
            {
                return await Node.CanConnect(this);
            }

            /// <summary>
            /// Alias of Node.GetOpenVPNConfig
            /// </summary>
            /// <returns></returns>
            public async Task<string> GetOpenVPNConfig()
            {
                return await Node.GetOpenVPNConfig(this);
            }

            /// <summary>
            /// Gets latency of server
            /// </summary>
            /// <returns></returns>
            public async Task<int> GetPing()
            {
                Logging.Info(string.Format("Node VPNNode GetPing - {0}, {1}", this.id, this.hostname));

                if (ping == -1)
                    return ping = await this._GetPing(0);
                else
                    return ping;
            }

            private async Task<int> _GetPing(int retryCount)
            {
                try
                {
                    if(retryCount > 3)
                    {
                        return -1;
                    }

                    Ping ping = new Ping();
                    PingReply reply = await ping.SendPingAsync(hostname, 500);
                    if(reply.Status == IPStatus.Success)
                    {
                        return (int)reply.RoundtripTime;
                    }
                    else
                    {
                        return await this._GetPing(retryCount + 1);
                    }
                }
                catch
                {
                    return await this._GetPing(retryCount + 1);
                }
            }
        }

        public struct VPNNodeConnectable
        {
            public bool permit;
            public string message;
        }

        public static async Task<VPNNode[]> GetNodes()
        {
            Logging.Info("Node GetNodes - Started");

            if (!Session.Authenticated)
            {
                Logging.Info("Node GetNodes - Unauthorized");
                throw new UnauthorizedAccessException("User not logged in");
            }

            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage())
                {
                    httpRequest.Method = HttpMethod.Post;
                    httpRequest.RequestUri = new Uri(await Context.Get("autoapi-list"));
                    httpRequest.Headers.Add("x-try-ignore-session", "true");
                    httpRequest.Headers.Add("x-api-token", Session.Token);

                    using (var httpResponse = await httpClient.SendAsync(httpRequest))
                    {
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            Logging.Info(string.Format("Node GetNodes - response bad status {0}", (int)httpResponse.StatusCode));
                            throw new Exception(string.Format("Bad response status for AutoAPI context retreival. Status: {0}", (int)httpResponse.StatusCode));
                        }

                        // ensuring content type was sent
                        if (httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                        {
                            Logging.Info(string.Format("Node GetNodes - response bad content type {0}", httpResponse.Content.Headers.ContentType.MediaType));
                            throw new Exception(string.Format("Unexpected content type, {0}", httpResponse.Content.Headers.ContentType.MediaType));
                        }

                        // reads response into json object
                        JObject jsonResponse = JObject.Parse(
                            await httpResponse.Content.ReadAsStringAsync());

                        string message = jsonResponse["message"].ToString();
                        if (message != "okay")
                        {
                            Logging.Info(string.Format("Node GetNodes - Failed to get nodes {0}", message));
                            throw new Exception("Failed to get nodes: " + message);
                        }

                        var ret = new List<VPNNode>();

                        var servers = jsonResponse["servers"].ToArray();
                        foreach (var server in servers)
                        {
                            var tempNode = new VPNNode()
                            {
                                id = int.Parse(server["id"].ToString()),
                                country = server["country"].ToString(),
                                city = server["city"].ToString(),
                                ip = server["ip"].ToString(),
                                hostname = server["hostname"].ToString(),
                                ovpn = server["ovpn"].ToString() == "1",
                                pptp = server["pptp"].ToString() == "1",
                            };

                            Logging.Info(string.Format(
                                "Node GetNodes - Node: {0}, {1}", tempNode.id, tempNode.hostname));

                            ret.Add(tempNode);
                        }

                        return ret.ToArray();
                    }
                }
            }
        }

        public static async Task<VPNNodeConnectable> CanConnect(VPNNode node)
        {
            Logging.Info(string.Format("Node CanConnect - {0}, {1}", node.id, node.hostname));

            if (!Session.Authenticated)
            {
                Logging.Info("Node CanConnect - Unauthorized");
                throw new UnauthorizedAccessException("User not logged in");
            }

            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage())
                {
                    httpRequest.Method = HttpMethod.Post;
                    httpRequest.RequestUri = new Uri(await Context.Get("autoapi-connect"));
                    httpRequest.Headers.Add("x-try-ignore-session", "true");
                    httpRequest.Headers.Add("x-api-token", Session.Token);
                    httpRequest.Content = new StringContent(
                        string.Format(
                            "node={0}",
                            HttpUtility.UrlEncode(node.id.ToString())),
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded");

                    using (var httpResponse = await httpClient.SendAsync(httpRequest))
                    {
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            Logging.Info(string.Format("Node CanConnect - response bad status {0}", (int)httpResponse.StatusCode));
                            throw new Exception(string.Format("Bad response status for AutoAPI context retreival. Status: {0}", (int)httpResponse.StatusCode));
                        }

                        // ensuring content type was sent
                        if (httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                        {
                            Logging.Info(string.Format("Node CanConnect - response bad content type {0}", httpResponse.Content.Headers.ContentType.MediaType));
                            throw new Exception(string.Format("Unexpected content type, {0}", httpResponse.Content.Headers.ContentType.MediaType));
                        }

                        // reads response into json object
                        JObject jsonResponse = JObject.Parse(
                            await httpResponse.Content.ReadAsStringAsync());

                        Logging.Info(string.Format("Node CanConnect - {0}", jsonResponse["message"].ToString()));

                        return new VPNNodeConnectable()
                        {
                            message = jsonResponse["message"].ToString(),
                            permit = jsonResponse["permit"].ToString() == "1"
                        };
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets OpenVPN configuration file from server
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static async Task<string> GetOpenVPNConfig(VPNNode node)
        {
            Logging.Info(string.Format("Node GetOpenVPNConfig - {0}, {1}", node.id, node.hostname));

            if (!Session.Authenticated)
            {
                Logging.Info("Node GetOpenVPNConfig - Unauthorized");
                throw new UnauthorizedAccessException("User not logged in");
            }

            using (var httpClient = new HttpClient())
            {
                using (var httpRequest = new HttpRequestMessage())
                {
                    httpRequest.Method = HttpMethod.Post;
                    httpRequest.RequestUri = new Uri(await Context.Get("autoapi-openvpnconfig"));
                    httpRequest.Headers.Add("x-try-ignore-session", "true");
                    httpRequest.Headers.Add("x-api-token", Session.Token);
                    httpRequest.Content = new StringContent(
                        string.Format(
                            "node={0}",
                            HttpUtility.UrlEncode(node.id.ToString())),
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded");

                    using (var httpResponse = await httpClient.SendAsync(httpRequest))
                    {
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            Logging.Info(string.Format("Node GetOpenVPNConfig - response bad status {0}", (int)httpResponse.StatusCode));
                            throw new Exception(string.Format("Bad response status for AutoAPI context retreival. Status: {0}", (int)httpResponse.StatusCode));
                        }

                        // ensuring content type was sent
                        if (httpResponse.Content.Headers.ContentType.MediaType != "application/ovpn-config")
                        {
                            Logging.Info(string.Format("Node GetOpenVPNConfig - response bad content type {0}", httpResponse.Content.Headers.ContentType.MediaType));
                            throw new Exception(string.Format("Unexpected content type, {0}", httpResponse.Content.Headers.ContentType.MediaType));
                        }

                        Logging.Info("Node GetOpenVPNConfig Success");
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
