using DevPlan.UICommon.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// トラックスケジュール数保持クラス
    /// </summary>
    public class TruckScheduleCountModel
    {
        /// <summary>
        /// トラックスケジュール数
        /// </summary>
        public long SCHEDULE_COUNT { get; set; }
    }

    /// <summary>
    /// トラック予約項目クラス
    /// </summary>
    public class TruckScheduleItemModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string 車両名 { get; set; }

        /// <summary>
        /// カレンダー表示用車両名
        /// </summary>
        public string CalendarCarName
        {
            get
            {
                var carName = 車両名;
                if (保管場所 == "SKC")
                {
                    carName += "\n(SKC常駐)";
                }

                if (予約可能開始日 > DateTime.Now || 予約可能開始日 == null)
                {
                    carName += "\n(予約不可)";
                }

                if (this.FLAG_定期便 == 1)
                {
                    return "【定期便" + Serial + "】（予約許可必要）\n--------------------\n" + carName;
                }
                else
                {
                    return carName;
                }
            }
        }

        /// <summary>
        /// 定期便連番コード
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// 保管場所
        /// </summary>
        public string 保管場所 { get; set; }

        /// <summary>
        /// 定期便
        /// </summary>
        public int FLAG_定期便 { get; set; }

        /// <summary>
        /// 種類
        /// </summary>
        public string 種類 { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }

        /// <summary>
        /// 登録番号
        /// </summary>
        public string 登録番号 { get; set; }

        /// <summary>
        /// ディーゼル規制
        /// </summary>
        public int FLAG_ディーゼル規制 { get; set; }

        /// <summary>
        /// 始発場所
        /// </summary>
        public string 始発場所 { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public double SORT_NO { get; set; }

        /// <summary>
        /// 予約可能開始日
        /// </summary>
        public DateTime? 予約可能開始日 { get; set; }
        
        /// <summary>
        /// 定期便時間帯
        /// </summary>
        public int REGULAR_TIME_ID { get; set; }

        /// <summary>
        /// 20日チェックフラグ
        /// </summary>
        public int FLAG_CHECK { get; set; }

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
        /// 項目カラー
        /// </summary>
        public CalendarScheduleColorEnum ItemColor
        {
            get
            {
                return CalendarScheduleColorEnum.GetValues(
                    Color.FromArgb(_colorCodeR, _colorCodeG, _colorCodeB),
                    Color.FromArgb(_fontColorCodeR, _fontColorCodeG, _fontColorCodeB));
            }
        }
    }

    /// <summary>
    /// トラックスケジュール情報保持クラス（詳細付き）
    /// </summary>
    public class TruckScheduleModel
    {
        /// <summary>
        /// 予約ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// トラックID
        /// </summary>
        public long トラック_ID { get; set; }

        /// <summary>
        /// 使用目的
        /// </summary>
        public string 使用目的 { get; set; }

        /// <summary>
        /// 予約開始時間
        /// </summary>
        public DateTime? 予約開始時間 { get; set; }

        /// <summary>
        /// 予約終了時間
        /// </summary>
        public DateTime? 予約終了時間 { get; set; }

        /// <summary>
        /// 空き時間状況
        /// </summary>
        public string 空き時間状況 { get; set; }

        /// <summary>
        /// 予約者ID
        /// </summary>
        public string 予約者_ID { get; set; }

        /// <summary>
        /// 修正者ID
        /// </summary>
        public string 修正者_ID { get; set; }

        /// <summary>
        /// 予約修正日時
        /// </summary>
        public DateTime? 予約修正日時 { get; set; }

        /// <summary>
        /// 運転者AID
        /// </summary>
        public string 運転者A_ID { get; set; }

        /// <summary>
        /// 運転者BID
        /// </summary>
        public string 運転者B_ID { get; set; }

        /// <summary>
        /// 運転者ATEL
        /// </summary>
        public string 運転者A_TEL { get; set; }

        /// <summary>
        /// 運転者BTEL
        /// </summary>
        public string 運転者B_TEL { get; set; }

        /// <summary>
        /// 仮予約フラグ  
        /// </summary>
        public int FLAG_仮予約 { get; set; }

        /// <summary>
        /// 定期便依頼者ID
        /// </summary>
        public string 定期便依頼者_ID { get; set; }

        /// <summary>
        /// 定期便依頼者TEL
        /// </summary>
        public string 定期便依頼者_TEL { get; set; }

        /// <summary>
        /// 定期便フラグ
        /// </summary>
        public int FLAG_定期便 { get; set; }

        /// <summary>
        /// 搬送車両名
        /// </summary>
        public string 搬送車両名 { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string 備考 { get; set; }

        /// <summary>
        /// 予約者名
        /// </summary>
        public string 予約者名 { get; set; }

        /// <summary>
        /// 運転者A名
        /// </summary>
        public string 運転者A名 { get; set; }

        /// <summary>
        /// 運転者B名
        /// </summary>
        public string 運転者B名 { get; set; }

        /// <summary>
        /// 定期便依頼者名
        /// </summary>
        public string 定期便依頼者名 { get; set; }

        /// <summary>
        /// 修正者名
        /// </summary>
        public string 修正者名 { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>
        /// 機密車
        /// </summary>
        public int FLAG_機密車 { get; set; }
        
        /// <summary>
        /// 車両名
        /// </summary>
        public string 車両名 { get; set; }

        /// <summary>
        /// 定期便時間帯
        /// </summary>
        public string DEPARTURE_TIME { get; set; }

        /// <summary>
        /// 発送者受領者情報
        /// </summary>
        public List<TruckShipperRecipientUserModel> ShipperRecipientUserList { get; set; }

        /// <summary>
        /// 発着地リスト
        /// </summary>
        public List<TruckScheduleSectionModel> SectionList { get; set; }

        /// <summary>
        /// 予約者アクセスレベル
        /// </summary>
        public string 予約者_ACCESS_LEVEL { get; set; }

        /// <summary>
        /// 予約者課コード
        /// </summary>
        public string 予約者_SECTION_ID { get; set; }

        /// <summary>
        /// 予約者担当コード
        /// </summary>
        public string 予約者_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 定期便依頼者アクセスレベル
        /// </summary>
        public string 定期便依頼者_ACCESS_LEVEL { get; set; }

        /// <summary>
        /// 定期便依頼者課コード
        /// </summary>
        public string 定期便依頼者_SECTION_ID { get; set; }

        /// <summary>
        /// 定期便依頼者担当コード
        /// </summary>
        public string 定期便依頼者_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 運転者Aアクセスレベル
        /// </summary>
        public string 運転者A_ACCESS_LEVEL { get; set; }

        /// <summary>
        /// 運転者A課コード
        /// </summary>
        public string 運転者A_SECTION_ID { get; set; }

        /// <summary>
        /// 運転者A担当コード
        /// </summary>
        public string 運転者A_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 運転者Bアクセスレベル
        /// </summary>
        public string 運転者B_ACCESS_LEVEL { get; set; }

        /// <summary>
        /// 運転者B課コード
        /// </summary>
        public string 運転者B_SECTION_ID { get; set; }

        /// <summary>
        /// 運転者B担当コード
        /// </summary>
        public string 運転者B_SECTION_GROUP_ID { get; set; }

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
        /// 搬送車両名に機密車情報文言を付与して返却します。
        /// 機密車情報がない場合は搬送車両名を返却します。
        /// </summary>
        /// <returns></returns>
        public string GetFullCarName()
        {
            if (FLAG_機密車 == 1)
            {
                return 搬送車両名 + "(機密車立会い)";
            }
            else
            {
                return 搬送車両名;
            }
        }
    }

    /// <summary>
    /// 発着地保持クラス
    /// </summary>
    public class TruckScheduleSectionModel
    {
        /// <summary>
        /// 発着地ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 予約ID
        /// </summary>
        public long 予約_ID { get; set; }

        /// <summary>
        /// 発着地
        /// </summary>
        public string 発着地 { get; set; }

        /// <summary>
        /// 空荷フラグ
        /// </summary>
        public int FLAG_空荷 { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int SORT_NO { get; set; }
    }
    
    /// <summary>
    /// トラック予約発送者受領者クラス
    /// </summary>
    public class TruckShipperRecipientUserModel
    {
        /// <summary>
        /// トラック予約発送者受領者ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// トラック予約ID
        /// </summary>
        public long 予約_ID { get; set; }

        /// <summary>
        /// 発送者ID
        /// </summary>
        public string 発送者_ID { get; set; }

        /// <summary>
        /// 発送者TEL
        /// </summary>
        public string 発送者_TEL { get; set; }

        /// <summary>
        /// 受領者ID
        /// </summary>
        public string 受領者_ID { get; set; }

        /// <summary>
        /// 受領者TEL
        /// </summary>
        public string 受領者_TEL { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        public int SORT_NO { get; set; }

        /// <summary>
        /// 発送者名
        /// </summary>
        public string 発送者名 { get; set; }

        /// <summary>
        /// 受領者名
        /// </summary>
        public string 受領者名 { get; set; }
    }
}
