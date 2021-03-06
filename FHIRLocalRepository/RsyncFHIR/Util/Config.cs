﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RsyncFHIR.Util
{
    public sealed class Config
    {
        private static Config? _config;
        private IDictionary<string, string> _configDictionary;

        private Config()
        {
            _configDictionary = new Dictionary<string, string>();
        }

        public void Save()
        {
            var saveconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (var key in _configDictionary.Keys)
            {
                saveconfig.AppSettings.Settings[key].Value = _configDictionary[key];
            }

            saveconfig.Save();
        }

        public static Config GetConfig()
        {
            if (_config != null) return _config;

            _config = new Config();
            _config._configDictionary = LoadConfigToDictionary();

            return _config;
        }

        private static Dictionary<string, string> LoadConfigToDictionary()
        {
            var configs = new Dictionary<string, string>();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                configs.Add(key, ConfigurationManager.AppSettings[key]);
            }

            return configs;
        }

        private const string CONS_TOPMOST = "TopMost";
        public bool TopMost
        {
            get
            {
                bool val;
                if (this._configDictionary.ContainsKey(CONS_TOPMOST) && bool.TryParse(this._configDictionary[CONS_TOPMOST], out val))
                {
                    return val;
                }
                return false;
            }
            set
            {
                this._configDictionary[CONS_TOPMOST] = value.ToString();
            }
        }

        private const string CONS_DOINITEXEC = "DoInitExec";
        public bool DoInitExec
        {
            get
            {
                bool val;
                if (this._configDictionary.ContainsKey(CONS_DOINITEXEC) && bool.TryParse(this._configDictionary[CONS_DOINITEXEC], out val))
                {
                    return val;
                }
                return false;
            }
            set
            {
                this._configDictionary[CONS_DOINITEXEC] = value.ToString();
            }
        }

        private const string CONS_LOGDAYS = "LogDays";
        public int LogDays
        {
            get
            {
                int val;
                if (this._configDictionary.ContainsKey(CONS_LOGDAYS) && int.TryParse(this._configDictionary[CONS_LOGDAYS], out val))
                {
                    return val;
                }
                return 30;
            }
            set
            {
                this._configDictionary[CONS_LOGDAYS] = value.ToString();
            }
        }

        private const string CONS_SYNCDIR = "SyncDir";
        public string SyncDir
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_SYNCDIR)) return string.Empty;
                return this._configDictionary[CONS_SYNCDIR];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_SYNCDIR] = string.Empty;
                this._configDictionary[CONS_SYNCDIR] = value;
            }
        }

        private const string CONS_DBHOST = "DBHost";
        public string DBHost
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_DBHOST)) return string.Empty;
                return this._configDictionary[CONS_DBHOST];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_DBHOST] = string.Empty;
                this._configDictionary[CONS_DBHOST] = value;
            }
        }

        private const string CONS_DBUSERID = "DBUserId";
        public string DBUserId
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_DBUSERID)) return string.Empty;
                return this._configDictionary[CONS_DBUSERID];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_DBUSERID] = string.Empty;
                this._configDictionary[CONS_DBUSERID] = value;
            }
        }

        private const string CONS_DBPASSWORD = "DBPassword";
        public string DBPassword
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_DBPASSWORD)) return string.Empty;
                return this._configDictionary[CONS_DBPASSWORD];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_DBPASSWORD] = string.Empty;
                this._configDictionary[CONS_DBPASSWORD] = value;
            }
        }

        private const string CONS_MONGOCONNECTIONSTRING = "MongoConnectionString";
        public string MongoConnectionString
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_MONGOCONNECTIONSTRING)) return string.Empty;
                return this._configDictionary[CONS_MONGOCONNECTIONSTRING];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_MONGOCONNECTIONSTRING] = string.Empty;
                this._configDictionary[CONS_MONGOCONNECTIONSTRING] = value;
            }
        }

        private const string CONS_MONGODBNAME = "MongoDBName";
        public string MongoDBName
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_MONGODBNAME)) return string.Empty;
                return this._configDictionary[CONS_MONGODBNAME];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_MONGODBNAME] = string.Empty;
                this._configDictionary[CONS_MONGODBNAME] = value;
            }
        }
        
        private const string CONS_MONGOCOLLECTIONNAME = "MongoCollectionName";
        public string MongoCollectionName
        {
            get
            {
                if (!this._configDictionary.ContainsKey(CONS_MONGOCOLLECTIONNAME)) return string.Empty;
                return this._configDictionary[CONS_MONGOCOLLECTIONNAME];
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) this._configDictionary[CONS_MONGOCOLLECTIONNAME] = string.Empty;
                this._configDictionary[CONS_MONGOCOLLECTIONNAME] = value;
            }
        }
    }
}
