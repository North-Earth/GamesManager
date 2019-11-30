using System.IO;
using System.IO.Compression;
using GamesManager.Common.Enums;

namespace GamesManager.Launcher.Models
{
    public static class DirectoryManager
    {
        #region Fields

        public const string CachePath = "cache";

        public const string AppsPath = "apps";

        #endregion

        #region Methods

        public static void CreateApplicationDirectory()
        {
            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);

            if (!Directory.Exists(AppsPath))
                Directory.CreateDirectory(AppsPath);
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void ClearDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }
            }
        }

        public static bool IsExistsFile(string fileName, long size)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists && fileInfo.Length == size)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ExtractToDirectory(string zipPath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        public static bool IsInstaled(GameName game) 
            => new FileInfo(GetPathGame(game)).Exists;

        public static string GetPathGame(GameName game) 
            => Path.Combine(AppsPath, ConvertEnumToName(game), $"{ConvertEnumToName(game)}.exe");

        public static string ConvertEnumToName(GameName gameName)
            => gameName.ToString().Replace('_', ' ');

        #endregion
    }
}
