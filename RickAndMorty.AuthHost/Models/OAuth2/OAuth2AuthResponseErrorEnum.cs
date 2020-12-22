namespace RickAndMorty.AuthHost.Models.OAuth2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc6749#section-4.1.2.1
    /// </summary>
    public enum OAuth2AuthResponseErrorEnum
    {
        invalid_request=0,
        unauthorized_client,
        access_denied,
        unsupported_response_type,
        invalid_scope,
        server_error,
        temporarily_unavailable
    }
}
