using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class shareManager : MonoBehaviour
{
    public GameObject sharePanel;
    public InputField roomId;


    public void shareClick()
    {
        roomId.text = gameController.singleton.roomId;
        sharePanel.SetActive(true);
        
    }

    public void shareClose()
    {
        sharePanel.SetActive(false);
        roomId.text = "";
    }
}
