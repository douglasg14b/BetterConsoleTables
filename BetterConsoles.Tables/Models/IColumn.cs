namespace BetterConsoles.Tables.Models
{
    public interface IColumn
    {
        string Title { get; }

        Alignment HeaderAlignment { get; }
        ICellFormat HeaderFormat { get; set; }
        Alignment RowsAlignment { get; }
        ICellFormat RowsFormat { get; set; }
        
        string ToString();
    }
}