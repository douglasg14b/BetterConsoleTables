using BetterConsoleTables.Configuration;
using System.Collections.Generic;

namespace BetterConsoleTables
{
    public interface ITableBase<TTable, THeader, TCell> where TTable : TableBase<TTable, THeader, TCell>
    {
        TableConfig Config { get; set; }
        IReadOnlyList<THeader> Headers { get; }
        int LongestRow { get; }
        IReadOnlyList<TCell[]> Rows { get; }

        TTable AddColumn(object title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);
        TTable AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);
        TTable AddColumn(THeader column);
        TTable AddRow(params object[] rowValues);
        TTable AddRow(params TCell[] rowValues);
        TTable AddRows(IEnumerable<object[]> rows);
        TTable AddRows(IEnumerable<TCell[]> rows);
        TTable From<T>(T[] items);
        TTable ReplaceRows(IEnumerable<object[]> rows);
        string ToString();
        string ToString(int[] columnWidths);
    }
}