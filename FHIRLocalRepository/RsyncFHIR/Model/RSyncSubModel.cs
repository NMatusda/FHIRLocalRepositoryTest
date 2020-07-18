using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RsyncFHIR.Model
{
    public class RSyncSubModel
    {
        public async Task TestProcAsync(string dirPath, string fileName)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(10000);
            });
        }
    }
}
