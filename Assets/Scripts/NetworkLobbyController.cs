using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkLobbyController : MonoBehaviourPunCallbacks
{
    public Text roomNameText;

    [SerializeField]
    private string nickName; // get from user managerment system
    private string roomName;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        JoinLobbyOnClick();
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.AutomaticallySyncScene = true; // all players load to the same scence at the same time;
    }

    public void UserNicknameUpdate()
    {
        PhotonNetwork.NickName = nickName;
        PlayerPrefs.SetString("NickName", nickName);
    }

    public void JoinLobbyOnClick()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        Debug.Log("Creating new room");
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)10
        };
        roomName = roomNameText.text;
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create new room");
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void JoinRoom()
    {
        roomName = roomNameText.text;
        PhotonNetwork.JoinRoom(roomName);
    }
}
