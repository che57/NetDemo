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
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        BindOtherPlayers();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer1"), Vector3.zero, Quaternion.identity);
    }

    void BindOtherPlayers() {

        playerPrefabs = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerPrefabs)
        {
            if(!player.GetComponent<PhotonView>().IsMine && player.transform.Find("ImageTarget").childCount == 1)
            {
                Debug.Log("y oyoyoetrhjiogasodifvhnxaocgnba;pERFg");
                Instantiate(otherPlayerPrefab, player.transform.GetChild(0)); 
            }
        }

    }

    
}
