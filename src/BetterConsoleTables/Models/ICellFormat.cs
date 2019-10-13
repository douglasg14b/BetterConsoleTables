using BetterConsole.Colors;

namespace BetterConsoleTables.Models
{
    public interface ICellFormat : IFormat
    {
        Alignment Alignment { get; set; }
    }
}