using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// CAPで課（専門部門）が使用している開発符号の閲覧権限モデルクラス（入力）
    /// </summary>
    public class CapSectionUseGeneralCodeInModel
    {
        /// <summary>
        /// 社員コード（パーソナルID）
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// CAPで課（専門部門）が使用している開発符号の閲覧権限モデルクラス（出力）
    /// </summary>
    public class CapSectionUseGeneralCodeOutModel
    {
        /// <summary>
        /// 専門部署名
        /// </summary>
        public string 専門部署名 { get; set; }

        /// <summary>
        /// 権限フラグ(0:閲覧権限なし 1:閲覧権限あり)
        /// </summary>
        public int PERMIT_FLG { get; set; }
    }
}
