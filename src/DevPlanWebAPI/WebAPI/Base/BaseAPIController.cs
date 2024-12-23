using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web.Http;

using DevPlanWebAPI.Common;
using DevPlanWebAPI.Filters;
using DevPlanWebAPI.Models;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace DevPlanWebAPI.Base
{
    /// <summary>
    /// APIコントローラ基底クラス
    /// </summary>
    /// <typeparam name="Logic">業務ロジック</typeparam>
    /// <typeparam name="Result">返却する型</typeparam>
    [CommonClassFilter()]
    public class BaseAPIController<Logic, Result> : ApiController
        where Logic : BaseLogic, new()
    {
        #region メンバ変数
        private Logic logic = null;
        #endregion

        #region　プロパティ
        /// <summary>
        /// DBアクセサ
        /// </summary>
        public DBAccess db
        {
            get { return DBAccess.GetInstance(); }

        }
        #endregion

        #region 非同期
        /// <summary>
        /// http非同期実行
        /// </summary>
        /// <returns>実行結果</returns>
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            using (new DBConnection(db))
            {
                return base.ExecuteAsync(controllerContext, cancellationToken);
            }
        }
        #endregion

        #region ロジッククラス取得
        /// <summary>
        /// ロジッククラス取得
        /// </summary>
        /// <returns>ロジッククラス</returns>
        protected Logic GetLogic()
        {
            //未取得なら取得
            if (this.logic == null)
            {
                this.logic = new Logic();
                this.logic.SetDBAccess(db);
            }

            return this.logic;

        }
        #endregion

        #region レスポンスクラス取得
        /// <summary>
        /// レスポンスクラス取得
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModel</returns>
        protected ResponseModel<Result> GetResponse(MessageType msg = MessageType.None, params object[] args)
        {
            return ResponseUtil.GetResponse<Result>(msg, args);

        }

        /// <summary>
        /// レスポンスクラス取得
        /// </summary>
        /// <param name="list">リスト</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        protected ResponseModel<Result> GetResponse(IEnumerable<Result> list, MessageType msg = MessageType.None, params object[] args)
        {
            return ResponseUtil.GetResponse(list, msg, args);

        }

        /// <summary>
        /// レスポンスクラス取得
        /// </summary>
        /// <param name="flg">登録可否</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        protected ResponseModel<Result> GetResponse(bool flg, MessageType msg = MessageType.None, params object[] args)
        {
            return ResponseUtil.GetResponse<Result>(flg, msg, args);

        }

        /// <summary>
        /// レスポンスクラス取得
        /// </summary>
        /// <param name="fileMap">ファイル</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="args">書式設定対象</param>
        /// <returns>ResponseModels</returns>
        protected HttpResponseMessage GetResponse(IDictionary<Stream, string> fileMap, MessageType msg = MessageType.None, params object[] args)
        {
            return ResponseUtil.GetResponse(fileMap, msg, args);

        }
        #endregion

    }
}
