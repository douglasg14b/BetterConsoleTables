using BetterConsole.Colors.Builders;
using BetterConsole.Core;
using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders
{
    public class CellFormatBuilder : CellFormatBuilder<ICellFormatBuilder>, ICellFormatBuilder
    {
        public CellFormatBuilder(ICellFormat format)
            : base(format) { }

        public ICellFormat GetFormat()
        {
            return format;
        }
    }

    public class CellFormatBuilder<TBuilder> : FormatBuilder<TBuilder, ICellFormat>, ICellFormatBuilder<TBuilder>
    where TBuilder : ICellFormatBuilder<TBuilder>
    {
        new protected ICellFormat format;

        internal CellFormatBuilder(ICellFormat format)
            :base(format)
        {
            this.format = format;
        }

        public TBuilder WithAlignment(Alignment alignment)
        {
            format.Alignment = alignment;
            return (TBuilder)(ICellFormatBuilder<TBuilder>)this;
        }
    }
}
