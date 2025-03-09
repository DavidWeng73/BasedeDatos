using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class User
{
    public string username;
    public string password;
    public string email;
    public float bestScore;
}

[Serializable]
public class UserDataBase
{
    public List <User> users = new List<User> ();
}

