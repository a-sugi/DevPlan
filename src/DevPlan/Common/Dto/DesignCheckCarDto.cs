using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 設計チェック対象車検索入力モデルクラス
    /// </summary>
    public class DesignCheckCarGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? 開催日_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック対象車検索出力モデルクラス
    /// </summary>
    public class DesignCheckCarGetOutModel
    {
        /// <summary>
        /// 行選択用
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 対象車両ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int 試験車_ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        public string グレード { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string 試験目的 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        public string 排気量 { get; set; }
        /// <summary>
        /// EG型式
        /// </summary>
        public string E_G型式 { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        public string 駆動方式 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }
    }
    /// <summary>
    /// 設計チェック対象車登録入力モデルクラス
    /// </summary>
    public class DesignCheckCarPostInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int 試験車_ID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
    }
    /// <summary>
    /// 設計チェック対象車削除入力モデルクラス
    /// </summary>
    public class DesignCheckCarDeleteInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int 試験車_ID { get; set; }
    }
}
