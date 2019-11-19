using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControl : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector3 vector3 = transform.position;
                vector3 += Vector3.forward;
                transform.position = vector3;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 vector3 = transform.position;
                vector3 += Vector3.back;
                transform.position = vector3;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 vector3 = transform.position;
                vector3 += Vector3.left;
                transform.position = vector3;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 vector3 = transform.position;
                vector3 += Vector3.right;
                transform.position = vector3;
            }
        }
    }
}
