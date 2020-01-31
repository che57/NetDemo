using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pv.IsMine)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerGun"), Vector3.left, Quaternion.identity);
        }
    }
}
