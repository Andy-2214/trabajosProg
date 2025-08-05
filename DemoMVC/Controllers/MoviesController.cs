using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class MoviesController : Controller
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Superman", Category = "Acción" },
            new Movie { Id = 2, Title = "Jurassic World Rebirth", Category = "Acción" },
            new Movie { Id = 3, Title = "The Fantastic 4: First Steps", Category = "Acción" },
            new Movie { Id = 4, Title = "I Know What You Did Last Summer", Category = "Terror" },
            new Movie { Id = 5, Title = "The Ritual", Category = "Terror" },
            new Movie { Id = 6, Title = "Bambi: The Reckoning", Category = "Terror" },
            new Movie { Id = 7, Title = "Materialists", Category = "Romance" },
            new Movie { Id = 8, Title = "Locos de amor, mi primer amor", Category = "Romance" }
        };
        public IActionResult Index()
        {
            var categories = movies.Select(m => m.Category).Distinct().ToList();
            return View(categories); 
        }
        public IActionResult Category(string category)
        {
        Console.WriteLine($"[DEBUG] Categoria recibida: '{category}'");

        var filtered = movies
        .Where(m => string.Equals(
            m.Category?.Trim(), 
            category?.Trim(), 
            StringComparison.OrdinalIgnoreCase))
        .ToList();

        Console.WriteLine($"[DEBUG] Peliculas encontradas: {filtered.Count}");

        ViewBag.Category = category?.Trim();
        return View(filtered);
        }
        [HttpGet]
        public IActionResult Suggest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Suggest(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title) || string.IsNullOrEmpty(movie.Category))
            {
                ViewBag.Error = "Por favor complete todos los campos.";
                return View(movie);
            }

            return RedirectToAction("Thanks");
        }
        public IActionResult Thanks()
        {
            return View();
        }
    }
}