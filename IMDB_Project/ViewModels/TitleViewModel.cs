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
    public class TitleViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Title> _titles;

        //for search
        private string _searchQuery;
        private ObservableCollection<Title> _filteredTitles;



        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set
            {
                _titles = value;
                OnPropertyChanged(nameof(Titles));
                FilterTitles();
            }
        }

        public ObservableCollection<Title> FilteredTitles
        {
            get { return _filteredTitles; }
            private set
            {
                _filteredTitles = value;
                OnPropertyChanged(nameof(FilteredTitles));
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
                    FilterTitles();
                }
            }
        }



        //Filtering Title after search
        private void FilterTitles()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredTitles = new ObservableCollection<Title>(Titles);
            }
            else
            {
                FilteredTitles = new ObservableCollection<Title>(
                    Titles.Where(a => a.PrimaryTitle.ToLower().Contains(SearchQuery.ToLower()))
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
