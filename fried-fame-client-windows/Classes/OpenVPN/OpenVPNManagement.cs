using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes.OpenVPN
{
    /// <summary>
    /// OpenVPN Management Implemtnation
    /// 
    /// This code was recycled from an old project. It certainly has its issues,
    /// but it will suffice.
    /// </summary>
    class OpenVPNManagement
    {
        public enum FailureReason : byte
        {
            AUTHENTICATION,
            UNKNOWN
        }

        public enum State : byte
        {
            CONNECTING,
            WAIT,
            AUTH,
            GET_CONFIG,
            ASSIGN_IP,
            ADD_ROUTES,
            CONNECTED,
            RECONNECTING,
            EXITING,
            UNKNOWN
        }

        private static TcpClient sock = null;
        private static NetworkStream networkStream = null;

        public delegate void OnConnectEventHandler(string localIp, string publicIp);
        public delegate void OnBandwidthEventHandler(ulong bytesIn, ulong bytesOut);
        public delegate void OnFailureEventHandler(FailureReason reason);
        public delegate void OnCloseEventHandler();
        public delegate void OnStateChangeEventHandler(State state, string localIP, string publicIP, uint unixTimestamp);

        public static event OnConnectEventHandler OnConnectEvent;
        public static event OnBandwidthEventHandler OnBandwidthEvent;
        public static event OnFailureEventHandler OnFailureEvent;
        public static event OnCloseEventHandler OnCloseEvent;
        public static event OnStateChangeEventHandler OnStateChangeEvent;

        private static string username = null;
        private static string password = null;

        private static readonly char[] CRLF = { '\r', '\n' };

        /// <summary>
        /// Starts the management interface with a given IP and port
        /// </summary>
        /// <param name="address">The local address we want to listen to (more than likely a loopback)</param>
        /// <param name="port">The port which we will use for the communications</param>
        /// <returns></returns>
        public static async Task Start(IPAddress address, int port, string username = null, string password = null)
        {
            Logging.Info("OpenVPNManagement - starting");
            // Checking if management is already active. If it is, disabling it.
            if (sock != null)
            {
                Logging.Info("OpenVPNManagement - Socket not null, destroying");
                if (sock.Connected)
                {
                    Logging.Info("OpenVPNManagement - Socket connected, closing");
                    sock.Close();
                }

                sock = null;
                networkStream = null;
                Logging.Info("OpenVPNManagement - Socket nulled");
            }


            // Creating new TCP client and configuring it
            sock = new TcpClient();
            sock.ReceiveTimeout = 400;
            sock.SendTimeout = 400;
            Logging.Info("OpenVPNManagement - Socket Created");


            // Connecting to management
            await sock.ConnectAsync(address, port);


            // Checking we connected successfully
            if (!sock.Connected)
            {
                Logging.Info("OpenVPNManagement - Socket failed to connect");
                throw new Exception("Unable to start connection with management interface");
            }


            Logging.Info("OpenVPNManagement - Socket connected");
            Logging.Info("OpenVPNManagement - Sending initial commands");


            // Getting network stream, and sending initial commands.
            networkStream = sock.GetStream();
            await SendCommand("state on");
            await SendCommand("bytecount 2");
            await SendCommand("hold off");
            await SendCommand("hold release");


            Logging.Info("OpenVPNManagement - Starting read loop");
            ReadLoop();

            // Setting OpenVPN management credetnials for authentication.
            OpenVPNManagement.username = username;
            OpenVPNManagement.password = password;
            Logging.Info("OpenVPNManagement - Startup procedure finished.");
        }

        /// <summary>
        /// Begins the read loop.
        /// </summary>
        /// <returns></returns>
        private static async Task ReadLoop()
        {
            try
            {
                Logging.Info("OpenVPNManagement - ReadLoop starting.");

                while (sock != null &&
                    networkStream != null &&
                    sock.Connected &&
                    networkStream.CanRead)
                {
                    const int bufferLength = 1024;
                    byte[] buffer = new byte[bufferLength];
                    int readCount = await networkStream.ReadAsync(buffer, 0, bufferLength);
                    if (readCount > 0)
                    {
                        // NOTE: Don't add await.
                        ProcessPacket(buffer);
                    }
                    else
                    {
                        await Task.Delay(500);
                    }
                }

                Logging.Info("OpenVPNManagement - ReadLoop stopped.");
                await OpenVPNManagement.Close();
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Processes a management packet.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static async Task ProcessPacket(byte[] buffer)
        {
            Logging.Info("OpenVPNManagement - ProcessingPacket.");

            string[] lines = Encoding.UTF8.GetString(buffer).Split(CRLF);

            foreach (string line in lines)
            {
                // Validating line
                if (line.Length < 3 || line[0] != '>')
                    continue;

                // Finding where the command ends
                int commandEnd = line.IndexOf(':');
                if (commandEnd <= 0)
                    return;

                // Subtracting command and parameter
                string command = line.Substring(1, commandEnd - 1);
                string parameter = line.Substring(commandEnd + 1);

                Logging.Info(string.Format("OpenVPNManagement - CMD {0}: {1}", command, parameter));

                switch (command)
                {
                    case "BYTECOUNT":
                        await HandleByteCount(command, parameter);
                        break;

                    case "HOLD":
                        await SendCommand("hold release");
                        break;

                    case "PASSWORD":
                        await HandlePassword(command, parameter);
                        break;

                    case "STATE":
                        await HandleState(command, parameter);
                        break;

                    // Ignored.
                    case "INFO":
                    case "LOG":
                        break;

                    // Deal with as needed.
                    default:
                        break;
                }
            }
        }

        private static async Task HandleByteCount(string command, string parameter)
        {
            ulong bytesReceived = 0;
            ulong bytesSent = 0;

            string[] paramSplit = parameter.Split(
                new char[] { ',' },
                2);

            // Param split requires two values, bytesReceived and bytesSent.
            if (paramSplit.Length != 2)
                throw new Exception("OpenVPNManagement HandleByteCount invalid parameter split length");

            // Parsing byte count, and invoking the event
            if(!ulong.TryParse(paramSplit[0], out bytesReceived) ||
                !ulong.TryParse(paramSplit[1], out bytesSent))
            {
                throw new Exception("OpenVPNManagement HandleByteCount failed to parse parameter");
            }

            Logging.Info(string.Format("OpenVPNManagement - HandleByteCount Sent: {0}, Received: {1}", bytesSent, bytesReceived));

            OnBandwidthEvent(bytesReceived, bytesSent);
        }

        private static async Task HandlePassword(string command, string parameter)
        {
            switch (parameter)
            {
                case "Need 'Private Key' password":
                    Logging.Info("OpenVPNManagement - Authentication - Private Key (unimplemented).");

                    // We do not support private key's, so let's do nothing.. as this should never happen.
                    OnFailureEvent(FailureReason.AUTHENTICATION);
                    break;

                case "Need 'Auth' username/password":
                    Logging.Info("OpenVPNManagement - Authentication - userpass");

                    // Send username
                    await SendCommand(string.Format("username \"Auth\" \"{0}\"",
                       username.Replace("\\", "\\\\").Replace("\"", "")));

                    Logging.Info("OpenVPNManagement - Authentication - userpass - Username sent");


                    // Delaying. there was a bug where it would send username twice.
                    // so this is just a padding to give the remote management some
                    // time to deal with it.
                    await Task.Delay(500);

                    // Send password
                    await SendCommand(string.Format("password \"Auth\" \"{0}\"",
                        password.Replace("\\", "\\\\").Replace("\"", "")));

                    Logging.Info("OpenVPNManagement - Authentication - userpass - Password sent");
                    break;

                default:
                    if (parameter.StartsWith("Verification Faied"))
                    {
                        Logging.Info("OpenVPNManagement - Authentication - Failed");
                        OnFailureEvent(FailureReason.AUTHENTICATION);
                    }
                    break;
            }
        }

        private static async Task HandleState(string command, string parameter)
        {
            string[] tempState = parameter.Split(',');
            uint.TryParse(tempState[0], out uint unixTime);
            string stateName = tempState[1];
            string localIP = tempState[3];
            string publicIP = tempState[4];

            Logging.Info(string.Format("OpenVPNManagement - STATE \"{0}\" \"{1}\" \"{2}\" \"{3}\"", stateName, unixTime, localIP, publicIP));

            switch (stateName)
            {
                case "CONNECTING":
                    OnStateChangeEvent(State.CONNECTING, localIP, publicIP, unixTime);
                    break;

                case "WAIT":
                    OnStateChangeEvent(State.WAIT, localIP, publicIP, unixTime);
                    break;

                case "AUTH":
                    OnStateChangeEvent(State.AUTH, localIP, publicIP, unixTime);
                    break;

                case "GET_CONFIG":
                    OnStateChangeEvent(State.GET_CONFIG, localIP, publicIP, unixTime);
                    break;

                case "ASSIGN_IP":
                    OnStateChangeEvent(State.ASSIGN_IP, localIP, publicIP, unixTime);
                    break;

                case "ADD_ROUTES":
                    OnStateChangeEvent(State.ADD_ROUTES, localIP, publicIP, unixTime);
                    break;

                case "CONNECTED":
                    OnStateChangeEvent(State.CONNECTED, localIP, publicIP, unixTime);
                    OnConnectEvent(localIP, publicIP);
                    break;

                case "RECONNECTING":
                    OnStateChangeEvent(State.RECONNECTING, localIP, publicIP, unixTime);
                    break;

                case "EXITING":
                    OnStateChangeEvent(State.EXITING, localIP, publicIP, unixTime);
                    await OpenVPNManagement.Close();
                    break;

                default:
                    OnStateChangeEvent(State.UNKNOWN, localIP, publicIP, unixTime);
                    break;
            }
        }

        private static async Task SendCommand(string command)
        {
            Logging.Info(string.Format("OpenVPNManagement - Sending Command \"{0}\"", command));

            if (networkStream == null || !networkStream.CanWrite)
                throw new Exception("Network stream error");

            await networkStream.WriteAsync(
                Encoding.UTF8.GetBytes(command + "\r\n"),
                0,
                command.Length + 2);

            Logging.Info("OpenVPNManagement - Command sent");
        }

        public static async Task Close()
        {
            Logging.Info("OpenVPNManagement - Closing procedure");
            if (sock != null && sock.Connected)
            {
                Logging.Info("OpenVPNManagement - Socket not null, connected. Calling close.");
                sock.Close();
            }

            sock = null;
            networkStream = null;

            Logging.Info("OpenVPNManagement - Socket nulled.");

            if (OnCloseEvent != null)
                OnCloseEvent();
        }
    }
}
