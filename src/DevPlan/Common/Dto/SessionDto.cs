using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    public static class SessionDto
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public static string UserId { get; set; }

        /// <summary>
        /// ユーザー名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 権限区分
        /// </summary>
        public static string AuthFlag { get; set; }

        /// <summary>
        /// アクセスレベル
        /// </summary>
        public static string AccessLevel { get; set; }

        /// <summary>
        /// 課グループID
        /// </summary>
        public static string SectionGroupID { get; set;}
        
        /// <summary>
        /// 職位
        /// </summary>
        public static string OfficialPosition { get; set; }
        
        /// <summary>
        /// 課グループコード
        /// </summary>
        public static string SectionGroupCode { get; set; }

        /// <summary>
        /// 課グループ名
        /// </summary>
        public static string SectionGroupName { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public static string SectionID { get; set; }
        
        /// <summary>
        /// 課コード
        /// </summary>
        public static string SectionCode { get; set; }
        
        /// <summary>
        /// 課名
        /// </summary>
        public static string SectionName { get; set; }
        
        /// <summary>
        /// 部ID
        /// </summary>
        public static string DepartmentID { get; set; }
        
        /// <summary>
        /// 部コード
        /// </summary>
        public static string DepartmentCode { get; set; }
        
        /// <summary>
        /// 部名
        /// </summary>
        public static string DepartmentName { get; set; }

        /// <summary>
        /// 研実フラグ
        /// </summary>
        public static int KenjitsuFlag { get; set; }  

        /// <summary>
        /// 所属
        /// </summary>
        public static string Affiliation { get; set; }

        /// <summary>
        /// 管理部署種別
        /// </summary>
        public static string[] ManagementDepartmentType { get; set; }

        /// <summary>
        /// ユーザー権限リスト
        /// </summary>
        public static List<UserAuthorityOutModel> UserAuthorityList { get; set; } = null;

        /// <summary>
        /// ログインID
        /// </summary>
        public static string LoginId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// ログイン日時
        /// </summary>
        public static DateTime? LoginDateTime { get; set; } = null;

        /// <summary>
        /// アクティブ日時
        /// </summary>
        public static DateTime? ActiveDateTime { get; set; } = null;
    }
}
