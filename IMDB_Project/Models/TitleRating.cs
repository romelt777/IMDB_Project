using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_Project.Models
{
    public class TitleRating
    {
        public int TitleId { get; set; }
        public string NumVotes { get; set; }
        public int RatingId { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }
}
