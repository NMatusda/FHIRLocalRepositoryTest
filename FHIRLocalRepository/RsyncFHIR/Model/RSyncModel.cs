using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RsyncFHIR.Accesser;
using RsyncFHIR.Resource;
using RsyncFHIR.Util;
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
        public event Action<MessageItem>? OnNewMessage;

        public async Task MainProcAsync(string dirPath, string fileName)
        {
            if (!FileWatchList.IsPastProcedure(new FileInfo($"{dirPath}\\{fileName}"))) return;

            await Task.Run(() =>
            {
                this.OnNewMessage?.Invoke(new MessageItem()
                {
                    SubDir = dirPath,
                    PastTime = DateTime.Now,
                    Comment = $"{fileName}の処理開始。",
                    Result = true,
                });

                try
                {
                    // ここでDBへのCRUD処理を行う。
                    //if (json != null) Accesser.Create(json);

                    this.OnNewMessage?.Invoke(new MessageItem()
                    {
                        SubDir = dirPath,
                        PastTime = DateTime.Now,
                        Comment = $"{fileName}の処理終了。",
                        Result = true,
                    });
                }
                catch (Exception ex)
                {
                    //Messageのみ画面出力してStackTraceはファイル出力等の別の手段にするのが良いか。
                    this.OnNewMessage?.Invoke(new MessageItem()
                    {
                        SubDir = dirPath,
                        PastTime = DateTime.Now,
                        Comment = $"エラーが発生しました：{fileName}{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}",
                        Result = false,
                    });
                }
            });
        }

        private JObject GetResourceFromFile(string filePath)
        {
            string read;

            using (var sr = new StreamReader(filePath, System.Text.Encoding.Unicode))
            {
                read = sr.ReadToEnd();
            }
            return JObject.Parse(read);
        }
    }
}
