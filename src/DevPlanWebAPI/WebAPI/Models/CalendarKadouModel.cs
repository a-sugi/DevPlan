﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// カレンダー稼働日非稼働日取得モデル。
    /// </summary>
    public class CalendarKadouModel
    {
        /// <summary>
        /// カレンダー日付
        /// </summary>
        public string CALENDAR_DATE { get; set; }

        /// <summary>
        /// 稼働日区分
        /// </summary>
        public string KADOBI_KBN { get; set; }
    }

    /// <summary>
    /// カレンダー稼働日非稼働日取得検索条件。
    /// </summary>
    [Serializable]
    public class CalendarKadouSearchModel
    {
        /// <summary>
        /// 稼働日取得開始日
        /// </summary>
        public DateTime FIRST_DATE { get; set; }

        /// <summary>
        /// 稼働日取得終了日
        /// </summary>
        public DateTime LAST_DATE { get; set; }
    }
}