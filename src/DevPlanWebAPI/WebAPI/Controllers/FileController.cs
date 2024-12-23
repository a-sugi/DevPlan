using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Http;
using DevPlanWebAPI.Models;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Base;
using DevPlanWebAPI.Logic;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DevPlanWebAPI.Controllers
{
    /// <summary>
    /// ファイル取得
    /// </summary>
    public class FileController : BaseAPIController<FileLogic, Stream>
    {
        #region ファイル取得
        /// <summary>
        /// ファイル取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns>IHttpActionResult</returns>
        [CommonMethodFilter]
        public HttpResponseMessage Post(FileSearchModel cond)
        {
            var stream = base.GetLogic().Get(cond);

            return base.GetResponse(new Dictionary<Stream, string> { { stream == null ? new MemoryStream() : stream, cond.SAVE_FILENAME } });

        }
        #endregion
    }
}
