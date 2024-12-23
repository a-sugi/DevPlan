using DevPlan.UICommon.Config;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// AD情報取得Util
    /// </summary>
    public class ADUtil
    {
        /// <summary>
        /// AD情報クエリオブジェクト
        /// </summary>
        private DirectorySearcher dSearcher;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ADUtil()
        {
            var dEntry = new DirectoryEntry(new AppConfigAccessor().GetAppSetting("ActiveDirectoryLDAPpath"));
            dSearcher = new DirectorySearcher(dEntry);
        }

        /// <summary>
        /// AD情報取得
        /// </summary>
        /// <param name="type">情報取得タイプ</param>
        /// <param name="personelId">職番（PERSONEL_ID）</param>
        /// <param name="userName">名前（スペースは除去して検索を行います）</param>
        /// <returns></returns>
        public string GetUserData(ADUtilTypeEnum type, string personelId, string userName)
        {
            try
            {
                string searchPersonelId = personelId;
                if (personelId.Length > 5)
                {
                    searchPersonelId = personelId.Substring(personelId.Length - 5, 5);
                }
                dSearcher.Filter = string.Format("(&(&(objectCategory=person)(objectClass=user))(cn=*{0})(!(userAccountControl:1.2.840.113556.1.4.803:=2)))", searchPersonelId);

                foreach (SearchResult result in dSearcher.FindAll())
                {
                    var resultEntry = result.GetDirectoryEntry();

                    var adUserName = string.Concat(
                        resultEntry.Properties["sn"].Value?.ToString(),
                        resultEntry.Properties["givenName"].Value?.ToString());

                    if (userName.Replace(" ", "").Replace("　", "") == adUserName.Replace(" ", "").Replace("　", ""))
                    {
                        return resultEntry.Properties[type.Key].Value?.ToString();
                    }
                }
            }
            catch (Exception)
            {
                //例外検知しても特に処理を行わない。
            }

            return string.Empty;
        }

        /// <summary>
        /// AD情報取得（全て）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ADUserInfo> AllUserData()
        {
            var dic = new Dictionary<string, ADUserInfo>();

            try
            {
                //Update Start 2021/10/14 矢作
                //dSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(cn=*)(sn=*)(givenName=*)(telephoneNumber=*))";
                dSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(cn=*)(sn=*)(givenName=*)(telephoneNumber=*)(mobile=*))";
                //Update End 2021/10/14 矢作
                dSearcher.PageSize = 1000;

                foreach (SearchResult result in dSearcher.FindAll())
                {
                    var resultEntry = result.GetDirectoryEntry();

                    var cn = (string)resultEntry.Properties["cn"].Value;
                    var sn = (string)resultEntry.Properties["sn"].Value;
                    var givenName = (string)resultEntry.Properties["givenName"].Value;
                    if (cn.Length > 5)
                    {
                        cn = cn.Substring(cn.Length - 5, 5);
                    }

                    dic.Add(string.Format("{0}_{1}{2}", cn, sn, givenName), new ADUserInfo()
                    {
                        PersonelId = cn,
                        Sn = sn,
                        GivenName = givenName,
                        //Update Start 2021/10/15 矢作
                        //Tel = resultEntry.Properties[ADUtilTypeEnum.TEL.Key].Value?.ToString()
                        Tel = resultEntry.Properties[ADUtilTypeEnum.TEL.Key].Value?.ToString(),
                        mobile = resultEntry.Properties[ADUtilTypeEnum.MOBILE.Key].Value?.ToString(),
                        //Update End 2021/10/15 矢作
                    });
                }
            }
            catch (Exception)
            {
                //例外検知しても特に処理を行わない。
            }

            return dic;
        }
    }

    /// <summary>
    /// AD情報ユーザーオブジェクトクラス
    /// </summary>
    public class ADUserInfo
    {
        /// <summary>
        /// PersonelId
        /// </summary>
        public string PersonelId { get; internal set; }

        /// <summary>
        /// 名
        /// </summary>
        public string GivenName { get; internal set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string Sn { get; internal set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Tel { get; internal set; }


        //Append Start 2021/10/14 矢作

        /// <summary>
        /// 携帯番号
        /// </summary>
        public string mobile { get; internal set; }

        //Append End 2021/10/14 矢作
    }

    /// <summary>
    /// AD情報ユーザーデータクラス
    /// </summary>
    public static class ADUserInfoData
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        public static Dictionary<string, ADUserInfo> Dictionary { get; set; }
    }

    /// <summary>
    /// 情報取得タイプ
    /// </summary>
    public sealed class ADUtilTypeEnum
    {
        /// <summary>内部定義インスタンス保持ディクショナリ。</summary>
        private static readonly Dictionary<string, ADUtilTypeEnum> dic = new Dictionary<string, ADUtilTypeEnum>();

        /// <summary>電話番号</summary>
        public static readonly ADUtilTypeEnum TEL = new ADUtilTypeEnum("telephoneNumber");

        /// <summary>メール</summary>
        public static readonly ADUtilTypeEnum MAIL = new ADUtilTypeEnum("mail");

        //Append Start 2021/10/14 矢作
        /// <summary>携帯番号</summary>
        public static readonly ADUtilTypeEnum MOBILE = new ADUtilTypeEnum("mobile");
        //Append End 2021/10/14 矢作

        /// <summary>
        /// ActiveDirectory propertyキー値
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="key">ActiveDirectory propertyキー値</param>
        public ADUtilTypeEnum(string key)
        {
            this.Key = key;
            dic.Add(key, this);
        }
    }
}
