using BetterConsoleTables.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class FormattedColumn
    {
        public FormattedColumn(string columnTitle, ValueFormat headerFormat = null, ValueFormat rowsFormat = null)
        {
            Title = columnTitle;
            HeaderFormat = headerFormat ?? ValueFormat.Default();
            RowsFormat = rowsFormat ?? ValueFormat.Default();
        }

        public FormattedColumn(object columnTitle, ValueFormat headerFormat = null, ValueFormat rowsFormat = null)
            :this(columnTitle.ToString(), headerFormat, rowsFormat) { }

        public string Title { get; private set; }
        public ValueFormat HeaderFormat { get; set; }
        public ValueFormat RowsFormat { get; set; }

        public static FormattedColumn Default(string columnTitle)
        {
            return new FormattedColumn(columnTitle);
        }

        public override string ToString()
        {
            return Title;
        }

        public static implicit operator FormattedColumn(string value) => new FormattedColumn(value);
    }




}
