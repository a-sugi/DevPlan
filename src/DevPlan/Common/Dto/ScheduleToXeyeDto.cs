using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    public class ScheduleToXeyeSearchModel
    {
        /// <summary>
        /// 物品名2
        /// </summary>
        public string 物品名2 { get; set; }
    }

    public class ScheduleToXeyeOutModel
    {
        public string 物品コード { get; set; }

        public string 物品名 { get; set; }

        public string 物品名2 { get; set; }

        public string 物品名カナ { get; set; }

        public string 物品区分コード { get; set; }

        public string 備考 { get; set; }

        public string ソート順 { get; set; }

        public string 有効無効 { get; set; }
    }

    public class ErrorScheduleToXeyeOutModel
    {
        public int rownum { get; set; }

        public string 管理票NO { get; set; }

        public string エラーメッセージ { get; set; }
    }
}
