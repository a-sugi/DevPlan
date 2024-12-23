using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region ファイル取得クラス
    /// <summary>
    /// ファイル取得クラス
    /// </summary>
    [Serializable]
    public class FileSearchModel
    {
        /// <summary>FILE_PATH</summary>
        [Display(Name = "FILE_PATH")]
        public string FILE_PATH { get; set; }

        /// <summary>SAVE_FILENAME</summary>
        [Display(Name = "SAVE_FILENAME")]
        public string SAVE_FILENAME { get; set; }
    }
    #endregion
}