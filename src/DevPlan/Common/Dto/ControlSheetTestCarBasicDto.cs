using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 試験車基本情報(管理票)検索入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicGetInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)検索出力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicGetOutModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        public int 管理ラベル発行有無 { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string 型式符号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// リースNO
        /// </summary>
        public string リースNO { get; set; }
        /// <summary>
        /// リース満了日
        /// </summary>
        public DateTime? リース満了日 { get; set; }
        /// <summary>
        /// 研実管理廃却申請受理日
        /// </summary>
        public DateTime? 研実管理廃却申請受理日 { get; set; }
        /// <summary>
        /// 廃却見積日
        /// </summary>
        public DateTime? 廃却見積日 { get; set; }
        /// <summary>
        /// 廃却決済承認年月
        /// </summary>
        public DateTime? 廃却決済承認年月 { get; set; }
        /// <summary>
        /// 車両搬出日
        /// </summary>
        public DateTime? 車両搬出日 { get; set; }
        /// <summary>
        /// 廃却見積額
        /// </summary>
        public int 廃却見積額 { get; set; }
        /// <summary>
        /// 貸与先
        /// </summary>
        public string 貸与先 { get; set; }
        /// <summary>
        /// 貸与返却予定期限
        /// </summary>
        public DateTime? 貸与返却予定期限 { get; set; }
        /// <summary>
        /// 貸与返却日
        /// </summary>
        public DateTime? 貸与返却日 { get; set; }
        /// <summary>
        /// メモ
        /// </summary>
        public string メモ { get; set; }
        /// <summary>
        /// 正式取得日
        /// </summary>
        public DateTime? 正式取得日 { get; set; }
        /// <summary>
        /// 棚卸実施日
        /// </summary>
        public DateTime? 棚卸実施日 { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)更新入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicPutInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        public int 管理ラベル発行有無 { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string 型式符号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// リースNO
        /// </summary>
        public string リースNO { get; set; }
        /// <summary>
        /// リース満了日
        /// </summary>
        public DateTime? リース満了日 { get; set; }
        /// <summary>
        /// 研実管理廃却申請受理日
        /// </summary>
        public DateTime? 研実管理廃却申請受理日 { get; set; }
        /// <summary>
        /// 廃却見積日
        /// </summary>
        public DateTime? 廃却見積日 { get; set; }
        /// <summary>
        /// 廃却決済承認年月
        /// </summary>
        public DateTime? 廃却決済承認年月 { get; set; }
        /// <summary>
        /// 車両搬出日
        /// </summary>
        public DateTime? 車両搬出日 { get; set; }
        /// <summary>
        /// 貸与先
        /// </summary>
        public string 貸与先 { get; set; }
        /// <summary>
        /// 貸与返却予定期限
        /// </summary>
        public DateTime? 貸与返却予定期限 { get; set; }
        /// <summary>
        /// 貸与返却日
        /// </summary>
        public DateTime? 貸与返却日 { get; set; }
        /// <summary>
        /// メモ
        /// </summary>
        public string メモ { get; set; }
        /// <summary>
        /// 正式取得日
        /// </summary>
        public DateTime? 正式取得日 { get; set; }
        /// <summary>
        /// 棚卸実施日
        /// </summary>
        public DateTime? 棚卸実施日 { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)削除入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicDeleteInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
    }
}