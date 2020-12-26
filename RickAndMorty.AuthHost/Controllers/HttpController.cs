using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty.Auth.Host.Controllers
{
    public class HttpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
