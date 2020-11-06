using BetterConsole.Colors.Builders;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    /// <summary>
    /// Formatter used to format individual cells
    /// </summary>
    public interface ICellFormatBuilder : ICellFormatBuilder<ICellFormatBuilder>
    {
        ICellFormat GetFormat();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TParentBuilder"></typeparam>
    public interface ICellFormatBuilder<TParentBuilder> : IFormatBuilder<TParentBuilder, ICellFormat>
    {
        TParentBuilder WithAlignment(Alignment alignment);
    }
}
