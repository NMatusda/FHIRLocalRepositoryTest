using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.ViewModel
{
    public sealed class GeneralOptionViewModel : OptionBaseViewModel
    {
        private Config _config;

        public GeneralOptionViewModel()
        {
            this._config = Config.GetConfig();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        internal override void Save()
        {
            this._config.Save();
        }

        public bool TopMost
        {
            get { return this._config.TopMost; }
            set
            {
                this._config.TopMost = value;
                base.OnPropertyChanged("TopMost");
            }
        }

        public bool DoInitExec
        {
            get { return this._config.DoInitExec; }
            set
            {
                this._config.DoInitExec = value;
                base.OnPropertyChanged("DoInitExec");
            }
        }

        public int LogDays
        {
            get { return this._config.LogDays; }
            set
            {
                this._config.LogDays = value;
                base.OnPropertyChanged("LogDays");
            }
        }

        public int SeparateDir
        {
            get { return this._config.SeparateDir; }
            set
            {
                this._config.SeparateDir = value;
                base.OnPropertyChanged("SeparateDir");
            }
        }
    }
}
