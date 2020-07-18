using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RsyncFHIR.Model
{
    public class RSyncModel
    {
        private readonly IList<string> _spellSet = new List<string>{
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",  }.AsReadOnly();
        private const string CONST_OTHERDIR = "other";

        public void CreateSubDirectory(string dirPath, int separeteCnt)
        {
            //既にサブディレクトリが存在する場合、すべてのファイルをルートまで移動してサブディレクトリを削除する
            var di = new DirectoryInfo(dirPath);
            foreach (var filename in Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories))
            {
                SafeMove(dirPath, new FileInfo(filename));
            }
            foreach (var dirname in Directory.GetDirectories(dirPath))
            {
                Directory.Delete(dirname, true);
            }

            //改めて、サブディレクトリを作成。[0-9a-zA-Z]を引数分で分割
            int inspell = _spellSet.Count / (separeteCnt);
            int ccnt = 0;
            var sspell = string.Empty;
            var espell = string.Empty;
            for (var i = 0; i < _spellSet.Count; i++)
            {
                if (i % inspell == 0)
                {
                    if (i != 0) Directory.CreateDirectory($"{dirPath}\\{sspell}-{espell}");
                    sspell = _spellSet[i];
                    ccnt++;
                    if (ccnt >= separeteCnt) break;
                }
                espell = _spellSet[i];
            }
            Directory.CreateDirectory($"{dirPath}\\{sspell}-{_spellSet[_spellSet.Count - 1]}");

            //idのRegexが[A-Za-z0-9\-\.]{1,64}のため[\.]+や[\-]+も許容するので、それらを格納するためのその他フォルダも作成
            Directory.CreateDirectory($"{dirPath}\\{CONST_OTHERDIR}");
        }

        public async Task MoveDirectoryAsync(string dirPath)
        {
            var di = new DirectoryInfo(dirPath);
            await Task.Run(() =>
            {
                foreach (var file in di.GetFiles())
                {
                    MoveFileToSubDirectory(dirPath, file.Name);
                }
            });
        }

        public async Task MoveDirectoryAsync(string dirPath, string fileName)
        {
            await Task.Run(() =>
            {
                MoveFileToSubDirectory(dirPath, fileName);
            });
        }

        private void MoveFileToSubDirectory(string dirPath, string fileName)
        {
            var fi = new FileInfo($"{dirPath}\\{fileName}");
            var idval = string.Empty;
            using (var sr = new StreamReader($"{dirPath}\\{fileName}", Encoding.Unicode))
            {
                var inread = sr.ReadToEnd();
                JObject jsonobj = (JObject)JsonConvert.DeserializeObject(inread);
                idval = jsonobj["id"].ToString();
            }
            SafeMove($"{GetSubDir(dirPath, idval)}", fi);
        }

        private string GetSubDir(string dirPath, string id)
        {
            var targetdir = id.Substring(0, 1);
            foreach (var dir in Directory.GetDirectories(dirPath))
            {
                var dstr = dir.Replace($"{dirPath}\\", "").Split("-");
                if (dstr.Length <= 1) continue;
                if (targetdir.CompareTo(dstr[0]) >= 0 && targetdir.CompareTo(dstr[1]) <= 0)
                {
                    return dir;
                }
            }

            return $"{dirPath}\\{CONST_OTHERDIR}";
        }

        private void SafeMove(string dirPath, FileInfo fInfo)
        {
            var fi = new FileInfo($"{dirPath}\\{fInfo.Name}");
            var ext = fi.Extension;
            var fname = fi.Name.Replace($"{ext}", "");
            var mvname = $"{dirPath}\\{fname}{ext}";
            int cnt = 0;

            while (File.Exists($"{mvname}"))
            {
                cnt++;
                mvname = $"{dirPath}\\{fname}_{cnt.ToString()}{ext}";
            }

            File.Move($"{fInfo.FullName}", mvname);
        }
    }
}
