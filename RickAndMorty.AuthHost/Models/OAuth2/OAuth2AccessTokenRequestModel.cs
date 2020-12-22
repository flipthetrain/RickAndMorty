using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc6749#section-4.1.3
    /// </summary>
    public class OAuth2AccessTokenRequestModel
    {
        [Required]
        public string grant_type { get; set; }
        [Required]
        public string code { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-4.1.1
        /// </summary>
        [Required]
        [Url]
        public string redirect_uri { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-3.2.1
        /// </summary>
        [Required]
        public string client_id { get; set; }
    }
}
