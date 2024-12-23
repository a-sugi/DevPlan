using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Enum
{
    #region ロールID
    /// <summary>ロールID</summary>
    public enum RollID : int
    {
        /// <summary>職制</summary>
        Syokusei = 1,

        /// <summary>担当</summary>
        Tanto = 2,

        /// <summary>一般</summary>
        Ippan = 3,

        /// <summary>STC</summary>
        STC = 4,

        /// <summary>パート</summary>
        Part = 5,

        /// <summary>シニア</summary>
        Senior = 6,

        /// <summary>派遣、外部委託</summary>
        BP = 7,

        /// <summary>STC_カーシェア利用者</summary>
        STC_CarShare = 8,

        /// <summary>PRINTSCREEN利用者</summary>
        PrintScreen = 9,

        /// <summary>システム管理者</summary>
        SystemAdmin = 10,

        /// <summary>カーシェア事務所</summary>
        CarShareOffice = 11,

        /// <summary>総括部署</summary>
        SJSB = 12,

        /// <summary>SKC管理課</summary>
        SKC = 13,

        /// <summary>試験車管理システム利用者</summary>
        TestCar = 14,

        /// <summary>試験車管理システム管理者</summary>
        TestCarManagement = 15
    }
    #endregion
}