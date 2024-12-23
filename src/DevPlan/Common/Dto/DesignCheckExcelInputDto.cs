using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェックExcel取込判断モデルクラス
    /// </summary>
    public class DesignCheckExcelInputModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? 開催日_ID { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? 登録日 { get; set; }

        /// <summary>
        /// 登録者_ID
        /// </summary>
        public string 登録者_ID { get; set; }
    }
    //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修
}
