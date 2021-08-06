namespace BetterConsoles.Tables.Models
{
    public interface IColumn
    {
        string Title { get; }

        
        ICellFormat HeaderFormat { get; set; }     
        ICellFormat RowsFormat { get; set; }

        /// <summary>
        /// Computed from the <see cref="HeaderFormat"/>
        /// </summary>
        Alignment HeaderAlignment { get; }

        /// <summary>
        /// Computed from the <see cref="RowsFormat"/>
        /// </summary>
        Alignment RowsAlignment { get; }

        string ToString();
    }
}