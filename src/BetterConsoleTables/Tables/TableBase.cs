using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public abstract class TableBase<THeader, TRow>
    {
        protected const char paddingChar = ' ';

        protected List<THeader> m_headers;
        public IReadOnlyList<THeader> Headers => m_headers;

        protected List<TRow[]> m_rows;
        public IReadOnlyList<TRow[]> Rows => m_rows;

        public TableConfiguration Config { get; set; }

        /// <summary>
        /// Gets the row with the greatest number of elements
        /// </summary>
        public int LongestRow
        {
            get
            {
                int max = 0;
                for (int i = 0; i < m_rows.Count; i++)
                {
                    max = m_rows[i].Length > max ? m_rows[i].Length : max;
                }
                return max;
            }
        }
        protected string PadString(string value, int maxLength, Alignment alignment)
        {
            if (value.Length == maxLength)
            {
                return value;
            }

            switch (alignment)
            {
                case Alignment.Left:
                    return value.PadRight(maxLength, paddingChar);
                case Alignment.Right:
                    return value.PadLeft(maxLength, paddingChar);
                case Alignment.Center:
                    return value.PadLeftAndRight(maxLength, paddingChar);
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Pads the row out to the edge of the console, if row is wider then console, expand console window
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected string PadRow(string row)
        {
            //No need to, and cannot pad out rows if there is no console
            if (!TableConfiguration.ConsoleAvailable)
            {
                return row;
            }

            try
            {
                if (row.Length < Console.WindowWidth)
                {
                    return row.PadRight(Console.WindowWidth - 1);
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WindowWidth = row.Length + 1;
                    }
                    return row;
                }
            }
            catch (IOException ex) //If a console is not available an IOException is thrown
            {
                TableConfiguration.ConsoleAvailable = false;
                return row;
            }
        }

        /// <summary>
        /// Increments the length of all row arrays
        /// Sets the new elements to default
        /// </summary>
        /// <param name="increments"></param>
        protected void IncrementRowElements(int increments)
        {
            for (int i = 0; i < m_rows.Count; i++)
            {
                TRow[] array = m_rows[i];
                int length = array.Length;

                Array.Resize(ref array, length + increments);

                m_rows[i] = array;
                for (int j = length; j < m_rows[i].Length; j++)
                {
                    m_rows[i][j] = default(TRow);
                }
            }
        }

        /// <summary>
        /// Resizes a row array to a specific length
        /// Sets the new elements to default
        /// </summary>
        /// <param name="row"></param>
        /// <param name="newSize"></param>
        protected void ResizeRow(ref TRow[] row, int newSize)
        {
            int length = row.Length;
            Array.Resize(ref row, newSize);
            for (int i = length; i < row.Length; i++)
            {
                row[i] = default(TRow);
            }
        }

        protected string WrapText(string text, int maxWidth)
        {
            throw new NotImplementedException();
        }
    }
}
