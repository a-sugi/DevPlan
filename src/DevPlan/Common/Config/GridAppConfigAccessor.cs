using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Config
{
    /// <summary>
    /// グリッド設定情報取得クラス（AppConfigAccessorのラッパーです）
    /// </summary>
    public class GridAppConfigAccessor
    {
        /// <summary>
        /// AppConfigAccessorインスタンス参照。
        /// </summary>
        private AppConfigAccessor accesser;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public GridAppConfigAccessor()
        {
            this.accesser = new AppConfigAccessor();
        }

        /// <summary>
        /// グリッドのマウスホイール移動量取得。
        /// </summary>
        /// <returns></returns>
        public int GetGridMouseWheelCount()
        {
            return int.Parse(this.accesser.GetAppSetting("GRID_MOUSE_WHEEL_COUNT"));
        }

        /// <summary>
        /// CalendarGridのマウスホイール移動量取得。
        /// </summary>
        /// <returns></returns>
        public int GetCalendarGridMouseWheelCount()
        {
            return int.Parse(this.accesser.GetAppSetting("CalendarMouseWheelCount"));
        }

        /// <summary>
        /// CalendarGridのボタンスクロール移動量取得。
        /// </summary>
        /// <returns></returns>
        public int GetCalendarGridButtonScrollCount()
        {
            return int.Parse(this.accesser.GetAppSetting("CalendarMouseWheelCount"));
        }

    }
}
