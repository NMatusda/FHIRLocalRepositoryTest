using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.ViewModel
{
    public sealed class ConnectOptionViewModel : OptionBaseViewModel
    {
        private Config _config;

        public ConnectOptionViewModel()
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

        public string DBHost
        {
            get { return this._config.DBHost; }
            set
            {
                this._config.DBHost = value;
                base.OnPropertyChanged("DBHost");
            }
        }

        public string DBUserId
        {
            get { return this._config.DBUserId; }
            set
            {
                this._config.DBUserId = value;
                base.OnPropertyChanged("DBUserId");
            }
        }

        public string DBPassword
        {
            get { return this._config.DBPassword; }
            set
            {
                this._config.DBPassword = value;
                base.OnPropertyChanged("DBPassword");
            }
        }
    }
}
