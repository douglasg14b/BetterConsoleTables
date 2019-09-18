using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public class FormattedTable : TableBase<FormattedTable, TableCell, TableCell>
    {
        public List<TableCellConfig[]> ConfigMatrix;



        public override FormattedTable AddHeader(string value)
        {
            throw new NotImplementedException();
        }

        public override FormattedTable AddRow(params object[] values)
        {
            throw new NotImplementedException();
        }

        public override FormattedTable AddRows(IEnumerable<object[]> values)
        {
            throw new NotImplementedException();
        }

        public override string ToString(int[] columnWidths)
        {
            throw new NotImplementedException();
        }
    }
}
