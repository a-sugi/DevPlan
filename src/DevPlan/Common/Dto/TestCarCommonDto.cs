using System;

namespace DevPlan.UICommon.Dto
{
    #region 試験車共通検索条件クラス
    /// <summary>
    /// 試験車共通検索条件クラス
    /// </summary>
    public class TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        public int? データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        public int? 履歴NO { get; set; }

        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }

        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string 固定資産NO { get; set; }

        /// <summary>
        /// 保険NO
        /// </summary>
        public string 保険NO { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }

        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }

        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }

        /// <summary>
        /// 車体番号
        /// </summary>
        public string 車体番号 { get; set; }

        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }

        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }

        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }

        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string 名称 { get; set; }

        /// <summary>
        /// 管理票発行有無
        /// </summary>
        public string 管理票発行有無 { get; set; }

        /// <summary>
        /// 廃却フラグ
        /// </summary>
        public bool? 廃却フラグ { get; set; }

        /// <summary>
        /// 期間(From)
        /// </summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// 期間(To)
        /// </summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }

        /// <summary>
        /// 申請状況
        /// </summary>
        public string APPLICATION_STATUS { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        public string 種別 { get; set; }

        //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
        /// <summary>
        /// 管理責任部署
        /// </summary>
        public string[] 管理責任部署 { get; set; }
        //Append End 2022/01/11 杉浦 使用履歴設定画面の追加
        #endregion
    }
    #endregion

    #region 試験車共通クラス
    /// <summary>
    /// 試験車共通クラス
    /// </summary>
    public class TestCarCommonModel
    {
        #region 主キー
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        public int 履歴NO { get; set; }
        #endregion

        #region 試験車基本情報
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }

        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        public int? 管理ラベル発行有無 { get; set; }

        /// <summary>
        /// 管理ラベル発行
        /// </summary>
        public string 管理ラベル発行 { get; set; }

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
        public int? 廃却見積額 { get; set; }

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

        /// <summary>
        /// 月例点検省略有無
        /// </summary>
        public int? 月例点検省略有無 { get; set; }

        /// <summary>
        /// 廃却勧告
        /// </summary>
        public string 廃却勧告 { get; set; }

        //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        /// <summary>
        /// 衝突試験済
        /// </summary>
        public int? 衝突試験済 { get; set; }
        //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

        //Append Start 2022/03/24 杉浦 月例点検省略部署管理
        /// <summary>
        /// 月例点検省略有無_BASE
        /// </summary>
        public int? 月例点検省略有無_BASE { get; set; }

        public int 月例点検省略対象部署 { get; set; }
        //Append End 2022/03/24 杉浦 月例点検省略部署管理
        #endregion

        #region 試験車履歴情報
        /// <summary>
        /// 管理票発行有無
        /// </summary>
        public string 管理票発行有無 { get; set; }

        /// <summary>
        /// 発行年月日
        /// </summary>
        public DateTime? 発行年月日 { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }

        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }

        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }

        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }

        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }

        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }

        /// <summary>
        /// 名称備考
        /// </summary>
        public string 名称備考 { get; set; }

        /// <summary>
        /// 車体番号
        /// </summary>
        public string 車体番号 { get; set; }

        /// <summary>
        /// E_G番号
        /// </summary>
        public string E_G番号 { get; set; }

        /// <summary>
        /// E_G型式
        /// </summary>
        public string E_G型式 { get; set; }

        /// <summary>
        /// 排気量
        /// </summary>
        public string 排気量 { get; set; }

        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }

        /// <summary>
        /// 駆動方式
        /// </summary>
        public string 駆動方式 { get; set; }

        /// <summary>
        /// グレード
        /// </summary>
        public string グレード { get; set; }

        /// <summary>
        /// 車体色
        /// </summary>
        public string 車体色 { get; set; }

        /// <summary>
        /// 試験目的
        /// </summary>
        public string 試験目的 { get; set; }

        /// <summary>
        /// 受領日
        /// </summary>
        public DateTime? 受領日 { get; set; }

        /// <summary>
        /// 受領部署
        /// </summary>
        public string 受領部署 { get; set; }

        /// <summary>
        /// 受領者
        /// </summary>
        public string 受領者 { get; set; }

        /// <summary>
        /// 受領時走行距離
        /// </summary>
        public string 受領時走行距離 { get; set; }

        /// <summary>
        /// 完成日
        /// </summary>
        public DateTime? 完成日 { get; set; }

        /// <summary>
        /// 管理責任部署
        /// </summary>
        public string 管理責任部署 { get; set; }

        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string 研命ナンバー { get; set; }

        /// <summary>
        /// 研命期間
        /// </summary>
        public DateTime? 研命期間 { get; set; }

        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string 固定資産NO { get; set; }

        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }

        /// <summary>
        /// 車検登録日
        /// </summary>
        public DateTime? 車検登録日 { get; set; }

        /// <summary>
        /// 車検期限
        /// </summary>
        public DateTime? 車検期限 { get; set; }

        /// <summary>
        /// 廃艦日
        /// </summary>
        public DateTime? 廃艦日 { get; set; }

        /// <summary>
        /// 保険NO
        /// </summary>
        public string 保険NO { get; set; }

        /// <summary>
        /// 保険加入日
        /// </summary>
        public DateTime? 保険加入日 { get; set; }

        /// <summary>
        /// 保険解約日
        /// </summary>
        public DateTime? 保険解約日 { get; set; }

        /// <summary>
        /// 保険料
        /// </summary>
        public int? 保険料 { get; set; }

        /// <summary>
        /// 自動車税
        /// </summary>
        public int? 自動車税 { get; set; }

        /// <summary>
        /// 移管依頼NO
        /// </summary>
        public string 移管依頼NO { get; set; }

        /// <summary>
        /// 三鷹移管先部署
        /// </summary>
        public string 三鷹移管先部署 { get; set; }

        /// <summary>
        /// 三鷹移管年月
        /// </summary>
        public DateTime? 三鷹移管年月 { get; set; }

        /// <summary>
        /// 三鷹移管先固資NO
        /// </summary>
        public string 三鷹移管先固資NO { get; set; }

        /// <summary>
        /// 試験着手日
        /// </summary>
        public DateTime? 試験着手日 { get; set; }

        /// <summary>
        /// 試験着手証明文書
        /// </summary>
        public string 試験着手証明文書 { get; set; }

        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string 工事区分NO { get; set; }

        /// <summary>
        /// FLAG_中古
        /// </summary>
        public short? FLAG_中古 { get; set; }

        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public short? FLAG_ETC付 { get; set; }

        /// <summary>
        /// EVデバイス
        /// </summary>
        public string EVデバイス { get; set; }

        /// <summary>
        /// 初年度登録年月
        /// </summary>
        public DateTime? 初年度登録年月 { get; set; }

        /// <summary>
        /// 資産種別
        /// </summary>
        public short? 資産種別 { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        public string 種別 { get; set; }

        /// <summary>
        /// 使用期限
        /// </summary>
        public DateTime? 使用期限 { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string 名称 { get; set; }

        //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        /// <summary>
        /// 自動車ﾘｻｲｸﾙ法
        /// </summary>
        public string 自動車ﾘｻｲｸﾙ法 { get; set; }

        /// <summary>
        /// A_C冷媒種類
        /// </summary>
        public string A_C冷媒種類 { get; set; }
        //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        #endregion

        #region 固定資産情報
        /// <summary>
        /// 勘定科目
        /// </summary>
        public string 勘定科目 { get; set; }

        /// <summary>
        /// 子資産
        /// </summary>
        public string 子資産 { get; set; }

        /// <summary>
        /// 所得年月
        /// </summary>
        public DateTime? 所得年月 { get; set; }

        /// <summary>
        /// 設置場所
        /// </summary>
        public string 設置場所 { get; set; }

        /// <summary>
        /// 耐用年数
        /// </summary>
        public int? 耐用年数 { get; set; }

        /// <summary>
        /// 取得価額
        /// </summary>
        public int? 取得価額 { get; set; }

        /// <summary>
        /// 減価償却累計額
        /// </summary>
        public int? 減価償却累計額 { get; set; }

        /// <summary>
        /// 期末簿価
        /// </summary>
        public int? 期末簿価 { get; set; }

        /// <summary>
        /// 資産タイプ
        /// </summary>
        public int? 資産タイプ { get; set; }

        /// <summary>
        /// 固定資産税
        /// </summary>
        public int? 固定資産税 { get; set; }

        /// <summary>
        /// 原価部門
        /// </summary>
        public string 原価部門 { get; set; }

        /// <summary>
        /// 管理部署
        /// </summary>
        public string 管理部署 { get; set; }

        /// <summary>
        /// 資産計上部署
        /// </summary>
        public string 資産計上部署 { get; set; }

        /// <summary>
        /// 事業所コード
        /// </summary>
        public string 事業所コード { get; set; }

        /// <summary>
        /// 処分コード
        /// </summary>
        public string 処分コード { get; set; }

        /// <summary>
        /// 処分予定年月
        /// </summary>
        public DateTime? 処分予定年月 { get; set; }

        /// <summary>
        /// 処分数
        /// </summary>
        public int? 処分数 { get; set; }

        /// <summary>
        /// 処分区分
        /// </summary>
        public string 処分区分 { get; set; }

        /// <summary>
        /// 除却年度
        /// </summary>
        public string 除却年度 { get; set; }

        /// <summary>
        /// 除却明細名称
        /// </summary>
        public string 除却明細名称 { get; set; }
        #endregion

        #region 開本内移管履歴情報
        /// <summary>
        /// 開本内移管履歴NO
        /// </summary>
        public int? 開本内移管履歴NO { get; set; }

        /// <summary>
        /// 開本内移管日
        /// </summary>
        public DateTime? 開本内移管日 { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string 内容 { get; set; }
        #endregion

        #region 開発符号情報
        /// <summary>
        /// 車名
        /// </summary>
        public string 車名 { get; set; }
        #endregion

        #region 車系情報
        /// <summary>
        /// 車区分
        /// </summary>
        public string 車区分 { get; set; }

        /// <summary>
        /// 車検区分1
        /// </summary>
        public string 車検区分1 { get; set; }

        /// <summary>
        /// 車検区分2
        /// </summary>
        public string 車検区分2 { get; set; }
        #endregion

        #region 部
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 管理所在地
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        #endregion

        #region 課
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        #endregion

        #region 担当
        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        #endregion

        #region 受領
        /// <summary>
        /// 受領部署_部ID
        /// </summary>
        public string 受領部署_DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 受領部署_部コード
        /// </summary>
        public string 受領部署_DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 受領部署_課ID
        /// </summary>
        public string 受領部署_SECTION_ID { get; set; }

        /// <summary>
        /// 受領部署_課コード
        /// </summary>
        public string 受領部署_SECTION_CODE { get; set; }

        /// <summary>
        /// 受領部署_担当ID
        /// </summary>
        public string 受領部署_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 受領部署_担当コード
        /// </summary>
        public string 受領部署_SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 受領者_NAME
        /// </summary>
        public string 受領者_NAME { get; set; }
        #endregion

        #region GPS識別子
        /// <summary>
        /// GPS識別子
        /// </summary>
        public string XEYE_EXIST { get; set; }
        #endregion

        #region クローン
        /// <summary>
        /// クローン
        /// </summary>
        public TestCarCommonModel Clone()
        {
            return (TestCarCommonModel)MemberwiseClone();
        }
        #endregion
    }
    #endregion

    #region 共通マスタ
    /// <summary>
    /// 共通マスタ
    /// </summary>
    public class CommonMasterModel
    {
        #region プロパティ
        /// <summary>
        /// コード
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        #endregion
    }
    #endregion

}