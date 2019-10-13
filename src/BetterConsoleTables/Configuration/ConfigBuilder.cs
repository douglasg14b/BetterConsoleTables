using BetterConsoleTables.Common;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Configuration
{
    public static class ConfigBuilder
    {
        public static IBasicBuilder<CellFormat> CellConfig => new BasicBuilder<CellFormat>();
        public static IBasicBuilder<TableConfig> TableConfig => new BasicBuilder<TableConfig>();
    }


}
