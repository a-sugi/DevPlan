using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 状況記号（設計チェック）検索入力モデルクラス
    /// </summary>
    public class DesignCheckProgressSymbolGetInModel
    {
        /// <summary>
        /// 状況記号ID
        /// </summary>
        public int? ID { get; set; }
    }
    /// <summary>
    /// 状況記号（設計チェック）検索出力モデルクラス
    /// </summary>
    public class DesignCheckProgressSymbolGetOutModel
    {
        /// <summary>
        /// 状況記号ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 記号
        /// </summary>
        public string 記号 { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string 説明 { get; set; }
    }
}
