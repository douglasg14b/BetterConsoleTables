using System;
using System.Collections.Generic;

namespace BetterConsoleTables
{
    public class ConsoleTables
    {
        //Expose interfaces over concrete classes, also CA2227
        private List<object> m_columns;
        public IList<object> Columns
        {
            get
            {
                return m_columns;
            }
        }

        private List<object[]> m_rows;
        public IList<object[]> Rows
        {
            get
            {
                return m_rows;
            }
        }

        public void AddRows(IEnumerable<object[]> rows)
        {
            m_rows.AddRange(rows);
        }
    }
}
