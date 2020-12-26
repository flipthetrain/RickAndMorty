using Microsoft.AspNetCore.Mvc;
using System;

namespace RickAndMorty.API.NET5.Controllers.v1
{
    [ApiController]
    //ApiVersion attribute is required to report every ApiVersion that this controller supports
    //every action will be associated with every ApiVersion unless and explicit MapToApiVersion attribute is used on the action
    [ApiVersion("1.0")]
    //To deprecate an entire version set the Deprecated property to true
    [ApiVersion("1.1",Deprecated = true)]
    [ApiVersion("1.2")]
    [ApiVersion("1.3")]
    [Route("api/v{apiVersion:ApiVersion}/[controller]")]
    public class CharacterController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Create(model Model)
        {
            return Content("create");
        }

        [HttpGet]
        //MapToApiVersion must use a version from the controller ApiVersion attributes or else the VersionedApiExplorer will not register the Api version
        [MapToApiVersion("1.0")]
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
        public IActionResult Get_12(model Model)
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
