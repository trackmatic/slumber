using System;

namespace Slumber
{
    /// <summary>
    /// An abstraction of the logging layer
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="e"></param>
        void Error(Exception e);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Warn(string message, params object[] parameters);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Info(string message, params object[] parameters);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Debug(string message, params object[] parameters);
    }
}
