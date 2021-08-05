using BetterConsoles.Core;
using BetterConsoles.Tables.Builders.Interfaces;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders
{
    public class CellBuilder : CellBuilder<string>
    {
        public CellBuilder(string value)
            : base(value)
        {
        }
    }
    public class StandaloneCellBuilder<TValue> : CellFormatBuilder<StandaloneCellBuilder<TValue>>
    {
        private CellBuilder<TValue> _instance;
        public StandaloneCellBuilder(ICellFormat format, CellBuilder<TValue> instance)
            : base(format) 
        {
            _instance = instance;
        }

        public ICell GetCell() => _instance.GetCell();
    }

    public class CellBuilder<TValue> : ICellBuilder<StandaloneCellBuilder<TValue>>
    {
        private ICell _cell;
        private StandaloneCellBuilder<TValue> _cellFormatBuilder;

        public CellBuilder(TValue value)
        {
            _cell = new Cell<TValue>(value);
            _cellFormatBuilder = new StandaloneCellBuilder<TValue>(_cell.Format, this);
        }

        public ICell GetCell()
        {
            return _cell;
        }

        public StandaloneCellBuilder<TValue> WithAlignment(Alignment alignment) => _cellFormatBuilder.WithAlignment(alignment);
        public StandaloneCellBuilder<TValue> WithBackgroundColor(Color color) => _cellFormatBuilder.WithBackgroundColor(color);
        public StandaloneCellBuilder<TValue> WithFontStyle(FontStyleExt styles) => _cellFormatBuilder.WithFontStyle(styles);
        public StandaloneCellBuilder<TValue> WithForegroundColor(Color color) => _cellFormatBuilder.WithForegroundColor(color);
        public StandaloneCellBuilder<TValue> WithInnerFormatting() => _cellFormatBuilder.WithInnerFormatting();
    }
}
