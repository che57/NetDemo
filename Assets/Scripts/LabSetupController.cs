using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class LabSetupController : MonoBehaviourPunCallbacks
{

    public GameObject otherPlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer1"), Vector3.zero, Quaternion.identity);
    }
}
