using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.ViewModel
{
    public sealed class AboutWindowViewModel : BaseViewModel
    {
        //AboutWindowにて、バージョン等の定数以外の情報を表示する場合はここで。
        public string Version
        {
            get
            {
                return $"1.0.0.0";
            }
        }
    }
}
