using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 機能マスター一覧入力モデルクラス
    /// </summary>
    public class FunctionInModel
    {
    }
    /// <summary>
    /// 機能マスター一覧出力モデルクラス
    /// </summary>
    public class FunctionOutModel
    {
        /// <summary>
        /// 機能名
        /// </summary>
        public string FUNCTION_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }
    }
}