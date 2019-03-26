using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    //Table that is either formatted with color or with wrapped text
    public class FormattedTable
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.DarkGray;
        public List<ColoredText> Values = new List<ColoredText>()
        {
            new ColoredText("Test Value", ConsoleColor.Red),
            new ColoredText("Test Value 2", ConsoleColor.Green),
            new ColoredText("Test Value 3", ConsoleColor.Magenta)
        };

        public override string ToString()
        {
            throw new NotImplementedException();
        }


    }
}
