using System.Drawing;
using BetterConsole.Core;

namespace BetterConsole.Colors
{
    /// <summary>
    /// Defines the formatting configuration for a string to be printed in a console
    /// </summary>
    public interface IFormat
    {
        Color BackgroundColor { get; set; }

        /// <summary>
        /// If this format should use the default background color of the console
        /// </summary>
        bool DefaultBackground { get; }

        /// <summary>
        /// If this format uses both the default background and the default foreground
        /// </summary>
        bool DefaultColors { get; }

        /// <summary>
        /// If this format should use the default foreground color of the console
        /// </summary>
        bool DefaultForeground { get; }
        FontStyleExt FontStyle { get; set; }
        Color ForegroundColor { get; set; }
    }
}