using System.Collections.ObjectModel;
using IMDB_Project.Models.Generated;

namespace IMDB_Project.ViewModels
{
    public class TitleGenreViewModel
    {
        private ObservableCollection<TitleGenre> _titlesAndGenres;    

        public ObservableCollection<TitleGenre> TitlesAndGenres
        {
            get { return _titlesAndGenres; }
            set
            {
                _titlesAndGenres = value;
            }
        }

        
    }
}
