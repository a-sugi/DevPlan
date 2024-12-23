using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region ユーザー表示設定情報検索条件クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class UserDisplayConfigurationSearchModel
    {
        #region プロパティ
        /// <summary>ユーザーID</summary>
        public string ユーザーID { get; set; }

        /// <summary>画面名</summary>
        public string 画面名 { get; set; }

        //Append Start 
        /// <summary>GENERAL_CODE</summary>
        //public string GENERAL_CODE { get; set; }

        //Append End
        #endregion

    }
    #endregion

    #region ユーザー表示設定情報クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class UserDisplayConfigurationModel
    {
        #region プロパティ
        /// <summary>ユーザーID</summary>
        public string ユーザーID { get; set; }

        /// <summary>画面名</summary>
        public string 画面名 { get; set; }

        /// <summary>表示列名</summary>
        public string 表示列名 { get; set; }

        /// <summary>非表示列名</summary>
        public string 非表示列名 { get; set; }

        /// <summary>固定列数</summary>
        public short? 固定列数 { get; set; }
        #endregion

    }
    #endregion

}
