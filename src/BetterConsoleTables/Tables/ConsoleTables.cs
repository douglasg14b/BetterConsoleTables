using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    public class ConsoleTables
    {
        public List<Table> m_tables;
        public IList<Table> Tables { get
            {
                return m_tables;
            }
        }

        public ConsoleTables()
        {
            m_tables = new List<Table>();
        }

        public ConsoleTables(params Table[] tables)
        {
            m_tables = new List<Table>(tables);
        }

        public ConsoleTables(IEnumerable<Table> tables)
        {
            m_tables = new List<Table>(tables);
        }

        public ConsoleTables AddTable(Table table)        
        {
            m_tables.Add(table);
            return this;
        }
        public ConsoleTables AddTables(params Table[] tables)
        {
            m_tables.AddRange(tables);
            return this;
        }
        public ConsoleTables AddTables(IEnumerable<Table> tables)
        {
            m_tables.AddRange(tables);
            return this;
        }

        public override string ToString()
        {
            if(m_tables.Count == 0)
            {
                throw new InvalidCastException("No tables exist");
            }

            StringBuilder builder = new StringBuilder();
            int[] columnLengths = GetMatchingColumnWidths();

            string[] tables = new string[m_tables.Count];
            for(int i = 0; i < m_tables.Count; i++)
            {
                builder.AppendLine(Tables[i].ToString(columnLengths));
            }
            return builder.ToString();
        }      

        private int[] GetMatchingColumnWidths()
        {
            List<int> output = new List<int>(); //Easier to resize

            for(int i = 0; i < m_tables.Count; i++)
            {
                int[] columns = m_tables[i].GetColumnLengths();
                for(int j = 0; j < columns.Length; j++)
                {
                    if(j >= output.Count)
                    {
                        output.Add(columns[j]);
                    }
                    else if(output[j] < columns[j])
                    {
                        output[j] = columns[j];
                    }
                }
            }
            return output.ToArray();
        }

        //Unused?
        private int[][] GetTablesColumnWidths()
        {
            int[][] output = new int[m_tables.Count][];
            for (int i = 0; i < m_tables.Count; i++)
            {
                output[i] = m_tables[i].GetColumnLengths();
            }
            return output;
        }
    }

    public enum Style
    {
        Default = 0,
        Markdown = 1,
        Simple = 2,
        MySql = 3,
        MySqlSimple = 4,
        Unicode = 5,
        UnicodeAlt = 6
    }
}
