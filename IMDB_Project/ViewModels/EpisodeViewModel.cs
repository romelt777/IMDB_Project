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
        public ICommand ToggleSortingCommand { get; }
        public EpisodeViewModel()
        {
            ToggleSortingCommand = new RelayCommand(ToggleSorting);
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
            var shows = context.Titles
                .Include(t => t.EpisodeParentTitles) 
                .Where(t => t.TitleType == "tvSeries")
                .Select(t => new TvShowWithEpisodeCount
                {
                    Title = t,
                    EpisodeCount = t.EpisodeParentTitles.Count
                })
                .OrderByDescending(s => s.EpisodeCount)
                .ToList();

            TvShows = new ObservableCollection<TvShowWithEpisodeCount>(shows);

            Console.WriteLine($"Found {shows.Count} shows.");
            foreach (var s in shows)
            {
                Console.WriteLine($"{s.Title.PrimaryTitle}: {s.EpisodeCount} episodes");
            }
        }
        public void ToggleSorting()
        {
            _isSortedDescending = !_isSortedDescending;
            UpdateSorting(TvShows.ToList());
        }

        private void UpdateSorting(List<TvShowWithEpisodeCount> shows)
        {
            if (_isSortedDescending)
            {
                TvShows = new ObservableCollection<TvShowWithEpisodeCount>(
                    shows.OrderByDescending(s => s.EpisodeCount));
            }
            else
            {
                TvShows = new ObservableCollection<TvShowWithEpisodeCount>(
                    shows.OrderBy(s => s.EpisodeCount));
            }
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
