using System;
using System.IO;

namespace Task2
{
    static class FileNameHelper
    {
        readonly static object lockObj = new object();
        public static string GetFileNameAndCreate(Uri uri, string path)
        {
            lock (lockObj) {
                string filename = Path.GetFileName(uri.LocalPath);
                string result = "";
                if (File.Exists(filename)) {
                    for (int i = 0; i < int.MaxValue; i++) {
                        string tempFileName = $"({i}){filename}";
                        if (!File.Exists(tempFileName)) {
                            result = Path.Combine(path, tempFileName);
                            break;
                        }
                    }
                } else {
                    result = Path.Combine(path, filename);
                }

                File.Create(result).Close();
                return result;
            }
        }
    }
}
