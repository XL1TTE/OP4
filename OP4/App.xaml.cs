using Microsoft.Extensions.DependencyInjection;
using OP4.Core;
using OP4.MVVM.Model;
using OP4.MVVM.View;
using OP4.MVVM.ViewModel;
using OP4.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OP4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>()
            });;

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<DataDictionaryViewModel>();
            services.AddSingleton<TextGeneratorViewModel>();


            services.AddSingleton<InavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(provider => ViewModelType => (ViewModelBase)provider.GetRequiredService(ViewModelType));

            services.AddSingleton<TextGenerator>();

            _serviceProvider =  services.BuildServiceProvider();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
