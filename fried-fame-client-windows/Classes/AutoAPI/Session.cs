using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace fried_fame_client_windows.Classes.AutoAPI
{
    class Session
    {
        /// <summary>
        /// Default authentication status.
        /// </summary>
        private static readonly AuthenticationStatus defaultAuthenticationStatus = new AuthenticationStatus()
        {
            isAuthenticated = false,
            message = string.Empty,
            token = string.Empty,
            nodeAuthentication = string.Empty,
            username = string.Empty,
            userId = 0
        };

        /// <summary>
        /// Curernt authentication status.
        /// </summary>
        private static AuthenticationStatus authenticationStatus = defaultAuthenticationStatus;

        /// <summary>
        /// Authentication status
        /// </summary>
        [Serializable]
        public struct AuthenticationStatus
        {
            public bool isAuthenticated;
            public string message;
            public string token;
            public string nodeAuthentication;
            public string username;
            public int userId;
        }

        /// <summary>
        /// Is session Authenticated
        /// </summary>
        public static bool Authenticated { get { return authenticationStatus.isAuthenticated; } }

        /// <summary>
        /// Gets authentication token
        /// </summary>
        public static string Token { get { return authenticationStatus.token; } }

        /// <summary>
        /// Node authentication token.
        /// </summary>
        public static string NodeAuthentication { get { return authenticationStatus.nodeAuthentication; } }

        /// <summary>
        ///  Gets ID of authenticated user
        /// </summary>
        public static int UserID { get { return authenticationStatus.userId; } }

        /// <summary>
        /// Location to the session file path for current logged in user
        /// </summary>
        private static string SessionFilePath
        {
            get
            {
                string usernameClean = Regex.Replace(Environment.UserName, "[^A-Za-z0-9 -]", "");
                string filename = string.Format("SecureSession-{0}.encr", usernameClean);
                return Path.Combine(Environment.CurrentDirectory, filename);
            }
        }

        /// <summary>
        /// Authenticated usign a username and password
        /// </summary>
        /// <param name="username">Username to authenticate with</param>
        /// <param name="password">Password to authenticate with.</param>
        /// <returns></returns>
        public static async Task<AuthenticationStatus> Authenticate(string username, string password)
        {
            Logging.Info(string.Format("Session - Authenticating {0}", username));

            using (var httpClient = new HttpClient())
            {
                using(var httpRequest = new HttpRequestMessage())
                {
                    httpRequest.Method = HttpMethod.Post;
                    httpRequest.RequestUri = new Uri(await Context.Get("autoapi-auth"));
                    httpRequest.Headers.Add("x-try-ignore-session", "true");

                    httpRequest.Content = new StringContent(
                        string.Format(
                            "username={0}&password={1}",
                            HttpUtility.UrlEncode(username),
                            HttpUtility.UrlEncode(password)),
                        Encoding.UTF8,
                        "application/x-www-form-urlencoded");

                    using(var httpResponse = await httpClient.SendAsync(httpRequest))
                    {
                        if(!httpResponse.IsSuccessStatusCode)
                        {
                            Logging.Info(string.Format("Session Authenticate - response bad status {0}", (int)httpResponse.StatusCode));
                            throw new Exception(string.Format("Bad response status for AutoAPI context retreival. Status: {0}", (int)httpResponse.StatusCode));
                        }

                        // ensuring content type was sent
                        if (httpResponse.Content.Headers.ContentType.MediaType != "application/json")
                        {
                            Logging.Info(string.Format("Session Authenticate - response bad content type {0}", httpResponse.Content.Headers.ContentType.MediaType));
                            throw new Exception(string.Format("Unexpected content type, {0}", httpResponse.Content.Headers.ContentType.MediaType));
                        }

                        // reads response into json object
                        JObject jsonResponse = JObject.Parse(
                            await httpResponse.Content.ReadAsStringAsync());

                        string message = jsonResponse["message"].ToString();
                        if (message == "okay")
                        {
                            Logging.Info(string.Format("Session Authenticate - Authenticated {0}", username));
                            return authenticationStatus = new AuthenticationStatus()
                            {
                                isAuthenticated = true,
                                message = message,
                                token = jsonResponse["token"].ToString(),
                                nodeAuthentication = jsonResponse["node-auth"].ToString(),
                                username = jsonResponse["username"].ToString(),
                                userId = int.Parse(jsonResponse["user-id"].ToString()),
                            };
                        }

                        Logging.Info(string.Format("Session Authenticate - Failed to authenticated {0}, {1}", username, message));
                        return authenticationStatus = new AuthenticationStatus()
                        {
                            isAuthenticated = false,
                            message = message,
                            token = string.Empty,
                            nodeAuthentication = string.Empty,
                            username = string.Empty,
                            userId = 0
                        };
                    }
                }
            }
        }

        public static async Task<bool> Deauthenticate()
        {
            Logging.Info("Session - Deauthenticating");
            authenticationStatus = defaultAuthenticationStatus;
            return true;
        }

        /// <summary>
        /// Saves current session to disk.
        /// </summary>
        /// <returns></returns>
        public static async Task SaveSession()
        {
            Logging.Info("Session - SaveSession");
            await Task.Run(() =>
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(SessionFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    Logging.Info(string.Format("Session - Save Session {0}", SessionFilePath));
                    formatter.Serialize(stream, authenticationStatus);
                }

                // encrypts file so only the current logged in user can access it.
                File.Encrypt(SessionFilePath);
            });
        }

        /// <summary>
        /// Loads session from session file.
        /// </summary>
        /// <returns></returns>
        public static async Task<AuthenticationStatus> LoadSession()
        {
            return await Task.Run(() =>
            {
                try
                {
                    Logging.Info("Session LoaadSession");

                    IFormatter formatter = new BinaryFormatter();
                    using (Stream stream = new FileStream(SessionFilePath, FileMode.Open, FileAccess.Read))
                    {
                        AuthenticationStatus fileSessionStatus = (AuthenticationStatus)formatter.Deserialize(stream);
                        if (fileSessionStatus.isAuthenticated)
                        {
                            Logging.Info(string.Format("Session LoadSession - Loading session for \"{0}\"", fileSessionStatus.username));

                            // Setting current authentication status and returning it.
                            return authenticationStatus = fileSessionStatus;
                        }
                        else
                        {
                            Logging.Info("Session LoadSession - Session not authenticated");

                            // Not authenticated in file session, so return current
                            // session and ignore all of this.
                            return authenticationStatus;
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Logging.Info("Session LoadSession - Session not found");
                    return authenticationStatus;
                }
            });
        }
    }
}
