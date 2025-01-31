using System.Collections.Generic;
using System.IO;

namespace Controller.Utils
{
    internal static class CsvReader
    {
        internal static List<string[]> GetCSV(string csvPath, bool isIncludeHeder)
        {
            List<string[]> list = [];
            // 読み込みたいCSVファイルのパスを指定して開く
            StreamReader sr = new(csvPath);
            {
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string? line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else
                    {
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        string[] values = line.Split(',');
                        list.Add(values);
                    }
                }
            }
            if (isIncludeHeder)
            {
                // 処理なし
            }
            else
            {
                // ヘッダを含まないのでゼロ行目を消す
                list.RemoveAt(0);
            }
            return list;
        }
    }
}
