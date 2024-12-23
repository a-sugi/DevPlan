using System;
using System.Reflection;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// Beanユーティリティークラス
    /// </summary>
    public static class BeanUtil
    {
        /// <summary>
        /// プロパティ値をコピーする
        /// </summary>
        /// <typeparam name="T">コピー元クラス</typeparam>
        /// <typeparam name="K">コピー先クラス</typeparam>
        /// <param name="prmSrcObject">コピー元</param>
        /// <param name="prmDest">コピー先</param>
        /// <param name="prmIgnoreCase">大文字小文字を区別しないか</param>
        /// <returns>コピー先クラスのインスタンス</returns>
        public static K Copy<T, K>(T prmSrcObject, K prmDest,bool prmIgnoreCase = true)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            if(prmIgnoreCase)
            {
                flags = flags | BindingFlags.IgnoreCase;
            }
            return copy(prmSrcObject, prmDest, flags);
        }

        /// <summary>
        /// コピー元のプロパティ値を使用してT型のインスタンスを作成する
        /// </summary>
        /// <typeparam name="T">コピー元クラス</typeparam>
        /// <typeparam name="K">コピー先クラス</typeparam>
        /// <param name="prmSrcObject">コピー元</param>
        /// <param name="prmIgnoreCase">大文字小文字を区別しないか</param>
        /// <returns>コピー先クラスのインスタンス</returns>
        public static K CreateAs<T, K>(T prmSrcObject, bool prmIgnoreCase = true)
            where K : class,new()
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            if (prmIgnoreCase)
            {
                flags = flags | BindingFlags.IgnoreCase;
            }

            var toObject = new K();
            return copy(prmSrcObject, toObject, flags);
        }

        /// <summary>
        /// シンプルな一階層のみのコピー
        /// </summary>
        /// <typeparam name="T">コピー元クラス</typeparam>
        /// <typeparam name="K">コピー先クラス</typeparam>
        /// <param name="prmSrcObject">コピー元</param>
        /// <param name="prmDest">コピー先</param>
        /// <param name="prmBindingFlags">BindingFlags</param>
        /// <returns>toObject</returns>
        private static K copy<T, K>(T prmSrcObject, K prmDest, BindingFlags prmBindingFlags)
        {
            Type childType = typeof(T);
            Type parentType = typeof(K);

            PropertyInfo[] parentProperties = parentType.GetProperties(prmBindingFlags);

            foreach (PropertyInfo property in parentProperties)
            {
                if (childType.GetProperty(property.Name, prmBindingFlags) != null)
                {
                    PropertyInfo to = property;
                    PropertyInfo from = childType.GetProperty(property.Name, prmBindingFlags);

                    //キャスト
                    object fromValue = from.GetValue(prmSrcObject, null);
                    try
                    {
                        if (to.PropertyType == typeof(string))
                        {
                            fromValue = System.Convert.ToString(fromValue);
                        }
                        else if (to.PropertyType == typeof(Decimal))
                        {
                            fromValue = System.Convert.ToDecimal(fromValue);
                        }
                        else if (to.PropertyType == typeof(Nullable<Decimal>))
                        {
                            if (fromValue != null)
                            {
                                fromValue = System.Convert.ToDecimal(fromValue);
                            }
                        }
                        else if (to.PropertyType == typeof(Int32))
                        {
                            fromValue = System.Convert.ToInt32(fromValue);
                        }
                        else if (to.PropertyType == typeof(DateTime))
                        {
                            fromValue = System.Convert.ToDateTime(fromValue);
                        }
                        else if (to.PropertyType == typeof(Nullable<DateTime>))
                        {
                            if (fromValue != null)
                            {
                                fromValue = System.Convert.ToDateTime(fromValue);
                            }
                        }
                        else
                        {
                            fromValue = from.GetValue(prmSrcObject, null);
                        }
                        //設定
                        to.SetValue(prmDest, fromValue, null);
                    }
                    catch (Exception)
                    {
                        if (to.PropertyType == typeof(Decimal))
                        {
                            fromValue = 0;
                        }
                        else if (to.PropertyType == typeof(Int32))
                        {
                            fromValue = 0;
                        }
                        else
                        {
                            fromValue = null;
                        }
                        //設定
                        to.SetValue(prmDest, fromValue, null);
                    }
                }
            }

            return prmDest;
        }
    }
}
