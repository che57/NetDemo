﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;



public class labRoomManager : MonoBehaviourPunCallbacks
{
    public string id;
    public string owner;
    public string username;
    // Start is called before the first frame update
    void Start()
    {
        id = gameController.singleton.roomId;
        owner = gameController.singleton.roomOwner;
        username = gameController.singleton.userName;
        Debug.Log("id is " + id);
        Debug.Log("owner is " + owner);

    }

    public void tryQuit(){
        if (username == owner)
            StartCoroutine(quitRoom());
        else
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("PhotonDemo");
        }

}

    IEnumerator quitRoom()
    {
        WWWForm form = new WWWForm();
        form.AddField("roomId", id);

        using (UnityWebRequest quitRoom = UnityWebRequest.Post("http://174.119.178.27:65530/room/quitRoom", form))
        {
            quitRoom.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return quitRoom.SendWebRequest();

            if (quitRoom.isNetworkError || quitRoom.isHttpError)
            {
                Debug.Log(quitRoom.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in quitRoom.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }
                JSONObject reqJSON = new JSONObject(quitRoom.downloadHandler.text);
                VerifyQuit(reqJSON);

            }
        }
    }

    public void VerifyQuit(JSONObject quitReq)
    {
        string quitMsg = quitReq.GetField("type").ToString();

        if (quitMsg == "true")
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("PhotonDemo");
        }

        else
        {
            Debug.Log("Quit failed");
        }
    }
}
