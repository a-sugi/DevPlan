using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// ファイル取得クラス
    /// </summary>
    public class FileLogic : BaseLogic
    {
        #region ファイル取得
        /// <summary>
        /// ファイル取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public Stream Get(FileSearchModel cond)
        {
            return File.Exists(cond.FILE_PATH) == false ? null : File.OpenRead(cond.FILE_PATH);

        }
        #endregion

    }
}