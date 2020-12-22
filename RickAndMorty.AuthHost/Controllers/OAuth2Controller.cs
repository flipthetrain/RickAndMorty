using Microsoft.AspNetCore.Mvc;
using RickAndMorty.AuthHost.Models.OAuth2;
using System.IO;
using System.Threading.Tasks;

namespace RickAndMorty.AuthHost.Controllers
{
    [Route("/oauth2")]
    public class OAuth2Controller : Controller
    {
        [HttpGet("auth")]
        public async Task<IActionResult> Auth(OAuth2AuthRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("request is incorrect");
            }

            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();

                // Do something
            };

            return View();
        }

    }
}
