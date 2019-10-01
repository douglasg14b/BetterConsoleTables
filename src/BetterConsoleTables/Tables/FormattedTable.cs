using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public class FormattedTable<TModel> : TableBase<FormattedTable<TModel>, TableCell, TableCell>
    {
        public List<TableCellConfig[]> ConfigMatrix;



        public override FormattedTable<TModel> AddColumn(Column column)
        {
            throw new NotImplementedException();
        }

        public override FormattedTable<TModel> AddRow(params TableCell[] values)
        {
            throw new NotImplementedException();
        }

        public override FormattedTable<TModel> AddRows(IEnumerable<TableCell[]> values)
        {
            throw new NotImplementedException();
        }

        public override string ToString(int[] columnWidths)
        {
            throw new NotImplementedException();
        }
    }
}
