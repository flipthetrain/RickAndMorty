using System.ComponentModel.DataAnnotations;

namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc6749#section-4.1.4
    /// https://tools.ietf.org/html/rfc6749#section-5.1
    /// </summary>
    public class OAuth2AccessTokenResponseModel
    {
        [Required]
        public string access_token { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-7.1
        /// </summary>
        [Required]
        public string token_type { get; set; }
        [ExpiresInValidation]
        public string expires_in { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-6
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// https://tools.ietf.org/html/rfc6749#section-3.3
        /// </summary>
        public string scope { get; set; }
    }

    public class ExpiresInValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string expiresInString = (string)validationContext.ObjectInstance;
            int expiresInInt=0;
            if (int.TryParse(expiresInString,out expiresInInt)
                || expiresInInt<0)
            {
                return new ValidationResult($"Expires in value must be an integer >= 0 : {expiresInString}.");
            }
            return ValidationResult.Success;
        }
    }

}
