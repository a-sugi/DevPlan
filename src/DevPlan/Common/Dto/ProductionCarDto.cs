using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 製作車検索入力モデルクラス
    /// </summary>
    public class ProductionCarSearchModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 完成日（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 完成日（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string VEHICLE { get; set; }

        /// <summary>
        /// 型式符号
        /// </summary>
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 使用部署
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// インポート日付（開始）
        /// </summary>
        public DateTime? IMPORT_START_DATE { get; set; }
        /// <summary>
        /// インポート日付（終了）
        /// </summary>
        public DateTime? IMPORT_END_DATE { get; set; }
        /// <summary>
        /// 反映フラグ
        /// </summary>
        public bool? FLAG_ENTRY { get; set; }
    }
    /// <summary>
    /// 製作車共通モデルクラス
    /// </summary>
    public class ProductionCarCommonModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string MAKER_NAME { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string CAR_MODEL { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        public string NAME_REMARKS { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        public DateTime? COMPLETE_DATE { get; set; }
        /// <summary>
        /// 担当コード（管理責任部署）
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 所属（管理所在地）
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string FIXED_ASSET_NO { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string CONSTRUCT_NO { get; set; }
        /// <summary>
        /// 処分予定年月
        /// </summary>
        public DateTime? DISPOSAL_PLAN_MONTH { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        public int? HISTORY_NO { get; set; }
        /// <summary>
        /// 通しNO
        /// </summary>
        public string SERIAL_NO { get; set; }
        /// <summary>
        /// 発行NO
        /// </summary>
        public string ISSUE_NO { get; set; }
        /// <summary>
        /// 改訂NO
        /// </summary>
        public string REVISION_NO { get; set; }
        /// <summary>
        /// 完成希望日
        /// </summary>
        public DateTime? COMPLETE_REQUEST_DATE { get; set; }
        /// <summary>
        /// 登録対象
        /// </summary>
        public string TARGET { get; set; }
        /// <summary>
        /// 制作方法
        /// </summary>
        public string METHOD { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string REMARKS { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ERROR_MESSAGE { get; set; }
        /// <summary>
        /// 登録(反映)日時
        /// </summary>
        public DateTime? ENTRY_DATETIME { get; set; }
        /// <summary>
        /// 登録(反映)者ID
        /// </summary>
        public string ENTRY_PERSONEL_ID { get; set; }
        /// <summary>
        /// 登録(反映)者名
        /// </summary>
        public string ENTRY_NAME { get; set; }
        /// <summary>
        /// 登録(インポート)日時
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
        /// <summary>
        /// 登録(インポート)者ID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
        /// <summary>
        /// 登録(インポート)者名
        /// </summary>
        public string INPUT_NAME { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? CHANGE_DATETIME { get; set; }
        /// <summary>
        /// 更新者ID
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
        /// <summary>
        /// 更新者名
        /// </summary>
        public string CHANGE_NAME { get; set; }
    }
    /// <summary>
    /// 製作車登録入力モデルクラス
    /// </summary>
    public class ProductionCarPostInModel
    {
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        ///号車
        /// </summary>
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string MAKER_NAME { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string CAR_MODEL { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        public string NAME_REMARKS { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        public DateTime? COMPLETE_DATE { get; set; }
        /// <summary>
        /// 担当コード（管理責任部署）
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string FIXED_ASSET_NO { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string CONSTRUCT_NO { get; set; }
        /// <summary>
        /// 処分予定年月
        /// </summary>
        public DateTime? DISPOSAL_PLAN_MONTH { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        public int? HISTORY_NO { get; set; }
        /// <summary>
        /// 通しNO
        /// </summary>
        public string SERIAL_NO { get; set; }
        /// <summary>
        /// 発行NO
        /// </summary>
        public string ISSUE_NO { get; set; }
        /// <summary>
        /// 改訂NO
        /// </summary>
        public string REVISION_NO { get; set; }
        /// <summary>
        /// 完成希望日
        /// </summary>
        public DateTime? COMPLETE_REQUEST_DATE { get; set; }
        /// <summary>
        /// 登録対象
        /// </summary>
        public string TARGET { get; set; }
        /// <summary>
        /// 制作方法
        /// </summary>
        public string METHOD { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string REMARKS { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ERROR_MESSAGE { get; set; }

        /// <summary>
        /// 登録者ID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 製作車更新入力モデルクラス
    /// </summary>
    public class ProductionCarPutInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        ///号車
        /// </summary>
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string DESTINATION { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string MAKER_NAME { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        public string CAR_MODEL { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        public string NAME_REMARKS { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        public DateTime? COMPLETE_DATE { get; set; }
        /// <summary>
        /// 担当コード（管理責任部署）
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string FIXED_ASSET_NO { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string CONSTRUCT_NO { get; set; }
        /// <summary>
        /// 処分予定年月
        /// </summary>
        public DateTime? DISPOSAL_PLAN_MONTH { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        public int? HISTORY_NO { get; set; }
        /// <summary>
        /// 通しNO
        /// </summary>
        public string SERIAL_NO { get; set; }
        /// <summary>
        /// 発行NO
        /// </summary>
        public string ISSUE_NO { get; set; }
        /// <summary>
        /// 改訂NO
        /// </summary>
        public string REVISION_NO { get; set; }
        /// <summary>
        /// 完成希望日
        /// </summary>
        public DateTime? COMPLETE_REQUEST_DATE { get; set; }
        /// <summary>
        /// 登録対象
        /// </summary>
        public string TARGET { get; set; }
        /// <summary>
        /// 制作方法
        /// </summary>
        public string METHOD { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string REMARKS { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ERROR_MESSAGE { get; set; }
        /// <summary>
        /// 更新者ID
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 製作車削除入力モデルクラス
    /// </summary>
    public class ProductionCarDeleteInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
    }
}