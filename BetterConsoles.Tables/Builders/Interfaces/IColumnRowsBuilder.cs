using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValueFormatBuilder">The parent value format builder this can return the call chain to</typeparam>
    /// <typeparam name="TColumnBuilder">The parent column builder this can return the call chain to</typeparam>
    public interface IColumnRowsBuilder<TColumnBuilder, TValueFormatBuilder>
    {
        TColumnBuilder RowFormatter(Func<object, string> formatter);

        /// <summary>
        /// Adds formatting to the rows of this column
        /// </summary>
        TValueFormatBuilder RowsFormat();

        /// <summary>
        /// Adds formatting to the rows of this column
        /// </summary>
        TValueFormatBuilder RowsFormat(ICellFormat format);

        /// <summary>
        /// Adds text alignment to the rows of this column
        /// </summary>
        TColumnBuilder RowsAlignment(Alignment alignment);
    }
}
