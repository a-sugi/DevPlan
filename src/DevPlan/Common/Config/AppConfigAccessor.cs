using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Config
{
    /// <summary>
    /// App.config アクセッサ。
    /// </summary>
    /// <remarks>
    /// App.config 内に定義された appSettings へのアクセッサです。
    /// </remarks>
    public class AppConfigAccessor
    {
        /// <summary>コンストラクタ。</summary>
        /// <remarks>特に何もしません。</remarks>
        public AppConfigAccessor() { return; }

        /// <summary>
        /// app.config 内の＜appSettings＞内定義値を取得する。
        /// </summary>
        /// <param name="key">＜appSettings＞内に定義したキー値</param>
        /// <returns>設定値</returns>
        /// <remarks>
        /// app.config 内の＜appSettings＞に設定した値を取得します。
        /// </remarks>
        /// <exception cref="CoreException">
        /// app.config 内にキー [key] の定義が存在しません。
        /// </exception>
        public string GetAppSetting(string key)
        {
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key))
                {
                    return System.Configuration.ConfigurationManager.AppSettings[key];
                }
                else
                {
                    throw new Exception("not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("app.config 内にキー [" + key + "] の定義が存在しません。", ex);
            }
        }
    }
}
