using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes
{
    class Logging
    {
        private static List<LogEntry> logs = new List<LogEntry>();

        public struct LogEntry
        {
            public string type;
            public string data;
        }

        public static void PushLog(LogEntry log, ConsoleColor color = ConsoleColor.Gray)
        {
            logs.Add(log);

            Console.ForegroundColor = color;
            Console.WriteLine("{0}: {1}", log.type, log.data);
        }

        public static void Info(string str)
        {
            PushLog(new LogEntry()
            {
                type = "INFO",
                data = str
            });
        }

        public static void ManagementReceive(string str)
        {
            PushLog(new LogEntry()
            {
                type = "MANAGEMENT_RECEIVE",
                data = str
            });
        }

        public static void ManagementSend(string str)
        {
            PushLog(new LogEntry()
            {
                type = "MANAGEMENT_SEND",
                data = str
            });
        }

        public static void Debug(string str)
        {
            PushLog(new LogEntry()
            {
                type = "DEBUG",
                data = str
            }, ConsoleColor.Yellow);
        }

        public static void Error(Exception ex)
        {
            PushLog(new LogEntry()
            {
                type = "ERROR",
                data = ex.ToString()
            }, ConsoleColor.Red);
        }

        public static void Save()
        {
            string logPath = Path.Combine(Environment.CurrentDirectory, "debug.log");
            if (File.Exists(logPath))
                File.Delete(logPath);

            using (var fs = new FileStream(logPath, FileMode.Append))
            {
                foreach (var log in logs)
                {
                    string line = string.Format("{0}: {1}\r\n", log.type, log.data);
                    fs.Write(Encoding.Default.GetBytes(line), 0, line.Length);
                }
            }
        }

        /// <summary>
        /// Console allocation
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool AllocConsole();
    }
}
