using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IMDB_Project.ViewModels
{
    public class MovieGenreViewModel : INotifyPropertyChanged
    {
        private MovieViewModel _movieViewModel;
        private GenreViewModel _genreViewModel;
        public TitleGenreViewModel _titleGenreViewModel;
        private Genre _selectedGenre;
        private Title _selectedMovie;
        public GenreViewModel GenreViewModel { get { return _genreViewModel; } set {_genreViewModel = value;}}
        public MovieViewModel MovieViewModel { get { return _movieViewModel; } set { _movieViewModel = value; } }
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                Debug.WriteLine("SelectedGenre SET fired");
                _selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
                FilterMoviesByGenre(SelectedGenre);
            }
        }
        public Title SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                Debug.WriteLine("SelectedMovie SET fired");
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }        

        public void FilterMoviesByGenre(Genre genre)
        {
            if (genre == null)
            {
                Debug.WriteLine("FilterMoviesByGenre called with null genre.");
                return;
            }

            Debug.WriteLine($"Filtering movies for genre: {genre.Name} ({genre.GenreId})");

            // Get all title-genre mappings for the selected genre
            var genreMatches = _titleGenreViewModel.TitlesAndGenres
                .Where(tg => tg.GenreId == genre.GenreId);

            // Join movies directly to title-genre matches on TitleId
            var filtered = (from movie in _movieViewModel.Movies
                            join tg in genreMatches
                            on movie.TitleId equals tg.TitleId
                            select movie)
                           .Take(100) // Optional limit
                           .ToList();

            Debug.WriteLine($"Filtered down to {filtered.Count} movies");

            _movieViewModel.FilteredMovies = new ObservableCollection<Title>(filtered);
        }


        //public void FilterMoviesByGenre(Genre genre)
        //{
        //    Debug.WriteLine($"Filtering movies for genre: {genre?.Name} ({genre?.GenreId})");
        //    var titlesByGenre = _titleGenreViewModel.TitlesAndGenres
        //        .Where(t => t.GenreId == genre.GenreId)
        //        .Select(t => t.TitleId)
        //        .ToList();

        //    Debug.WriteLine(_movieViewModel.Movies.Count());

        //    var filtered = _movieViewModel.Movies
        //        .Where(m => titlesByGenre.Contains(m.TitleId))
        //        .Take(100)
        //        .ToList();

        //    Debug.WriteLine($"Found {filtered.Count} matching movies");

        //    // Replace the entire collection to avoid performance hit
        //    _movieViewModel.FilteredMovies = new ObservableCollection<Title>(filtered);
        //}


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

