using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    static public string userName;
    public InputField username;

    public void LoginPassName()
    {
        userName = username.text;
    }
}
