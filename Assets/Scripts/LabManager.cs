using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LabManager : MonoBehaviour
{

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.singleton;
        gameManager.SetupRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
