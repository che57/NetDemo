using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    public string userName;
    public string roomOwner;
    public string roomId;
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

    public void reset()
    {
        userName = null;
    }
}
