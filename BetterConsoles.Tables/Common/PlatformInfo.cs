using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Common
{
    internal static class PlatformInfo
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        static PlatformInfo()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Platform = OSPlatform.Windows;

                // Enable console colors if not enabled by default
                var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
                HasFormattingSupport = GetConsoleMode(iStdOut, out var outConsoleMode)
                             && SetConsoleMode(iStdOut, outConsoleMode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                HasFormattingSupport = true;
                Platform = OSPlatform.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                HasFormattingSupport = true;
                Platform = OSPlatform.OSX;
            }
        }

        public static OSPlatform Platform { get; private set; }
        public static bool HasFormattingSupport { get; private set; }

        private static bool m_consoleAvailableInitilized;
        private static bool m_consoleAvailable;

        /// <summary>
        /// Lazy loaded flag to see if a console is available
        /// </summary>
        public static bool ConsoleAvailable 
        { 
            get
            {
                if (!m_consoleAvailableInitilized)
                {
                    try
                    {
                        m_consoleAvailable = Environment.UserInteractive && Console.Title.Length > 0;
                    }
                    catch
                    {
                        m_consoleAvailable = false;
                    }
                    finally
                    {
                        m_consoleAvailableInitilized = true;
                    }
                }
                return m_consoleAvailable;
            } 
        }
    }
}
