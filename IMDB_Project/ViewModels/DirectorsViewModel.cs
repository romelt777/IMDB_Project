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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
