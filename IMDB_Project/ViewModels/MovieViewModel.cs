using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IMDB_Project.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Title> _movies;

        //for search
        //private string _searchQuery;
        private ObservableCollection<Title> _filteredMovies;

        public MovieViewModel()
        {
            _movies = new ObservableCollection<Title>();
            _filteredMovies = new ObservableCollection<Title>();
        }

        public ObservableCollection<Title> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        public ObservableCollection<Title> FilteredMovies
        {
            get { return _filteredMovies; }
            set
            {
                _filteredMovies = value;
                OnPropertyChanged(nameof(FilteredMovies));
            }
        }

        //private void FilterMoviesByGenre(string genre)
        //{
        //    FilteredMovies = Movies.Where()
        //}



        //public string SearchQuery
        //{
        //    get { return _searchQuery; }
        //    set
        //    {
        //        if (_searchQuery != value)
        //        {
        //            _searchQuery = value;
        //            OnPropertyChanged(nameof(SearchQuery));
        //            FilterMovies();
        //        }
        //    }
        //}

        //Filtering genre after search
        //private void FilterMovies()
        //{
        //    if (string.IsNullOrEmpty(SearchQuery))
        //    {
        //        FilteredMovies = new ObservableCollection<Title>(Movies);
        //    }
        //    else
        //    {
        //        FilteredMovies = new ObservableCollection<Title>(
        //            Movies.Where(a => a.PrimaryTitle.ToLower().Contains(SearchQuery.ToLower()))
        //        );
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
