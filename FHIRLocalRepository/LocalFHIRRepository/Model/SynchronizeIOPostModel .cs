using LocalFHIRRepository.ParameterContainer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Model
{
    public class SynchronizeIOPostModel : BasePostModel
    {
        /// <summary>
        /// POST実行 To IO
        /// </summary>
        /// <param name="value"></param>
        /// <returns>実行成否</returns>
        public override bool TryPost(BaseParameterContainer value)
        {
            try
            {
                if (!value.IsValid()) return false;

                return OutputSynchronizeFile(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 連携ファイルの出力
        /// </summary>
        /// <param name="value"></param>
        /// <returns>実行成否</returns>
        private bool OutputSynchronizeFile(BaseParameterContainer value)
        {
            try
            {
                using (var sw = new StreamWriter($"{Startup.GetSettings("SynchronizeDirectory")}\\{value.SynchronizeFileName()}", false, System.Text.Encoding.Unicode))
                {
                    sw.Write(value.OutputJson());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
