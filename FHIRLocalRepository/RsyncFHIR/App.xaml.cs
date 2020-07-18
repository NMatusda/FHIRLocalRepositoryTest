using RsyncFHIR.Util;
using RsyncFHIR.View;
using RsyncFHIR.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RsyncFHIR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string _appName = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Name;
        private System.Threading.Mutex _mutex = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 多重起動の禁止
            _mutex = new System.Threading.Mutex(false, _appName);
            var hashandle = false;
            try
            {
                hashandle = _mutex.WaitOne(0, false);
            }
            catch (System.Threading.AbandonedMutexException)
            {
                hashandle = true;
            }

            if (!hashandle)
            {
                MessageBox.Show($"既に{_appName}が起動しています。{Environment.NewLine}多重起動はできません。");
                _mutex.Close();
                _mutex = null;
                Application.Current.Shutdown();
                return;
            }

            try
            {
                var model = new MainWindowViewModel();

                var window = new MainWindow
                {
                    DataContext = model,
                };

                window.ContentRendered += (e2, args) =>
                {
                    model.Initalize();
                };

                window.Closed += (e2, args) =>
                {
                    model.Dispose();
                };

                window.Show();
            }
            catch
            {
                // MainWindow生成時の例外処理をhogehoge
                Application.Current.Shutdown();
                return;
            }
        }
    }
}
