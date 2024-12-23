using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 定時間日の設定検索条件保持クラス
    /// </summary>
    public class FixedTimeDaySettingModel
    {
        /// <summary>
        /// 年月日
        /// </summary>
        public DateTime DATE { get; set; }

        /// <summary>
        /// 時間帯
        /// </summary>
        public string 時間帯 { get; set; }

        /// <summary>
        /// トラックID
        /// </summary>
        public long トラック_ID { get; set; }

        /// <summary>
        /// FLAG_定時間日フラグ
        /// </summary>
        public int FLAG_定時間日 { get; set; }

        /// <summary>
        /// FLAG_運休日フラグ
        /// </summary>
        public int FLAG_運休日 { get; set; }

        /// <summary>
        /// 休止時間帯リスト
        /// </summary>
        public List<string> TimeList
        {
            get
            {
                if (時間帯 == null)
                {
                    return new List<string>();
                }
                else
                {
                    return 時間帯.Split(',').ToList();
                }
            }
        }
    }

    /// <summary>
    /// トラック定期便予約時間帯マスタ格納クラス
    /// </summary>
    public class TruckRegularTimeModel
    {
        /// <summary>
        /// 定期便時間帯マスタID
        /// </summary>
        public int REGULAR_ID { get; set; }

        /// <summary>
        /// 時間ID（＝時間）
        /// </summary>
        public int TIME_ID { get; set; }

        /// <summary>
        /// 定期便ヘッダ表示名称
        /// </summary>
        public string DEPARTURE_TIME { get; set; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DISPLAY_NAME { get; set; }
        
        /// <summary>
        /// ヘッダ色（R）
        /// </summary>
        private int _colorCodeR;
        public int COLOR_CODE_R
        {
            set
            {
                _colorCodeR = value;
            }
        }

        /// <summary>
        /// ヘッダ色（G）
        /// </summary>
        private int _colorCodeG;
        public int COLOR_CODE_G
        {
            set
            {
                _colorCodeG = value;
            }
        }

        /// <summary>
        /// ヘッダ色（B）
        /// </summary>
        private int _colorCodeB;
        public int COLOR_CODE_B
        {
            set
            {
                _colorCodeB = value;
            }
        }

        /// <summary>
        /// 文字色（R）
        /// </summary>
        private int _fontColorCodeR;
        public int FONT_COLOR_CODE_R
        {
            set
            {
                _fontColorCodeR = value;
            }
        }

        /// <summary>
        /// 文字色（G）
        /// </summary>
        private int _fontColorCodeG;
        public int FONT_COLOR_CODE_G
        {
            set
            {
                _fontColorCodeG = value;
            }
        }

        /// <summary>
        /// 文字色（B）
        /// </summary>
        private int _fontColorCodeB;
        public int FONT_COLOR_CODE_B
        {
            set
            {
                _fontColorCodeB = value;
            }
        }

        /// <summary>
        /// 文字色
        /// </summary>
        public Color FontColor
        {
            get
            {
                return Color.FromArgb(_fontColorCodeR, _fontColorCodeG, _fontColorCodeB);
            }
        }

        /// <summary>
        /// ヘッダ色
        /// </summary>
        public Color ItemColor
        {
            get
            {
                return Color.FromArgb(_colorCodeR, _colorCodeG, _colorCodeB);
            }
        }

        /// <summary>
        /// 予約可能時間フラグ
        /// </summary>
        public int IS_RESERVATION { get; set; }
    }
    
    /// <summary>
    /// トラック管理者情報格納クラス
    /// </summary>
    public class TruckManagementUserModel
    {
        /// <summary>
        /// トラック管理者ID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// トラック管理者TEL
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// トラック管理者名
        /// </summary>
        public string NAME { get; set; }
    }

    /// <summary>
    /// トラック予約行先マスタ情報格納クラス
    /// </summary>
    public class TruckSectionModel
    {
        /// <summary>
        /// 行き先
        /// </summary>
        public string 行き先 { get; set; }
    }

    /// <summary>
    /// よく使う目的地格納クラス
    /// </summary>
    public class FrequentlyUsedDestinations
    {
        /// <summary>
        /// 出発地
        /// </summary>
        public string Departure { get; set; }

        /// <summary>
        /// 到着地
        /// </summary>
        public string Arrival { get; set; }

        /// <summary>
        /// 空荷チェック
        /// </summary>
        public bool EmptyCheck { get; set; }
    }

    /// <summary>
    /// メール原文データ格納クラス
    /// </summary>
    public class RegularMailDetailModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// TRACK_ID
        /// </summary>
        public long TRACK_ID { get; set; }

        /// <summary>
        /// 予約種別
        /// </summary>
        public string 予約種別 { get; set; }

        /// <summary>
        /// 件名
        /// </summary>
        public string 件名 { get; set; }

        /// <summary>
        /// 本文
        /// </summary>
        public string 本文 { get; set; }
    }

    /// <summary>
    /// メール特殊文字格納クラス
    /// </summary>
    public class RegularMailDetailMasterModel
    {
        /// <summary>
        /// メール特殊文字
        /// </summary>
        public string 文字列 { get; set; }
    }

    /// <summary>
    /// トラックコメント情報保持クラス
    /// </summary>
    public class TruckCommentModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// コメント種別
        /// </summary>
        public string コメント種別 { get; set; }

        /// <summary>
        /// 定型文
        /// </summary>
        public string 定型文 { get; set; }

        /// <summary>
        /// コメント
        /// </summary>
        public string コメント { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string タイトル { get; set; }        
    }
}
