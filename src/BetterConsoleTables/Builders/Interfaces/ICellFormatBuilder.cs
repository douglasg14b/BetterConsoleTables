using BetterConsole.Colors.Builders;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    public interface ICellFormatBuilder : ICellFormatBuilder<ICellFormatBuilder>
    {
        ICellFormat GetFormat();
    }

    public interface ICellFormatBuilder<TParentBuilder> : IFormatBuilder<TParentBuilder, ICellFormat>
    {
        TParentBuilder WithAlignment(Alignment alignment);
    }
}
