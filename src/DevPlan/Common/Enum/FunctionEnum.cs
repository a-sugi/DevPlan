using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Enum
{
    #region 機能ID
    /// <summary>機能ID</summary>
    public enum FunctionID : int
    {
        /* 2018年上半期 */

        /// <summary>ログイン制御</summary>
        Login = 1,

        /// <summary>試験車日程</summary>
        TestCar = 11,

        /// <summary>カーシェア日程</summary>
        CarShare = 12,

        /// <summary>外製車日程</summary>
        OuterCar = 13,

        /// <summary>カーシェア管理</summary>
        CarShareOffice = 14,

        /// <summary>車両検索</summary>
        CarSearch = 15,

        /// <summary>お知らせ</summary>
        Notice = 81,

        /// <summary>ロール設定</summary>
        Roll = 82,

        /// <summary>機能権限設定</summary>
        FunctionAuthority = 83,

        /// <summary>閲覧権限設定</summary>
        BrowsingAuthority = 84,

        /// <summary>試験車管理</summary>
        TestCarManagement = 101,

        /* 2019年上半期 */

        /// <summary>トラック予約表</summary>
        Truck = 16,

        /// <summary>ＣＡＰ・商品力</summary>
        CAP = 20,

        /// <summary>設計チェック</summary>
        DesignCheck = 21,

        /* TODO:未定 */

        /// <summary>計画表(TODO:未定)</summary>
        Plan = 2,

        /// <summary>目標進度リスト(TODO:未定)</summary>
        GoalProgress = 3,

        /// <summary>週報(TODO:未定)</summary>
        WeeklyReport = 7,

        /// <summary>月報(TODO:未定)</summary>
        MonthlyReport = 8,

        /// <summary>検討会資料(TODO:未定)</summary>
        ConsiderationDocument = 9,

        /// <summary>Xeyeデータ取込</summary>
        XeyeReadCsv = 85
    }
    #endregion
}