using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc6749#section-4.1.2.1
    /// </summary>
    public class OAuth2ErrorModel
    {
        /// <summary>
        /// oneof: invalid_request, unauthorized_client, access_denied, unsupported_response_type, invalid_scope
        /// </summary>
        private OAuth2AuthResponseErrorEnum _error;
        [ErrorValidation]
        public string error
        {
            get
            {
                return Enum.GetName(typeof(OAuth2AuthResponseErrorEnum), _error);
            }
            set
            {
                _error=Enum.Parse<OAuth2AuthResponseErrorEnum>(value);
            }
        }
        public string error_description { get; set; }
        [Url]
        public string error_uri { get; set; }
        public string state { get; set; }
    }

    public class ErrorValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = (string)validationContext.ObjectInstance;
            if (!Enum.GetNames(typeof(OAuth2AuthResponseErrorEnum)).Any(n=>n.Equals(errorMessage, StringComparison.Ordinal)))
            {
                return new ValidationResult($"Invalid OAuth2 error {errorMessage}.");
            }
            return ValidationResult.Success;
        }
    }
}
