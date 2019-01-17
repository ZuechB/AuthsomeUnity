using UnityEngine;

namespace Authsome
{
    [System.Serializable]
    public class Provider
    {
        public string clientId;
        public string secret;
        public string redirectUrl;
        public string state;
        public string authorizationUrl; // always like this = client_id={clientId}&response_type={response_type}&scope={scope}&redirect_uri={redirectUrl}&state={state}
        public string[] scope;
        public string response_type;
        public string RefreshAccessTokenUrl;
        public string TokenBearerUrl; // often oauth2/token
        public string APIBaseUrl; // for api calling (not authentication)
        public string RevokeUrl; // Revokes the token
        public string GrantType;  // change this to an enum later!


        public TokenResponse TokenResponse { get; set; }
    }
}
