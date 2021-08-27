using BetterConsoles.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Colors.Builders
{
    public class FormatBuilder<TFormat> : FormatBuilder<IFormatBuilder<TFormat>, TFormat>, IFormatBuilder<TFormat>
        where TFormat : IFormat
    {
        public FormatBuilder(TFormat format)
            :base(format) { }

        public TFormat GetFormat()
        {
            return format;
        }
    }

    public class FormatBuilder<TBuilder, TFormat> : IFormatBuilder<TBuilder, TFormat>
        where TFormat : IFormat
    {
        protected TFormat format;

        internal protected FormatBuilder(TFormat format)
        {
            this.format = format;
        }

        public TBuilder BackgroundColor(Color color)
        {
            format.BackgroundColor = color;
            return (TBuilder)(IFormatBuilder<TBuilder, TFormat>)this;
        }

        public TBuilder ForegroundColor(Color color)
        {
            format.ForegroundColor = color;
            return (TBuilder)(IFormatBuilder<TBuilder, TFormat>)this;
        }

        public TBuilder FontStyle(FontStyleExt styles)
        {
            format.FontStyle = styles;
            return (TBuilder)(IFormatBuilder<TBuilder, TFormat>)this;
        }
    }
}
