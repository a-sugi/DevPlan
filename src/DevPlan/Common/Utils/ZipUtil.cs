using ICSharpCode.SharpZipLib.Zip;
using System;

namespace DevPlan.UICommon.Utils
{
    public class ZipUtil
    {
        /// <summary>
        /// フォルダをZip化する
        /// </summary>
        /// <param name="dirPath">圧縮元フォルダのファイルパス</param>
        /// <param name="folderPath">圧縮後のファイル名</param>
        /// <returns></returns>
        public static bool FolderCompression(string dirPath, string saveFileName, string pass = "")
        {
            try
            {
                var fastZip = new FastZip();

                fastZip.CreateEmptyDirectories = true;
                fastZip.UseZip64 = UseZip64.Dynamic;

                if (!string.IsNullOrWhiteSpace(pass))
                {
                    fastZip.Password = pass;
                }

                fastZip.CreateZip(saveFileName, dirPath, true, null, null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
