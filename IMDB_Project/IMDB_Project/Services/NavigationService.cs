using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB_Project.ViewModels;

namespace IMDB_Project.Services
{
    //navigation service is service available across entire app, 
    //given a viewmodel will navigate to that viewmodel
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : class;
        void GoBack();
    }
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Stack<object> _navigationStack = new Stack<object>();
        private MainViewModel _mainViewModel;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //giving the mainviewmodel to the navigation service
        public void SetMainViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            var viewModel = _serviceProvider.GetService(typeof(TViewModel)) as TViewModel;
            if (viewModel != null)
            {
                _navigationStack.Push(viewModel);
                // Logic to update the current view with the new ViewModel
                //this will change the view model.
                _mainViewModel.CurrentViewModel = viewModel;
            }
        }

        public void GoBack()
        {
            if (_navigationStack.Count > 1)
            {
                _navigationStack.Pop();
                var viewModel = _navigationStack.Peek();
                // Logic to update the current view with the previous ViewModel
            }
        }
    }
}
