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
    internal class ColumnValueFormatBuilder : ValueFormatBuilder<IColumnValueFormatBuilder>, IColumnValueFormatBuilder, IColumnHeaderBuilder, IColumnRowsBuilder
    {
        private IColumnBuilder instance;
        internal ColumnValueFormatBuilder(ValueFormat format, IColumnBuilder instance)
            : base(format)
        {
            this.instance = instance;
        }

        public Column GetColumn() => instance.GetColumn();
        public IColumnValueFormatBuilder WithRowsFormat() => instance.WithRowsFormat();
        public IColumnValueFormatBuilder WithHeaderFormat() => instance.WithHeaderFormat();
        public IColumnValueFormatBuilder WithRowsFormat(ValueFormat format) => instance.WithRowsFormat(format);
        public IColumnValueFormatBuilder WithHeaderFormat(ValueFormat format) => instance.WithHeaderFormat(format);

        public IColumnBuilder WithHeaderAlignment(Alignment alignment) => instance.WithHeaderAlignment(alignment);
        public IColumnBuilder WithRowsAlignment(Alignment alignment) => instance.WithRowsAlignment(alignment);
    }
}
