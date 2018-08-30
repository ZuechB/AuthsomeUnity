﻿using Assets.Libs.Authsome;
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
            AuthsomeService.instance.Post<LoginResult>("/api/Login", new LoginRequest()
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
    }
}