using BetterConsoles.Colors;

namespace BetterConsoles.Tables.Models
{
    /// <summary>
    /// Defines the formatting for the string contained in a specific table cell, to be printed in the console
    /// </summary>
    public interface ICellFormat : IFormat
    {
        /// <summary>
        /// The alignment of the text within the cell. Left, Center, or Right
        /// Text will only be aligned if there is sufficent room for padding
        /// </summary>
        Alignment Alignment { get; set; }

        bool InnerFormatting { get; set;}

#if NETCOREAPP3_0_OR_GREATER || NET5_0_OR_GREATER
        public static ICellFormat operator +(ICellFormat a, ICellFormat b)
        {
            return CellFormat.Merge(a, b);
        }
# endif
    }
}