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
    public class GenreViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Genre> _genres;

        //for search
        private string _searchQuery;
        private ObservableCollection<Genre> _filteredGenres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
                FilterGenres();
            }
        }

        public ObservableCollection<Genre> FilteredGenres
        {
            get { return _filteredGenres; }
            private set
            {
                _filteredGenres = value;
                OnPropertyChanged(nameof(FilteredGenres));
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
                    FilterGenres();
                }
            }
        }

        //Filtering genre after search
        private void FilterGenres()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredGenres = new ObservableCollection<Genre>(Genres);
            }
            else
            {
                FilteredGenres = new ObservableCollection<Genre>(
                    Genres.Where(a => a.Name.ToLower().Contains(SearchQuery.ToLower()))
                );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
