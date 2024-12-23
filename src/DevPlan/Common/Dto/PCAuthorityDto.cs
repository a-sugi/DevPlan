using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// PC端末権限検索入力モデルクラス
    /// </summary>
    public class PCAuthorityGetInModel
    {
        /// <summary>
        /// NetBIOS名
        /// </summary>
        public string PC_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        public int? FUNCTION_ID { get; set; }
    }
    /// <summary>
    /// PC端末権限検索出力モデルクラス
    /// </summary>
    public class PCAuthorityGetOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// NetBIOS名
        /// </summary>
        public string PC_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        public int FUNCTION_ID { get; set; }
        /// <summary>
        /// 機能名
        /// </summary>
        public string FUNCTION_NAME { get; set; }
    }
}
