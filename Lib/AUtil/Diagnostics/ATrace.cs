using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AUtil.Diagnostics
{
    public class ATrace
    {
        public const string DEFAULT_PATH = "Log\\";
        public const string DEFAULT_DATE_PATTERN = "_yyyyMMdd";
        public const string DEFAULT_LAYOUT_PATTERN = "%date [%-5level] %message%newline";
        private const string TRACE_FORMAT = "[{0}] {1}";
        private const string TRACE_FORMAT_EX = "[{0}] {1}\r\n{2}";
        private const string TRACE_DATE_FORMAT = "{0} [{1}] {2}";
        private const string TRACE_DATE_FORMAT_EX = "{0} [{1}] {2}\r\n{3}";

        public delegate void LogMsgHandler(DateTime time, LogLevel level, string msg);

        private static ILog theLog = null;

        public static void InitFile(string moduleName, LogLevel level)
        { Init(moduleName, level, DEFAULT_PATH, null, DEFAULT_DATE_PATTERN, DEFAULT_LAYOUT_PATTERN); }
        public static void Init(string moduleName, LogLevel level)
        { Init(moduleName, level, null, null, DEFAULT_DATE_PATTERN, DEFAULT_LAYOUT_PATTERN); }
        public static void Init(string moduleName, LogLevel level, string path)
        { Init(moduleName, level, path, null, DEFAULT_DATE_PATTERN, DEFAULT_LAYOUT_PATTERN); }
        public static void Init(string moduleName, LogLevel level, LogMsgHandler handler)
        { Init(moduleName, level, null, handler, DEFAULT_DATE_PATTERN, DEFAULT_LAYOUT_PATTERN); }
        public static void Init(string moduleName, LogLevel level, string path, LogMsgHandler handler)
        { Init(moduleName, level, path, handler, DEFAULT_DATE_PATTERN, DEFAULT_LAYOUT_PATTERN); }
        public static void Init(string moduleName, LogLevel level,
            string path, LogMsgHandler handler, string datePattern, string layoutPattern)
        {
            Hierarchy hierarchy;
            PatternLayout layout;
            RollingFileAppender rollingFileAppender = null;
            LogMsgAppender logMsgAppender = null;

            hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Configured = true;

            TraceAppender traceAppender = new TraceAppender();
            traceAppender.Name = String.Format("{0}_trace", moduleName);

            if (!String.IsNullOrEmpty(path))
            {
                string filePath;

                if (Path.IsPathRooted(path))
                {
                    filePath = String.Format("{0}{1}.log",
                        path.Trim().EndsWith("\\") ? path.Trim() : path.Trim() + "\\",
                        moduleName);
                }
                else
                {
                    filePath = String.Format("{0}\\{1}{2}.log",
                        AppDomain.CurrentDomain.BaseDirectory,
                        path.Trim().EndsWith("\\") ? path.Trim() : path.Trim() + "\\",
                        moduleName);
                }
                rollingFileAppender = new RollingFileAppender();
                rollingFileAppender.Name = String.Format("{0}_file", moduleName);
                rollingFileAppender.File = filePath;
                rollingFileAppender.AppendToFile = true;
                rollingFileAppender.StaticLogFileName = false;
                rollingFileAppender.CountDirection = 1;
                rollingFileAppender.PreserveLogFileNameExtension = true;
                rollingFileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
                rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
                rollingFileAppender.DatePattern = datePattern;
                rollingFileAppender.Encoding = Encoding.UTF8;
            }

            if (handler != null)
            {
                logMsgAppender = new LogMsgAppender(handler);
                logMsgAppender.Name = String.Format("{0}_messgae", moduleName);
            }

            if (!String.IsNullOrEmpty(layoutPattern))
            {
                layout = new PatternLayout(layoutPattern);
                traceAppender.Layout = layout;
                if (rollingFileAppender != null)
                    rollingFileAppender.Layout = layout;
                if (logMsgAppender != null)
                    logMsgAppender.Layout = layout;
            }

            hierarchy.Root.AddAppender(traceAppender);
            if (rollingFileAppender != null)
                hierarchy.Root.AddAppender(rollingFileAppender);
            if (logMsgAppender != null)
                hierarchy.Root.AddAppender(logMsgAppender);

            traceAppender.ActivateOptions();
            if (rollingFileAppender != null)
                rollingFileAppender.ActivateOptions();
            if (logMsgAppender != null)
                logMsgAppender.ActivateOptions();

            hierarchy.Root.Level = GetLevel(level);

            theLog = LogManager.GetLogger(moduleName);
        }

        public static void Shutdown()
        {
            if (theLog == null)
                return;
            LogManager.Shutdown();
        }

        public static void i(string tag, bool used, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg));
            else
                theLog.InfoFormat(TRACE_FORMAT, tag, msg);
        }
        public static void i(string tag, bool used, string format, params object[] args)
        { i(tag, used, String.Format(format, args)); }
        public static void i(string tag, bool used, string format, object arg0)
        { i(tag, used, String.Format(format, arg0)); }
        public static void i(string tag, bool used, string format, object arg0, object arg1)
        { i(tag, used, String.Format(format, arg0, arg1)); }
        public static void i(string tag, bool used, string format, object arg0, object arg1, object arg2)
        { i(tag, used, String.Format(format, arg0, arg1, arg2)); }

        public static void d(string tag, bool used, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg));
            else
                theLog.DebugFormat(TRACE_FORMAT, tag, msg);
        }
        public static void d(string tag, bool used, string format, params object[] args)
        { d(tag, used, String.Format(format, args)); }
        public static void d(string tag, bool used, string format, object arg0)
        { d(tag, used, String.Format(format, arg0)); }
        public static void d(string tag, bool used, string format, object arg0, object arg1)
        { d(tag, used, String.Format(format, arg0, arg1)); }
        public static void d(string tag, bool used, string format, object arg0, object arg1, object arg2)
        { d(tag, used, String.Format(format, arg0, arg1, arg2)); }

        public static void d(string tag, bool used, Exception ex, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT_EX,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg, ex.StackTrace));
            else
                theLog.DebugFormat(TRACE_FORMAT_EX, tag, msg, ex.StackTrace);
        }
        public static void d(string tag, bool used, Exception ex, string format, params object[] args)
        { d(tag, used, ex, String.Format(format, args)); }
        public static void d(string tag, bool used, Exception ex, string format, object arg0)
        { d(tag, used, ex, String.Format(format, arg0)); }
        public static void d(string tag, bool used, Exception ex, string format, object arg0, object arg1)
        { d(tag, used, ex, String.Format(format, arg0, arg1)); }
        public static void d(string tag, bool used, Exception ex, string format, object arg0, object arg1, object arg2)
        { d(tag, used, ex, String.Format(format, arg0, arg1, arg2)); }

        public static void w(string tag, bool used, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg));
            else
                theLog.WarnFormat(TRACE_FORMAT, tag, msg);
        }
        public static void w(string tag, bool used, string format, params object[] args)
        { w(tag, used, String.Format(format, args)); }
        public static void w(string tag, bool used, string format, object arg0)
        { w(tag, used, String.Format(format, arg0)); }
        public static void w(string tag, bool used, string format, object arg0, object arg1)
        { w(tag, used, String.Format(format, arg0, arg1)); }
        public static void w(string tag, bool used, string format, object arg0, object arg1, object arg2)
        { w(tag, used, String.Format(format, arg0, arg1, arg2)); }

        public static void w(string tag, bool used, Exception ex, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT_EX,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg, ex.StackTrace));
            else
                theLog.WarnFormat(TRACE_FORMAT_EX, tag, msg, ex.StackTrace);
        }
        public static void w(string tag, bool used, Exception ex, string format, params object[] args)
        { w(tag, used, ex, String.Format(format, args)); }
        public static void w(string tag, bool used, Exception ex, string format, object arg0)
        { w(tag, used, ex, String.Format(format, arg0)); }
        public static void w(string tag, bool used, Exception ex, string format, object arg0, object arg1)
        { w(tag, used, ex, String.Format(format, arg0, arg1)); }
        public static void w(string tag, bool used, Exception ex, string format, object arg0, object arg1, object arg2)
        { w(tag, used, ex, String.Format(format, arg0, arg1, arg2)); }

        public static void e(string tag, bool used, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg));
            else
                theLog.ErrorFormat(TRACE_FORMAT, tag, msg);
        }
        public static void e(string tag, bool used, string format, params object[] args)
        { e(tag, used, String.Format(format, args)); }
        public static void e(string tag, bool used, string format, object arg0)
        { e(tag, used, String.Format(format, arg0)); }
        public static void e(string tag, bool used, string format, object arg0, object arg1)
        { e(tag, used, String.Format(format, arg0, arg1)); }
        public static void e(string tag, bool used, string format, object arg0, object arg1, object arg2)
        { e(tag, used, String.Format(format, arg0, arg1, arg2)); }

        public static void e(string tag, bool used, Exception ex, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT_EX,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg, ex.StackTrace));
            else
                theLog.ErrorFormat(TRACE_FORMAT_EX, tag, msg, ex.StackTrace);
        }
        public static void e(string tag, bool used, Exception ex, string format, params object[] args)
        { e(tag, used, ex, String.Format(format, args)); }
        public static void e(string tag, bool used, Exception ex, string format, object arg0)
        { e(tag, used, ex, String.Format(format, arg0)); }
        public static void e(string tag, bool used, Exception ex, string format, object arg0, object arg1)
        { e(tag, used, ex, String.Format(format, arg0, arg1)); }
        public static void e(string tag, bool used, Exception ex, string format, object arg0, object arg1, object arg2)
        { e(tag, used, ex, String.Format(format, arg0, arg1, arg2)); }

        public static void f(string tag, bool used, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg));
            else
                theLog.FatalFormat(TRACE_FORMAT, tag, msg);
        }
        public static void f(string tag, bool used, string format, params object[] args)
        { f(tag, used, String.Format(format, args)); }
        public static void f(string tag, bool used, string format, object arg0)
        { f(tag, used, String.Format(format, arg0)); }
        public static void f(string tag, bool used, string format, object arg0, object arg1)
        { f(tag, used, String.Format(format, arg0, arg1)); }
        public static void f(string tag, bool used, string format, object arg0, object arg1, object arg2)
        { f(tag, used, String.Format(format, arg0, arg1, arg2)); }

        public static void f(string tag, bool used, Exception ex, string msg)
        {
            if (!used) return;
            if (theLog == null)
                Trace.WriteLine(String.Format(TRACE_DATE_FORMAT_EX,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, msg, ex.StackTrace));
            else
                theLog.FatalFormat(TRACE_FORMAT_EX, tag, msg, ex.StackTrace);
        }
        public static void f(string tag, bool used, Exception ex, string format, params object[] args)
        { f(tag, used, ex, String.Format(format, args)); }
        public static void f(string tag, bool used, Exception ex, string format, object arg0)
        { f(tag, used, ex, String.Format(format, arg0)); }
        public static void f(string tag, bool used, Exception ex, string format, object arg0, object arg1)
        { f(tag, used, ex, String.Format(format, arg0, arg1)); }
        public static void f(string tag, bool used, Exception ex, string format, object arg0, object arg1, object arg2)
        { f(tag, used, ex, String.Format(format, arg0, arg1, arg2)); }

        public static string Dump(bool used, byte[] data)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Length, "{0:X02}");
        }
        public static string Dump(bool used, byte[] data, int length)
        { return Dump(used, data, 0, length, "{0:X02}"); }
        public static string Dump(bool used, byte[] data, int offset, int length)
        { return Dump(used, data, offset, length, "{0:X02}"); }

        public static string DumpNum<T>(bool used, T[] data)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Length, "{0}");
        }
        public static string DumpNum<T>(bool used, T[] data, int length)
        { return Dump(used, data, 0, length, "{0}"); }
        public static string DumpNum<T>(bool used, T[] data, int offset, int length)
        { return Dump(used, data, offset, length, "{0}"); }

        public static string Dump<T>(bool used, T[] data)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Length, "[{0}]");
        }
        public static string Dump<T>(bool used, T[] data, int length)
        { return Dump(used, data, 0, length, "[{0}]"); }
        public static string Dump<T>(bool used, T[] data, int offset, int length)
        { return Dump(used, data, offset, length, "[{0}]"); }

        public static string Dump<T>(bool used, T[] data, string format)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Length, format);
        }
        public static string Dump<T>(bool used, T[] data, int length, string format)
        { return Dump(used, data, 0, length, format); }
        public static string Dump<T>(bool used, T[] data, int offset, int length, string format)
        {
            if (!used) return String.Empty;
            if (data == null) return String.Empty;
            if (data.Length < offset) return String.Empty;
            if (data.Length < offset + length)
                length = data.Length - offset;
            if (length == 0) return String.Empty;

            StringBuilder builder = new StringBuilder();
            for (int i = offset; i < offset + length; i++)
            {
                if (i > data.Length) break;
                if (builder.Length > 0) builder.Append(", ");
                builder.AppendFormat(format, data[i]);
            }
            return builder.ToString();
        }

        public static string Dump<T>(bool used, Collection<T> data)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Count, "[{0}]");
        }
        public static string Dump<T>(bool used, Collection<T> data, int length)
        { return Dump(used, data, 0, length, "[{0}]"); }
        public static string Dump<T>(bool used, Collection<T> data, int offset, int length)
        { return Dump(used, data, offset, length, "[{0}]"); }

        public static string Dump<T>(bool used, Collection<T> data, string format)
        {
            if (data == null) return String.Empty;
            return Dump(used, data, 0, data.Count, format);
        }
        public static string Dump<T>(bool used, Collection<T> data, int length, string format)
        { return Dump(used, data, 0, length, format); }
        public static string Dump<T>(bool used, Collection<T> data, int offset, int length, string format)
        {
            if (!used) return String.Empty;
            if (data == null) return String.Empty;
            if (data.Count < offset) return String.Empty;
            if (data.Count < offset + length)
                length = data.Count - offset;
            if (length == 0) return String.Empty;

            StringBuilder builder = new StringBuilder();
            for (int i = offset; i < offset + length; i++)
            {
                if (i > data.Count) break;
                if (builder.Length > 0) builder.Append(", ");
                builder.AppendFormat(format, data[i]);
            }
            return builder.ToString();
        }

        private static LogLevel GetLogLevel(Level level)
        {
            if (level == Level.Off) return LogLevel.Off;
            else if (level == Level.Log4Net_Debug) return LogLevel.Log4Net_Debug;
            else if (level == Level.Emergency) return LogLevel.Emergency;
            else if (level == Level.Fatal) return LogLevel.Fatal;
            else if (level == Level.Alert) return LogLevel.Alert;
            else if (level == Level.Critical) return LogLevel.Critical;
            else if (level == Level.Severe) return LogLevel.Severe;
            else if (level == Level.Error) return LogLevel.Error;
            else if (level == Level.Warn) return LogLevel.Warn;
            else if (level == Level.Notice) return LogLevel.Notice;
            else if (level == Level.Info) return LogLevel.Info;
            else if (level == Level.Debug) return LogLevel.Debug;
            else if (level == Level.Fine) return LogLevel.Fine;
            else if (level == Level.Trace) return LogLevel.Trace;
            else if (level == Level.Finer) return LogLevel.Finer;
            else if (level == Level.Verbose) return LogLevel.Verbose;
            else if (level == Level.Finest) return LogLevel.Finest;
            else return LogLevel.All;
        }

        private static Level GetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Off: return Level.Off;
                case LogLevel.Verbose: return Level.Verbose;
                case LogLevel.Finer: return Level.Finer;
                case LogLevel.Trace: return Level.Trace;
                case LogLevel.Fine: return Level.Fine;
                case LogLevel.Debug: return Level.Debug;
                case LogLevel.Info: return Level.Info;
                case LogLevel.Notice: return Level.Notice;
                case LogLevel.Finest: return Level.Finest;
                case LogLevel.Error: return Level.Error;
                case LogLevel.Severe: return Level.Severe;
                case LogLevel.Critical: return Level.Critical;
                case LogLevel.Alert: return Level.Alert;
                case LogLevel.Fatal: return Level.Fatal;
                case LogLevel.Emergency: return Level.Emergency;
                case LogLevel.Log4Net_Debug: return Level.Log4Net_Debug;
                case LogLevel.Warn: return Level.Warn;
            }
            return Level.All;
        }

        public class LogMsgAppender : AppenderSkeleton
        {
            private LogMsgHandler m_fnHandler;

            public LogMsgAppender(LogMsgHandler handler)
            {
                m_fnHandler = handler;
            }

            protected override void Append(LoggingEvent loggingEvent)
            {
                if (m_fnHandler == null)
                    return;
                m_fnHandler(loggingEvent.TimeStamp, GetLogLevel(loggingEvent.Level),
                    loggingEvent.RenderedMessage + Environment.NewLine);
            }
        }

        public enum LogLevel
        {
            Off,
            All,
            Verbose,
            Finer,
            Trace,
            Fine,
            Debug,
            Info,
            Notice,
            Finest,
            Error,
            Severe,
            Critical,
            Alert,
            Fatal,
            Emergency,
            Log4Net_Debug,
            Warn
        }
    }
}
