using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserMenuManager : MonoBehaviour
{
    public UserManager userManager;
    public InputField usernameInput;
    public InputField passwordInput;

    public void OnRegister ()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            userManager.RegisterUser(username, username + "@game.com", password);
        }
        else
        {
            Debug.Log("Debe completar ambos campos");
        }
    }

    public void OnLogin ()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (userManager.LoginUser(username, password))
        {
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Inicio de sesion correcto");
        }
        else
        {
            Debug.Log("Inicio de sesion fallido");
        }
    }
}
