using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Abstract;
using System.Threading.Tasks;

namespace Presentation.ViewComponents.Home
{
    public class _CatalogComponent : ViewComponent
    {
        private readonly IMovieService _movieService;

        public _CatalogComponent(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var movies = await _movieService.GetMovies();
            return View(movies);
        }
    }
}
