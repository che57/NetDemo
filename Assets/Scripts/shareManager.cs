using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class shareManager : MonoBehaviour
{
    
    public void shareClick()
    {
        TextEditor _clipBoard = new TextEditor
        { text = gameController.singleton.roomId };
        _clipBoard.Copy();
    }
}
