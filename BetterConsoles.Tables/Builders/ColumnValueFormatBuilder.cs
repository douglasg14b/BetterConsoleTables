using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders
{
    /// <summary>
    /// The base column format builder.
    /// Used by the table builder
    /// </summary>
    /// <typeparam name="TColumnBuilder"></typeparam>
    internal class ColumnValueFormatBuilder<TColumnBuilder, TValueBuilder> : 
        CellFormatBuilder<TValueBuilder>,
        IColumnValueFormatBuilder<TColumnBuilder, TValueBuilder>

        where TColumnBuilder : IGenericColumnBuilder<TColumnBuilder, TValueBuilder>
    {
        private TColumnBuilder instance;
        internal ColumnValueFormatBuilder(CellFormat format, TColumnBuilder instance)
            : base(format)
        {
            this.instance = instance;
        }

        public TValueBuilder WithRowsFormat() => instance.WithRowsFormat();
        public TValueBuilder WithHeaderFormat() => instance.WithHeaderFormat();
        public TValueBuilder WithRowsFormat(CellFormat format) => instance.WithRowsFormat(format);
        public TValueBuilder WithHeaderFormat(CellFormat format) => instance.WithHeaderFormat(format);

        public TColumnBuilder WithHeaderAlignment(Alignment alignment) => instance.WithHeaderAlignment(alignment);
        public TColumnBuilder WithRowsAlignment(Alignment alignment) => instance.WithRowsAlignment(alignment);
    }

    /// <summary>
    /// The value format builder that's used for teh standalone column builder, not the table builder
    /// </summary>
    /// <typeparam name="TColumnBuilder"></typeparam>
    internal class StandaloneColumnValueFormatBuilder : 
        ColumnValueFormatBuilder<IStandaloneColumnBuilder, IStandaloneColumnValueFormatBuilder>, 
        IStandaloneColumnValueFormatBuilder
    {
        private IStandaloneColumnBuilder instance;
        internal StandaloneColumnValueFormatBuilder(CellFormat format, IStandaloneColumnBuilder instance)
            : base(format, instance)
        {
            this.instance = instance;
        }
        public IColumn GetColumn() => instance.GetColumn();
    }

    /// <summary>
    /// The value format builder that's used for teh standalone column builder, not the table builder
    /// </summary>
    /// <typeparam name="TColumnBuilder"></typeparam>
    internal class TableColumnValueFormatBuilder :
        ColumnValueFormatBuilder<ITableColumnBuilder, ITableColumnValueFormatBuilder>,
        ITableColumnValueFormatBuilder
    {
        private ITableColumnBuilder instance;
        internal TableColumnValueFormatBuilder(CellFormat format, ITableColumnBuilder instance)
            : base(format, instance)
        {
            this.instance = instance;
        }

        public ITableColumnBuilder WithColumn(string columnTitle) => instance.WithColumn(columnTitle);

        public ITableColumnBuilder WithColumn(IColumn column) => instance.WithColumn(column);
        public Table Build() => instance.Build();
    }
}
