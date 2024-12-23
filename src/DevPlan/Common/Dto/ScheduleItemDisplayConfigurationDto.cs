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
    public class ScheduleItemDisplayConfigurationSearchModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string 開発符号 { get; set; }

        //Append End
        #endregion

    }
    #endregion

    #region ユーザー表示設定情報クラス
    /// <summary>
    /// ユーザー表示設定情報クラス
    /// </summary>
    public class ScheduleItemDisplayConfigurationModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string 開発符号 { get; set; }

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
