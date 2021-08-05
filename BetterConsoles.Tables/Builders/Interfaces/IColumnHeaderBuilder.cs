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
    /// <typeparam name="TColumnBuilder">The parent column builder this can return the call chain to</typeparam>
    /// <typeparam name="TColumnValueFormatBuilder">The parent column format builder this can return the call chain to</typeparam>
    public interface IColumnHeaderBuilder<TColumnBuilder, TColumnValueFormatBuilder>
    {
        TColumnValueFormatBuilder HeaderFormat();
        TColumnValueFormatBuilder HeaderFormat(CellFormat format);
        TColumnBuilder HeaderAlignment(Alignment alignment);
    }
}
