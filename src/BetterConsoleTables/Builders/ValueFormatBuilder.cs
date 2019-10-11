using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders
{
    public class ValueFormatBuilder : ValueFormatBuilder<IValueFormatBuilder>, IValueFormatBuilder
    {
        public ValueFormatBuilder(ValueFormat format)
            :base(format) { }

        public ValueFormat GetFormat()
        {
            return format;
        }
    }

    public class ValueFormatBuilder<TBuilder> : IValueFormatBuilder<TBuilder>
        where TBuilder: IValueFormatBuilder<TBuilder>
    {
        protected ValueFormat format;

        internal ValueFormatBuilder(ValueFormat format)
        {
            this.format = format;
        }

        public TBuilder WithAlignment(Alignment alignment)
        {
            format.Alignment = alignment;
            return (TBuilder)(IValueFormatBuilder<TBuilder>)this;
        }

        public TBuilder WithBackgroundColor(Color color)
        {
            format.BackgroundColor = color;
            return (TBuilder)(IValueFormatBuilder<TBuilder>)this;
        }

        public TBuilder WithForegroundColor(Color color)
        {
            format.ForegroundColor = color;
            return (TBuilder)(IValueFormatBuilder<TBuilder>)this;
        }

        public TBuilder Bold()
        {
            format.Bold = true;
            return (TBuilder)(IValueFormatBuilder<TBuilder>)this;
        }

        public TBuilder Underline()
        {
            format.Underline = true;
            return (TBuilder)(IValueFormatBuilder<TBuilder>)this;
        }
    }
}
