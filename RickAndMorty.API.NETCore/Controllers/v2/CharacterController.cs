using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty.API.NETCore.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{apiVersion:ApiVersion}/[controller]")]
    public class CharacterController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Create()
        {
            return Content("create");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Content("get");
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            return Content("list");
        }

        [HttpDelete()]
        public IActionResult Delete()
        {
            return Content("delete");
        }
    }
}
