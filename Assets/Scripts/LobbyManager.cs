using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Lobby manager controls all the UI components;
/// </summary>

public class LobbyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static LobbyManager singleton;
    public GameObject inputGroup;
    public Text portText;
    public Text noticeText;
    public Text ipAddressText;

    GameManager gameManager;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(this);
    }
    void Start()
    {        
        gameManager = GameManager.singleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickNextBtn()
    {
        GameManager.singleton.SetNetworkAddressAndPort(ipAddressText.text, Int32.Parse(portText.text));
        SceneManager.LoadScene("LabScene");
    }

    public void ClickCreateRoomBtn()
    {
        noticeText.text = "Create a room as a Host";
        inputGroup.SetActive(true);
        gameManager.SetHost();
    }
    public void ClickJoinRoomBtn()
    {
        noticeText.text = "Join a room as a Guest";
        inputGroup.SetActive(true);
        gameManager.SetGuest();
    }
}
