using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Data;
using IMDB_Project.Commands;
using Microsoft.EntityFrameworkCore;
using Models;

namespace IMDB_Project.ViewModels
{
    public class DirectorsViewModel : INotifyPropertyChanged
    {
        private ImdbContext _dbContext;
        private ObservableCollection<Name> _directors;

        //for search
        private string _searchQuery;
        private ObservableCollection<Name> _filteredDirectors;
        public DirectorsViewModel()
        {
            // Initialize commands
            NextPageCommand = new RelayCommand(_ => NextPage(), _ => CanNavigateNext);
            PreviousPageCommand = new RelayCommand(_ => PreviousPage(), _ => CanNavigatePrevious);

            // Initialize empty collections
            _directors = new ObservableCollection<Name>();
            _filteredDirectors = new ObservableCollection<Name>();

            UpdatePageDisplay();

        }

        // Method to set DbContext after creation
        public void SetDbContext(ImdbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ObservableCollection<Name> Directors
        {
            get { return _directors; }
            set
            {
                _directors = value;
                OnPropertyChanged(nameof(Directors));
                FilterDirectors();

                // Update pagination when directors change
                CalculateTotalPages();
                UpdatePageDisplay();
                OnPropertyChanged(nameof(CanNavigateNext));
                OnPropertyChanged(nameof(CanNavigatePrevious));
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
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                    UpdatePageDisplay();

                    // Update command can execute status
                    OnPropertyChanged(nameof(CanNavigateNext));
                    OnPropertyChanged(nameof(CanNavigatePrevious));
                }
            }
        }
        public int PageSize { get; set; } = 50;

        private int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set
            {
                if (_totalPages != value)
                {
                    _totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));
                    UpdatePageDisplay();

                    // Update command can execute status
                    OnPropertyChanged(nameof(CanNavigateNext));
                    OnPropertyChanged(nameof(CanNavigatePrevious));
                }
            }
        }

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

        private void UpdatePageDisplay()
        {
            CurrentPageDisplay = $"Page {CurrentPage} of {TotalPages}";
        }

        private void CalculateTotalPages()
        {
            if (_dbContext != null)
            {
                var totalCount = _dbContext.Names.Count();
                TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            }
            else
            {
                TotalPages = 1;
            }
        }

        public void LoadDirectorsPage()
        {
            CalculateTotalPages();


            var pageData = _dbContext.Names
                .OrderBy(n => n.PrimaryName)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            Directors = new ObservableCollection<Name>(pageData);
            UpdatePageDisplay();

        }

        public void NextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                LoadDirectorsPage();
            }
        }

        public void PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadDirectorsPage();
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
