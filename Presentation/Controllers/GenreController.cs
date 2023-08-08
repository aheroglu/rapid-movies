using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Abstract;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GenreController : Controller
    {
        private readonly IMovieService _movieService;

        public GenreController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index(string genre)
        {
            var movies = await _movieService.MoviesByGenre(genre);
            ViewBag.Genre = genre;
            return View(movies);
        }
    }
}
