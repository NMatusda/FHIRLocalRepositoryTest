using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace RsyncFHIR.Util
{
    public static class FileWatchList
    {
        private static List<FileInfoExt> _fileList = new List<FileInfoExt>();

        public static bool IsPastProcedure(FileInfo newFileInfo)
        {
            _fileList = DiscardFileInfo(_fileList);
            var addfiext = new FileInfoExt() { FileInfo = newFileInfo, AddTime = DateTime.Now, };
            foreach (var fie in _fileList)
            {
                if (fie.Equals(addfiext)) return false;
            }

            _fileList.Add(new FileInfoExt() { FileInfo = newFileInfo, AddTime = DateTime.Now, });
            return true;
        }

        private static List<FileInfoExt> DiscardFileInfo(List<FileInfoExt> fileList)
        {
            try
            {
                return fileList.Where((x) => x.AddTime.AddSeconds(10) >= DateTime.Now).ToList();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return new List<FileInfoExt>();
            }
        }
    }

    public class FileInfoExt
    {
        public FileInfo? FileInfo { get; set; }
        public DateTime AddTime { get; set; }

        public override bool Equals(object? obj)
        {
            //前後1秒は許容する。厳密に比較するなら、今回の場合はファイルの内容まで見るべきか？ 速度と相談。
            if (obj == null) return false;
            var fi = (FileInfoExt)obj;
            if (fi.FileInfo == null && this.FileInfo == null) return true;
            if (fi.FileInfo == null || this.FileInfo == null) return false;

            return (fi.FileInfo.FullName == this.FileInfo.FullName)
                && (this.AddTime.CompareTo(fi.AddTime.AddSeconds(1)) < 0 || this.AddTime.AddSeconds(1).CompareTo(fi.AddTime) > 0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
