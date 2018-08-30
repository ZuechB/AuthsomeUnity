using Assets.Libs.Authsome;
using Assets.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Login : MonoBehaviour
    {
        public InputField Email;
        public InputField Password;

        public void Login_Click()
        {
            AuthsomeService.instance.Post<User>("/api/Login", new LoginRequest()
            {
                Email = Email.text,
                Password = Password.text

            }, result => {

                if (result.httpStatusCode == System.Net.HttpStatusCode.OK && result.Content != null)
                {
                    Debug.Log("Welcome : " + result.Content.firstName + " " + result.Content.lastName);
                }
                else
                {
                    Debug.Log("Login failed :(");
                }
            });
        }

        public void TestGet_Click()
        {
            AuthsomeService.instance.Get<User>("/api/User/5", result => {

                if (result.httpStatusCode == System.Net.HttpStatusCode.OK && result.Content != null)
                {
                    Debug.Log("got the user : " + result.Content.firstName + " " + result.Content.lastName);
                }
                else
                {
                    Debug.Log("Failed to get");
                }
            });
        }

        public void TestPut_Click()
        {
            AuthsomeService.instance.Post<User>("/api/Login/5", new LoginRequest()
            {
                Email = Email.text,
                Password = Password.text

            }, result => {

                if (result.httpStatusCode == System.Net.HttpStatusCode.OK && result.Content != null)
                {
                    Debug.Log("Welcome : " + result.Content.firstName + " " + result.Content.lastName);
                }
                else
                {
                    Debug.Log("Login failed :(");
                }
            });
        }

        public void TestDelete_Click()
        {
            AuthsomeService.instance.Delete<object>("/api/User/5", result => {

                if (result.httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.Log("User Deleted");
                }
                else
                {
                    Debug.Log("Failed to delete");
                }
            });
        }

    }
}