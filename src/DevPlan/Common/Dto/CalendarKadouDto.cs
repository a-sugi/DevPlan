using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// カレンダー稼働日取得検索入力モデルクラス
    /// </summary>
    public class CalendarKadouGetInModel
    {
        /// <summary>
        /// 稼働日取得開始日
        /// </summary>
        public DateTime FIRST_DATE { get; set; }

        /// <summary>
        /// 稼働日取得終了日
        /// </summary>
        public DateTime LAST_DATE { get; set; }
    }

    /// <summary>
    /// カレンダー稼働日取得検索出力モデルクラス
    /// </summary>
    public class CalendarKadouGetOutModel
    {
        /// <summary>
        /// カレンダー日付
        /// </summary>
        public string CALENDAR_DATE { get; set; }

        /// <summary>
        /// 稼働日区分
        /// </summary>
        public string KADOBI_KBN { get; set; }
    }
}
