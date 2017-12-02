using Merlin.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merlin
{
    /// <summary>
    /// Class responsible for logging messages to GUI and to file
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        private static void Log(string message, string category)
        {
            CommunicationService.Client.Send(new LogMessage
            {
                CurentTime = DateTime.Now,
                Category = category,
                Message = message
            });
        }

        /// <summary>
        /// Clears the log.
        /// </summary>
        /// <param name="category">The category.</param>
        public static void ClearLog(string category = "")
        {
            CommunicationService.Client.Send(new LogMessage
            {
                Category = category,
                ClearLog = true
            });
        }
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Log(string message) => Log(message, "Log");

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogAssert(string message) => Log(message, "Assert");

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogWarning(string message) => Log(message, "Warning");

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogError(string message) => Log(message, "Error");

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="message">The exception.</param>
        public static void LogException(Exception ex) => Log(ex.ToString(), "Exception");


        /// <summary>
        /// Logs the specified message formatted.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void LogFormat(string format, params object[] args) => Log(string.Format(format, args), "Log");
    }
}
