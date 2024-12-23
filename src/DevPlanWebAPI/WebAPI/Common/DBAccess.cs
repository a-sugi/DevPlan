using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// DB 接続クラス
    /// </summary>
    public sealed class DBAccess
    {
        #region メンバ変数
        private string connectionString;
        private OracleConnection connection;
        private OracleTransaction transaction;
        private Logging logger = new Logging();
        private Logging Log = new Logging();
        #endregion

        #region プロパティ
        /// <summary>
        /// ExecuteNonQuery 影響件数
        /// </summary>
        public int? ResultCount { get; set; } = 0;
        /// <summary>
        /// RETURNING 句バインド変数
        /// </summary>
        public List<BindModel> Returns { get; set; } = new List<BindModel>();
        #endregion

        [ThreadStatic]
        private static DBAccess _singleInstance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static DBAccess GetInstance()
        {
            if (_singleInstance == null)
            {
                _singleInstance = new DBAccess();
            }
            return _singleInstance;
        }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private DBAccess()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        #endregion

        #region DB コネクションの開始
        /// <summary>
        /// DB コネクションの開始
        /// </summary>
        public void Connect()
        {
            connection = new OracleConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

        }
        #endregion

        #region DB コネクションの終了
        /// <summary>
        /// DB コネクションの終了
        /// </summary>
        public void Close()
        {
            connection.Close();
            connection.Dispose();

        }
        #endregion

        #region データの取得
        /// <summary>
        /// データリーダの取得（SELECT）
        /// </summary>
        /// <param name="sql">SELECT 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>OracleDataReader</returns>
        public OracleDataReader Reader(string sql, List<BindModel> prms)
        {
            try
            {
                var cmd = GetOracleCommand(sql, prms);
                sql = CommandTextWithParameters(cmd);

                var reader = cmd.ExecuteReader();

                return reader;

            }
            catch (Exception ex)
            {
                logger.WriteErrorLog(logger.Fatal, "SQLの実行でエラーが発生しました。" + Const.CrLf + sql, ex);

                return null;

            }

        }

        /// <summary>
        /// データセットの取得（SELECT）
        /// </summary>
        /// <param name="sql">SELECT 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>DataSet</returns>
        public DataSet ReadDataSet(string sql, List<BindModel> prms)
        {
            try
            {
                var cmd = GetOracleCommand(sql, prms);
                sql = CommandTextWithParameters(cmd);

                var da = new OracleDataAdapter(cmd);

                var ds = new DataSet();

                da.Fill(ds);

                return ds;

            }
            catch (Exception ex)
            {
                logger.WriteErrorLog(logger.Fatal, "SQLの実行でエラーが発生しました。" + Const.CrLf + sql, ex);

                return null;

            }

        }

        /// <summary>
        /// データテーブルの取得（SELECT）
        /// </summary>
        /// <param name="sql">SELECT 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>DataTable</returns>
        public DataTable ReadDataTable(string sql, List<BindModel> prms)
        {
            try
            {
                var dt = new DataTable();

                // データテーブルに読込
                using (var reader = Reader(sql, prms))
                {
                    dt.Load(reader);

                }

                logger.WriteLog(logger.Trace, "実行結果:Load=" + dt?.ToString());

                return dt;

            }
            catch (Exception ex)
            {
                logger.WriteErrorLog(logger.Fatal, "SQLの実行でエラーが発生しました。" + Const.CrLf + sql, ex);

                return null;

            }

        }

        /// <summary>
        /// モデルのリストの取得（SELECT）
        /// </summary>
        /// <typeparam name="T">モデルの型</typeparam>
        /// <param name="sql">SELECT 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>DataTable</returns>
        public List<T> ReadModelList<T>(string sql, List<BindModel> prms) where T : class, new()
        {
            try
            {
                // データテーブル取得
                var dt = ReadDataTable(sql, prms);
                if (dt == null)
                {
                    return null;

                }

                // モデルの書き込み可能なプロパティだけ取得
                var prop = typeof(T).GetProperties().Where(x => x.CanWrite == true).ToArray();

                // ログ出力データ
                var data = new StringBuilder();

                //モデルのリストに変換
                var list = dt.AsEnumerable().Select(row =>
                {
                    // ログ出力行
                    var line = new StringBuilder();

                    //行のデータを取得
                    var map = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //取得済の列名の場合は次へ
                        if (map.ContainsKey(dc.ColumnName) == true)
                        {
                            continue;

                        }

                        var value = row[dc];

                        // NULLの場合はNULLに変換
                        if (value == null || value == DBNull.Value)
                        {
                            value = null;

                        }

                        map[dc.ColumnName] = value;
                        line.Append(line.Length == 0 ? value : "," + value);
                    }

                    data.AppendLine(line.ToString());

                    //モデルのプロパティにセット
                    var model = new T();
                    this.SetModelPropertie(model, prop, map);

                    return model;

                }).ToList();

                logger.WriteLog(logger.Trace, "実行結果:Model=" + prop[0]?.ReflectedType?.FullName?.ToString() + "," + Const.CrLf + "List=" + data.ToString());

                return list;
            }
            catch (Exception ex)
            {
                logger.WriteErrorLog(logger.Fatal, "SQLの実行でエラーが発生しました。" + Const.CrLf + sql, ex);

                return null;

            }

        }
        #endregion

        #region データの変更
        /// <summary>
        /// データの削除（DELETE）
        /// </summary>
        /// <param name="sql">DELETE 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>bool</returns>
        public bool DeleteData(string sql, List<BindModel> prms)
        {
            try
            {
                return ExecuteNonQuery(sql, prms);

            }
            catch
            {
                return false;

            }

        }

        /// <summary>
        /// データの挿入（INSERT）
        /// </summary>
        /// <param name="sql">INSERT 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>bool</returns>
        public bool InsertData(string sql, List<BindModel> prms)
        {
            try
            {
                return ExecuteNonQuery(sql, prms);

            }
            catch
            {
                return false;

            }

        }

        /// <summary>
        /// データの更新（UPDATE）
        /// </summary>
        /// <param name="sql">UPDATE 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>bool</returns>
        public bool UpdateData(string sql, List<BindModel> prms)
        {
            try
            {
                return ExecuteNonQuery(sql, prms);

            }
            catch
            {
                return false;

            }

        }

        /// <summary>
        /// SQLの実行
        /// </summary>
        /// <param name="sql">UPDATE 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns></returns>
        private bool ExecuteNonQuery(string sql, List<BindModel> prms)
        {
            try
            {
                var cmd = GetOracleCommand(sql, prms);

                sql = CommandTextWithParameters(cmd);

                // パラメータがある場合は設定
                if (Returns != null && Returns.Any() == true)
                {
                    Returns.ForEach(r =>
                    {
                        if (r.Type == OracleDbType.Varchar2)
                        {
                            cmd.Parameters.Add(new OracleParameter(r.Name, r.Type, r.Size, r.Object, r.Direct));

                            return;
                        }

                        cmd.Parameters.Add(new OracleParameter(r.Name, r.Type, r.Direct));
                    });

                }

                ResultCount = cmd.ExecuteNonQuery();

                logger.WriteLog(logger.Trace, "実行結果:ExecuteNonQuery=" + ResultCount.ToString());

                // パラメータがある場合は設定
                if (Returns != null && Returns.Any() == true)
                {
                    Returns.ForEach(r => r.Object = cmd.Parameters[r.Name].Value);

                }

                return true;

            }
            catch (Exception ex)
            {
                logger.WriteErrorLog(logger.Fatal, "SQLの実行でエラーが発生しました。" + Const.CrLf + sql, ex);
                throw;

            }

        }
        #endregion

        #region コマンドの取得
        /// <summary>
        /// コマンドの取得
        /// </summary>
        /// <param name="sql">SQL 文</param>
        /// <param name="prms">バインドパラメータ</param>
        /// <returns>OracleCommand</returns>
        public OracleCommand GetOracleCommand(string sql, List<BindModel> prms)
        {
            var cmd = new OracleCommand(sql, connection)
            {
                // パラメータは名前でバインド
                BindByName = true

            };

            // パラメータがある場合は設定
            if (prms != null && prms.Any() == true)
            {
                prms.ForEach(p =>
                {
                    var value = p.Object;

                    //NULLかどうか
                    if (value == null || value == DBNull.Value)
                    {
                        value = null;

                    }
                    //列挙対かどうか
                    else if (value.GetType().IsEnum == true)
                    {
                        value = (int)value;

                    }

                    cmd.Parameters.Add(new OracleParameter(p.Name, p.Type, value, p.Direct));

                });

            }

            logger.WriteLog(logger.Debug, "実行SQL:" + Const.CrLf + CommandTextWithParameters(cmd));

            return cmd;

        }
        #endregion

        #region コマンドテキストをログ用に整形
        /// <summary>
        /// コマンドテキストをログ用に整形
        /// </summary>
        /// <param name="cmd">コマンド</param>
        /// <returns></returns>
        public string CommandTextWithParameters(OracleCommand cmd)
        {
            var sql = cmd.CommandText;

            // パラメータがある場合はSQL分に組み込み
            if (cmd.Parameters != null && cmd.Parameters.Count > 0)
            {
                foreach (OracleParameter p in cmd.Parameters)
                {
                    var value = p.Value;

                    var parameter = "NULL";

                    //値がNULLかどうか
                    if (value != null && value != DBNull.Value)
                    {
                        //型ごとの分岐
                        switch (p.OracleDbType)
                        {
                            //文字列
                            case OracleDbType.Char:
                            case OracleDbType.NChar:
                            case OracleDbType.NVarchar2:
                            case OracleDbType.Varchar2:
                            case OracleDbType.Long:
                            case OracleDbType.Clob:
                            case OracleDbType.NClob:
                                parameter = string.Format("'{0}'", value.ToString());
                                break;

                            //数値
                            case OracleDbType.Byte:
                            case OracleDbType.Decimal:
                            case OracleDbType.Double:
                            case OracleDbType.Int16:
                            case OracleDbType.Int32:
                            case OracleDbType.Int64:
                            case OracleDbType.Single:
                            case OracleDbType.BinaryDouble:
                            case OracleDbType.BinaryFloat:
                                parameter = value.ToString();
                                break;

                            //日時
                            case OracleDbType.Date:
                            case OracleDbType.TimeStamp:
                            case OracleDbType.TimeStampLTZ:
                            case OracleDbType.TimeStampTZ:
                            case OracleDbType.IntervalDS:
                            case OracleDbType.IntervalYM:
                                parameter = string.Format("TO_TIMESTAMP('{0:yyyy/MM/dd HH:mm:ss.fffff}','YYYY/MM/DD HH24:MI:SS.FF5')", value.ToString());
                                break;

                            //その他
                            default:
                                parameter = string.Format("'{0}'", value.ToString());
                                break;

                        }

                    }

                    sql = Regex.Replace(sql, p.ParameterName + @"\b", parameter);

                }

            }

            return sql;

        }
        #endregion

        #region トランザクション
        /// <summary>
        /// DB トランザクションの開始
        /// </summary>
        public void Begin()
        {
            Begin(IsolationLevel.ReadCommitted);

        }

        /// <summary>
        /// DB トランザクションの開始
        /// </summary>
        public void Begin(IsolationLevel level)
        {
            transaction = connection.BeginTransaction(level);

        }

        /// <summary>
        /// DB トランザクションのコミット
        /// </summary>
        public void Commit()
        {
            transaction.Commit();
        }

        /// <summary>
        /// DB トランザクションのロールバック
        /// </summary>
        public void Rollback()
        {
            transaction.Rollback();
        }
        #endregion

        #region モデルにセット
        /// <summary>
        /// モデルにセット
        /// </summary>
        /// <param name="model">モデル</param>
        /// <param name="prop">プロパティ</param>
        /// <param name="map">値</param>
        private void SetModelPropertie(object model, IEnumerable<PropertyInfo> prop, IDictionary<string, object> map)
        {
            foreach (var p in prop)
            {
                var type = p.PropertyType;

                //NULL許容型かどうか
                if (type.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    //元の型を取得
                    type = Nullable.GetUnderlyingType(type);

                }

                //配列の場合は次へ
                if (type.IsArray == true)
                {
                    continue;

                }

                //プロパティがクラスかどうか
                if (type.IsClass == false || type == typeof(string) || type == typeof(DateTime))
                {
                    //存在する列名のプロパティならセット
                    if (map.ContainsKey(p.Name) == true)
                    {
                        var value = map[p.Name];

                        //NULLかどうか
                        if (value == null || value == DBNull.Value)
                        {
                            value = null;

                        }
                        //文字列かどうか
                        else if (type == typeof(string))
                        {
                            value = value.ToString();

                        }
                        //型が違う場合は変換
                        else if (type != value.GetType())
                        {
                            value = Convert.ChangeType(value, type);

                        }

                        //プロパティにセット
                        p.SetValue(model, value);

                    }

                }
                else
                {
                    //NULLの場合はインスタンス化
                    var value = p.GetValue(model);
                    if (value == null)
                    {
                        value = Activator.CreateInstance(type);

                    }

                    //再帰でモデルに設定
                    this.SetModelPropertie(value, type.GetProperties().Where(x => x.CanWrite == true).ToArray(), map);

                    //プロパティにセット
                    p.SetValue(model, value);

                }

            }

        }
        #endregion

    }
}