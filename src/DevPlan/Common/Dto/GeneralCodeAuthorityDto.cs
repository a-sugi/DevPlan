using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlan.UICommon.Dto
{
    #region 開発符号権限検索モデル（入力）
    /// <summary>
    /// 開発符号権限検索モデル（入力）
    /// </summary>
    public class GeneralCodeAuthorityInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string SECTION_RELATIONAL_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// ユーザーステータスコード
        /// </summary>        
        public string STATUS_CODE { get; set; }

        /// <summary>
        /// 開発フラグ(0:開発外、1:開発中)
        /// </summary>
        public string UNDER_DEVELOPMENT { get; set; }

        /// <summary>
        /// 先開フラグ(true:含む、false:含まない)
        /// </summary>
        public bool PRE_FLG { get; set; } = true;

        /// <summary>
        /// 派遣・外注フラグ(true:含む、false:含まない)
        /// </summary>
        public bool BP_FLG { get; set; } = true;
    }
    #endregion

    #region 開発符号権限検索モデル（出力）
    /// <summary>
    /// 開発符号権限検索モデル（出力）
    /// </summary>
    public class GeneralCodeAuthorityOutModel
    {
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 許可期間（開始）
        /// </summary>
        public DateTime? PERMISSION_PERIOD_START { get; set; }

        /// <summary>
        /// 許可期間（終了）
        /// </summary>
        public DateTime? PERMISSION_PERIOD_END { get; set; }

        /// <summary>
        /// 開発フラグ
        /// </summary>
        public string UNDER_DEVELOPMENT { get; set; }

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
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 担当名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }

        /// <summary>
        /// 請負関係課ID
        /// </summary>
        public string SECTION_RELATIONAL_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 会社
        /// </summary>
        public string COMPANY { get; set; }

        /// <summary>
        /// アクセスレベル
        /// </summary>
        public string ACCESS_LEVEL { get; set; }

        /// <summary>
        /// 役職
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }

        /// <summary>
        /// ユーザーステータスコード
        /// </summary>        
        public string STATUS_CODE { get; set; }
    }
    #endregion

    #region 開発符号権限登録モデル
    /// <summary>
    /// 開発符号権限登録モデル
    /// </summary>
    public class GeneralCodeAuthorityEntryModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 許可期間（開始）
        /// </summary>
        public DateTime? PERMISSION_PERIOD_START { get; set; }

        /// <summary>
        /// 許可期間（終了）
        /// </summary>
        public DateTime? PERMISSION_PERIOD_END { get; set; }

        //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
        /// <summary>
        /// アクセスレベル
        /// </summary>
        public string ACCESS_LEVEL { get; set; }
        //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
    }
    #endregion

    #region 開発符号権限削除モデル
    /// <summary>
    /// 開発符号権限削除モデル
    /// </summary>
    public class GeneralCodeAuthorityDeleteModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
    }
    #endregion
}