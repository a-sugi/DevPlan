﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 指定月台数リスト検索条件クラス
    /// <summary>取得種別</summary>
    public enum DesignatedMonthNumberTargetType : int
    {
        /// <summary>保有台数</summary>
        Possession = 1,

        /// <summary>新規作成</summary>
        New = 2,

        /// <summary>改修</summary>
        Refurbishment = 3,

        /// <summary>廃却</summary>
        Disposal = 4

    }

    /// <summary>資産種別</summary>
    public enum AssetType : int
    {
        /// <summary>全て</summary>
        All = 0,

        /// <summary>白ナンバー</summary>
        WhiteNumber = 1,

        /// <summary>ナンバー無し</summary>
        NoNumber = 2,

        /// <summary>資産外</summary>
        OutAsset = 3,

        /// <summary>リース</summary>
        Lease = 4

    }

    /// <summary>集計種別</summary>
    public enum AggregateType : int
    {
        /// <summary>部</summary>
        Department = 1,

        /// <summary>課</summary>
        Section = 2,

        /// <summary>担当</summary>
        SectionGroup = 3

    }

    /// <summary>
    /// 指定月台数リスト検索条件クラス
    /// </summary>
    public class DesignatedMonthNumberSearchModel : TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>取得種別</summary>
        [Required]
        public DesignatedMonthNumberTargetType TARGET_TYPE { get; set; }

        /// <summary>資産種別</summary>
        [Required]
        public AssetType ASSET_TYPE { get; set; }

        /// <summary>集計種別</summary>
        public AggregateType AGGREGATE_TYPE { get; set; }
        #endregion
    }
    #endregion

    #region 台数集計結果クラス
    /// <summary>
    /// 台数集計結果クラス
    /// </summary>
    public class NumberAggregateModel
    {
        #region プロパティ
        /// <summary>
        /// 部コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "部コード")]
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当コード")]
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 白ナンバー
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "白ナンバー")]
        public int? WHITE_NUMBER { get; set; }

        /// <summary>
        /// ナンバー無し
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "ナンバー無し")]
        public int? NO_NUMBER { get; set; }

        /// <summary>
        /// 試算外
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "試算外")]
        public int? OUT_ASSET { get; set; }

        /// <summary>
        /// リース
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "リース")]
        public int? LEASE { get; set; }

        /// <summary>
        /// 合計
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "合計")]
        public int? TOTAL { get; set; }
        #endregion

    }
    #endregion
}