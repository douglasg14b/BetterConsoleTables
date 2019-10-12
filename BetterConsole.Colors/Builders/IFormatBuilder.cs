using BetterConsole.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsole.Colors.Builders
{
    public interface IFormatBuilder<TFormat> : IFormatBuilder<IFormatBuilder<TFormat>, TFormat>
        where TFormat : IFormat
    {
        TFormat GetFormat();
    }

    public interface IFormatBuilder<TBuilder, TFormat> where TFormat : IFormat
    {
        TBuilder WithForegroundColor(Color color);
        TBuilder WithBackgroundColor(Color color);
        TBuilder WithFontStyle(FontStyleExt styles);
    }
}
