using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Libs.Authsome
{
    public class AuthsomeService : MonoBehaviour
    {
        public static AuthsomeService instance;

        public string BaseUrl = "";

        private void Start()
        {
            instance = this;
        }

        public void Post<T>(string url, object obj, Action<HttpResponseWrapper<T>> result = null)
        {
            StartCoroutine(SendRequest<T>(BaseUrl + url, "POST", obj, result));
        }

        public void Get<T>(string url, Action<HttpResponseWrapper<T>> result = null)
        {
            StartCoroutine(SendRequest<T>(BaseUrl + url, "GET", null, result));
        }

        public void Delete<T>(string url, Action<HttpResponseWrapper<T>> result = null)
        {
            StartCoroutine(SendRequest<T>(BaseUrl + url, "DELETE", null, result));
        }

        public void Put<T>(string url, object obj, Action<HttpResponseWrapper<T>> result = null)
        {
            StartCoroutine(SendRequest<T>(BaseUrl + url, "PUT", obj, result));
        }


        IEnumerator SendRequest<T>(string url, string method, object obj, Action<HttpResponseWrapper<T>> result = null)
        {
            var request = new UnityWebRequest(url, method);

            if (obj != null)
            {
                var body = JsonUtility.ToJson(obj);
                byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(body);
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            var httpStatusCode = (HttpStatusCode)request.responseCode;

            if (!request.isHttpError)
            {
                if (request.responseCode == (long)HttpStatusCode.OK)
                {
                    var returnObject = request.downloadHandler.text;

                    if (result != null)
                    {
                        result(new HttpResponseWrapper<T>()
                        {
                            Content = JsonUtility.FromJson<T>(returnObject),
                            httpStatusCode = httpStatusCode
                        });
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}
































//if (request.isHttpError)
//{
//    //Debug.Log("Something went wrong, and returned error: " + request.error);
//    requestErrorOccurred = true;
//}
//else
//{
//    if (request.responseCode == 200)
//    {
//        var result = JsonUtility.FromJson<LoginResult>(request.downloadHandler.text);
//        Debug.Log("Welcome : " + result.firstName + " " + result.lastName);
//    }
//    else if (request.responseCode == 201)
//    {
//        //Debug.Log("Request finished successfully! New User created successfully.");
//    }
//    else if (request.responseCode == 401)
//    {
//        //Debug.Log("Error 401: Unauthorized.");
//        requestErrorOccurred = true;
//    }
//    else
//    {
//        //Debug.Log("Request failed (status:" + request.responseCode + ").");
//        requestErrorOccurred = true;
//    }

//    if (!requestErrorOccurred)
//    {
//        yield return null;
//        // process results
//    }
//}