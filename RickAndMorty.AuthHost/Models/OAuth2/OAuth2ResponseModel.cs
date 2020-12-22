using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc6749#section-4.1.2
    /// </summary>
    public class OAuth2ResponseModel
    {
        [Required]
        public string code { get; set; }
        [Required]
        public string state { get; set; }
    }
}
