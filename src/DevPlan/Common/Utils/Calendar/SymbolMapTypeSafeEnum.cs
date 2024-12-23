using DevPlan.UICommon.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Utils.Calendar
{
    public class SymbolMapTypeSafeEnum
    {
        /// <summary>内部定義インスタンス保持ディクショナリ。</summary>
        private static readonly Dictionary<int, SymbolMapTypeSafeEnum> dic = new Dictionary<int, SymbolMapTypeSafeEnum>();

        /// <summary>スケジュール区分-無し</summary>
        public static readonly SymbolMapTypeSafeEnum DEFAULT = new SymbolMapTypeSafeEnum(1, "", CalendarScheduleColorEnum.DefaultColor);

        /// <summary>スケジュール区分-2:■</summary>
        public static readonly SymbolMapTypeSafeEnum SQUARE = new SymbolMapTypeSafeEnum(2, "■", CalendarScheduleColorEnum.SquareColor);

        /// <summary>スケジュール区分-3:▲</summary>
        public static readonly SymbolMapTypeSafeEnum TRIANGLE = new SymbolMapTypeSafeEnum(3, "▲", CalendarScheduleColorEnum.TriangleColor);

        /// <summary>スケジュール区分-4:◎</summary>
        public static readonly SymbolMapTypeSafeEnum DOUBLE_CIRCLE = new SymbolMapTypeSafeEnum(4, "◎", CalendarScheduleColorEnum.DoubleCircleColor);

        public int StautsKey { get; private set; }

        public string Mark { get; private set; }

        public CalendarScheduleColorEnum ColorType { get; private set; }

        public SymbolMapTypeSafeEnum(int key, string mark, CalendarScheduleColorEnum colorType)
        {
            this.StautsKey = key;
            this.Mark = mark;
            this.ColorType = colorType;

            dic.Add(key, this);
        }

        public static SymbolMapTypeSafeEnum KeyOf(int? data)
        {
            if (data == null) { return null; }

            var key = data.Value;
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return null;
            }
        }

    }
}
