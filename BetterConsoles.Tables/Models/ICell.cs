namespace BetterConsoles.Tables.Models
{
    public interface ICell : ICell<string>
    {

    }

    public interface ICell<TValue>
    {
        TValue Value { get; set; }
        ICellFormat Format { get; set; }
    }
}