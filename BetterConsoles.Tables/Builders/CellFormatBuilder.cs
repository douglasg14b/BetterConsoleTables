using BetterConsoles.Colors.Builders;
using BetterConsoles.Core;
using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders
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
    {
        new protected ICellFormat format;

        internal CellFormatBuilder(ICellFormat format)
            :base(format)
        {
            this.format = format;
        }

        public TBuilder Alignment(Alignment alignment)
        {
            format.Alignment = alignment;
            return (TBuilder)(ICellFormatBuilder<TBuilder>)this;
        }

        public TBuilder HasInnerFormatting()
        {
            format.InnerFormatting = true;
            return (TBuilder)(ICellFormatBuilder<TBuilder>)this;
        }
    }
}
