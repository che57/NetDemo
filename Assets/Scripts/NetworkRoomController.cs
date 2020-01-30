using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkRoomController : MonoBehaviourPunCallbacks
{
    public int labRoomSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("one user has joined the room");
        StartGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // send a put http request to connection management server to updates the info of room
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(labRoomSceneIndex);
    }

}
