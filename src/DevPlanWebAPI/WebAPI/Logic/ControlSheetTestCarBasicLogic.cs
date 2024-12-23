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
    /// 試験車基本情報(管理票)ロジッククラス
    /// </summary>
    /// <remarks>試験車基本情報(管理票)の操作</remarks>
    public class ControlSheetTestCarBasicLogic : TestCarBaseLogic
    {
        private Func<DateTime?, DateTime?> getDateTime = dt => dt == null ? null : (DateTime?)dt.Value.Date;

        /// <summary>
        /// 試験車基本情報(管理票)の取得
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<ControlSheetTestCarBasicGetOutModel> GetData(ControlSheetTestCarBasicGetInModel val)
        {
            List<BindModel> prms = new List<BindModel>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     データID");
            sql.AppendLine("    ,管理票NO");
            sql.AppendLine("    ,管理ラベル発行有無");
            sql.AppendLine("    ,車系");
            sql.AppendLine("    ,車型");
            sql.AppendLine("    ,型式符号");
            sql.AppendLine("    ,駐車場番号");
            sql.AppendLine("    ,リースNO");
            sql.AppendLine("    ,リース満了日");
            sql.AppendLine("    ,研実管理廃却申請受理日");
            sql.AppendLine("    ,廃却見積日");
            sql.AppendLine("    ,廃却決済承認年月");
            sql.AppendLine("    ,車両搬出日");
            sql.AppendLine("    ,廃却見積額");
            sql.AppendLine("    ,貸与先");
            sql.AppendLine("    ,貸与返却予定期限");
            sql.AppendLine("    ,貸与返却日");
            sql.AppendLine("    ,メモ");
            sql.AppendLine("    ,正式取得日");
            sql.AppendLine("    ,棚卸実施日");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車基本情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    0 = 0");

            // データID
            if (val?.データID > 0)
            {
                sql.AppendLine("    AND データID = :データID");

                prms.Add(new BindModel
                {
                    Name = ":データID",
                    Type = OracleDbType.Int32,
                    Object = val.データID,
                    Direct = ParameterDirection.Input
                });
            }

            // 管理票NO
            if (!string.IsNullOrWhiteSpace(val?.管理票NO))
            {
                sql.AppendLine("    AND 管理票NO = :管理票NO");

                prms.Add(new BindModel
                {
                    Name = ":管理票NO",
                    Type = OracleDbType.Varchar2,
                    Object = val.管理票NO,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     データID ASC");
            sql.AppendLine("    ,管理票NO ASC");

            return db.ReadModelList<ControlSheetTestCarBasicGetOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// 試験車基本情報(管理票)の更新
        /// </summary>
        /// <returns>bool</returns>
        public bool PutData(ControlSheetTestCarBasicPutInModel val)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE");
            sql.AppendLine("    試験車基本情報");
            sql.AppendLine("SET");
            sql.AppendLine("     管理ラベル発行有無 = :管理ラベル発行有無");
            sql.AppendLine("    ,車系 = :車系");
            sql.AppendLine("    ,車型 = :車型");
            sql.AppendLine("    ,型式符号 = :型式符号");
            sql.AppendLine("    ,駐車場番号 = :駐車場番号");
            sql.AppendLine("    ,リースNO = :リースNO");
            sql.AppendLine("    ,リース満了日 = :リース満了日");
            sql.AppendLine("    ,研実管理廃却申請受理日 = :研実管理廃却申請受理日");
            sql.AppendLine("    ,廃却見積日 = :廃却見積日");
            sql.AppendLine("    ,廃却決済承認年月 = :廃却決済承認年月");
            sql.AppendLine("    ,車両搬出日 = :車両搬出日");
            sql.AppendLine("    ,貸与先 = :貸与先");
            sql.AppendLine("    ,貸与返却予定期限 = :貸与返却予定期限");
            sql.AppendLine("    ,貸与返却日 = :貸与返却日");
            sql.AppendLine("    ,メモ = :メモ");
            sql.AppendLine("    ,正式取得日 = :正式取得日");
            sql.AppendLine("    ,棚卸実施日 = :棚卸実施日");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");
            sql.AppendLine("    AND 管理票NO = :管理票NO");

            List<BindModel> prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = val.データID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理票NO", Type = OracleDbType.Varchar2, Object = val.管理票NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理ラベル発行有無", Type = OracleDbType.Int32, Object = val.管理ラベル発行有無, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車系", Type = OracleDbType.Varchar2, Object = val.車系, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = val.車型, Direct = ParameterDirection.Input },
                new BindModel { Name = ":型式符号", Type = OracleDbType.Varchar2, Object = val.型式符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":駐車場番号", Type = OracleDbType.Varchar2, Object = val.駐車場番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リースNO", Type = OracleDbType.Varchar2, Object = val.リースNO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":リース満了日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.リース満了日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":研実管理廃却申請受理日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.研実管理廃却申請受理日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却見積日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却見積日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":廃却決済承認年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.廃却決済承認年月), Direct = ParameterDirection.Input },
                new BindModel { Name = ":車両搬出日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.車両搬出日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":貸与先", Type = OracleDbType.Varchar2, Object = val.貸与先, Direct = ParameterDirection.Input },
                new BindModel { Name = ":貸与返却予定期限", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.貸与返却予定期限), Direct = ParameterDirection.Input },
                new BindModel { Name = ":貸与返却日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.貸与返却日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":メモ", Type = OracleDbType.Varchar2, Object = val.メモ, Direct = ParameterDirection.Input },
                new BindModel { Name = ":正式取得日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.正式取得日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":棚卸実施日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.棚卸実施日), Direct = ParameterDirection.Input }
            };

            if (!db.UpdateData(sql.ToString(), prms))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 試験車基本情報(管理票)の削除
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteData(ControlSheetTestCarBasicDeleteInModel val)
        {
            StringBuilder sql = new StringBuilder();
            List<BindModel> prms = new List<BindModel>();

            sql.AppendLine("DELETE");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車基本情報");
            sql.AppendLine("WHERE");
            sql.AppendLine("    データID = :データID");

            prms.Add(new BindModel
            {
                Name = ":データID",
                Type = OracleDbType.Int32,
                Object = val.データID,
                Direct = ParameterDirection.Input
            });

            if (!db.DeleteData(sql.ToString(), prms))
            {
                return false;
            }

            return true;
        }
    }
}