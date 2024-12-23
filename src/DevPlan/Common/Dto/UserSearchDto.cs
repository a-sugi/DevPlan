using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// ユーザー検索入力モデルクラス
    /// </summary>
    [Serializable]
    public class UserSearchInModel
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string[] PERSONEL_ID { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string[] DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string[] SECTION_ID { get; set; }
        /// <summary>
        /// 氏名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        public string[] SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string[] SECTION_RELATIONAL_ID { get; set; }
        /// <summary>
        /// 管理部署別利用フラグ
        /// </summary>
        public bool MANAGE_FLG { get; set; } = false;
        /// <summary>
        /// ステータスコード
        /// </summary>
        public string STATUS_CODE { get; set; }
    }
    /// <summary>
    /// ユーザー検索出力モデルクラス
    /// </summary>
    public class UserSearchOutModel
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 氏名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 課グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// アクセス権限
        /// </summary>
        public string ACCESS_LEVEL { get; set; }
        /// <summary>
        /// 職位
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }
        /// <summary>
        /// 課グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 課グループ名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string SECTION_RELATIONAL_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// </summary>
        /// 研実フラグ
        /// </summary>
        public int FLAG_KENJITSU { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string MAIL_ADDRESS { get; set; }
        /// <summary>
        /// 管理部署種別
        /// </summary>
        public string[] 管理部署種別 { get; set; }
        /// <summary>
        /// ユーザーステータスコード
        /// </summary>        
        public string STATUS_CODE { get; set; }

        /// <summary>
        /// 表示名
        /// </summary>
        public string DISPLAY_NAME
        {
            //Update Start 2021/10/22 矢作
            //get { return string.Format("{0} {1}", this.SECTION_CODE, this.NAME); }
            get { return string.Format("{0} {1} {2}", this.SECTION_CODE, this.NAME, new Utils.ADUtil().GetUserData(Utils.ADUtilTypeEnum.MOBILE, this.PERSONEL_ID, this.NAME)); }
            //Update End 2021/10/22 矢作
        }

        //Append Start 2021/10/22 矢作
        /// <summary>
        /// 表示名2
        /// </summary>
        public string DISPLAY_NAME2 { get; set; }
        //Append End 2021/10/22 矢作
    }

    /// <summary>
    /// ユーザー検索モデルクラス
    /// </summary>
    public class UserSearchModel
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string PERSONEL_NAME { get; set; }
        /// <summary>
        /// 社員コード
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 担当名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// ユーザーステータスコード
        /// </summary>        
        public string STATUS_CODE { get; set; }
        /// <summary>
        /// 管理部署別利用フラグ(true：利用する、false：利用しない)
        /// </summary>
        public bool MANAGE_FLG { get; set; } = false;
        /// <summary>
        /// 大小文字区別フラグ(true：区別する、false：区別しない)
        /// </summary>
        public bool DISTINCT_FLG { get; set; } = true;
    }
}