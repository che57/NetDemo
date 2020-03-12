using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class shareManager : MonoBehaviour
{
    public GameObject sharePanel;
    public Text roomId;


    public void shareClick()
    {
        roomId.text = "Room Id has been copied to your clipper borad. Send it!";
        sharePanel.SetActive(true);
        GUIUtility.systemCopyBuffer = gameController.singleton.roomId;
    }

    public void shareClose()
    {
        sharePanel.SetActive(false);
        roomId.text = "";
    }
}
