using System.Collections.Generic;

namespace DevPlan.UICommon.Utils.Calendar
{
    /// <summary>
    /// カレンダーテンプレートオブジェクトTypeSafeEnum。
    /// </summary>
    public class CalendarTemplateTypeSafeEnum
    {
        /// <summary>内部定義インスタンス保持ディクショナリ。</summary>
        private static readonly Dictionary<int, CalendarTemplateTypeSafeEnum> dic = new Dictionary<int, CalendarTemplateTypeSafeEnum>();

        /// <summary>標準テンプレート</summary>
        public static readonly CalendarTemplateTypeSafeEnum DEFAULT = new CalendarTemplateTypeSafeEnum(1, "標準");

        /// <summary>拡大１テンプレート</summary>
        public static readonly CalendarTemplateTypeSafeEnum EXPANSION1 = new CalendarTemplateTypeSafeEnum(2, "拡大１");

        /// <summary>拡大２テンプレート</summary>
        public static readonly CalendarTemplateTypeSafeEnum EXPANSION2 = new CalendarTemplateTypeSafeEnum(3, "拡大２");

        public int Key { get; private set; }

        public string Name { get; private set; }

        public static CalendarTemplateTypeSafeEnum KeyOf(int key)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return null;
            }
        }

        private CalendarTemplateTypeSafeEnum(int key, string name)
        {
            this.Key = key;
            this.Name = name;
            dic.Add(key, this);
        }
    }
}
