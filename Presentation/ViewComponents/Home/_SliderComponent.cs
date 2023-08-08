using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Abstract;
using System.Threading.Tasks;

namespace Presentation.ViewComponents.Home
{
    public class _SliderComponent : ViewComponent
    {
        private readonly IMovieService _movieService;

        public _SliderComponent(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var movies = await _movieService.TopEightMovies();
            return View(movies);
        }
    }
}
