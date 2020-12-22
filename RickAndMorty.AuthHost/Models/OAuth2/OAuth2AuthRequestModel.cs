using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// OAuth2 Request Model
    /// https://tools.ietf.org/html/rfc6749#section-4.1.1
    /// </summary>
    public class OAuth2AuthRequestModel
    {
        [Required]
        public string response_type { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-2.2
        /// </summary>
        [Required]
        public string client_id { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-2.2
        /// </summary>
        public string client_secret { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-2.3.1
        /// </summary>
        [Required]
        [Url]
        public string redirect_uri { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-3.3
        /// </summary>
        public string scope { get; set; }
        public string state { get; set; }
    }
}
