using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 設計チェック参加者検索入力モデルクラス
    /// </summary>
    public class DesignCheckUserGetInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? 開催日_ID { get; set; }
        /// <summary>
        /// 参加者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者検索出力モデルクラス
    /// </summary>
    public class DesignCheckUserGetOutModel
    {
        /// <summary>
        /// 参加ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 参加者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 参加者名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者登録入力モデルクラス
    /// </summary>
    public class DesignCheckUserPostInModel
    {
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 参加者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック参加者削除入力モデルクラス
    /// </summary>
    public class DesignCheckUserDeleteInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
    }
}
