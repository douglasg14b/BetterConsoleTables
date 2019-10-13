using System.Collections.Generic;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;

namespace BetterConsoleTables
{
    public interface ITable
    {
        IReadOnlyList<Column> Columns { get; }
        TableConfig Config { get; set; }
        int LongestRow { get; }
        IReadOnlyList<object[]> Rows { get; }

        Table AddColumn(object title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);
        Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);

        Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns);
        Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params string[] columns);

        Table AddRow(params object[] values);
        Table AddRows(IEnumerable<object[]> rows);
        Table From<T>(IList<T> items);
        string ToString();
        string ToString(int[] columnLengths);
    }

    public interface ITableBase<THeader, TRow, TCell>
    {
        IReadOnlyList<THeader> Headers { get; }
        IReadOnlyList<TRow> Rows { get; }

        TableConfig Config { get; set; }
        int LongestRow { get; }

        Table AddRow(params string[] values);
        Table AddRow(params TCell[] values);

        Table AddRows(IEnumerable<string[]> rows);
        Table AddRows(IEnumerable<TCell[]> rows);

        Table From<T>(IList<T> items);

        string ToString();
        string ToString(int[] columnWidths);
    }
}