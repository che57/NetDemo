using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class LabSetupController : MonoBehaviourPunCallbacks
{

    public GameObject [] playerPrefabs;
    public GameObject [] playerViews;
    public GameObject [] models;
    public GameObject otherPlayerPrefab;
    public GameObject brain;
    public GameObject hand;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        BindOtherPlayers();
        player.transform.position = hand.transform.position - brain.transform.position;

      
    }

    private void CreatePlayer()
    {
        player = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer1"), Vector3.zero, Quaternion.identity);

        brain = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Brain Target"), new Vector3(-100,-100,-100), Quaternion.identity);

        hand = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Hand Target"), new Vector3(-100, -100, -100), Quaternion.identity);


  }

  void BindOtherPlayers() {

        playerPrefabs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerPrefabs)
        {
            if(!player.GetComponent<PhotonView>().IsMine && player.transform.childCount == 0)
            {

            GameObject otherPlayer = Instantiate(otherPlayerPrefab, player.transform);
            otherPlayer.transform.position = player.transform.position + brain.transform.position;

            }
        }

    }

}
