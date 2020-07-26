using Microsoft.VisualBasic;
using RsyncFHIR.Model;
using RsyncFHIR.Util;
using RsyncFHIR.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace RsyncFHIR.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Config _config;

        public Config Config
        {
            get { return this._config; }
            set { this._config = value; }
        }

        private SettingCommand? _settingCommand;
        /// <summary>
        /// オプションコマンド
        /// </summary>
        public SettingCommand SettingCommand
        {
            get
            {
                if (this._settingCommand == null) this._settingCommand = new SettingCommand(this);
                return this._settingCommand;
            }
        }

        private AboutCommand? _abooutCommand;
        /// <summary>
        /// RSYNC FHIRについてコマンド
        /// </summary>
        public AboutCommand AboutBoxCommand
        {
            get
            {
                if (this._abooutCommand == null) this._abooutCommand = new AboutCommand(this);
                return this._abooutCommand;
            }
        }

        private RSyncStartCommand? _rSyncStartCommand;
        /// <summary>
        /// 開始コマンド
        /// </summary>
        public RSyncStartCommand RSyncStartCommand
        {
            get
            {
                if (this._rSyncStartCommand == null) this._rSyncStartCommand = new RSyncStartCommand(this);
                return this._rSyncStartCommand;
            }
        }

        private RSyncStopCommand? _SyncStopCommand;
        /// <summary>
        /// 停止コマンド
        /// </summary>
        public RSyncStopCommand RSyncStopCommand
        {
            get
            {
                if (this._SyncStopCommand == null) this._SyncStopCommand = new RSyncStopCommand(this);
                return this._SyncStopCommand;
            }
        }

        private bool _isRsyncFHIR;
        /// <summary>
        /// 連携中フラグ
        /// </summary>
        public bool IsRsyncFHIR
        {
            get { return this._isRsyncFHIR; }
            set
            {
                this._isRsyncFHIR = value;
                base.OnPropertyChanged("IsRsyncFHIR");
            }
        }

        private int _jsonFileCount;
        /// <summary>
        /// 残ファイル処理件数
        /// </summary>
        public int JsonFileCount
        {
            get { return this._jsonFileCount; }
            set
            {
                this._jsonFileCount = value;
                base.OnPropertyChanged("JsonFileCount");
            }
        }

        private FileSystemWatcher _watcher = new FileSystemWatcher();
        /// <summary>
        /// FileSystemWatcher
        /// </summary>
        public FileSystemWatcher Watcher
        {
            get { return this._watcher; }
            set
            {
                this._watcher = value;
                base.OnPropertyChanged("Watcher");
            }
        }

        public ObservableCollection<MessageItem> MessageItems { get; } = new ObservableCollection<MessageItem>();

        public void Initalize()
        {
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        public MainWindowViewModel() : base()
        {
            this._config = Config.GetConfig();
            IsRsyncFHIR = false;
            BindingOperations.EnableCollectionSynchronization(this.MessageItems, new object());

            Watcher = CreateWatcher(this.Config.SyncDir);
        }

        /// <summary>
        /// ルートディレクトリのFileSystemWatcherを生成する
        /// </summary>
        /// <param name="watchPath">ルートディレクトリのパス</param>
        /// <returns></returns>
        private FileSystemWatcher CreateWatcher(string watchPath)
        {
            var rsmodel = new RSyncModel();
            rsmodel.OnNewMessage += AddMessageItems;

            FileSystemEventHandler? createdHandler = null;
            RenamedEventHandler? renamedHandler = null;
            createdHandler = async (s, e) => await rsmodel.MainProcAsync(watchPath, e.Name);
            renamedHandler = async (s, e) => await rsmodel.MainProcAsync(watchPath, e.Name);

            var watcher = new FileSystemWatcher(watchPath)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime,
            };
            watcher.Created += createdHandler;
            watcher.Changed += createdHandler;
            watcher.Renamed += renamedHandler;

            return watcher;
        }

        /// <summary>
        /// MessageItemsへの追加
        /// </summary>
        /// <param name="addMessage"></param>
        public void AddMessageItems(MessageItem addMessage)
        {
            var itemcount = this.MessageItems.Count > 0 ? this.MessageItems.First().No + 1 : 1;
            if (itemcount > 100) itemcount--;

            addMessage.No = itemcount;
            this.MessageItems.Insert(0, addMessage);
        }
    }

    /// <summary>
    /// オプションコマンド
    /// </summary>
    public class SettingCommand : CommandBase
    {
        private MainWindowViewModel _vm;
        public SettingCommand(MainWindowViewModel vm) : base()
        {
            this._vm = vm;
        }

        public override bool CanExecuteCore(object parameter) => !_vm.IsRsyncFHIR;

        public override void ExecuteCoreAsync(object parameter)
        {
            var option = new OptionWindow();
            var viewmodel = new OptionWindowViewModel();

            viewmodel.OnClose += () => option.Close();
            viewmodel.OnClose += () => viewmodel.Dispose();
            option.DataContext = viewmodel;
            option.ShowDialog();

            _vm.Config = Config.GetConfig();
        }
    }

    /// <summary>
    /// RSYNC FHIRについてコマンド
    /// </summary>
    public class AboutCommand : CommandBase
    {
        private MainWindowViewModel _vm;
        public AboutCommand(MainWindowViewModel vm) : base()
        {
            this._vm = vm;
        }

        public override bool CanExecuteCore(object parameter) => true;

        public override void ExecuteCoreAsync(object parameter)
        {
            var about = new AboutWindow();
            var viewmodel = new AboutWindowViewModel();

            about.DataContext = viewmodel;
            about.ShowDialog();
        }
    }

    /// <summary>
    /// 開始コマンド
    /// </summary>
    public class RSyncStartCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public RSyncStartCommand(MainWindowViewModel vm) : base() => _vm = vm;

        public override bool CanExecuteCore(object parameter) => _vm != null && !_vm.IsRsyncFHIR;

        public override void ExecuteCoreAsync(object parameter)
        {
            if (_vm == null) return;
            _vm.IsRsyncFHIR = true;

            // Watcherを起動
            _vm.Watcher.EnableRaisingEvents = true;
            _vm.AddMessageItems(new MessageItem()
            {
                No = _vm.MessageItems.Count + 1,
                PastTime = DateTime.Now,
                Comment = $"連携処理を開始しました。",
                Result = true,
            });
        }
    }

    /// <summary>
    /// 停止コマンド
    /// </summary>
    public class RSyncStopCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public RSyncStopCommand(MainWindowViewModel vm) : base() => _vm = vm;

        public override bool CanExecuteCore(object parameter) => _vm.IsRsyncFHIR;

        public override void ExecuteCoreAsync(object parameter)
        {
            _vm.IsRsyncFHIR = false;
            // ルートディレクトリのwatcherを停止
            _vm.Watcher.EnableRaisingEvents = false;
        }
    }
}
