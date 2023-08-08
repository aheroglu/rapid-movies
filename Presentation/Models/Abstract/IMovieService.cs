using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models.Concrete;

namespace Presentation.Models.Abstract
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<List<Movie>> TopEightMovies();
        Task<List<string>> Genres();
        Task<IEnumerable<Movie>> MoviesByGenre(string genre);
    }
}
