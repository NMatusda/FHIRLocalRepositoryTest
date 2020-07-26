using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RsyncFHIR.Model
{
    public class RSyncSubModel
    {
        public event Action<MessageItem> OnNewMessage;
        public int SubCount;

        public async Task TestProcAsync(string dirPath, string fileName)
        {
            await Task.Run(() =>
            {
                this.OnNewMessage?.Invoke(new MessageItem()
                {
                    SubDir = dirPath,
                    Result = SubCount.ToString(),
                    PastTime = DateTime.Now,
                    Comment = $"{fileName}の処理開始。",
                });
                //サブディレクトリ内の
                Thread.Sleep(10000);
                this.OnNewMessage?.Invoke(new MessageItem()
                {
                    SubDir = dirPath,
                    Result = SubCount.ToString(),
                    PastTime = DateTime.Now,
                    Comment = $"{fileName}の処理終了。"
                });
            });
        }
    }
}
