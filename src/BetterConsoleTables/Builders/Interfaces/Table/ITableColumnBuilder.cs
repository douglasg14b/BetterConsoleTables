using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces.Table
{
    /// <summary>
    /// The column builder <see cref="ITableBuilder"/> drills down into
    /// </summary>
    public interface ITableColumnBuilder : ITableBuilder, IColumnHeaderBuilder<ITableColumnBuilder>, IColumnRowsBuilder<ITableColumnBuilder>
    {

    }
}
