using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    public class Gradient
    {
        public Gradient(Color start, Color end)
        {
            Start = start;
            End = end;
        }

        Color Start { get; set; }
        Color End { get; set; }
    }
}
