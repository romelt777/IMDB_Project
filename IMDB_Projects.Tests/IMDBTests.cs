using IMDB_Project.ViewModels;
using IMDB_Project.Models;
using Data;
using System.Diagnostics;
using Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using IMDB_Project.Models.Generated;

namespace IMDB_Projects.Tests
{
    [TestClass]
    public sealed class IMDBTests
    {
        //private DirectorsViewModel _directorVM;
        private GenreViewModel _genreVM;
        private MovieGenreViewModel _movieGenreVM;
        private MovieRatingViewModel _movieRatingVM;
        private MovieViewModel _movieVM;
        private TitleGenreViewModel _titleGenreVM;
        private TitleViewModel _titleVM;
        private ImdbContext _context;

        [STATestMethod]
        [TestInitialize]
        public void Test_Setup()
        {
            _movieVM = new MovieViewModel();
            _titleVM = new TitleViewModel();
            //_directorVM = new DirectorsViewModel();
            _genreVM = new GenreViewModel();
            _movieGenreVM = new MovieGenreViewModel();
            _movieRatingVM = new MovieRatingViewModel();
            _titleGenreVM = new TitleGenreViewModel();
            _titleVM = new TitleViewModel();
            _context = new ImdbContext();
            _movieGenreVM.GenreViewModel = _genreVM;
            _movieGenreVM.MovieViewModel = _movieVM;
            _movieGenreVM._titleGenreViewModel = _titleGenreVM;
        }

#region Filter by search methods
        [STATestMethod]
        public void FilterGenres_ValidSearch_ExpectedResult()
        {
            _genreVM.Genres = new ObservableCollection<Genre>(_context.Genres);
            Debug.WriteLine(_context.Genres.ToList().Count());
            _genreVM.SearchQuery = "e";
            var numOfFilteredGenres = _genreVM.FilteredGenres.Count();
            var count = 0;

            foreach (var genre in _genreVM.Genres)
            {
                if (genre.Name.ToLower().Contains(_genreVM.SearchQuery.ToLower()))
                {
                    count++;
                }
            }
            Debug.WriteLine(count);
            Assert.AreEqual(numOfFilteredGenres, count);
        }

        [STATestMethod]
        public void FilterTitle_ValidSearch_ExpectedResult()
        {
            _titleVM.Titles = new ObservableCollection<Title>(_context.Titles);
            _titleVM.SearchQuery = "am";
            var numOfFilteredTitles = _titleVM.FilteredTitles.Count();
            var count = 0;

            foreach (var title in _titleVM.Titles)
            {
                if (title.PrimaryTitle.ToLower().Contains(_titleVM.SearchQuery.ToLower()))
                {
                    count++;
                }
            }
            Debug.WriteLine(count);
            Assert.AreEqual(numOfFilteredTitles, count);
        }

        [STATestMethod]
        public void FilterMovieRatings_ValidSearch_ExpectedResult()
        {
            _movieRatingVM.MovieRatings = new ObservableCollection<Rating>(_context.Ratings.Include(r => r.Title));
            _movieRatingVM.SearchQuery = "am";
            var numOfFilteredRatings = _movieRatingVM.FilteredRatings.Count();
            var count = 0;

            foreach (var rating in _movieRatingVM.MovieRatings)
            {
                if (rating.Title.PrimaryTitle.ToLower().Contains(_movieRatingVM.SearchQuery.ToLower()))
                {
                    count++;
                }
            }
            Debug.WriteLine(count);
            Assert.AreEqual(numOfFilteredRatings, count);
        }
        #endregion

        [STATestMethod]
        public void FilterMovieByGenre_SelectedGenreFromList_ExpectedResult()
        {
            _genreVM.Genres = new ObservableCollection<Genre>(_context.Genres);
            _movieVM.Movies = new ObservableCollection<Title>(_context.Titles.Where(t => t.TitleType == "movie"));
            _titleGenreVM.TitlesAndGenres = new ObservableCollection<TitleGenre>(_context.TitleGenres);

            var validGenre = new Genre
            {
                GenreId = 2,
                Name = "Documentary"
            };

            _movieGenreVM.SelectedGenre = validGenre;
            var genreMatches = _titleGenreVM.TitlesAndGenres
                .Where(tg => tg.GenreId == validGenre.GenreId);

            var filteredMoviesLength = _movieVM.FilteredMovies.Count();
            var count = 0;

            foreach (var movie in _movieVM.FilteredMovies)
            {
                if (genreMatches.Any(g => g.TitleId == movie.TitleId))
                {
                    count++;
                }
            }
            Debug.WriteLine(count);
            Assert.AreEqual(filteredMoviesLength, count);
        }
    }
}
