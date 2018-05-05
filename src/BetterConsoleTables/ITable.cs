using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    public interface ITable
    {
        IList<object[]> Rows { get; }
        IList<object> Columns { get; }
        int LongestRow { get; }
        Config Config { get; set; }

        ITable AddRow(params object[] values);
        ITable AddRows(IEnumerable<object[]> rows);
        ITable AddColumn(object title);
        ITable AddColumns(params object[] columns);
        ITable From<T>(IList<T> items);

        string ToString();
        string ToString(int[] columnLengths);
    }
}
