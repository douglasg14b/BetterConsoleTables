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
        internal ColumnValueFormatBuilder(ICellFormat format, TColumnBuilder instance)
            : base(format)
        {
            this.instance = instance;
        }

        public TColumnBuilder RowFormatter(Func<object, string> formatter) => instance.RowFormatter(formatter);

        public TValueBuilder RowsFormat() => instance.RowsFormat();
        public TValueBuilder HeaderFormat() => instance.HeaderFormat();
        public TValueBuilder RowsFormat(ICellFormat format) => instance.RowsFormat(format);
        public TValueBuilder HeaderFormat(ICellFormat format) => instance.HeaderFormat(format);

        public TColumnBuilder HeaderAlignment(Alignment alignment) => instance.HeaderAlignment(alignment);
        public TColumnBuilder RowsAlignment(Alignment alignment) => instance.RowsAlignment(alignment);
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
        internal StandaloneColumnValueFormatBuilder(ICellFormat format, IStandaloneColumnBuilder instance)
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
        internal TableColumnValueFormatBuilder(ICellFormat format, ITableColumnBuilder instance)
            : base(format, instance)
        {
            this.instance = instance;
        }

        public ITableColumnBuilder AddColumn(string columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null) => instance.AddColumn(columnTitle, headerFormat, rowsFormat);

        public ITableColumnBuilder AddColumn(IColumn column) => instance.AddColumn(column);
        public Table Build() => instance.Build();
    }
}
