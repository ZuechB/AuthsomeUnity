using Authsome.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Authsome.Portable.Extentions
{
    public class OAuth
    {
        public Provider Provider;

        public async Task RequestAuthorization(string username, string password, Action<TokenResponse> result = null)
        {
            if (Provider != null)
            {
                var scope = "";
                for (int i = 0; i < Provider.scope.Length; i++)
                {
                    if (i == 0)
                    {
                        scope += Provider.scope[i];
                    }
                    else
                    {
                        scope += " " + Provider.scope[i];
                    }
                }

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("client_id", Provider.clientId),
                    new KeyValuePair<string, string>("client_secret", Provider.secret),
                    new KeyValuePair<string, string>("grant_type", Provider.GrantType),
                    new KeyValuePair<string, string>("scope", scope)
                });

                await AuthsomeService.instance.PostAsync<TokenResponse>(Provider.TokenBearerUrl, content, (headerRequest) =>
                {
                    headerRequest.IncludeBasicAuth(Provider.clientId, Provider.secret);
                }, result: response =>
                {
                    if (response.httpStatusCode == HttpStatusCode.OK)
                    {
                        Provider.TokenResponse = response.Content;

                        StoreAccessToken(Provider.TokenResponse);

                        if (result != null)
                        {
                            result(Provider.TokenResponse);
                        }
                    }
                    else
                    {
                        if (result != null)
                        {
                            result(null);
                        }
                    }
                });
            }
        }

        private void StoreAccessToken(TokenResponse token)
        {
            if (!String.IsNullOrWhiteSpace(Provider.TokenResponse.access_token))
            {
                PlayerPrefs.SetString("authsome_tokenResponse", JsonConvert.SerializeObject(Provider.TokenResponse));
            }
        }


        // used for web on confirmation
        public async Task RequestBearerTokenAsync(string code, string grant_type = "authorization_code", string redirectUrl = "", Action<TokenResponse> result = null)
        {
            if (Provider != null)
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri",redirectUrl),
                    new KeyValuePair<string, string>("grant_type", grant_type)
                });

                await AuthsomeService.instance.PostAsync<TokenResponse>(Provider.TokenBearerUrl, content, (headerRequest) =>
                {
                    headerRequest.IncludeBasicAuth(Provider.clientId, Provider.secret);
                }, result: response =>
                {
                    if (response.httpStatusCode == HttpStatusCode.OK)
                    {
                        Provider.TokenResponse = response.Content;

                        if (result != null)
                        {
                            result(Provider.TokenResponse);
                        }
                    }
                    else
                    {
                        if (result != null)
                        {
                            result(null);
                        }
                    }
                });
            }
        }

        public async Task RefreshTheAccessTokenAsync(TokenResponse tokenResponse, Action<HttpResponseWrapper<TokenResponse>> result = null)
        {
            if (Provider != null)
            {
                var httpResponseWrapper = new HttpResponseWrapper<TokenResponse>();

                await AuthsomeService.instance.PostAsync<TokenResponse>(Provider.RefreshAccessTokenUrl, 
                    new StringContent("grant_type=refresh_token&refresh_token=" + tokenResponse.refresh_token, Encoding.UTF8, "application/x-www-form-urlencoded"),
                    (header) =>
                    {
                        header.IncludeBasicAuth(Provider.clientId, Provider.secret);
                    }, result: response =>
                    {
                        if (response.httpStatusCode == HttpStatusCode.OK)
                        {
                            var contentResponse = response.Content;
                            contentResponse.Id = tokenResponse.Id;

                            httpResponseWrapper.Content = contentResponse;

                            StoreAccessToken(contentResponse);
                        }
                        else
                        {
                            httpResponseWrapper.ErrorJson = response.ErrorJson;
                        }

                        httpResponseWrapper.httpStatusCode = response.httpStatusCode;

                        if (result != null)
                        {
                            result(httpResponseWrapper);
                        }
                    });
            }
            else
            {
                if (result != null)
                {
                    result(null);
                }
            }
        }

        //public async Task<bool> RevokeTokenAsync(TokenType tokenType, string token)
        //{
        //    if (Provider != null)
        //    {
        //        var tokenHintType = "refresh_token";
        //        if (tokenType == TokenType.AccessToken)
        //        {
        //            tokenHintType = "access_token";
        //        }

        //        var client = new HttpClient();
        //        client.SetBasicAuthentication(Provider.clientId, Provider.secret);
        //        var response = await client.PostAsJsonAsync(Provider.RevokeUrl, new
        //        {
        //            token_type_hint = tokenHintType,
        //            token = token
        //        });

        //        if (response.StatusCode == HttpStatusCode.OK)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}