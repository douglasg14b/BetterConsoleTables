using System.Drawing;
using BetterConsole.Core;

namespace BetterConsole.Colors
{
    public interface IFormat
    {
        Color BackgroundColor { get; set; }
        bool DefaultBackground { get; }
        bool DefaultColors { get; }
        bool DefaultForeground { get; }
        FontStyleExt FontStyle { get; set; }
        Color ForegroundColor { get; set; }
    }
}