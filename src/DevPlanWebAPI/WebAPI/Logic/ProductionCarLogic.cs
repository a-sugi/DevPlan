using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 製作車ロジッククラス
    /// </summary>
    /// <remarks>製作車の操作</remarks>
    public class ProductionCarLogic : TestCarBaseLogic
    {
        /// <summary>
        /// 製作車の取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<ProductionCarCommonModel> GetData(ProductionCarSearchModel val)
        {
            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     PC.ID");
            sql.AppendLine("    ,PC.CAR_GROUP");
            sql.AppendLine("    ,PC.CAR_TYPE");
            sql.AppendLine("    ,PC.GENERAL_CODE");
            sql.AppendLine("    ,PC.PROTOTYPE_PERIOD");
            sql.AppendLine("    ,PC.VEHICLE");
            sql.AppendLine("    ,PC.MODEL_CODE");
            sql.AppendLine("    ,PC.DESTINATION");
            sql.AppendLine("    ,PC.MAKER_NAME");
            sql.AppendLine("    ,PC.CAR_MODEL");
            sql.AppendLine("    ,PC.NAME_REMARKS");
            sql.AppendLine("    ,PC.VEHICLE_NO");
            sql.AppendLine("    ,PC.TEST_PURPOSE");
            sql.AppendLine("    ,PC.COMPLETE_DATE");
            sql.AppendLine("    ,PC.SECTION_GROUP_CODE");
            sql.AppendLine("    ,PC.RESEARCH_NO");
            sql.AppendLine("    ,PC.FIXED_ASSET_NO");
            sql.AppendLine("    ,PC.CONSTRUCT_NO");
            sql.AppendLine("    ,PC.DISPOSAL_PLAN_MONTH");
            sql.AppendLine("    ,PC.MANAGEMENT_NO");
            sql.AppendLine("    ,PC.HISTORY_NO");
            sql.AppendLine("    ,PC.SERIAL_NO");
            sql.AppendLine("    ,PC.ISSUE_NO");
            sql.AppendLine("    ,PC.REVISION_NO");
            sql.AppendLine("    ,PC.TARGET");
            sql.AppendLine("    ,PC.METHOD");
            sql.AppendLine("    ,PC.REMARKS");
            sql.AppendLine("    ,PC.COMPLETE_REQUEST_DATE");
            sql.AppendLine("    ,PC.ERROR_MESSAGE");
            sql.AppendLine("    ,PC.ENTRY_DATETIME");
            sql.AppendLine("    ,PC.ENTRY_PERSONEL_ID");
            sql.AppendLine("    ,PL0.NAME ENTRY_NAME");
            sql.AppendLine("    ,PC.INPUT_DATETIME");
            sql.AppendLine("    ,PC.INPUT_PERSONEL_ID");
            sql.AppendLine("    ,PL1.NAME INPUT_NAME");
            sql.AppendLine("    ,PC.CHANGE_DATETIME");
            sql.AppendLine("    ,PC.CHANGE_PERSONEL_ID");
            sql.AppendLine("    ,PL2.NAME CHANGE_NAME");
            sql.AppendLine("FROM");
            sql.AppendLine("    PRODUCTION_CAR PC");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL0");
            sql.AppendLine("    ON PC.ENTRY_PERSONEL_ID = PL0.PERSONEL_ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL1");
            sql.AppendLine("    ON PC.INPUT_PERSONEL_ID = PL1.PERSONEL_ID");
            sql.AppendLine("    LEFT JOIN PERSONEL_LIST PL2");
            sql.AppendLine("    ON PC.CHANGE_PERSONEL_ID = PL2.PERSONEL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // ID
            if (val?.ID > 0)
            {
                sql.AppendLine("    AND PC.ID = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int64,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });
            }

            // 反映フラグ
            if (val?.FLAG_ENTRY == true)
            {
                sql.AppendLine("    AND PC.ENTRY_DATETIME IS NOT NULL");

                // 反映日（開始）
                if (val?.START_DATE != null)
                {
                    sql.AppendLine("    AND PC.ENTRY_DATETIME >= :START_DATE");

                    prms.Add(new BindModel
                    {
                        Name = ":START_DATE",
                        Type = OracleDbType.Date,
                        Object = val.START_DATE,
                        Direct = ParameterDirection.Input
                    });
                }

                // 反映日（終了）
                if (val?.END_DATE != null)
                {
                    sql.AppendLine("    AND PC.ENTRY_DATETIME <= :END_DATE");

                    prms.Add(new BindModel
                    {
                        Name = ":END_DATE",
                        Type = OracleDbType.Date,
                        Object = val.END_DATE,
                        Direct = ParameterDirection.Input
                    });
                }
            }
            else if (val?.FLAG_ENTRY == false)
            {
                sql.AppendLine("    AND PC.ENTRY_DATETIME IS NULL");
            }
            else
            {
                // 検索条件なし
            }

            // 研命ナンバー
            SetStringWhere(sql, prms, "PC", "RESEARCH_NO", val?.RESEARCH_NO);

            // 開発符号
            SetStringWhere(sql, prms, "PC", "GENERAL_CODE", val?.GENERAL_CODE);

            // 試作時期
            SetStringWhere(sql, prms, "PC", "PROTOTYPE_PERIOD", val?.PROTOTYPE_PERIOD);

            // 号車
            SetStringWhere(sql, prms, "PC", "VEHICLE", val?.VEHICLE);

            // 型式符号
            SetStringWhere(sql, prms, "PC", "MODEL_CODE", val?.MODEL_CODE);

            // 車体番号
            SetStringWhere(sql, prms, "PC", "VEHICLE_NO", val?.VEHICLE_NO);

            // 使用部署
            SetStringWhere(sql, prms, "PC", "SECTION_GROUP_CODE", val?.SECTION_GROUP_CODE);

            // 試験目的
            SetStringWhere(sql, prms, "PC", "TEST_PURPOSE", val?.TEST_PURPOSE);

            // 作成日（開始）
            if (val?.IMPORT_START_DATE != null)
            {
                sql.AppendLine("    AND PC.INPUT_DATETIME >= :IMPORT_START_DATE");

                prms.Add(new BindModel
                {
                    Name = ":IMPORT_START_DATE",
                    Type = OracleDbType.Date,
                    Object = val.IMPORT_START_DATE,
                    Direct = ParameterDirection.Input
                });
            }

            // 作成日（終了）
            if (val?.IMPORT_END_DATE != null)
            {
                sql.AppendLine("    AND PC.INPUT_DATETIME <= :IMPORT_END_DATE");

                prms.Add(new BindModel
                {
                    Name = ":IMPORT_END_DATE",
                    Type = OracleDbType.Date,
                    Object = val.IMPORT_END_DATE,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     PC.ID ASC");

            return db.ReadModelList<ProductionCarCommonModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 製作車の作成
        /// </summary>
        /// <returns>bool</returns>
        public bool PostData(List<ProductionCarPostInModel> list, ref List<ProductionCarCommonModel> res)
        {
            var sql = new StringBuilder();

            sql.AppendLine("MERGE INTO PRODUCTION_CAR");
            sql.AppendLine("    USING (");
            sql.AppendLine("        SELECT 1 FLAG_INSERT FROM DUAL");
            sql.AppendLine("    ) DUMMY");
            sql.AppendLine("    ON (");
            sql.AppendLine("        PRODUCTION_CAR.GENERAL_CODE = :GENERAL_CODE ");
            sql.AppendLine("        AND PRODUCTION_CAR.PROTOTYPE_PERIOD = :PROTOTYPE_PERIOD ");
            sql.AppendLine("        AND PRODUCTION_CAR.VEHICLE = :VEHICLE ");
            sql.AppendLine("    )");

            sql.AppendLine("WHEN MATCHED THEN");

            // 更新
            sql.AppendLine("UPDATE SET");
            sql.AppendLine("     CAR_GROUP = :CAR_GROUP");
            sql.AppendLine("    ,CAR_TYPE = :CAR_TYPE");
            sql.AppendLine("    ,MODEL_CODE = :MODEL_CODE");
            sql.AppendLine("    ,DESTINATION = :DESTINATION");
            sql.AppendLine("    ,MAKER_NAME = :MAKER_NAME");
            sql.AppendLine("    ,CAR_MODEL = :CAR_MODEL");
            sql.AppendLine("    ,NAME_REMARKS = :NAME_REMARKS");
            sql.AppendLine("    ,VEHICLE_NO = :VEHICLE_NO");
            sql.AppendLine("    ,TEST_PURPOSE = :TEST_PURPOSE");
            sql.AppendLine("    ,COMPLETE_DATE = :COMPLETE_DATE");
            sql.AppendLine("    ,SECTION_GROUP_CODE = :SECTION_GROUP_CODE");
            sql.AppendLine("    ,RESEARCH_NO = :RESEARCH_NO");
            sql.AppendLine("    ,FIXED_ASSET_NO = :FIXED_ASSET_NO");
            sql.AppendLine("    ,CONSTRUCT_NO = :CONSTRUCT_NO");
            sql.AppendLine("    ,DISPOSAL_PLAN_MONTH = :DISPOSAL_PLAN_MONTH");
            sql.AppendLine("    ,MANAGEMENT_NO = :MANAGEMENT_NO");
            sql.AppendLine("    ,HISTORY_NO = :HISTORY_NO");
            sql.AppendLine("    ,SERIAL_NO = :SERIAL_NO");
            sql.AppendLine("    ,ISSUE_NO = :ISSUE_NO");
            sql.AppendLine("    ,REVISION_NO = :REVISION_NO");
            sql.AppendLine("    ,COMPLETE_REQUEST_DATE = :COMPLETE_REQUEST_DATE");
            sql.AppendLine("    ,TARGET = :TARGET");
            sql.AppendLine("    ,METHOD = :METHOD");
            sql.AppendLine("    ,REMARKS = :REMARKS");
            sql.AppendLine("    ,ERROR_MESSAGE = :ERROR_MESSAGE");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID = :INPUT_PERSONEL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ENTRY_DATETIME IS NULL");

            sql.AppendLine("WHEN NOT MATCHED THEN");

            // 登録
            sql.AppendLine("INSERT (");
            sql.AppendLine("     ID");
            sql.AppendLine("    ,CAR_GROUP");
            sql.AppendLine("    ,CAR_TYPE");
            sql.AppendLine("    ,GENERAL_CODE");
            sql.AppendLine("    ,PROTOTYPE_PERIOD");
            sql.AppendLine("    ,VEHICLE");
            sql.AppendLine("    ,MODEL_CODE");
            sql.AppendLine("    ,DESTINATION");
            sql.AppendLine("    ,MAKER_NAME");
            sql.AppendLine("    ,CAR_MODEL");
            sql.AppendLine("    ,NAME_REMARKS");
            sql.AppendLine("    ,VEHICLE_NO");
            sql.AppendLine("    ,TEST_PURPOSE");
            sql.AppendLine("    ,COMPLETE_DATE");
            sql.AppendLine("    ,SECTION_GROUP_CODE");
            sql.AppendLine("    ,RESEARCH_NO");
            sql.AppendLine("    ,FIXED_ASSET_NO");
            sql.AppendLine("    ,CONSTRUCT_NO");
            sql.AppendLine("    ,DISPOSAL_PLAN_MONTH");
            sql.AppendLine("    ,MANAGEMENT_NO");
            sql.AppendLine("    ,HISTORY_NO");
            sql.AppendLine("    ,SERIAL_NO");
            sql.AppendLine("    ,ISSUE_NO");
            sql.AppendLine("    ,REVISION_NO");
            sql.AppendLine("    ,COMPLETE_REQUEST_DATE");
            sql.AppendLine("    ,TARGET");
            sql.AppendLine("    ,METHOD");
            sql.AppendLine("    ,REMARKS");
            sql.AppendLine("    ,ERROR_MESSAGE");
            sql.AppendLine("    ,INPUT_DATETIME");
            sql.AppendLine("    ,INPUT_PERSONEL_ID");
            sql.AppendLine("    ,CHANGE_DATETIME");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     (SELECT NVL(MAX(ID), 0) +1 FROM PRODUCTION_CAR)");
            sql.AppendLine("    ,:CAR_GROUP");
            sql.AppendLine("    ,:CAR_TYPE");
            sql.AppendLine("    ,:GENERAL_CODE");
            sql.AppendLine("    ,:PROTOTYPE_PERIOD");
            sql.AppendLine("    ,:VEHICLE");
            sql.AppendLine("    ,:MODEL_CODE");
            sql.AppendLine("    ,:DESTINATION");
            sql.AppendLine("    ,:MAKER_NAME");
            sql.AppendLine("    ,:CAR_MODEL");
            sql.AppendLine("    ,:NAME_REMARKS");
            sql.AppendLine("    ,:VEHICLE_NO");
            sql.AppendLine("    ,:TEST_PURPOSE");
            sql.AppendLine("    ,:COMPLETE_DATE");
            sql.AppendLine("    ,:SECTION_GROUP_CODE");
            sql.AppendLine("    ,:RESEARCH_NO");
            sql.AppendLine("    ,:FIXED_ASSET_NO");
            sql.AppendLine("    ,:CONSTRUCT_NO");
            sql.AppendLine("    ,:DISPOSAL_PLAN_MONTH");
            sql.AppendLine("    ,:MANAGEMENT_NO");
            sql.AppendLine("    ,:HISTORY_NO");
            sql.AppendLine("    ,:SERIAL_NO");
            sql.AppendLine("    ,:ISSUE_NO");
            sql.AppendLine("    ,:REVISION_NO");
            sql.AppendLine("    ,:COMPLETE_REQUEST_DATE");
            sql.AppendLine("    ,:TARGET");
            sql.AppendLine("    ,:METHOD");
            sql.AppendLine("    ,:REMARKS");
            sql.AppendLine("    ,:ERROR_MESSAGE");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine("    ,SYSDATE");
            sql.AppendLine("    ,:INPUT_PERSONEL_ID");
            sql.AppendLine(")");

            foreach (var val in list)
            {
                var prms = new List<BindModel>()
                {
                    new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = val.CAR_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CAR_TYPE", Type = OracleDbType.Varchar2, Object = val.CAR_TYPE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PROTOTYPE_PERIOD", Type = OracleDbType.Varchar2, Object = val.PROTOTYPE_PERIOD, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":VEHICLE", Type = OracleDbType.Varchar2, Object = val.VEHICLE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MODEL_CODE", Type = OracleDbType.Varchar2, Object = val.MODEL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESTINATION", Type = OracleDbType.Varchar2, Object = val.DESTINATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MAKER_NAME", Type = OracleDbType.Varchar2, Object = val.MAKER_NAME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CAR_MODEL", Type = OracleDbType.Varchar2, Object = val.CAR_MODEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME_REMARKS", Type = OracleDbType.Varchar2, Object = val.NAME_REMARKS, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":VEHICLE_NO", Type = OracleDbType.Varchar2, Object = val.VEHICLE_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TEST_PURPOSE", Type = OracleDbType.Varchar2, Object = val.TEST_PURPOSE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":COMPLETE_DATE", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.COMPLETE_DATE), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_CODE", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":RESEARCH_NO", Type = OracleDbType.Varchar2, Object = val.RESEARCH_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FIXED_ASSET_NO", Type = OracleDbType.Varchar2, Object = val.FIXED_ASSET_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CONSTRUCT_NO", Type = OracleDbType.Varchar2, Object = val.CONSTRUCT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DISPOSAL_PLAN_MONTH", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.DISPOSAL_PLAN_MONTH), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MANAGEMENT_NO", Type = OracleDbType.Varchar2, Object = val.MANAGEMENT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":HISTORY_NO", Type = OracleDbType.Int16, Object = val.HISTORY_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SERIAL_NO", Type = OracleDbType.Varchar2, Object = val.SERIAL_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ISSUE_NO", Type = OracleDbType.Varchar2, Object = val.ISSUE_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":REVISION_NO", Type = OracleDbType.Varchar2, Object = val.REVISION_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":COMPLETE_REQUEST_DATE", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.COMPLETE_REQUEST_DATE), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TARGET", Type = OracleDbType.Varchar2, Object = val.TARGET, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":METHOD", Type = OracleDbType.Varchar2, Object = val.METHOD, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":REMARKS", Type = OracleDbType.Varchar2, Object = val.REMARKS, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ERROR_MESSAGE", Type = OracleDbType.Varchar2, Object = val.ERROR_MESSAGE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INPUT_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.INPUT_PERSONEL_ID, Direct = ParameterDirection.Input },
                };

                if (!db.InsertData(sql.ToString(), prms))
                {
                    return false;
                }

                if (db.ResultCount > 0)
                {
                    res.Add(new ProductionCarCommonModel()
                    {
                        GENERAL_CODE = val.GENERAL_CODE,
                        PROTOTYPE_PERIOD = val.PROTOTYPE_PERIOD,
                        VEHICLE = val.VEHICLE,
                        MANAGEMENT_NO = val.MANAGEMENT_NO,
                        HISTORY_NO = val.HISTORY_NO
                    });
                }
            }

            return true;
        }

        /// <summary>
        /// 製作車の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(List<ProductionCarPutInModel> list)
        {
            var sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    PRODUCTION_CAR");
            sql.AppendLine("SET");
            sql.AppendLine("     CAR_GROUP = :CAR_GROUP");
            sql.AppendLine("    ,CAR_TYPE = :CAR_TYPE");
            sql.AppendLine("    ,GENERAL_CODE = :GENERAL_CODE");
            sql.AppendLine("    ,PROTOTYPE_PERIOD = :PROTOTYPE_PERIOD");
            sql.AppendLine("    ,VEHICLE = :VEHICLE");
            sql.AppendLine("    ,MODEL_CODE = :MODEL_CODE");
            sql.AppendLine("    ,DESTINATION = :DESTINATION");
            sql.AppendLine("    ,MAKER_NAME = :MAKER_NAME");
            sql.AppendLine("    ,CAR_MODEL = :CAR_MODEL");
            sql.AppendLine("    ,NAME_REMARKS = :NAME_REMARKS");
            sql.AppendLine("    ,VEHICLE_NO = :VEHICLE_NO");
            sql.AppendLine("    ,TEST_PURPOSE = :TEST_PURPOSE");
            sql.AppendLine("    ,COMPLETE_DATE = :COMPLETE_DATE");
            sql.AppendLine("    ,SECTION_GROUP_CODE = :SECTION_GROUP_CODE");
            sql.AppendLine("    ,RESEARCH_NO = :RESEARCH_NO");
            sql.AppendLine("    ,FIXED_ASSET_NO = :FIXED_ASSET_NO");
            sql.AppendLine("    ,CONSTRUCT_NO = :CONSTRUCT_NO");
            sql.AppendLine("    ,DISPOSAL_PLAN_MONTH = :DISPOSAL_PLAN_MONTH");
            sql.AppendLine("    ,MANAGEMENT_NO = :MANAGEMENT_NO");
            sql.AppendLine("    ,HISTORY_NO = :HISTORY_NO");
            sql.AppendLine("    ,SERIAL_NO = :SERIAL_NO");
            sql.AppendLine("    ,ISSUE_NO = :ISSUE_NO");
            sql.AppendLine("    ,REVISION_NO = :REVISION_NO");
            sql.AppendLine("    ,COMPLETE_REQUEST_DATE = :COMPLETE_REQUEST_DATE");
            sql.AppendLine("    ,TARGET = :TARGET");
            sql.AppendLine("    ,METHOD = :METHOD");
            sql.AppendLine("    ,REMARKS = :REMARKS");
            sql.AppendLine("    ,ERROR_MESSAGE = :ERROR_MESSAGE");
            sql.AppendLine("    ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("    ,CHANGE_PERSONEL_ID = :CHANGE_PERSONEL_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    ID = :ID");

            foreach (var val in list)
            {
                var prms = new List<BindModel>()
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = val.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CAR_GROUP", Type = OracleDbType.Varchar2, Object = val.CAR_GROUP, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CAR_TYPE", Type = OracleDbType.Varchar2, Object = val.CAR_TYPE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":GENERAL_CODE", Type = OracleDbType.Varchar2, Object = val.GENERAL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":PROTOTYPE_PERIOD", Type = OracleDbType.Varchar2, Object = val.PROTOTYPE_PERIOD, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":VEHICLE", Type = OracleDbType.Varchar2, Object = val.VEHICLE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MODEL_CODE", Type = OracleDbType.Varchar2, Object = val.MODEL_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DESTINATION", Type = OracleDbType.Varchar2, Object = val.DESTINATION, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MAKER_NAME", Type = OracleDbType.Varchar2, Object = val.MAKER_NAME, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CAR_MODEL", Type = OracleDbType.Varchar2, Object = val.CAR_MODEL, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME_REMARKS", Type = OracleDbType.Varchar2, Object = val.NAME_REMARKS, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":VEHICLE_NO", Type = OracleDbType.Varchar2, Object = val.VEHICLE_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TEST_PURPOSE", Type = OracleDbType.Varchar2, Object = val.TEST_PURPOSE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":COMPLETE_DATE", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.COMPLETE_DATE), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SECTION_GROUP_CODE", Type = OracleDbType.Varchar2, Object = val.SECTION_GROUP_CODE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":RESEARCH_NO", Type = OracleDbType.Varchar2, Object = val.RESEARCH_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FIXED_ASSET_NO", Type = OracleDbType.Varchar2, Object = val.FIXED_ASSET_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CONSTRUCT_NO", Type = OracleDbType.Varchar2, Object = val.CONSTRUCT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DISPOSAL_PLAN_MONTH", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.DISPOSAL_PLAN_MONTH), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":MANAGEMENT_NO", Type = OracleDbType.Varchar2, Object = val.MANAGEMENT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":HISTORY_NO", Type = OracleDbType.Int16, Object = val.HISTORY_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SERIAL_NO", Type = OracleDbType.Varchar2, Object = val.SERIAL_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ISSUE_NO", Type = OracleDbType.Varchar2, Object = val.ISSUE_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":REVISION_NO", Type = OracleDbType.Varchar2, Object = val.REVISION_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":COMPLETE_REQUEST_DATE", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.COMPLETE_REQUEST_DATE), Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TARGET", Type = OracleDbType.Varchar2, Object = val.TARGET, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":METHOD", Type = OracleDbType.Varchar2, Object = val.METHOD, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":REMARKS", Type = OracleDbType.Varchar2, Object = val.REMARKS, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ERROR_MESSAGE", Type = OracleDbType.Varchar2, Object = val.ERROR_MESSAGE, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CHANGE_PERSONEL_ID", Type = OracleDbType.Varchar2, Object = val.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input },
                };

                if (!db.UpdateData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 製作車の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(List<ProductionCarDeleteInModel> list)
        {
            foreach(var val in list)
            {
                var sql = new StringBuilder();
                var prms = new List<BindModel>();

                sql.AppendLine("DELETE");
                sql.AppendLine("FROM");
                sql.AppendLine("    PRODUCTION_CAR");
                sql.AppendLine("WHERE");
                sql.AppendLine("    ID = :ID");
                sql.AppendLine("    AND ENTRY_DATETIME IS NULL");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Int64,
                    Object = val.ID,
                    Direct = ParameterDirection.Input
                });

                if (!db.DeleteData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
    }
}