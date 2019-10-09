﻿using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    public interface IColumnRowsBuilder
    {
        IColumnValueFormatBuilder WithRowsFormat();
        IColumnValueFormatBuilder WithRowsFormat(ValueFormat format);
    }
}
