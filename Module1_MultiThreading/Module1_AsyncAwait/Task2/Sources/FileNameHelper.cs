using System;
using System.IO;

namespace Task2
{
    static class FileNameHelper
    {
        public static string GetFileName(Uri uri, string path)
        {
            string filename = Path.GetFileName(uri.LocalPath);
            if (File.Exists(filename)) {
                for(int i = 0; i < int.MaxValue; i++) {
                    string tempFileName = $"({i}){filename}";
                    if (!File.Exists(tempFileName)) {
                        return Path.Combine(path, tempFileName);
                    }
                }
            }
            return Path.Combine(path, filename);
        }
    }
}
