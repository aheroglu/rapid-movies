using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.ViewComponents.MainLayout
{
    public class _GenresComponent : ViewComponent
    {
        private readonly IMovieService _movieService;

        public _GenresComponent(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _movieService.Genres();
            genres = genres.OrderBy(x => x).ToList();
            return View(genres);
        }
    }
}