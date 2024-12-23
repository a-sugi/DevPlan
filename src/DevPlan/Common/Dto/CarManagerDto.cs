using System;
using System.Text;

namespace DevPlan.UICommon.Dto
{
    #region 車両管理担当検索条件クラス
    /// <summary>
    /// 車両管理担当検索条件クラス
    /// </summary>
    public class CarManagerSearchModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>カテゴリID</summary>
        public string CATEGORY_ID { get; set; }

        /// <summary>STATUS</summary>
        public string STATUS { get; set; }

        /// <summary>機能ID</summary>
        public int FUNCTION_ID { get; set; }

        #endregion
    }
    #endregion

    #region 車両管理担当クラス
    /// <summary>
    /// 車両管理担当クラス
    /// </summary>
    public class CarManagerModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>課コード</summary>
        public string SECTION_CODE { get; set; }

        /// <summary>ユーザーID</summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>名前</summary>
        public string NAME { get; set; }

        /// <summary>電話番号</summary>
        public string TEL { get; set; }

        /// <summary>正副</summary>
        public string STATUS { get; set; }

        /// <summary>ID</summary>
        public string ID { get; set; }

        /// <summary>カテゴリID</summary>
        public string CATEGORY_ID { get; set; }

        /// <summary>備考</summary>
        public string REMARKS { get; set; }

        /// <summary>機能ID</summary>
        public int FUNCTION_ID { get; set; }
        #endregion
    }
    #endregion

}
