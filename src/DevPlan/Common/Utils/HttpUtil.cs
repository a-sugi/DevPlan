using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.IO;

using Newtonsoft.Json;

using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using log4net;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// HTTP共通関数
    /// </summary>
    public class HttpUtil
    {
        protected static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region メンバ変数
        private const string Delete = "DEELTE";

        private static readonly string WebApiBaseUrl;

        private static readonly string WebApiBasePath;

        private static readonly int WebApiTimeout;

        private static readonly int WebApiMaxUrlLength = 4096;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static HttpUtil()
        {
            //WebApiの設定を読み込み
            WebApiBaseUrl = ConfigurationManager.AppSettings["WebApiBaseUrl"];
            WebApiBasePath = ConfigurationManager.AppSettings["WebApiBasePath"];

            //WebApiのタイムアウト
            var webApiTimeout = ConfigurationManager.AppSettings["WebApiTimeout"];
            if (string.IsNullOrWhiteSpace(webApiTimeout) == false)
            {
                WebApiTimeout = int.Parse(webApiTimeout);

            }

        }
        #endregion

        #region GET
        /// <summary>
        /// レスポンスの取得(GET)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> GetResponse<Result>(ControllerType cntl, bool isErrorShow = true)
        {
            return GetResponse<Result>(cntl, new Dictionary<string, object>(), isErrorShow);

        }


        /// <summary>
        /// レスポンスの取得(GET)
        /// </summary>
        /// <typeparam name="Condition"検索条件の型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="cond">検索条件</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> GetResponse<Condition, Result>(ControllerType cntl, Condition cond, bool isErrorShow = true) where Condition : class
        {
            var map = new Dictionary<string, object>();

            //検索条件があるかどうか
            if (cond != null)
            {
                //読み込み可能なプロパティをパラメータとして取得
                foreach (var pi in typeof(Condition).GetProperties().Where(x => x.CanRead))
                {
                    //有効な値の場合のみパラメータとして設定
                    var value = pi.GetValue(cond);
                    if (value != null)
                    {
                        map[pi.Name] = value;

                    }

                }

            }

            return GetResponse<Result>(cntl, map, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(GET)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="map">パラメータ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> GetResponse<Result>(ControllerType cntl, Dictionary<string, object> map, bool isErrorShow = true)
        {
            var query = "";

            //パラメータがあるかどうか
            if (map != null && map.Any() == true)
            {
                Func<string, string, string> get = (key, value) => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));

                //パラメータ組み立て
                var param = map.Where(x => string.IsNullOrEmpty(x.Key) == false && x.Value != null && string.IsNullOrEmpty(x.Value.ToString()) == false)
                .Select(x =>
                {
                    var value = "";

                    var type = x.Value.GetType();

                    var list = new List<string>();

                    //配列かどうか
                    if (type.IsArray == true)
                    {
                        foreach (var obj in (Array)x.Value)
                        {
                            list.Add(get(x.Key, obj.ToString()));

                        }

                    }
                    else
                    {
                        //日付型の場合は書式を設定
                        if (type == typeof(DateTime) || type == typeof(DateTime?))
                        {
                            value = ((DateTime)x.Value).ToString("yyyy/MM/dd HH:mm:ss");

                        }
                        //列挙対の場合は数値に変換
                        else if (type.IsEnum == true)
                        {
                            value = ((int)x.Value).ToString();

                        }
                        else
                        {
                            value = x.Value.ToString();

                        }

                        list.Add(get(x.Key, value));
                    }

                    return list;

                }).SelectMany(x => x).ToArray();

                query = string.Join("&", param);
            }

            var url = new UriBuilder(WebApiBaseUrl);

            //コントローラ名設定
            url.Path = string.Format("/{0}/{1}", WebApiBasePath.Replace("/", ""), cntl.ToString());

            //クエリ設定
            url.Query = query;

            return GetResponse<Result>(WebRequestMethods.Http.Get, url.ToString(), isErrorShow: isErrorShow);

        }
        #endregion

        #region POST
        /// <summary>
        /// レスポンスの取得(POST)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="value">データ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PostResponse<Result>(ControllerType cntl, object value, bool isErrorShow = true)
        {
            return GetResponse<Result>(WebRequestMethods.Http.Post, cntl, value, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(POST)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PostResponse<Result>(ControllerType cntl, IEnumerable<Result> list, bool isErrorShow = true)
        {
            return PostResponse<Result, Result>(cntl, list, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(POST)
        /// </summary>
        /// <typeparam name="Model">モデルの型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PostResponse<Model, Result>(ControllerType cntl, IEnumerable<Model> list, bool isErrorShow = true)
        {
            return GetResponse<Model, Result>(WebRequestMethods.Http.Post, cntl, list, isErrorShow);

        }
        #endregion

        #region PUT
        /// <summary>
        /// レスポンスの取得(PUT)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="value">データ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PutResponse<Result>(ControllerType cntl, object value, bool isErrorShow = true)
        {
            return GetResponse<Result>(WebRequestMethods.Http.Put, cntl, value, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(PUT)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PutResponse<Result>(ControllerType cntl, IEnumerable<Result> list, bool isErrorShow = true)
        {
            return PutResponse<Result, Result>(cntl, list, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(PUT)
        /// </summary>
        /// <typeparam name="Model">モデルの型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> PutResponse<Model, Result>(ControllerType cntl, IEnumerable<Model> list, bool isErrorShow = true)
        {
            return GetResponse<Model, Result>(WebRequestMethods.Http.Put, cntl, list, isErrorShow);

        }
        #endregion

        #region DELETE
        /// <summary>
        /// レスポンスの取得(DELETE)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="value">データ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> DeleteResponse<Result>(ControllerType cntl, object value, bool isErrorShow = true)
        {
            return GetResponse<Result>(Delete, cntl, value, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(DELETE)
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> DeleteResponse<Result>(ControllerType cntl, IEnumerable<Result> list, bool isErrorShow = true)
        {
            return DeleteResponse<Result, Result>(cntl, list, isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得(DELETE)
        /// </summary>
        /// <typeparam name="Model">モデルの型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="cntl">コントローラ</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto</returns>
        public static ResponseDto<Result> DeleteResponse<Model, Result>(ControllerType cntl, IEnumerable<Model> list, bool isErrorShow = true)
        {
            return GetResponse<Model, Result>(Delete, cntl, list, isErrorShow);

        }
        #endregion

        #region レスポンスの取得
        /// <summary>
        /// レスポンスの取得
        /// </summary>
        /// <typeparam name="Model">モデルの型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="method">メソッド</param>
        /// <param name="url">URL</param>
        /// <param name="value">データ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns></returns>
        public static ResponseDto<Result> GetResponse<Result>(string method, ControllerType cntl, object value, bool isErrorShow = true)
        {
            var url = new UriBuilder(WebApiBaseUrl);

            //コントローラ名設定
            url.Path = string.Format("/{0}/{1}", WebApiBasePath.Replace("/", ""), cntl.ToString());

            return GetResponse<Result>(method, url.ToString(), new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json"), isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得
        /// </summary>
        /// <typeparam name="Model">モデルの型</typeparam>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="method">メソッド</param>
        /// <param name="url">URL</param>
        /// <param name="list">データのリスト</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns></returns>
        public static ResponseDto<Result> GetResponse<Model, Result>(string method, ControllerType cntl, IEnumerable<Model> list, bool isErrorShow = true)
        {
            var url = new UriBuilder(WebApiBaseUrl);

            //コントローラ名設定
            url.Path = string.Format("/{0}/{1}", WebApiBasePath.Replace("/", ""), cntl.ToString());

            return GetResponse<Result>(method, url.ToString(), new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json"), isErrorShow);

        }

        /// <summary>
        /// レスポンスの取得
        /// </summary>
        /// <typeparam name="Result">データの型</typeparam>
        /// <param name="method">メソッド</param>
        /// <param name="url">URL</param>
        /// <param name="content">コンテンツ</param>
        /// <param name="isErrorShow">エラー表示可否</param>
        /// <returns>ResponseDto<Result></returns>
        public static ResponseDto<Result> GetResponse<Result>(string method, string url, HttpContent content = null, bool isErrorShow = true)
        {
            try
            {
                // ハンドラ
                var handler = new HttpClientHandler()
                {
                    UseProxy = false,               // 初回アクセス遅延防止策 ※Proxyを利用しない場合が前提となる
                    UseDefaultCredentials = true    // Windows認証
                };
                //using (var hc = new HttpClient { Timeout = TimeSpan.FromSeconds(WebApiTimeout) })

                using (var hc = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(WebApiTimeout) })
                {
                    HttpResponseMessage res = null;

                    //メソッドごとに分岐
                    switch (method.ToUpperInvariant())
                    {
                        //GET
                        case WebRequestMethods.Http.Get:
                            res = hc.GetAsync(url).Result;
                            break;

                        //POST
                        case WebRequestMethods.Http.Post:
                            res = hc.PostAsync(url, content).Result;
                            break;

                        //PUT
                        case WebRequestMethods.Http.Put:
                            res = hc.PutAsync(url, content).Result;
                            break;

                        //DELETE
                        case Delete:
                            var req = new HttpRequestMessage
                            {
                                Method = HttpMethod.Delete,
                                RequestUri = new Uri(url),
                                Content = content,

                            };

                            res = hc.SendAsync(req).Result;
                            break;

                    }

                    if (res == null)
                    {
                        throw new Exception("HttpResponseMessageが取得できませんでした。");
                    }

                    if (res.StatusCode >= HttpStatusCode.BadRequest)
                    {
                        // IIS maxUrl 制限
                        if (res.StatusCode == HttpStatusCode.NotFound &&
                            method?.ToUpperInvariant() == WebRequestMethods.Http.Get && url?.Length > WebApiMaxUrlLength)
                        {
                            Messenger.Warn(Resources.TCM03017);
                            return new ResponseDto<Result>
                            {
                                Status = Const.StatusFailure
                            };
                        }
                        else
                        {
                            throw new Exception("HTTPステータスコードが異常値になりました。[コード]" + res.StatusCode.ToString());
                        }
                    }

                    var resContent = res.Content;
                    var contentDisposition = resContent.Headers.ContentDisposition;

                    //ファイルがあるかどうか
                    if (contentDisposition != null && contentDisposition.FileName != null)
                    {
                        return new ResponseDto<Result>
                        {
                            Status = Const.StatusSuccess,
                            FileMap = new Dictionary<Stream, string>
                                {
                                    { resContent.ReadAsStreamAsync().Result, contentDisposition.FileName }
                                }
                        };
                    }

                    //APIの結果を取得
                    var result = JsonConvert.DeserializeObject<ResponseDto<Result>>(resContent.ReadAsStringAsync().Result);

                    if (result == null)
                    {
                        throw new Exception("JsonConvertに失敗しました。 [変換対象データ]" + resContent.ReadAsStringAsync().Result);
                    }

                    if (result.Status == "" || result.Status == null)
                    {
                        throw new Exception("実行結果(ResponseDto.Status)の取得に失敗しました。");
                    }

                    //エラー表示で処理が成功したかどうか
                    if (isErrorShow == true && result.Status != Const.StatusSuccess)
                    {
                        //該当データなしの場合はここでは表示しない
                        if (result.ErrorCode != ApiMessageType.KKE03002.ToString())
                        {
                            Messenger.Warn((result.ErrorMessage == null || result.ErrorMessage == "") ?
                                Resources.KKM00037 : result.ErrorMessage);
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Messenger.Warn(Resources.KKM00037);

                logger.Warn("WebAPI接続でエラーが発生しました。：" + ex.Message);
                logger.Warn("　[WebRequestMethods]" + method);
                logger.Warn("　[接続先]" + url);

                throw;
            }
        }
        #endregion
    }
}
