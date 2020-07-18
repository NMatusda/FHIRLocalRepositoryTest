using Microsoft.VisualBasic;
using RsyncFHIR.Model;
using RsyncFHIR.Util;
using RsyncFHIR.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private SettingCommand _settingCommand;
        public SettingCommand SettingCommand
        {
            get
            {
                if (_settingCommand == null) _settingCommand = new SettingCommand(this);
                return _settingCommand;
            }
        }

        public ICommand AboutBoxCommand { get; set; }

        private RSyncStartCommand _rSyncStartCommand;
        public RSyncStartCommand RSyncStartCommand
        {
            get
            {
                if (_rSyncStartCommand == null) _rSyncStartCommand = new RSyncStartCommand(this);
                return _rSyncStartCommand;
            }
        }

        private RSyncStopCommand _SyncStopCommand;
        public RSyncStopCommand RSyncStopCommand
        {
            get
            {
                if (_SyncStopCommand == null) _SyncStopCommand = new RSyncStopCommand(this);
                return _SyncStopCommand;
            }
        }

        private bool _isRsyncFHIR;
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
        public int JsonFileCount
        {
            get { return this._jsonFileCount; }
            set
            {
                this._jsonFileCount = value;
                base.OnPropertyChanged("JsonFileCount");
            }
        }

        private FileSystemWatcher _watcher;
        public FileSystemWatcher Watcher
        {
            get { return this._watcher; }
            set
            {
                this._watcher = value;
                base.OnPropertyChanged("Watcher");
            }
        }

        private List<FileSystemWatcher> _subWatcher;
        public List<FileSystemWatcher> SubWatcher
        {
            get { return this._subWatcher; }
            set
            {
                this._subWatcher = value;
                base.OnPropertyChanged("SubWatcher");
            }
        }

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

            var rsm = new RSyncModel();
            rsm.CreateSubDirectory($"C:\\SynchronizeDirectory", Config.SeparateDir);

            Watcher = CreateWatcher($"C:\\SynchronizeDirectory");

            SubWatcher = CreateSubWatcher($"C:\\SynchronizeDirectory");

            this.AboutBoxCommand = new RelayCommand(p => this.ShowAbout());
        }

        private FileSystemWatcher CreateWatcher(string watchPath)
        {
            var rsmodel = new RSyncModel();
            FileSystemEventHandler createdHandler = null;
            RenamedEventHandler renamedHandler = null;
            createdHandler = async (s, e) => await rsmodel.MoveDirectoryAsync(watchPath, e.Name);
            renamedHandler = async (s, e) => await rsmodel.MoveDirectoryAsync(watchPath, e.Name);

            var watcher = new FileSystemWatcher(watchPath)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess
            };
            watcher.Created += createdHandler;
            watcher.Renamed += renamedHandler;

            return watcher;
        }

        private List<FileSystemWatcher> CreateSubWatcher(string watchPath)
        {
            var fswlist = new List<FileSystemWatcher>();
            foreach (var subdir in Directory.GetDirectories(watchPath))
            {
                var rsmodel = new RSyncSubModel();
                FileSystemEventHandler createdHandler = null;
                RenamedEventHandler renamedHandler = null;
                createdHandler = async (s, e) => await rsmodel.TestProcAsync(subdir, e.Name);
                renamedHandler = async (s, e) => await rsmodel.TestProcAsync(subdir, e.Name);

                var watcher = new FileSystemWatcher(subdir)
                {
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess
                };
                watcher.Created += createdHandler;
                watcher.Renamed += renamedHandler;

                fswlist.Add(watcher);
            }

            return fswlist;
        }

        private void ShowAbout()
        {
        }
    }

    public class SettingCommand : CommandBase
    {
        private MainWindowViewModel _vm;
        public SettingCommand(MainWindowViewModel vm) : base()
        {
            _vm = vm;
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

    public class RSyncStartCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public RSyncStartCommand(MainWindowViewModel vm) : base() => _vm = vm;

        public override bool CanExecuteCore(object parameter) => !_vm.IsRsyncFHIR;

        public override async void ExecuteCoreAsync(object parameter)
        {
            _vm.IsRsyncFHIR = true;
            // watcherを起動
            foreach (var subw in _vm.SubWatcher)
            {
                subw.EnableRaisingEvents = true;
            }
            // 蓄積している分を処理
            var rs = new RSyncModel();
            await rs.MoveDirectoryAsync($"C:\\SynchronizeDirectory");
            _vm.Watcher.EnableRaisingEvents = true;
        }
    }

    public class RSyncStopCommand : CommandBase
    {
        private MainWindowViewModel _vm;

        public RSyncStopCommand(MainWindowViewModel vm) : base() => _vm = vm;

        public override bool CanExecuteCore(object parameter) => _vm.IsRsyncFHIR;

        public override void ExecuteCoreAsync(object parameter)
        {
            _vm.IsRsyncFHIR = false;
            _vm.Watcher.EnableRaisingEvents = false;
            foreach (var subw in _vm.SubWatcher)
            {
                subw.EnableRaisingEvents = false;
            }
        }
    }
}
