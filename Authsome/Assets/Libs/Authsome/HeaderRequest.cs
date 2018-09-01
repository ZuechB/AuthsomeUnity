using System;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Libs.Authsome
{
    public interface IHeaderRequest
    {
        IHeaderRequest IncludeHeader(string key, string value);
    }

    public class HeaderRequest : IHeaderRequest
    {
        public UnityWebRequest unityWebRequest;

        public HeaderRequest(UnityWebRequest unityWebRequest)
        {
            this.unityWebRequest = unityWebRequest;
        }

        public IHeaderRequest IncludeHeader(string name, string value)
        {
            unityWebRequest.SetRequestHeader(name, value);
            return this;
        }

        public IHeaderRequest IncludeBasicAuth(string username, string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(username + ":" + password);
            unityWebRequest.SetRequestHeader("Authorization", "Basic " + Convert.ToBase64String(byteArray));
            return this;
        }

        public IHeaderRequest IncludeBearerAuthentication(string token)
        {
            unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);
            return this;
        }

        public IHeaderRequest IncludeUserAgent(string value)
        {
            IncludeHeader("User-Agent", value);
            return this;
        }
    }
}
