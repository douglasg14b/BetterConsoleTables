using BetterConsoleTables.Builders;
using BetterConsoleTables.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class Column
    {
        public Column(string columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null)
        {
            Title = columnTitle;
            HeaderFormat = headerFormat ?? CellFormat.Default();
            RowsFormat = rowsFormat ?? CellFormat.Default();
        }

        public Column(object columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null)
            :this(columnTitle.ToString(), headerFormat, rowsFormat) { }

        public static Column Default(string columnTitle)
        {
            return new Column(columnTitle);
        }

        public static Column Simple(string columnTitle, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return new ColumnBuilder(columnTitle)
                .WithRowsAlignment(rowsAlignment)
                .WithHeaderAlignment(headerAlignment)
                .GetColumn();
        }

        public static Column Simple(object columnTitle, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
            => Column.Simple(columnTitle.ToString(), rowsAlignment, headerAlignment);

        public string Title { get; private set; }
        public ICellFormat HeaderFormat { get; set; }
        public ICellFormat RowsFormat { get; set; }

        public Alignment HeaderAlignment => HeaderFormat?.Alignment ?? default;
        public Alignment RowsAlignment => RowsFormat?.Alignment ?? default;



        public override string ToString()
        {
            return Title;
        }

        public static implicit operator Column(string value) => new Column(value);
    }
}
