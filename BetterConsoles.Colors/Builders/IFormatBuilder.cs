using BetterConsoles.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Colors.Builders
{
    public interface IFormatBuilder<TFormat> : IFormatBuilder<IFormatBuilder<TFormat>, TFormat>
        where TFormat : IFormat
    {
        TFormat GetFormat();
    }

    public interface IFormatBuilder<TParentBuilder, TFormat> where TFormat : IFormat
    {
        TParentBuilder ForegroundColor(Color color);
        TParentBuilder BackgroundColor(Color color);
        TParentBuilder FontStyle(FontStyleExt styles);
    }
}
