using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevPlan.UICommon.Utils
{
    public static class FileUtil
    {
        #region ファイルのコピー
        /// <summary>
        /// ファイルのコピー
        /// </summary>
        /// <param name="oldPath">コピー元パス</param>
        /// <param name="newPath">コピー先パス</param>
        public static void FileCopy(string oldPath, string newPath)
        {
            FileCopy(oldPath, newPath, true);

        }

        /// <summary>
        /// ファイルのコピー
        /// </summary>
        /// <param name="oldPath">コピー元パス</param>
        /// <param name="newPath">コピー先パス</param>
        /// <param name="isOverwrite">上書き可否</param>
        public static void FileCopy(string oldPath, string newPath, bool isOverwrite)
        {
            //コピー元が存在するかどうか
            if (IsFileExists(oldPath) == true)
            {
                //コピー先のディレクトリ作成
                CreateDirectory(newPath);

                //ファイルコピー
                File.Copy(oldPath, newPath, isOverwrite);

            }

        }
        #endregion

        #region ファイルの移動
        /// <summary>
        /// ファイルの移動
        /// </summary>
        /// <param name="oldPath">移動元パス</param>
        /// <param name="newPath">移動先パス</param>
        public static void FileMove(string oldPath, string newPath)
        {
            FileMove(oldPath, newPath, true);

        }

        /// <summary>
        /// ファイルの移動
        /// </summary>
        /// <param name="oldPath">移動元パス</param>
        /// <param name="newPath">移動先パス</param>
        /// <param name="isOverwrite">上書き可否</param>
        public static void FileMove(string oldPath, string newPath, bool isOverwrite)
        {
            //コピー元が存在するかどうか
            if (IsFileExists(oldPath) == true)
            {
                //ファイルのコピー
                FileCopy(oldPath, newPath, isOverwrite);

                //移動元ファイルの削除
                FileDelete(oldPath);

            }

        }
        #endregion

        #region ファイルの削除
        /// <summary>
        /// ファイルの削除
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static void FileDelete(string path)
        {
            //ファイルが存在するかどうか
            if (IsFileExists(path) == true)
            {
                var fi = new FileInfo(path);

                //読み取り専用の場合は解除
                if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    fi.Attributes = FileAttributes.Normal;

                }

                //ファイル削除
                fi.Delete();

            }

        }
        #endregion

        #region ディレクトリ名の取得
        /// <summary>
        /// ディレクトリ名の取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static string GetDirectoryPath(string path)
        {
            return GetExtension(path) != "" ? Path.GetDirectoryName(path) : path;

        }
        #endregion

        #region ファイルの存在可否
        /// <summary>
        /// ファイルの存在可否
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static bool IsFileExists(string path)
        {
            return File.Exists(path);

        }

        /// <summary>
        /// ディレクトリの存在可否
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static bool IsDirectoryExists(string path)
        {
            return Directory.Exists(path);

        }
        #endregion

        #region ディレクトリの作成
        /// <summary>
        /// ディレクトリの作成
        /// </summary>
        /// <param name="path">パス</param>
        public static void CreateDirectory(string path)
        {
            //ディレクトリが存在していないなら作成
            var dirPath = GetDirectoryPath(path);
            if (IsDirectoryExists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);

            }

        }
        #endregion

        #region ディレクトリの移動
        /// <summary>
        /// ディレクトリの移動
        /// </summary>
        /// <param name="path">パス</param>
        public static void MoveDirectory(string oldPath, string newPath)
        {
            //移動元のディレクトリが存在するかどうか
            var dirPath = GetDirectoryPath(oldPath);
            if (IsDirectoryExists(dirPath) == true)
            {
                //移動先の親フォルダ作成
                CreateDirectory(Directory.GetParent(newPath).FullName);

                //フォルダ移動
                Directory.Move(dirPath, newPath);

            }

        }
        #endregion

        #region ディレクトリの削除
        /// <summary>
        /// ディレクトリの削除
        /// </summary>
        /// <param name="path">パス</param>
        public static void DeleteDirectory(string path)
        {
            Action<DirectoryInfo> action = d =>
            {
                if ((d.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    d.Attributes = FileAttributes.Normal;

                }

            };

            //ディレクトリが存在しているかどうか
            var dirPath = GetDirectoryPath(path);
            if (IsDirectoryExists(dirPath) == true)
            {
                var di = new DirectoryInfo(dirPath);

                //読み取り専用の場合は解除
                action(di);
                foreach (var x in di.GetDirectories("*", SearchOption.AllDirectories))
                {
                    action(x);

                }

                //配下のファイルを削除
                foreach (var x in GetRootAllFilePath(dirPath))
                {
                    FileDelete(x);

                }

                //配下のフォルダとファイルごと削除
                di.Delete(true);

            }

        }
        #endregion

        #region 相対パスから絶対パスを取得
        /// <summary>
        /// 相対パスから絶対パスを取得
        /// </summary>
        /// <param name="rootPath">ルートパス</param>
        /// <param name="path">相対パス</param>
        /// <returns></returns>
        public static string GetFullPath(string rootPath, string path)
        {
            var ｃurrentDir = Environment.CurrentDirectory;
            var fullPath = "";

            try
            {
                //ルートパスのディレクトリを作成
                CreateDirectory(rootPath);

                //一旦カレントディレクトリを変更してから絶対パスを取得
                Environment.CurrentDirectory = rootPath;
                fullPath = Path.GetFullPath(path);

            }
            catch
            {
                throw;

            }
            finally
            {
                //カレントディレクトリを元に戻す
                Environment.CurrentDirectory = ｃurrentDir;

            }

            return fullPath;

        }
        #endregion

        #region ファイル名を取得
        /// <summary>
        /// ファイル名を取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="isExtension">拡張子取得可否</param>
        /// <returns></returns>
        public static string GetFileName(string path, bool isExtension = true)
        {
            return isExtension == true ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);

        }
        #endregion

        #region ファイルの拡張子を取得
        /// <summary>
        /// ファイルの拡張子を取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="isLower">拡張子小文字取得可否</param>
        /// <returns></returns>
        public static string GetExtension(string path, bool isLower = true)
        {
            var extension = Path.GetExtension(path);

            return isLower == true ? extension.ToLowerInvariant() : extension;

        }
        #endregion

        #region パスを結合
        /// <summary>
        /// パスを結合
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static string GetPathCombine(params object[] path)
        {
            return Path.Combine(path.Select(x => x.ToString()).ToArray());

        }
        #endregion

        #region ファイルストリームの取得
        /// <summary>
        /// ファイルストリームの取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static Stream GetFileStream(string path)
        {
            return File.OpenRead(path);

        }

        /// <summary>
        /// メモリーストリームの取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static MemoryStream GetMemoryStream(string path)
        {
            using (var stream = GetFileStream(path))
            {
                return stream.ToMemoryStream();

            }

        }
        #endregion

        #region バイト配列の取得
        /// <summary>
        /// バイト配列の取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static byte[] GetByteArray(string path)
        {
            return File.ReadAllBytes(path);

        }
        #endregion

        #region ストリームの保存
        /// <summary>
        /// ストリームの保存
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="path">パス</param>
        public static void WriteStream(this Stream stream, string path)
        {
            //ストリームが存在するかどうか
            if (stream != null && stream.Length > 0)
            {
                var buf = new byte[stream.Length];

                //バイト配列に読み込み
                stream.Position = 0;
                stream.Read(buf, 0, buf.Length);
                stream.Position = 0;

                //保存先のディレクトリ作成
                CreateDirectory(path);

                //書き込み
                File.WriteAllBytes(path, buf);

            }

        }
        #endregion

        #region ストリームに読込
        /// <summary>
        /// ストリームの保存
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="path">パス</param>
        public static void ReadStream(this Stream stream, string path)
        {
            //ストリームがNULL以外でファイルが存在するかどうか
            if (stream != null && IsFileExists(path) == true)
            {
                var buf = GetByteArray(path);

                //バイト配列から読み込み
                stream.Position = 0;
                stream.Write(buf, 0, buf.Length);
                stream.Position = 0;

            }

        }
        #endregion

        #region メモリーストリームに変換
        /// <summary>
        /// メモリーストリームに変換
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            var ms = new MemoryStream();

            //ストリームをコピー
            stream.Position = 0;
            stream.CopyTo(ms);

            //ポジションを先頭に設定
            stream.Position = 0;
            ms.Position = 0;

            return ms;

        }

        /// <summary>
        /// メモリーストリームに変換
        /// </summary>
        /// <param name="buf">バイト配列</param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this byte[] buf)
        {
            var ms = new MemoryStream();

            if (buf != null && buf.Any() == true)
            {
                //書き込み
                ms.Write(buf, 0, buf.Length);

                //ポジションを先頭に設定
                ms.Position = 0;

            }

            return ms;

        }
        #endregion

        #region バイト配列に変換
        /// <summary>
        /// バイト配列に変換
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            return stream.ToMemoryStream().ToArray();

        }
        #endregion

        #region 一時保存先パスの取得
        /// <summary>
        /// 一時保存先パスの取得
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        public static string GetTmpFilePath(string fileName)
        {
            return GetTmpFilePath(Path.GetTempPath(), fileName);

        }

        /// <summary>
        /// 一時保存先パスの取得
        /// </summary>
        /// /// <param name="rootPath">ルートパス</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns></returns>
        public static string GetTmpFilePath(string rootPath, string fileName)
        {
            return GetPathCombine(rootPath, "Temp", DateTime.Now.ToString("yyMMdd/HHmmss"), Guid.NewGuid(), fileName);

        }
        #endregion

        #region テキストの読み込み
        /// <summary>
        /// テキストの読み込み(SJIS)
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static string ReadSjisText(string path)
        {
            return ReadText(path, Const.Sjis);

        }

        /// <summary>
        /// テキストの読み込み(UTF-8)
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static string ReadUtf8Text(string path)
        {
            return ReadText(path, Const.Utf8);

        }

        /// <summary>
        /// テキストの読み込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="enc">文字コード</param>
        /// <returns></returns>
        public static string ReadText(string path, Encoding enc)
        {
            var txt = "";

            //パスにファイルが存在するかどうか
            if (IsFileExists(path) == true)
            {
                txt = File.ReadAllText(path, enc);

            }

            return txt;

        }
        #endregion

        #region テキストの行読み込み
        /// <summary>
        /// テキストの行読み込み(SJIS)
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadSjisTextLines(string path)
        {
            return ReadTextLines(path, Const.Sjis);

        }

        /// <summary>
        /// テキストの行読み込み(UTF-8)
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadUtf8TextLines(string path)
        {
            return ReadTextLines(path, Const.Utf8);

        }

        /// <summary>
        /// テキストの行読み込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="enc">文字コード</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadTextLines(string path, Encoding enc)
        {
            var txt = Enumerable.Empty<string>();

            //パスにファイルが存在するかどうか
            if (IsFileExists(path) == true)
            {
                txt = File.ReadAllLines(path, enc);

            }

            return txt;

        }
        #endregion

        #region テキストの書き込み
        /// <summary>
        /// テキストの書き込み(SJIS)
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        public static void WriterSjisText(string path, StringBuilder value, bool append = false)
        {
            WriterText(path, value, append, Const.Sjis);

        }

        /// <summary>
        /// テキストの書き込み(UTF-8)
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        public static void WriterUtf8Text(string path, StringBuilder value, bool append = false)
        {
            WriterText(path, value, append, Const.Utf8);

        }

        /// <summary>
        /// テキストの書き込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        /// <param name="enc">文字コード</param>
        public static void WriterText(string path, StringBuilder value, bool append, Encoding enc)
        {
            WriterText(path, value.ToString(), append, enc);

        }

        /// <summary>
        /// テキストの書き込み(SJIS)
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        public static void WriterSjisText(string path, string value, bool append = false)
        {
            WriterText(path, value, append, Const.Sjis);

        }

        /// <summary>
        /// テキストの書き込み(UTF-8)
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        public static void WriterUtf8Text(string path, string value, bool append = false)
        {
            WriterText(path, value, append, Const.Utf8);

        }

        /// <summary>
        /// テキストの書き込み
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="value">値</param>
        /// <param name="append">追記</param>
        /// <param name="enc">文字コード</param>
        public static void WriterText(string path, string value, bool append, Encoding enc)
        {
            //ディレクトリの作成
            CreateDirectory(path);

            //書き込み
            using (var sw = new StreamWriter(path, append, enc))
            {
                sw.Write(value);

            }

        }
        #endregion

        #region ルートの配下のファイルパス取得
        /// <summary>
        /// ルートの配下のファイルパス取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="pattern">パターン</param>
        /// <param name="option">オプション</param>
        /// <returns></returns>
        public static IEnumerable<string> GetRootAllFilePath(string path, string pattern = "*", SearchOption option = SearchOption.AllDirectories)
        {
            return Directory.GetFiles(path, pattern, option);

        }

        /// <summary>
        /// ルートの配下のファイル情報取得
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="pattern">パターン</param>
        /// <param name="option">オプション</param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetRootAllFileInfo(string path, string pattern = "*", SearchOption option = SearchOption.AllDirectories)
        {
            return new DirectoryInfo(path).GetFiles(pattern, option);

        }
        #endregion

        #region ファイルプロセスチェック
        /// <summary>
        /// ファイルプロセスチェック
        /// </summary>
        /// <param name="path">パス</param>
        public static bool IsFileLocked(string path)
        {
            //ファイルが無い場合はロックなし
            if (IsFileExists(path) == false)
            {
                return false;

            }

            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;

                }

            }
            catch
            {

            }

            return true;

        }
        #endregion

        #region Excelファイル判定
        /// <summary>
        /// Excelファイル判定
        /// </summary>
        /// <param name="path">パス</param>
        public static bool IsFileExcel(string path)
        {
            // 旧 Excel 判定コード
            var ole2 = new byte[] { 0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1a, 0xe1 };
            var new1 = new byte[] { 0x57, 0x6f, 0x72, 0x6b, 0x62, 0x6f, 0x6f, 0x6b };
            var new2 = new byte[] { 0x57, 0x0, 0x6f, 0x0, 0x72, 0x0, 0x6b, 0x0, 0x62, 0x0, 0x6f, 0x0, 0x6f, 0x0, 0x6b, 0x0 };
            var old1 = new byte[] { 0x42, 0x6f, 0x6f, 0x6b };
            var old2 = new byte[] { 0x42, 0x0, 0x6f, 0x0, 0x6f, 0x0, 0x6b, 0x0 };

            // 新 Excel 判定ファイル
            var xml = "xl/workbook.xml";

            try
            {
                // 旧 Excel
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                { 
                    var bs = new byte[8];
                    var n = stream.Read(bs, 0, bs.Length);

                    // OLE2
                    if (bs.SequenceEqual(ole2))
                    {
                        bs = new byte[2];
                        stream.Seek(30, SeekOrigin.Begin);
                        n = stream.Read(bs, 0, bs.Length);
                        var bits = bs[0];

                        bs = new byte[4];
                        stream.Seek(48, SeekOrigin.Begin);
                        n = stream.Read(bs, 0, bs.Length);
                        var pos = bs[0];

                        var stat = 512 + pos * (long)Math.Pow(2, bits) + 128;

                        bs = new byte[4];
                        stream.Seek(stat, SeekOrigin.Begin);
                        n = stream.Read(bs, 0, bs.Length);

                        if (bs.SequenceEqual(old1)) return true;

                        bs = new byte[8];
                        stream.Seek(stat, SeekOrigin.Begin);
                        n = stream.Read(bs, 0, bs.Length);

                        if (bs.SequenceEqual(new1)) return true;
                        if (bs.SequenceEqual(old2)) return true;

                        bs = new byte[16];
                        stream.Seek(stat, SeekOrigin.Begin);
                        n = stream.Read(bs, 0, bs.Length);

                        if (bs.SequenceEqual(new2)) return true;
                    }
                }

                // 新 Excel（2007 以降）
                using (var archive = ZipFile.Open(path, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry(xml);

                    if (entry != null) return true;

                }

            }
            catch
            {
                return false;

            }

            return false;

        }
        #endregion
    }
}
