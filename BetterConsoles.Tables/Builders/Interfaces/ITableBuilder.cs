using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders.Interfaces
{
    public interface ITableBuilder
    {
        /// <summary>
        /// Adds a column to the builder and enables chaining to configure this column
        /// </summary>
        /// <param name="columnTitle">The title of the column</param>
        /// <returns>The column builder instance</returns>
        ITableColumnBuilder AddColumn(string columnTitle);

        /// <summary>
        /// Adds a column to the builder and enables chaining to configure this column
        /// </summary>
        /// <param name="column">The column configuration</param>
        /// <returns>The column builder instance</returns>
        ITableColumnBuilder AddColumn(IColumn column);

        /// <summary>
        /// Creates the table with the specified configuration
        /// </summary>
        /// <returns></returns>
        Table Build();
    }
}
