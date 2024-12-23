using System;

namespace DevPlan.UICommon.Dto
{
    #region ファイルクラス
    /// <summary>
    /// ファイルクラス
    /// </summary>
    [Serializable]
    public class FileSearchModel
    {
        /// <summary>FILE_PATH</summary>
        public string FILE_PATH { get; set; }

        /// <summary>SAVE_FILENAME</summary>
        public string SAVE_FILENAME { get; set; }
    }
    #endregion
}
