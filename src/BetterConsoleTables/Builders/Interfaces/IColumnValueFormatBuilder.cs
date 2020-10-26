using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TColumnBuilder">The parent column builder this can return the call chain to</typeparam>
    public interface IColumnValueFormatBuilder<TColumnBuilder> : 
        ICellFormatBuilder<IColumnValueFormatBuilder<TColumnBuilder>>, 
        IColumnHeaderBuilder<TColumnBuilder>, 
        IColumnRowsBuilder<TColumnBuilder>
    {
        IColumn GetColumn();
    }
}
