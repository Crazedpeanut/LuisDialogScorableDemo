using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace ScorableDemp
{
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Error(string message);
        void Error(string message, Exception e);
    }

    public enum LogLevel
    {
        Info,
        Debug,
        Warn,
        Error
    }

    public class Logger : ILogger
    {
        public void Info(string message)
        {
            Trace.TraceInformation(MessageFormat(message));
        }

        public void Debug(string message)
        {
            Trace.TraceInformation(MessageFormat(message));
        }

        public void Error(string message)
        {
            Trace.TraceError(MessageFormat(message));
        }

        public void Error(string message, Exception e)
        {
            Trace.TraceError(MessageFormat($"{message} {e.Message}"));
        }

        public void Warn(string message)
        {
            Trace.TraceWarning(MessageFormat(message));
        }

        private string MessageFormat(string message)
        {
            return $"{message} - {DateTime.Now}";
        }
    }
}