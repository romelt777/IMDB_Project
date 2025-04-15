using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace IMDB_Project.ViewModels
{
    public class DirectorsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Name> _directors;

        //for search
        private string _searchQuery;
        private ObservableCollection<Name> _filteredDirectors;



        public ObservableCollection<Name> Directors
        {
            get { return _directors; }
            set
            {
                _directors = value;
                OnPropertyChanged(nameof(Directors));
                FilterDirectors();
            }
        }

        public ObservableCollection<Name> FilteredDirectors
        {
            get { return _filteredDirectors; }
            private set
            {
                _filteredDirectors = value;
                OnPropertyChanged(nameof(FilteredDirectors));
            }
        }

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    FilterDirectors();
                }
            }
        }



        //Filtering director after search
        private void FilterDirectors()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredDirectors = new ObservableCollection<Name>(Directors);
            }
            else
            {
                FilteredDirectors = new ObservableCollection<Name>(
                    Directors.Where(a => a.PrimaryName.ToLower().Contains(SearchQuery.ToLower()))
                );
            }
        }


        //PAGINATION
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 50; 
        public int TotalPages { get; set; }
        private string _currentPageDisplay;
        public string CurrentPageDisplay
        {
            get => _currentPageDisplay;
            set
            {
                _currentPageDisplay = value;
                OnPropertyChanged(nameof(CurrentPageDisplay));
            }
        }

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        public void LoadDirectorsPage(ImdbContext dbContext)
        {
            var totalCount = dbContext.Names.Count();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var pageData = dbContext.Names
                .OrderBy(n => n.PrimaryName)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            Directors = new ObservableCollection<Name>(pageData);
        }

        public void NextPage(ImdbContext dbContext)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                LoadDirectorsPage(dbContext);
            }
        }

        public void PreviousPage(ImdbContext dbContext)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadDirectorsPage(dbContext);
            }
        }

        public bool CanNavigateNext => CurrentPage < TotalPages;
        public bool CanNavigatePrevious => CurrentPage > 1;


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
