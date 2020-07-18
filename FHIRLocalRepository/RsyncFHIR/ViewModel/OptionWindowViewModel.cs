using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RsyncFHIR.ViewModel
{
    public class OptionWindowViewModel : BaseViewModel
    {
        public ICommand GeneralOptionOpenCommand { get; private set; }
        public ICommand ConnectOptionOpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private Config _config;

        public event Action OnClose;

        private OptionBaseViewModel _generalOption;
        private OptionBaseViewModel _connectOption;

        private OptionBaseViewModel _optionWindow;
        public OptionBaseViewModel OptionWindow
        {
            get { return _optionWindow; }
            set
            {
                _optionWindow = value;
                base.OnPropertyChanged("OptionWindow");
            }
        }
        public OptionWindowViewModel()
        {
            this._config = Config.GetConfig();

            // App.xamlに記載しているResourceDictionaryにて指定した、Resource > OptionWindow.xaml ファイルの更新が必要なので注意
            this._generalOption = new GeneralOptionViewModel();
            this._connectOption = new ConnectOptionViewModel();

            this.OptionWindow = this._generalOption;

            this.GeneralOptionOpenCommand = new RelayCommand(p => { this.OptionWindow = this._generalOption; });
            this.ConnectOptionOpenCommand = new RelayCommand(p => { this.OptionWindow = this._connectOption; });

            this.SaveCommand = new RelayCommand(p =>
            {
                this._generalOption.Save();
                this._connectOption.Save();
                this.OnClose?.Invoke();
            });

            this.CancelCommand = new RelayCommand(p => { this.OnClose?.Invoke(); });
        }
    }
}
