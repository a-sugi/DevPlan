using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 製作車検索入力モデルクラス
    /// </summary>
    public class ProductionCarSearchModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Range(0, 9999999999)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// 完成日（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "START_DATE")]
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 完成日（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "END_DATE")]
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "RESEARCH_NO")]
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "PROTOTYPE_PERIOD")]
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "VEHICLE")]
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "MODEL_CODE")]
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "VEHICLE_NO")]
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 使用部署
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "SECTION_GROUP_CODE")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "TEST_PURPOSE")]
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// インポート日付（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "IMPORT_START_DATE")]
        public DateTime? IMPORT_START_DATE { get; set; }
        /// <summary>
        /// インポート日付（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "IMPORT_END_DATE")]
        public DateTime? IMPORT_END_DATE { get; set; }
        /// <summary>
        /// 反映フラグ
        /// </summary>
        [Display(Name = "FLAG_ENTRY")]
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
        [StringLength(10)]
        [Display(Name = "CAR_GROUP")]
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "CAR_TYPE")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "PROTOTYPE_PERIOD")]
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        ///号車
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "VEHICLE")]
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "MODEL_CODE")]
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "DESTINATION")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "MAKER_NAME")]
        public string MAKER_NAME { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        [StringLength(50)]
        [Display(Name = "CAR_MODEL")]
        public string CAR_MODEL { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "NAME_REMARKS")]
        public string NAME_REMARKS { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(30)]
        [Display(Name = "VEHICLE_NO")]
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(255)]
        [Display(Name = "TEST_PURPOSE")]
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "COMPLETE_DATE")]
        public DateTime? COMPLETE_DATE { get; set; }
        /// <summary>
        /// 担当コード（管理責任部署）
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_CODE")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "RESEARCH_NO")]
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "FIXED_ASSET_NO")]
        public string FIXED_ASSET_NO { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "CONSTRUCT_NO")]
        public string CONSTRUCT_NO { get; set; }
        /// <summary>
        /// 処分予定年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "DISPOSAL_PLAN_MONTH")]
        public DateTime? DISPOSAL_PLAN_MONTH { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "MANAGEMENT_NO")]
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        [Display(Name = "HISTORY_NO")]
        public int? HISTORY_NO { get; set; }
        /// <summary>
        /// 通しNO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "SERIAL_NO")]
        public string SERIAL_NO { get; set; }
        /// <summary>
        /// 発行NO
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ISSUE_NO")]
        public string ISSUE_NO { get; set; }
        /// <summary>
        /// 改訂NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "REVISION_NO")]
        public string REVISION_NO { get; set; }
        /// <summary>
        /// 完成希望日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "COMPLETE_REQUEST_DATE")]
        public DateTime? COMPLETE_REQUEST_DATE { get; set; }
        /// <summary>
        /// 登録対象
        /// </summary>
        [StringLength(10)]
        [Display(Name = "TARGET")]
        public string TARGET { get; set; }
        /// <summary>
        /// 制作方法
        /// </summary>
        [StringLength(10)]
        [Display(Name = "METHOD")]
        public string METHOD { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "REMARKS")]
        public string REMARKS { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        [StringLength(255)]
        [Display(Name = "ERROR_MESSAGE")]
        public string ERROR_MESSAGE { get; set; }

        /// <summary>
        /// 登録者ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "INPUT_PERSONEL_ID")]
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
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "ID")]
        public long ID { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "CAR_GROUP")]
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "CAR_TYPE")]
        public string CAR_TYPE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "PROTOTYPE_PERIOD")]
        public string PROTOTYPE_PERIOD { get; set; }
        /// <summary>
        ///号車
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "VEHICLE")]
        public string VEHICLE { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "MODEL_CODE")]
        public string MODEL_CODE { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "DESTINATION")]
        public string DESTINATION { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "MAKER_NAME")]
        public string MAKER_NAME { get; set; }
        /// <summary>
        /// 車種
        /// </summary>
        [StringLength(50)]
        [Display(Name = "CAR_MODEL")]
        public string CAR_MODEL { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "NAME_REMARKS")]
        public string NAME_REMARKS { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(30)]
        [Display(Name = "VEHICLE_NO")]
        public string VEHICLE_NO { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(255)]
        [Display(Name = "TEST_PURPOSE")]
        public string TEST_PURPOSE { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "COMPLETE_DATE")]
        public DateTime? COMPLETE_DATE { get; set; }
        /// <summary>
        /// 担当コード（管理責任部署）
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_CODE")]
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "RESEARCH_NO")]
        public string RESEARCH_NO { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "FIXED_ASSET_NO")]
        public string FIXED_ASSET_NO { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "CONSTRUCT_NO")]
        public string CONSTRUCT_NO { get; set; }
        /// <summary>
        /// 処分予定年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "DISPOSAL_PLAN_MONTH")]
        public DateTime? DISPOSAL_PLAN_MONTH { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "MANAGEMENT_NO")]
        public string MANAGEMENT_NO { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        [Display(Name = "HISTORY_NO")]
        public int? HISTORY_NO { get; set; }
        /// <summary>
        /// 通しNO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "SERIAL_NO")]
        public string SERIAL_NO { get; set; }
        /// <summary>
        /// 発行NO
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ISSUE_NO")]
        public string ISSUE_NO { get; set; }
        /// <summary>
        /// 改訂NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "REVISION_NO")]
        public string REVISION_NO { get; set; }
        /// <summary>
        /// 完成希望日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "COMPLETE_REQUEST_DATE")]
        public DateTime? COMPLETE_REQUEST_DATE { get; set; }
        /// <summary>
        /// 登録対象
        /// </summary>
        [StringLength(10)]
        [Display(Name = "TARGET")]
        public string TARGET { get; set; }
        /// <summary>
        /// 制作方法
        /// </summary>
        [StringLength(10)]
        [Display(Name = "METHOD")]
        public string METHOD { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "REMARKS")]
        public string REMARKS { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        [StringLength(255)]
        [Display(Name = "ERROR_MESSAGE")]
        public string ERROR_MESSAGE { get; set; }
        /// <summary>
        /// 更新者ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "CHANGE_PERSONEL_ID")]
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
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "ID")]
        public long ID { get; set; }
    }
}