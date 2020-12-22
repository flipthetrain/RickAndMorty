using Microsoft.AspNetCore.Mvc;
using System;

namespace RickAndMorty.API.NETCore.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion:ApiVersion}/[controller]")]
    public class CharacterController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Create(model Model)
        {
            return Content("create");
        }

        [HttpGet]
        [Obsolete]
        public IActionResult Get()
        {
            return Content("get");
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [Obsolete]
        public IActionResult Get_11()
        {
            return Content("get 11");
        }

        [HttpGet]
        [MapToApiVersion("1.2")]
        public IActionResult Get_12()
        {
            return Content("get 12");
        }

        [HttpGet]
        [MapToApiVersion("1.3")]
        public IActionResult Get_13()
        {
            return Content("get 13");
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

        public class model
        {
            public string name { get; set; }
            public string desc { get; set; }
        }
    }
}
