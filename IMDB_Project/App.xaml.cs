using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using IMDB_Project.Services;
using IMDB_Project.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Data;
using System.DirectoryServices;
using Models;
using System.Collections.ObjectModel;

namespace IMDB_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);


            ServiceProvider = serviceCollection.BuildServiceProvider();

            //load data from db
            LoadData();

            //instantiating the main window and navigation service
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;
            navigationService.SetMainViewModel(mainViewModel);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection serviceCollection)
        {
            //chinookContext to hide string
            var connectionString = ConfigurationManager.ConnectionStrings["ImdbConn"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("ERROR: Connection string is NULL in App.xaml.cs! Check App.config.");
            }


            //instantiating the viewmodels and other classes and adding them to service collection
            //making instant of each class and storing in collection
            serviceCollection.AddDbContext<ImdbContext>(options =>
                options.UseSqlServer(connectionString));



            // Register your services and view models here
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<MainWindow>();
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<HomeViewModel>();
            serviceCollection.AddSingleton<TitleViewModel>();
        }

        private void LoadData()
        {
            //load all data from db, into view model collections
            using (var scope = ServiceProvider.CreateScope())
            {
                //get instance of imbdcontext to variable
                var dbContext = scope.ServiceProvider.GetRequiredService<ImdbContext>();

                //get instance of viewmodel classes
                var titleViewModel = scope.ServiceProvider.GetRequiredService<TitleViewModel>();

                //load data from database into viewmodel collections
                titleViewModel.Titles = new ObservableCollection<Title>(dbContext.Titles.ToList());


            }

        }
    }

}
