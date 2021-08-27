using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Colors
{
    public class Gradient
    {
        public Gradient(Color start, Color end)
        {
            Start = start;
            End = end;
        }

        public Color Start { get; set; }
        public Color End { get; set; }
    }
}
