using BetterConsoleTables.Builders.Interfaces;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders
{
    internal class ColumnValueFormatBuilder : CellFormatBuilder<IColumnValueFormatBuilder<IColumnBuilder>>, IColumnValueFormatBuilder<IColumnBuilder>, IColumnHeaderBuilder<IColumnBuilder>, IColumnRowsBuilder<IColumnBuilder>
    {
        private IColumnBuilder instance;
        internal ColumnValueFormatBuilder(CellFormat format, IColumnBuilder instance)
            : base(format)
        {
            this.instance = instance;
        }

        public IColumn GetColumn() => instance.GetColumn();
        public IColumnValueFormatBuilder<IColumnBuilder> WithRowsFormat() => instance.WithRowsFormat();
        public IColumnValueFormatBuilder<IColumnBuilder> WithHeaderFormat() => instance.WithHeaderFormat();
        public IColumnValueFormatBuilder<IColumnBuilder> WithRowsFormat(CellFormat format) => instance.WithRowsFormat(format);
        public IColumnValueFormatBuilder<IColumnBuilder> WithHeaderFormat(CellFormat format) => instance.WithHeaderFormat(format);

        public IColumnBuilder WithHeaderAlignment(Alignment alignment) => instance.WithHeaderAlignment(alignment);
        public IColumnBuilder WithRowsAlignment(Alignment alignment) => instance.WithRowsAlignment(alignment);
    }
}
