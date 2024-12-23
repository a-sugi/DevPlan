using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 処理待ち車両検索入力モデルクラス
    /// </summary>
    public class PendingCarSearchModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 一般フラグ
        /// </summary>
        public bool IPPAN_FLAG { get; set; } = false;
    }
    /// <summary>
    /// 処理待ち車両モデルクラス
    /// </summary>
    public class PendingCarModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int? データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        public int? 履歴NO { get; set; }
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
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名{ get; set; }
        /// <summary>
        /// 承認状況
        /// </summary>
        public string 承認状況 { get; set; }
        /// <summary>
        /// 承認要件名
        /// </summary>
        public string 承認要件名 { get; set; }
        /// <summary>
        /// 管理責任課名
        /// </summary>
        public string 管理責任課名 { get; set; }
        /// <summary>
        /// 管理責任部署名
        /// </summary>
        public string 管理責任部署名 { get; set; }
        /// <summary>
        /// 管理責任部署
        /// </summary>
        public string 管理責任部署 { get; set; }
        /// <summary>
        /// 移管先部署名
        /// </summary>
        public string 移管先部署名 { get; set; }
        /// <summary>
        /// 移管先部署ID
        /// </summary>
        public string 移管先部署ID { get; set; }
        /// <summary>
        /// 承認者レベル
        /// </summary>
        public string 承認者レベル { get; set; }
        /// <summary>
        /// 管理部署承認
        /// </summary>
        public string 管理部署承認 { get; set; }
        /// <summary>
        /// 承認日
        /// </summary>
        public DateTime? 承認日 { get; set; }
        /// <summary>
        /// 履歴STEPNO
        /// </summary>
        public int? 履歴STEPNO { get; set; }
        /// <summary>
        /// 承認STEPNO
        /// </summary>
        public int? 承認STEPNO { get; set; }
        /// <summary>
        /// 編集日
        /// </summary>
        public DateTime? 編集日 { get; set; }
    }
}
