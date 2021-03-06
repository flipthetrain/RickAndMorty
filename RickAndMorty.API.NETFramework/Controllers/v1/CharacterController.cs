﻿using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RickAndMorty.API.NetFramework.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [ApiVersion("1.3")]
    [RoutePrefix("api/v{apiVersion:ApiVersion}/character")]
    public class CharacterController : ApiController
    {
        [HttpPost()]
        [Route("")]
        public string Create([FromBody] model Model)
        {
            return ("create");
        }

        [HttpGet]
        [Route("")]
        [Obsolete]
        [MapToApiVersion("1.0")]
        public string Get()
        {
            return ("get");
        }

        [HttpGet]
        [Route("")]
        [Obsolete]
        [MapToApiVersion("1.1")]
        public string Get_11()
        {
            return ("get 11");
        }

        [HttpGet]
        [Route("")]
        [MapToApiVersion("1.2")]
        public string Get_12()
        {
            return ("get 12");
        }

        [HttpGet]
        [Route("")]
        [MapToApiVersion("1.3")]
        public string Get_13()
        {
            return ("get 13");
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

        public class model
        {
            public string name { get; set; }
            public string desc { get; set; }
        }
    }
}
