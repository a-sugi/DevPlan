using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 試験車日程スケジュール作業簡易入力一覧取得検索条件クラス
    /// </summary>
    public class TestCarCompleteScheduleSearchModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 閲覧権限
        /// </summary>
        public bool GeneralCodeFlg { get; set; }

        /// <summary>
        /// 表示対象ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 検索の対象開始日
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 設定者ID
        /// </summary>
        public string 設定者_ID { get; set; }

        /// <summary>
        /// 予約者ID
        /// </summary>
        public string 予約者_ID { get; set; }
    }

    /// <summary>
    /// 試験車日程スケジュール作業簡易入力クラス
    /// </summary>
    public class TestCarCompleteScheduleModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CATEGORY_ID { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string CATEGORY { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// スケジュール名
        /// </summary>
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// スケジュール開始
        /// </summary>
        public DateTime START_DATE { get; set; }

        /// <summary>
        /// スケジュール終了
        /// </summary>
        public DateTime END_DATE { get; set; }

        /// <summary>
        /// 予約者Id
        /// </summary>
        public string 予約者_ID { get; set; }

        /// <summary>
        /// 予約者SectionCode
        /// </summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>
        /// 予約者名
        /// </summary>
        public string 予約者_NAME { get; set; }
    }
}