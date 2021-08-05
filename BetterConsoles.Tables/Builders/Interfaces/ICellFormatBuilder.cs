using BetterConsoles.Colors.Builders;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders.Interfaces
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

        /// <summary>
        /// If this specific cell has inner formatting, meaning there is console formatting sequences inside of it's string
        /// This triggers a more expensive string length calculation that ignores formatting sequences for table sizing
        /// </summary>
        TParentBuilder WithInnerFormatting();
    }
}
