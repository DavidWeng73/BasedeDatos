using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserManager : MonoBehaviour
{
    private string filePath;

    void Start ()
    {
        filePath = Application.persistentDataPath + "/users.json ";

        if(!File.Exists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(new UserDataBase(), true));
        }
    }

    public bool RegisterUser (string username, string email, string password)
    {
        UserDataBase dataBase = LoadUserData();

        foreach (User user in dataBase.users)
        {
            if (user.username == username || user.email == email)
            {
                Debug.Log("Usuario o mail ya registrado");
                return false;
            }
        }

        dataBase.users.Add (new User { username = username, email = email, password = password });
        SaveUserData(dataBase);
        Debug.Log("Regisro completado");
        return true;
    }

    public bool LoginUser (string userOrEmail,  string password)
    {
        UserDataBase dataBase = LoadUserData();

        foreach(User user in dataBase.users)
        {
            if ((user.username == userOrEmail || user.email == userOrEmail) && user.password == password)
            {
                Debug.Log("Login Completado");
                return true;
            }
        }

        Debug.Log("Usuario o contraseña incorrectos");
        return false;
    }

    private UserDataBase LoadUserData ()
    {
        if (!File.Exists(filePath)) return new UserDataBase();
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<UserDataBase>(json) ?? new UserDataBase();
    }

    private void SaveUserData (UserDataBase dataBase)
    {
        string json = JsonUtility.ToJson(dataBase, true);
        File.WriteAllText(filePath, json);
    }
}
