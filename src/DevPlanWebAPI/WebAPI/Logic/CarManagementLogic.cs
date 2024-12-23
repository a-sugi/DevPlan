using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 車両管理業務ロジッククラス
    /// </summary>
    public class CarManagementLogic : TestCarBaseLogic
    {
        #region メンバ変数
        /// <summary>製作一覧リスト</summary>
        List<ProductionCarCommonModel> ProductionCarList = new List<ProductionCarCommonModel>();
        #endregion

        #region 管理車両登録
        /// <summary>
        /// 管理車両登録
        /// </summary>
        /// <param name="list"></param>
        /// <returns>更新可否</returns>
        public bool Post(List<ProductionCarCommonModel> list)
        {
            if (list == null || list.Count <= 0)
            {
                return true;
            }

            // 対象データの設定
            this.ProductionCarList = list;

            // トランザクション開始
            db.Begin();

            var results = new List<bool>();

            // 試験車情報テーブルに反映
            results.Add(this.EntryTestCarInfo());

            // 制作一覧テーブルの更新
            results.Add(this.UpdateEntryProductionCar());

            // 失敗
            if (results.All(x => x == true) != true)
            {
                // ロールバック
                db.Rollback();

                return false;
            }

            // コミット
            db.Commit();

            return true;
        }
        #endregion

        #region 試験車情報テーブルの反映
        /// <summary>
        /// 試験車情報テーブルの反映
        /// </summary>
        /// <returns>bool</returns>
        private bool EntryTestCarInfo()
        {
            var results = new List<bool>();

            foreach (var val in this.ProductionCarList)
            {
                // エラーメッセージの初期化
                val.ERROR_MESSAGE = null;
                
                // 担当コードチェック
                if (!string.IsNullOrWhiteSpace(val.SECTION_GROUP_CODE))
                {
                    // 担当コードによる担当情報の取得
                    var sectionGroupDatas = this.GetCheckSectionGroupData(val.SECTION_GROUP_CODE);

                    // 担当コードがマスタに存在しない場合
                    if (sectionGroupDatas == null || sectionGroupDatas.Count <= 0)
                    {
                        val.ERROR_MESSAGE = string.Format("試験車情報反映エラー：使用部署({0})がマスタに登録されていません。使用部署を修正するか、管理者にお問い合わせください。", val.SECTION_GROUP_CODE);
                        continue;
                    }
                }

                // 管理票番号による試験車情報の取得
                var cars = val.MANAGEMENT_NO != null ? this.GetCheckTestCarData(val.MANAGEMENT_NO) : null;

                var returns = new TestCarCommonModel();
                
                // 試験車情報がない場合
                if (cars == null || cars.Count() <= 0)
                {
                    // 管理票番号の発行（新車の登録）

                    // 車体番号による試験車情報の取得
                    var list = this.GetCheckTestCarData(null, val.VEHICLE_NO);

                    // 車体番号がすでに登録されている場合
                    if (list != null && list.Count() > 0)
                    {
                        val.ERROR_MESSAGE = string.Format("管理票番号発行エラー：車体番号が他の車両({0})で利用されています。車体番号を修正するか、管理票NOを入力してください。", list[0].管理票NO);
                        continue;
                    }
                    
                    // 試験車基本情報、試験車履歴情報、固定資産情報に登録
                    results.Add(this.PostTestCarData(new TestCarCommonBaseModel()
                    {
                        管理票NO = val.MANAGEMENT_NO,
                        車系 = val.CAR_GROUP,
                        車型 = val.CAR_TYPE,
                        開発符号 = val.GENERAL_CODE,
                        試作時期 = val.PROTOTYPE_PERIOD,
                        号車 = val.VEHICLE,
                        型式符号 = val.MODEL_CODE,
                        仕向地 = val.DESTINATION,
                        メーカー名 = val.MAKER_NAME,
                        名称備考 = val.NAME_REMARKS,
                        車体番号 = val.VEHICLE_NO,
                        試験目的 = val.TEST_PURPOSE,
                        完成日 = val.COMPLETE_DATE,
                        管理責任部署 = val.SECTION_GROUP_CODE,
                        研命ナンバー = val.RESEARCH_NO,
                        固定資産NO = val.FIXED_ASSET_NO,
                        工事区分NO = val.CONSTRUCT_NO,
                        処分予定年月 = val.DISPOSAL_PLAN_MONTH,
                    }, returns));

                    // 戻り値のセット
                    val.MANAGEMENT_NO = returns.管理票NO;
                    val.HISTORY_NO = returns.履歴NO;
                }
                // 試験車情報がある場合
                else
                {
                    // 既存車の改修

                    // 車体番号による試験車情報の取得
                    var list = this.GetCheckTestCarData(null, val.VEHICLE_NO).Where(x => x.管理票NO != val.MANAGEMENT_NO).ToList();

                    // 車体番号が他の車両ですでに利用されている場合
                    if (list != null && list.Count() > 0)
                    {
                        val.ERROR_MESSAGE = string.Format("既存車改修エラー：車体番号が他の車両({0})で利用されています。車体番号か管理票NOを修正してください。", list[0].管理票NO);
                        continue;
                    }

                    // 試験車履歴情報に登録
                    results.Add(this.PutTestCarHistoryData(new ControlSheetTestCarHistoryPostInModel()
                    {
                        データID = cars[0].データID,
                        開発符号 = val.GENERAL_CODE,
                        試作時期 = val.PROTOTYPE_PERIOD,
                        号車 = val.VEHICLE,
                        仕向地 = val.DESTINATION,
                        メーカー名 = val.MAKER_NAME,
                        名称備考 = val.NAME_REMARKS,
                        車体番号 = val.VEHICLE_NO,
                        試験目的 = val.TEST_PURPOSE,
                        完成日 = val.COMPLETE_DATE,
                        管理責任部署 = val.SECTION_GROUP_CODE,
                        研命ナンバー = val.RESEARCH_NO,
                        固定資産NO = val.FIXED_ASSET_NO,
                        工事区分NO = val.CONSTRUCT_NO,
                        管理票発行有無 = "未",
                    }, returns));

                    // 戻り値のセット
                    val.HISTORY_NO = returns.履歴NO;
                }
            }

            return results.All(x => x == true);
        }
        #endregion

        #region 試験車情報の登録
        /// <summary>
        /// 試験車情報の登録
        /// </summary>
        /// <params name="val"></params>
        /// <params name="returns"></params>
        /// <returns>bool</returns>
        private bool PostTestCarData(TestCarCommonBaseModel val, TestCarCommonModel returns = null)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験車基本情報 (");
            sql.AppendLine("    データID");
            sql.AppendLine("   ,管理票NO");
            sql.AppendLine("   ,車系");
            sql.AppendLine("   ,車型");
            sql.AppendLine("   ,型式符号");
            sql.AppendLine(") VALUES (");
            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            //sql.AppendLine("     (SELECT NVL(MAX(データID), 0) + 1 FROM 試験車基本情報)");
            sql.AppendLine("     (SELECT MAX(A.ID) FROM (SELECT NVL(MAX(データID), 0) + 1 ID FROM 試験車基本情報 UNION SELECT NVL(MAX(データID), 0) + 1 ID FROM DUMMY_TESTCAR) A)");
            //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            sql.AppendLine("   ,(SELECT CONCAT(TO_CHAR(SYSDATE, 'RR'),LPAD(NVL(MAX(SUBSTR(管理票NO, 3, 4)), '0') + 1, 4, '0')) FROM 試験車基本情報 WHERE 管理票NO LIKE CONCAT(TO_CHAR(SYSDATE, 'RR'), '%'))");
            sql.AppendLine("   ,:車系");
            sql.AppendLine("   ,:車型");
            sql.AppendLine("   ,:型式符号");
            sql.AppendLine(") RETURNING");
            sql.AppendLine("    データID, 管理票NO INTO :newid, :newno");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":車系", Type = OracleDbType.Varchar2, Object = val.車系, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車型", Type = OracleDbType.Varchar2, Object = val.車型, Direct = ParameterDirection.Input },
                new BindModel { Name = ":型式符号", Type = OracleDbType.Varchar2, Object = val.型式符号, Direct = ParameterDirection.Input },
            };

            // 戻り値設定
            db.Returns = new List<BindModel>()
            {
                new BindModel { Name = ":newid", Type = OracleDbType.Int32, Direct = ParameterDirection.Output },
                new BindModel { Name = ":newno", Type = OracleDbType.Varchar2, Size = 10, Object = "0000000000", Direct = ParameterDirection.InputOutput },
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            // 戻り値の格納
            returns.データID = Convert.ToInt32(db.Returns.Where(r => r.Name == ":newid").FirstOrDefault().Object.ToString());
            returns.管理票NO = db.Returns.Where(r => r.Name == ":newno").FirstOrDefault().Object.ToString();

            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("試験車履歴情報 (");
            sql.AppendLine("    データID");
            sql.AppendLine("   ,履歴NO");
            sql.AppendLine("   ,管理票発行有無");
            sql.AppendLine("   ,開発符号");
            sql.AppendLine("   ,試作時期");
            sql.AppendLine("   ,号車");
            sql.AppendLine("   ,仕向地");
            sql.AppendLine("   ,メーカー名");
            sql.AppendLine("   ,名称備考");
            sql.AppendLine("   ,車体番号");
            sql.AppendLine("   ,試験目的");
            sql.AppendLine("   ,完成日");
            sql.AppendLine("   ,管理責任部署");
            sql.AppendLine("   ,研命ナンバー");
            sql.AppendLine("   ,固定資産NO");
            sql.AppendLine("   ,工事区分NO");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("    :データID");
            sql.AppendLine("   ,1");
            sql.AppendLine("   ,:管理票発行有無");
            sql.AppendLine("   ,:開発符号");
            sql.AppendLine("   ,:試作時期");
            sql.AppendLine("   ,:号車");
            sql.AppendLine("   ,:仕向地");
            sql.AppendLine("   ,:メーカー名");
            sql.AppendLine("   ,:名称備考");
            sql.AppendLine("   ,:車体番号");
            sql.AppendLine("   ,:試験目的");
            sql.AppendLine("   ,:完成日");
            sql.AppendLine("   ,:管理責任部署");
            sql.AppendLine("   ,:研命ナンバー");
            sql.AppendLine("   ,:固定資産NO");
            sql.AppendLine("   ,:工事区分NO");
            sql.AppendLine(")");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = returns.データID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理票発行有無", Type = OracleDbType.Varchar2, Object = "未", Direct = ParameterDirection.Input },
                new BindModel { Name = ":開発符号", Type = OracleDbType.Varchar2, Object = val.開発符号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試作時期", Type = OracleDbType.Varchar2, Object = val.試作時期, Direct = ParameterDirection.Input },
                new BindModel { Name = ":号車", Type = OracleDbType.Varchar2, Object = val.号車, Direct = ParameterDirection.Input },
                new BindModel { Name = ":仕向地", Type = OracleDbType.Varchar2, Object = val.仕向地, Direct = ParameterDirection.Input },
                new BindModel { Name = ":メーカー名", Type = OracleDbType.Varchar2, Object = val.メーカー名, Direct = ParameterDirection.Input },
                new BindModel { Name = ":名称備考", Type = OracleDbType.Varchar2, Object = val.名称備考, Direct = ParameterDirection.Input },
                new BindModel { Name = ":車体番号", Type = OracleDbType.Varchar2, Object = val.車体番号, Direct = ParameterDirection.Input },
                new BindModel { Name = ":試験目的", Type = OracleDbType.Varchar2, Object = val.試験目的, Direct = ParameterDirection.Input },
                new BindModel { Name = ":完成日", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.完成日), Direct = ParameterDirection.Input },
                new BindModel { Name = ":管理責任部署", Type = OracleDbType.Varchar2, Object = val.管理責任部署, Direct = ParameterDirection.Input },
                new BindModel { Name = ":研命ナンバー", Type = OracleDbType.Varchar2, Object = val.研命ナンバー, Direct = ParameterDirection.Input },
                new BindModel { Name = ":固定資産NO", Type = OracleDbType.Varchar2, Object = val.固定資産NO, Direct = ParameterDirection.Input },
                new BindModel { Name = ":工事区分NO", Type = OracleDbType.Varchar2, Object = val.工事区分NO, Direct = ParameterDirection.Input },
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            // 戻り値の格納
            returns.履歴NO = 1;

            sql = new StringBuilder();

            sql.AppendLine("INSERT INTO");
            sql.AppendLine("固定資産情報 (");
            sql.AppendLine("     データID");
            sql.AppendLine("    ,処分予定年月");
            sql.AppendLine(") VALUES (");
            sql.AppendLine("     :データID");
            sql.AppendLine("    ,:処分予定年月");
            sql.AppendLine(")");

            prms = new List<BindModel>()
            {
                new BindModel { Name = ":データID", Type = OracleDbType.Int32, Object = returns.データID, Direct = ParameterDirection.Input },
                new BindModel { Name = ":処分予定年月", Type = OracleDbType.Date, Object = DateTimeUtil.GetDate(val.処分予定年月), Direct = ParameterDirection.Input }
            };

            if (!db.InsertData(sql.ToString(), prms))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 試験車履歴情報の登録
        /// <summary>
        /// 試験車履歴情報の登録
        /// </summary>
        /// <param name="val"></param>
        /// <param name="returns"></param>
        /// <returns>bool</returns>
        private bool PutTestCarHistoryData(ControlSheetTestCarHistoryPostInModel val, TestCarCommonModel returns = null)
        {
            // 試験車履歴情報(管理票)ロジック
            var logic = new ControlSheetTestCarHistoryLogic();
            logic.SetDBAccess(this.db);

            return logic.PostData(val, returns);
        }
        #endregion

        #region チェックデータの取得
        /// <summary>
        /// 試験車情報の取得
        /// </summary>
        /// <param name="managementNo"></param>
        /// <param name="vehicleNo"></param>
        /// <returns></returns>
        private List<TestCarCommonModel> GetCheckTestCarData(string managementNo = null, string vehicleNo = null)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    A.管理票NO");
            sql.AppendLine("   ,B.データID");
            sql.AppendLine("   ,B.履歴NO");
            sql.AppendLine("   ,B.車体番号");
            sql.AppendLine("FROM");
            sql.AppendLine("    試験車基本情報 A");
            sql.AppendLine("    LEFT JOIN (SELECT データID, 車体番号, MAX(履歴NO) AS 履歴NO FROM 試験車履歴情報 GROUP BY データID, 車体番号) B");
            sql.AppendLine("    ON A.データID = B.データID");
            sql.AppendLine("WHERE 0 = 0");

            if (managementNo != null)
            {
                sql.AppendLine("    AND A.管理票NO = :管理票NO");
            }

            if (vehicleNo != null)
            {
                sql.AppendLine("    AND B.車体番号 = :車体番号");
            }

            var prms = new List<BindModel>
            {
                new BindModel{Name = ":車体番号",Type = OracleDbType.Varchar2,Object = vehicleNo, Direct = ParameterDirection.Input},
                new BindModel{Name = ":管理票NO",Type = OracleDbType.Varchar2,Object = managementNo, Direct = ParameterDirection.Input}
            };

            // 取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// チェック開発符号情報の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<GeneralCodeSearchOutModel> GetCheckGeneralCodeData(string code)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT * FROM GENERAL_CODE WHERE GENERAL_CODE = :GENERAL_CODE");

            var prms = new List<BindModel>
            {
                new BindModel{Name = ":GENERAL_CODE",Type = OracleDbType.Varchar2,Object = code, Direct = ParameterDirection.Input}
            };

            // 取得
            return db.ReadModelList<GeneralCodeSearchOutModel>(sql.ToString(), prms);
        }

        /// <summary>
        /// チェック担当情報の取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<SectionGroupSearchOutModel> GetCheckSectionGroupData(string code)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT * FROM SECTION_GROUP_DATA WHERE SECTION_GROUP_CODE = :SECTION_GROUP_CODE AND FLAG_EXIST = 1");

            var prms = new List<BindModel>
            {
                new BindModel{Name = ":SECTION_GROUP_CODE",Type = OracleDbType.Varchar2,Object = code, Direct = ParameterDirection.Input}
            };

            // 取得
            return db.ReadModelList<SectionGroupSearchOutModel>(sql.ToString(), prms);
        }
        #endregion

        #region 反映製作一覧テーブルの更新
        /// <summary>
        /// 反映制作一覧テーブルの更新
        /// </summary>
        /// <returns>処理結果</returns>
        private bool UpdateEntryProductionCar()
        {
            var sql = new StringBuilder();

            // SQL
            sql.AppendLine("UPDATE");
            sql.AppendLine("    PRODUCTION_CAR");
            sql.AppendLine("SET");
            sql.AppendLine("    MANAGEMENT_NO = :MANAGEMENT_NO");
            sql.AppendLine("   ,HISTORY_NO = :HISTORY_NO");
            sql.AppendLine("   ,ERROR_MESSAGE = :ERROR_MESSAGE");
            sql.AppendLine("   ,ENTRY_DATETIME = CASE WHEN :ERROR_MESSAGE IS NULL THEN SYSDATE ELSE NULL END");
            sql.AppendLine("   ,ENTRY_PERSONEL_ID = CASE WHEN :ERROR_MESSAGE IS NULL THEN :PERSONEL_ID ELSE NULL END");
            sql.AppendLine("   ,CHANGE_DATETIME = SYSDATE");
            sql.AppendLine("   ,CHANGE_PERSONEL_ID = :PERSONEL_ID");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND ID = :ID");

            foreach (var item in this.ProductionCarList)
            {
                // パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel{ Name = ":ID", Type = OracleDbType.Int64, Object = item.ID, Direct = ParameterDirection.Input },
                    new BindModel{ Name = ":MANAGEMENT_NO", Type = OracleDbType.Varchar2, Object = item.MANAGEMENT_NO, Direct = ParameterDirection.Input },
                    new BindModel{ Name = ":HISTORY_NO", Type = OracleDbType.Int16, Object = item.HISTORY_NO, Direct = ParameterDirection.Input },
                    new BindModel{ Name = ":ERROR_MESSAGE", Type = OracleDbType.Varchar2, Object = item.ERROR_MESSAGE, Direct = ParameterDirection.Input },
                    new BindModel{ Name = ":PERSONEL_ID", Type = OracleDbType.Varchar2, Object = item.CHANGE_PERSONEL_ID, Direct = ParameterDirection.Input }
                };

                if (!db.UpdateData(sql.ToString(), prms))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}