using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
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
        /// 項目色（R)
        /// </summary>
        public int COLOR_CODE_R { get; set; }

        /// <summary>
        /// 項目色（G）
        /// </summary>
        public int COLOR_CODE_G { get; set; }

        /// <summary>
        /// 項目色（B）
        /// </summary>
        public int COLOR_CODE_B { get; set; }

        /// <summary>
        /// 文字色（R）
        /// </summary>
        public int FONT_COLOR_CODE_R { get; set; }

        /// <summary>
        /// 文字色（G）
        /// </summary>
        public int FONT_COLOR_CODE_G { get; set; }

        /// <summary>
        /// 文字色（B）
        /// </summary>
        public int FONT_COLOR_CODE_B { get; set; }

        /// <summary>
        /// 定期便時間帯
        /// </summary>
        public int REGULAR_TIME_ID { get; set; }

        /// <summary>
        /// 20日チェックフラグ
        /// </summary>
        public int FLAG_CHECK { get; set; }

        /// <summary>
        /// 定期便連番コード（丸数字）
        /// </summary>
        public string Serial { get; set; }
        
        /// <summary>
        /// 定期便連番コード
        /// </summary>
        public int SERIAL_NUMBER { get; set; }
    }

    /// <summary>
    /// トラックスケジュール情報保持クラス
    /// </summary>
    public class TruckScheduleDetailsModel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleDetailsModel()
        {
            return;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="schedule">初期値</param>
        public TruckScheduleDetailsModel(TruckScheduleDetailsModel schedule)
        {
            ID = schedule.ID;
            FLAG_仮予約 = schedule.FLAG_仮予約;
            FLAG_定期便 = schedule.FLAG_定期便;
            FLAG_機密車 = schedule.FLAG_機密車;
            PARALLEL_INDEX_GROUP = schedule.PARALLEL_INDEX_GROUP;
            トラック_ID = schedule.トラック_ID;
            予約修正日時 = schedule.予約修正日時;
            予約終了時間 = schedule.予約終了時間;
            予約者_ID = schedule.予約者_ID;
            予約者名 = schedule.予約者名;
            予約開始時間 = schedule.予約開始時間;
            使用目的 = schedule.使用目的;
            修正者_ID = schedule.修正者_ID;
            修正者名 = schedule.修正者名;
            備考 = schedule.備考;
            定期便依頼者_ID = schedule.定期便依頼者_ID;
            定期便依頼者_TEL = schedule.定期便依頼者_TEL;
            定期便依頼者名 = schedule.定期便依頼者名;
            搬送車両名 = schedule.搬送車両名;
            空き時間状況 = schedule.空き時間状況;
            運転者A_TEL = schedule.運転者A_TEL;
            運転者A_ID = schedule.運転者A_ID;
            運転者A名 = schedule.運転者A名;
            運転者B_ID = schedule.運転者B_ID;
            運転者B_TEL = schedule.運転者B_TEL;
            運転者B名 = schedule.運転者B名;
            COLOR_CODE_R = schedule.COLOR_CODE_R;
            COLOR_CODE_G = schedule.COLOR_CODE_G;
            COLOR_CODE_B = schedule.COLOR_CODE_B;
            FONT_COLOR_CODE_B = schedule.FONT_COLOR_CODE_B;
            FONT_COLOR_CODE_G = schedule.FONT_COLOR_CODE_G;
            FONT_COLOR_CODE_R = schedule.FONT_COLOR_CODE_R;
            車両名 = schedule.車両名;
            DEPARTURE_TIME = schedule.DEPARTURE_TIME;            
        }

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
        /// スケジュール色（R)
        /// </summary>
        public int COLOR_CODE_R { get; set; }

        /// <summary>
        /// スケジュール色（G）
        /// </summary>
        public int COLOR_CODE_G { get; set; }

        /// <summary>
        /// スケジュール色（B）
        /// </summary>
        public int COLOR_CODE_B { get; set; }

        /// <summary>
        /// 文字色（R）
        /// </summary>
        public int FONT_COLOR_CODE_R { get; set; }

        /// <summary>
        /// 文字色（G）
        /// </summary>
        public int FONT_COLOR_CODE_G { get; set; }

        /// <summary>
        /// 文字色（B）
        /// </summary>
        public int FONT_COLOR_CODE_B { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string 車両名 { get; set; }

        /// <summary>
        /// 定期便時間帯
        /// </summary>
        public string DEPARTURE_TIME { get; set; }

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
    }

    /// <summary>
    /// トラックスケジュール情報（詳細付き）
    /// </summary>
    public class TruckScheduleModel : TruckScheduleDetailsModel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleModel()
        {
            return;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="schedule">初期値</param>
        public TruckScheduleModel(TruckScheduleDetailsModel schedule) : base(schedule)
        {
            return;
        }

        /// <summary>
        /// 発着地リスト
        /// </summary>
        public List<TruckScheduleSectionModel> SectionList { get; set; }

        /// <summary>
        /// 発送者受領者情報
        /// </summary>
        public List<TruckShipperRecipientUserModel> ShipperRecipientUserList { get; set; }
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