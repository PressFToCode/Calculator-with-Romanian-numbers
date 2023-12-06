using System.Text;
using System.IO;

namespace Fatkullin_AA_BIVT_21_14_HW_1_Calculator
{
    internal class Logger
    {
        public const string LogFilePath = "log.txt";
        public static string LogText => GetLogText();
        public static bool IsInited { get; private set; }
        public static StreamWriter LogFileWriter { get; set; }
        public static void Init()
        {
            if (IsInited)
            {
                return;
            }
            IsInited = true;
            LogFileWriter = new StreamWriter(LogFilePath, true, Encoding.UTF8);
        }
        public static void Close()
        {
            if (!IsInited)
            {
                return;
            }
            IsInited = false;
            LogFileWriter.Close();
            LogFileWriter.Dispose();
            LogFileWriter = null;
        }
        public static void Log(string text)
        {
            LogFileWriter.Write(text);
            LogFileWriter.Flush();
        }
        public static void EndLine()
        {
            LogFileWriter.Flush();
            LogFileWriter.WriteLine();
        }
        public static string GetLogText()
        {
            Close();
            string text = File.ReadAllText(LogFilePath);
            Init();
            return text;
        }
    }
}
