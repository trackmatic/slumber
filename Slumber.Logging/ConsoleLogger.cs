using System;

namespace Slumber.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Error(Exception e)
        {
            Log(ConsoleColor.Red, "ERROR", e.Message);
        }

        public void Warn(string message, params object[] parameters)
        {
            Log(ConsoleColor.Yellow, "WARN", message, parameters);
        }

        public void Info(string message, params object[] parameters)
        {
            Log(ConsoleColor.Blue, "INFO", message, parameters);
        }

        public void Debug(string message, params object[] parameters)
        {
            Log(ConsoleColor.Green, "DEBUG", message, parameters);
        }

        public void Log(ConsoleColor colour, string type, string message, params object[] parameters)
        {
            using (new Session(colour))
            {
                Console.WriteLine($"{DateTime.Now}-{type}] {message}", parameters);
            }
        }

        private class Session : IDisposable
        {
            private readonly ConsoleColor _original;

            public Session(ConsoleColor color)
            {
                _original = Console.ForegroundColor;
                Console.ForegroundColor = color;
            }

            public void Dispose()
            {
                Console.ForegroundColor = _original;
            }
        }
    }
}
