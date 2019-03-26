using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables.Models
{
    public class ColoredText
    {
        public ColoredText(string value, ConsoleColor color)
        {
            Value = value;
            Color = color;
        }

        public string Value { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
