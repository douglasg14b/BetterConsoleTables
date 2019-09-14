using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public struct ColumnHeader
    {
        public ColumnHeader(string name, ColumnAlignment alignment = ColumnAlignment.Left)
        {
            Title = name;
            Alignment = alignment;
        }

        public ColumnHeader(object name, ColumnAlignment alignment = ColumnAlignment.Left)
            :this(name.ToString(), alignment){ }

        public string Title { get; }
        public ColumnAlignment Alignment { get; }

        public override string ToString()
        {
            return Title;
        }

        public static implicit operator ColumnHeader(string value) => new ColumnHeader(value);
    }
}
