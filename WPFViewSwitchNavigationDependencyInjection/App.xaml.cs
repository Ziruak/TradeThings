using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.View;
using WPFViewSwitchNavigationDependencyInjection.MVVM.ViewModel;
using WPFViewSwitchNavigationDependencyInjection.Services;

namespace WPFViewSwitchNavigationDependencyInjection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<GridViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<TradeViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<GridService>();
            services.AddSingleton<PlayerService>();
            services.AddSingleton<CitiesService>();

            services.AddSingleton<Func<Type, ViewModel>>(
                serviceProvider => 
                viewModelType =>
                (ViewModel)serviceProvider.GetRequiredService(viewModelType)
                );

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
