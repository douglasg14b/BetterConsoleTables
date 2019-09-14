using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public struct ColumnHeader
    {
        public ColumnHeader(string name, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            Title = name;
            RowsAlignment = rowsAlignment;
            HeaderAlignment = headerAlignment;
        }

        public ColumnHeader(object name, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
            :this(name.ToString(), rowsAlignment, headerAlignment) { }

        public string Title { get; }
        public Alignment RowsAlignment { get; }
        public Alignment HeaderAlignment { get; }

        public override string ToString()
        {
            return Title;
        }

        public static implicit operator ColumnHeader(string value) => new ColumnHeader(value);
    }
}
