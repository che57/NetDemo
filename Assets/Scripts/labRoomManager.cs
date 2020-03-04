using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;



public class labRoomManager : MonoBehaviour
{
    public string id;
    public string owner;
    public string username;
    // Start is called before the first frame update
    void Start()
    {
        id = gameController.singleton.roomId;
        owner = gameController.singleton.roomOwner;
        username = gameController.userName;
    }

    public void tryQuit(){
        if (username == owner)
            StartCoroutine(quitRoom());
        else
            SceneManager.LoadScene("PhotonDemo");
}

    IEnumerator quitRoom()
    {
        WWWForm form = new WWWForm();
        form.AddField("roomId", id);

        using (UnityWebRequest quitRoom = UnityWebRequest.Post("http://127.0.0.1:8081/room/quitRoom", form))
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
            SceneManager.LoadScene("PhotonDemo");
        }

        else
        {
            Debug.Log("Quit failed");
        }
    }
}
