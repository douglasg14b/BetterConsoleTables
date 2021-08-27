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
    public interface IColumnValueFormatBuilder<TColumnBuilder, TValueFormatBuilder> :
        ICellFormatBuilder<TValueFormatBuilder>,
        IColumnHeaderBuilder<TColumnBuilder, TValueFormatBuilder>,
        IColumnRowsBuilder<TColumnBuilder, TValueFormatBuilder>
    {
    }

    /// <summary>
    /// Standalone value format builder that is not used in the table builder
    /// </summary>
    public interface IStandaloneColumnValueFormatBuilder : IColumnValueFormatBuilder<IStandaloneColumnBuilder, IStandaloneColumnValueFormatBuilder>
    {
        IColumn GetColumn();
    }

    /// <summary>
    /// Table value format builder that is used in the table builder
    /// </summary>
    public interface ITableColumnValueFormatBuilder : ITableBuilder, IColumnValueFormatBuilder<ITableColumnBuilder, ITableColumnValueFormatBuilder>
    {
    }
}
