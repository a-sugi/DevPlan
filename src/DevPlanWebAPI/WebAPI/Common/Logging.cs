using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using System;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// API ログクラス
    /// </summary>
    public class Logging
    {
        #region メンバ変数
        /// <summary>
        /// トレース
        /// </summary>
        public readonly int Trace = 0;
        /// <summary>
        /// デバッグ
        /// </summary>
        public readonly int Debug = 1;
        /// <summary>
        /// 処理／操作
        /// </summary>
        public readonly int Info = 2;
        /// <summary>
        /// 注意／警告
        /// </summary>
        public readonly int Warn = 3;
        /// <summary>
        /// API エラー
        /// </summary>
        public readonly int Error = 4;
        /// <summary>
        /// システム障害
        /// </summary>
        public readonly int Fatal = 5;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region ログ出力
        /// <summary>
        /// ログ出力
        /// </summary>
        public void WriteLog(int level, string message)
        {
            if (level.Equals(Trace)) logger.Trace(message);
            if (level.Equals(Debug)) logger.Debug(message);
            if (level.Equals(Info)) logger.Info(message);
            if (level.Equals(Warn)) logger.Warn(message);
            if (level.Equals(Error)) logger.Error(message);
            if (level.Equals(Fatal)) logger.Fatal(message);
        }
        /// <summary>
        /// ログ出力（ex）
        /// </summary>
        public void WriteErrorLog(int level, string message, Exception ex)
        {
            if (level.Equals(Error)) logger.Error(message, ex);
            if (level.Equals(Fatal)) logger.Fatal(message, ex);
        }
        #endregion
    }

    /// <summary>
    /// ログ拡張クラス
    /// </summary>
    public static class ILogExtentions
    {
        #region トレース
        /// <summary>
        /// トレース
        /// </summary>
        public static void Trace(this ILog log, string message)
        {
            log.Trace(message, null);
        }
        /// <summary>
        /// トレース（ex）
        /// </summary>
        public static void Trace(this ILog log, string message, Exception exception)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, log4net.Core.Level.Trace, message, exception);
        }
        #endregion
    }
}