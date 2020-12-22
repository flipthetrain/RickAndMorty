using Microsoft.Web.Http;
using System.Web.Http;

namespace RickAndMorty.API.NetFramework.Controllers.v2
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/v{apiVersion:ApiVersion}/[controller]")]
    public class CharacterController : ApiController
    {
        [HttpPost()]
        [Route("")]
        public string Create()
        {
            return ("create");
        }

        [HttpGet]
        [Route("")]
        public string Get()
        {
            return ("get");
        }

        [HttpGet]
        [Route("list")]
        public string List()
        {
            return ("list");
        }

        [HttpDelete()]
        [Route("")]
        public string Delete()
        {
            return ("delete");
        }
    }
}
