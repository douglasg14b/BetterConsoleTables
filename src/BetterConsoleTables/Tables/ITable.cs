using BetterConsoleTables.Models;
using System.Collections.Generic;

namespace BetterConsoleTables
{
    public interface ITable
    {
        Table AddColumn(IColumn column);
        Table AddColumn(object title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);
        Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left);
        Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns);
        Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params string[] columns);
        Table AddColumns(params IColumn[] columns);
        Table AddRow(params object[] rowValues);
        Table AddRows(IEnumerable<object[]> rows);
        Table ReplaceRows(IEnumerable<object[]> rows);
        string ToString();
        string ToString(int[] columnLengths);
    }
}