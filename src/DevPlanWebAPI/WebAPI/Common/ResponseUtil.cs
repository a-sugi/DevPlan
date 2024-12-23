using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Web.Http;

using DevPlanWebAPI.Models;
using DevPlanWebAPI.Properties;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// レスポンスの共通クラス
    /// </summary>
    public class ResponseUtil
    {
        #region レスポンスモデル取得
        /// <summary>
        /// レスポンスモデル取得
        /// </summary>
        /// <typeparam name="T">取得結果の型</typeparam>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModel</returns>
        public static ResponseModel<T> GetResponse<T>(MessageType msg, params object[] args)
        {
            var errorCode = "";
            var errorMessage = "";

            //なし以外はエラーコードを設定
            if (msg != MessageType.None)
            {
                var resource = Resources.ResourceManager;

                //エラーコード
                errorCode = msg.ToString();

                //メッセージ取得
                errorMessage = resource.GetString(errorCode);

                //書式設定対象があれば設定
                if (args != null && args.Any() == true)
                {
                    errorMessage = string.Format(errorMessage, args);

                }

            }

            return new ResponseModel<T>
            {
                //ステータス
                Status = Resources.CommonFailure,

                //エラーコード
                ErrorCode = errorCode,

                //エラー内容
                ErrorMessage = errorMessage,

                //取得データ
                Results = Enumerable.Empty<T>(),

            };

        }

        /// <summary>
        /// レスポンスデータ取得
        /// </summary>
        /// <typeparam name="T">取得結果の型</typeparam>
        /// <param name="list">リスト</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        public static ResponseModel<T> GetResponse<T>(IEnumerable<T> list, MessageType msg, params object[] args)
        {
            var isList = list != null && list.Any() == true;

            //メッセージなしかどうか
            if (msg == MessageType.None)
            {
                //データの取得に失敗しているかどうか
                if (list == null)
                {
                    msg = MessageType.KKE03001;

                }
                //データがあるかどうか
                else if (list.Any() == false)
                {
                    msg = MessageType.KKE03002;

                }

            }

            var res = GetResponse<T>(msg, args);

            //データが取得できたかどうか
            if (isList == true)
            {
                //ステータス
                res.Status = Resources.CommonSuccess;

                //取得データ
                res.Results = list;

            }

            return res;

        }

        /// <summary>
        /// レスポンスデータ取得
        /// </summary>
        /// <typeparam name="T">取得結果の型</typeparam>
        /// <param name="flg">登録可否</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        public static ResponseModel<T> GetResponse<T>(bool flg, MessageType msg, params object[] args)
        {
            //登録失敗でメッセージなしはシステムエラーのメッセージを設定
            if (flg == false && msg == MessageType.None)
            {
                msg = MessageType.KKE03001;

            }

            var res = GetResponse<T>(msg, args);

            //データが登録できたかどうか
            if (flg == true)
            {
                //ステータス
                res.Status = Resources.CommonSuccess;

            }

            return res;

        }
        #endregion

        #region
        /// <summary>
        /// レスポンスデータ取得
        /// </summary>
        /// <param name="fileMap">ファイル</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        public static HttpResponseMessage GetResponse(IDictionary<Stream, string> fileMap, MessageType msg = MessageType.None, params object[] args)
        {
            var res = new HttpResponseMessage(HttpStatusCode.OK);

            //ファイルがあるかどうか
            var isFile = fileMap != null && fileMap.Any(x => x.Key != null && x.Key.Length > 0);
            if (isFile == false)
            {
                res.StatusCode = HttpStatusCode.BadRequest;

                var kv = fileMap.First();
                var content = new StreamContent(new MemoryStream());
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                content.Headers.ContentDisposition.FileName = kv.Value;
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                content.Headers.ContentLength = 0;
                res.Content = content;
            }
            else
            {
                Func<Stream, string, HttpContent> getContent = (s, n) =>
                {
                    var content = new StreamContent(s);
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    content.Headers.ContentDisposition.FileName = n;
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content.Headers.ContentLength = s.Length;

                    return content;

                };

                var map = isFile == false ? null : fileMap.Where(x => x.Key != null && x.Key.Length > 0).Select(x =>
                {
                    var key = x.Key;

                    key.Position = 0;

                    return new
                    {
                        Key = key,
                        x.Value,

                    };

                }).ToDictionary(x => x.Key, x => x.Value);

                if (map.Count == 1)
                {
                    var kv = map.First();

                    res.Content = getContent(kv.Key, kv.Value);

                }
                else
                {
                    var multipart = new MultipartFormDataContent();

                    foreach (var kv in map)
                    {
                        multipart.Add(getContent(kv.Key, kv.Value));

                    }

                    res.Content = multipart;

                }

            }

            return res;

        }

        #endregion
    }
}