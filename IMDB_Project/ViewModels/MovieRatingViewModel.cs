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
    public class MovieRatingViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Rating> _movieRatings;

        //for search
        private string _searchQuery;
        private ObservableCollection<Rating> _filteredRatings;


        public ObservableCollection<Rating> MovieRatings
        {
            get { return _movieRatings; }
            set
            {
                _movieRatings = value;
                OnPropertyChanged(nameof(MovieRatings));
                FilterRatings();
            }
        }

        public ObservableCollection<Rating> FilteredRatings
        {
            get { return _filteredRatings; }
            private set
            {
                _filteredRatings = value;
                OnPropertyChanged(nameof(FilteredRatings));
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
                    FilterRatings();
                }
            }
        }



        //Filtering Title after search
        private void FilterRatings()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredRatings = new ObservableCollection<Rating>(MovieRatings);
            }
            else
            {
                FilteredRatings = new ObservableCollection<Rating>(
                    MovieRatings.Where(a => a.Title.PrimaryTitle.ToLower().Contains(SearchQuery.ToLower()))
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
