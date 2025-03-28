﻿using System.Web;
using System.Web.Mvc;

namespace DevPlanWebAPI
{
    /// <summary>
    /// FilterConfigクラス
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
