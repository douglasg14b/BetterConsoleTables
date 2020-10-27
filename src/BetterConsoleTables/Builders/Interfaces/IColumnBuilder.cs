using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    /// <summary>
    /// Generic column builder interface designed to be used to maintain basic API, but be abstractable
    /// </summary>
    /// <typeparam name="TColumnBuilder"></typeparam>
    public interface IGenericColumnBuilder<TColumnBuilder, TValueFormatBuilder> : 
        IColumnHeaderBuilder<TColumnBuilder, TValueFormatBuilder>, 
        IColumnRowsBuilder<TColumnBuilder, TValueFormatBuilder>
        where TColumnBuilder : IGenericColumnBuilder<TColumnBuilder, TValueFormatBuilder>
    {

    }

    /// <summary>
    /// The column builder <see cref="ITableBuilder"/> drills down into.
    /// This is not the standalone builder 
    /// </summary>
    public interface ITableColumnBuilder : ITableBuilder, IGenericColumnBuilder<ITableColumnBuilder, ITableColumnValueFormatBuilder>
    {

    }

    /// <summary>
    /// Standalone Column builder, used when creating columns outside of the table builder
    /// </summary>
    public interface IStandaloneColumnBuilder : IGenericColumnBuilder<IStandaloneColumnBuilder, IStandaloneColumnValueFormatBuilder>
    {
        IColumn GetColumn();
    }
}
