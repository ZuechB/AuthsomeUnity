using Authsome.Models;
using Authsome.Portable.Builder;
using Authsome.Portable.Extentions;
using Authsome.Portable.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Authsome
{
    public class AuthsomeService : MonoBehaviour
    {
        public static AuthsomeService instance;

        private Action<HttpResponseWrapper<TokenResponse>> RefreshedToken;

        public OAuth oAuth { get; set; }
        public Provider Provider;

        private void Start()
        {
            instance = this;

            oAuth = new OAuth();
            oAuth.Provider = Provider;
        }

        public string BaseUrl;

        public void InitGlobalRefreshToken(Action<HttpResponseWrapper<TokenResponse>> RefreshedToken = null)
        {
            this.RefreshedToken = RefreshedToken;
        }

        public async Task GetAsync<T>(string url, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Get, url, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PostAsync<T>(string url, object body, string mediaType = "application/json", Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            HttpContent bodyContent = null;
            if (body != null)
            {
                bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, mediaType);
            }
            await factory.Request<T>(HttpOption.Post, url, bodyContent, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PostAsync<T>(string url, object body, MediaType mediaType, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            HttpContent bodyContent = null;
            if (body != null)
            {
                bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, mediaType.GetMediaType());
            }
            await factory.Request<T>(HttpOption.Post, url, bodyContent, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PostAsync<T>(string url, FormUrlEncodedContent content = null, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Post, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PostAsync<T>(string url, StringContent content = null, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Post, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PostAsync<T>(string url, MultipartFormDataContent content, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Post, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PutAsync<T>(string url, FormUrlEncodedContent content = null, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Put, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PutAsync<T>(string url, object body, string mediaType = "application/json", Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            HttpContent bodyContent = null;
            if (body != null)
            {
                bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, mediaType);
            }
            await factory.Request<T>(HttpOption.Put, url, bodyContent, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PutAsync<T>(string url, object body, MediaType mediaType, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            HttpContent bodyContent = null;
            if (body != null)
            {
                bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, mediaType.GetMediaType());
            }
            await factory.Request<T>(HttpOption.Put, url, bodyContent, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PutAsync<T>(string url, StringContent content = null, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Put, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        public async Task PutAsync<T>(string url, MultipartFormDataContent content, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Put, url, content, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }


        public async Task DeleteAsync<T>(string url, Action<IHeaderRequest> HeaderBuilder = null, Action<HttpResponseWrapper<T>> result = null)
        {
            if (!IsAbsolute(url))
            {
                url = BaseUrl + url;
            }

            var factory = new RequestFactory();
            await factory.Request<T>(HttpOption.Delete, url, oAuth: oAuth, HeaderBuilder: HeaderBuilder, RefreshedToken: RefreshedToken, result: result);
        }

        private bool IsAbsolute(string url)
        {
            if (url.Contains("http://") || url.Contains("https://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}