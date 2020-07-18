using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace RsyncFHIR.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose() { }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class CommandBase : ICommand
    {
        protected CommandBase() { }

        abstract public void ExecuteCoreAsync(object parameter);
        abstract public bool CanExecuteCore(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return this.CanExecuteCore(parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                this.ExecuteCoreAsync(parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
