using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Models
{
    public class Column : IColumn
    {
        public Column(string columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null)
        {
            Title = columnTitle;
            HeaderFormat = headerFormat ?? CellFormat.Default();
            RowsFormat = rowsFormat ?? CellFormat.Default();
        }

        public Column(object columnTitle, ICellFormat headerFormat = null, ICellFormat rowsFormat = null)
            : this(columnTitle.ToString(), headerFormat, rowsFormat) { }

        public static IColumn Default(string columnTitle)
        {
            return new Column(columnTitle);
        }

        public static IColumn Simple(string columnTitle, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return new ColumnBuilder(columnTitle)
                .RowsAlignment(rowsAlignment)
                .HeaderAlignment(headerAlignment)
                .GetColumn();
        }

        public static IColumn Simple(object columnTitle, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
            => Column.Simple(columnTitle.ToString(), rowsAlignment, headerAlignment);

        public string Title { get; }
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
