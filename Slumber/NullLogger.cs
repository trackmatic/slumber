using System;

namespace Slumber
{
    internal class NullLogger : ILogger
    {
        public void Error(Exception e)
        {
        }

        public void Warn(string message, params object[] parameters)
        {
        }

        public void Info(string message, params object[] parameters)
        {
        }

        public void Debug(string message, params object[] parameters)
        {
        }
    }
}
