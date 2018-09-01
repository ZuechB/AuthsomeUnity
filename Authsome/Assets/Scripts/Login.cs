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

            }, result: data => {

                if (data.httpStatusCode == System.Net.HttpStatusCode.OK && data.Content != null)
                {
                    Debug.Log("Welcome : " + data.Content.firstName + " " + data.Content.lastName);
                }
                else
                {
                    Debug.Log("Login failed :(");
                }
            });
        }

        public void TestGet_Click()
        {
            AuthsomeService.instance.Get<User>("/api/User/5", result: data => {

                if (data.httpStatusCode == System.Net.HttpStatusCode.OK && data.Content != null)
                {
                    Debug.Log("got the user : " + data.Content.firstName + " " + data.Content.lastName);
                }
                else
                {
                    Debug.Log("Failed to get");
                }
            });
        }

        public void TestPut_Click()
        {
            AuthsomeService.instance.Put<User>("/api/User/5", new User()
            {
                firstName = "new first name",
                lastName = "new last name",
                email = "new email"

            }, result: data => {

                if (data.httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.Log("User updated");
                }
                else
                {
                    Debug.Log("failed to update user");
                }
            });
        }

        public void TestDelete_Click()
        {
            AuthsomeService.instance.Delete<object>("/api/User/5", result: data => {

                if (data.httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.Log("User Deleted");
                }
                else
                {
                    Debug.Log("Failed to delete");
                }
            });
        }


        public void TestHeader_Click()
        {
            // note this will not work with the server sample, this is just an example showing you how you can do it

            var testUser = new User()
            {
                firstName = "test",
                lastName = "test2",
                email = "test@test.com"
            };

            AuthsomeService.instance.Post<object>("http://myfullurlworks.com", testUser, HeaderBuilder: header => {

                header.IncludeHeader("MyKey", "MyValue");

            }, result: data => {

                if (data.httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.Log("test");
                }
                else
                {
                    Debug.Log("failed");
                }
            });
        }

    }
}