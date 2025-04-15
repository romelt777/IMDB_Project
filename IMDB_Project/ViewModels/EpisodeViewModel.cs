using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IMDB_Project.Commands;

namespace IMDB_Project.ViewModels
{
    //placeholder for episode view model
    public partial class EpisodeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TvShowWithEpisodeCount> _tvShows;
        private bool _isSortedDescending = true;
        //number of items to load at once
        private readonly int _pageSize = 10;
        //cache for all pages
        private List<TvShowWithEpisodeCount> _allShows;


        public ICommand ToggleSortingCommand { get; }
        public ICommand LoadMoreCommand { get; }


        public EpisodeViewModel()
        {
            TvShows = new ObservableCollection<TvShowWithEpisodeCount>();
            ToggleSortingCommand = new RelayCommand(ToggleSorting, CanToggleSorting);
            LoadMoreCommand = new RelayCommand(LoadMore);

        }

        public ObservableCollection<TvShowWithEpisodeCount> TvShows
        {
            get { return _tvShows; }
            set
            {
                _tvShows = value;
                OnPropertyChanged(nameof(TvShows));
            }
        }

        public void LoadTvShowsWithEpisodes(ImdbContext context)
        {
            _allShows = context.Titles
                .Include(t => t.EpisodeParentTitles)
                .Where(t => t.TitleType == "tvSeries")
                .Select(t => new TvShowWithEpisodeCount
                {
                    Title = t,
                    EpisodeCount = t.EpisodeParentTitles.Count
                })
                .OrderByDescending(s => s.EpisodeCount)
                .ToList();

            LoadMore(0);

            Console.WriteLine($"Found {_allShows.Count} shows in total.");
        }


        public void LoadMore(object parameter = null)
        {
            if (_allShows == null || TvShows.Count >= _allShows.Count)
                return;
            var currentCount = TvShows.Count;
            var remainingItems = _allShows.Skip(currentCount).Take(_pageSize);

            foreach (var item in remainingItems)
            {
                TvShows.Add(item);
            }
        }

        private bool CanToggleSorting(object parameter)
        {
            return TvShows != null && TvShows.Any();
        }
        public void ToggleSorting()
        {
            if (TvShows == null || !TvShows.Any())
                return;
            _isSortedDescending = !_isSortedDescending;
            UpdateSorting();

        }

        private void UpdateSorting()
        {
            _allShows = _isSortedDescending
                ? _allShows.OrderByDescending(s => s.EpisodeCount).ToList()
                  : _allShows.OrderBy(s => s.EpisodeCount).ToList();

             
                TvShows.Clear();
                LoadMore();
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class TvShowWithEpisodeCount
    {
        public Title Title { get; set; }
        public int EpisodeCount { get; set; }
    }
}
