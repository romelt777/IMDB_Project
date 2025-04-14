using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using IMDB_Project.Commands;
using IMDB_Project.Services;

namespace IMDB_Project.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private object _currentViewModel;

        //command to exit 
        private ICommand _exitCommand;

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // Set the current view model to the HomeViewModel
            //current view model is the view model that is currently being displayed
            CurrentViewModel = new HomeViewModel();
        }

        //pass in the view model to navigate to
        public ICommand NavigateToHomeCommand => new RelayCommand(_ => _navigationService.NavigateTo<HomeViewModel>());
        public ICommand NavigateToTitleCommand => new RelayCommand(_ => _navigationService.NavigateTo<TitleViewModel>());

        //command to navigate to other view models located on the sidebar
        public ICommand NavigateToGenreCommand => new RelayCommand(_ => _navigationService.NavigateTo<GenreViewModel>());
        public ICommand NavigateToEpisodeCommand => new RelayCommand(_ => _navigationService.NavigateTo<EpisodeViewModel>());
        public ICommand NavigateToCreatorCommand => new RelayCommand(_ => _navigationService.NavigateTo<CreatorViewModel>());
        public ICommand NavigateToRatingCommand => new RelayCommand(_ => _navigationService.NavigateTo<RatingViewModel>());



        //command to exit
        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new RelayCommand(ExecuteExitCommand));
            }
        }
        private void ExecuteExitCommand(object parameter)
        {
            Application.Current.Shutdown();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
