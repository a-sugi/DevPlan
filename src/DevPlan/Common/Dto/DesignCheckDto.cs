using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 設計チェック検索入力モデルクラス
    /// </summary>
    public class DesignCheckGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日（開始）
        /// </summary>
        public DateTime? OPEN_START_DATE { get; set; }
        /// <summary>
        /// 開催日（終了）
        /// </summary>
        public DateTime? OPEN_END_DATE { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
        /// <summary>
        /// オープンフラグ
        /// </summary>
        public bool OPEN_FLG { get; set; }
        /// <summary>
        /// クローズフラグ
        /// </summary>
        public bool CLOSE_FLG { get; set; }
    }
    /// <summary>
    /// 設計チェック検索出力モデルクラス
    /// </summary>
    public class DesignCheckGetOutModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日
        /// </summary>
        public DateTime? 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
        /// <summary>
        /// オープン件数
        /// </summary>
        public long? OPEN_COUNT { get; set; }
        /// <summary>
        /// クローズ件数
        /// </summary>
        public long? CLOSE_COUNT { get; set; }
    }
    /// <summary>
    /// 設計チェック登録入力モデルクラス
    /// </summary>
    public class DesignCheckPostInModel
    {
        /// <summary>
        /// 開催日
        /// </summary>
        public DateTime 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
    }
    /// <summary>
    /// 設計チェック更新入力モデルクラス
    /// </summary>
    public class DesignCheckPutInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日
        /// </summary>
        public DateTime 開催日 { get; set; }
        /// <summary>
        /// 開催回
        /// </summary>
        public int? 回 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
    }
    /// <summary>
    /// 設計チェック削除入力モデルクラス
    /// </summary>
    public class DesignCheckDeleteInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int ID { get; set; }
    }
    //Append Start 2021/08/19 杉浦 設計チェック請負
    /// <summary>
    /// 設計チェック表示領域比較クラス
    /// </summary>
    public class DesignCheckLengthCompareModel
    {
        /// <summary>
        /// カラム名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// カラムサイズ
        /// </summary>
        public int size { get; set; }
    }
    //Append Start 2021/08/21 杉浦 設計チェック請負
}
