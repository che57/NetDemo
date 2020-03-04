using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    static public string userName;
    public string roomId;
    public InputField username;
    public Text id;
    static public gameController singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoginPassName()
    {
        userName = username.text;
    }
}
