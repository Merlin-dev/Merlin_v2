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
        public static void Log(string message)
        {
            CommunicationService.Client.Send(new LogMessage
            {
                CurentTime = DateTime.Now,
                Message = message
            });
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public static void Log(string message, string category)
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
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogVerbose(string message) => Log(message, "Verbose");

        /// <summary>
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogInfo(string message) => Log(message, "Info");

        /// <summary>
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogUser(string message) => Log(message, "User");

        /// <summary>
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogWarning(string message) => Log(message, "Warning");

        /// <summary>
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogError(string message) => Log(message, "Error");

        /// <summary>
        /// Logs to predetermined category.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void LogException(Exception ex) => Log(ex.ToString(), "Exception");
    }
}
