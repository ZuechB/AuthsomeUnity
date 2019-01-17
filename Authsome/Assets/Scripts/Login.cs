using Assets.Scripts.Models;
using Authsome;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    public async void Login_Click()
    {
        await AuthsomeService.instance.oAuth.RequestAuthorization("email", "password",  result =>
        {
            Debug.Log("access Token: " + result.access_token);
        });
    }

    public async void GetCurrentUser_Click()
    {
        await AuthsomeService.instance.GetAsync<User>("/users", result: response =>
        {
            if (response.httpStatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                Debug.Log("Welcome: " + response.Content.firstName + " " + response.Content.lastName);
                Debug.Log("Email: " + response.Content.email);
            }
        });
    }

    public async void GetActiveQuests_Click()
    {
        await AuthsomeService.instance.GetAsync<List<Quest>>("/Quests", result: response =>
        {
            if (response.httpStatusCode == System.Net.HttpStatusCode.OK && response.Content != null)
            {
                foreach (var quest in response.Content)
                {
                    Debug.Log("Quest: " + quest.name);
                }
            }
        });
    }
}
