using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Models
{
    public class TableRow
    {
        public TableRow(IEnumerable<TableCell> cells)
        {
            Cells = cells;
            ColumCount = cells.Count();
        }

        public IEnumerable<TableCell> Cells { get; }
        public int ColumCount { get; }
        public int Height { get; } //Implement later
    }
}
