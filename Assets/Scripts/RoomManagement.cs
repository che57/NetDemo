using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;


//this script is used to room search, browse, create.
public class RoomManagement : MonoBehaviour
{
    public InputField roomName;
    public InputField roomPW;
    public InputField searchRoomId;
    public GameObject createPop;
    public GameObject roomLine;
    public GameObject scrollContent;
    public GameObject CreateErr;
    public GameObject searchPanel;
    public GameObject searchResult;



    public Text UserNameText;
    public Text roomsName;
    public Text roomsTitle;
    public Text roomsCreator;
    public Text CreateErrMsg;

    //Especially for Search Room function
    public Text SearchRoomName;
    public Text SearchRoomTitle;
    public Text SearchRoomCreator;
    public Text SearchRoomPub;
    public Text SearchErrMsg;

    //array to save the list of all public rooms
    public List<string> roomNames;
    public List<string> roomTitles;
    public List<string> roomCreators;
    public List<string> roomIDs;


    private void Start()
    {
        UserNameText.text = "Welcome, " + gameController.userName;
    }


    //create a room
    IEnumerator createRoom()
    {
        //So if the password is left empty, it will be set the same as roomName;

        

        WWWForm form = new WWWForm();
        form.AddField("roomName", roomName.text);
        form.AddField("password", roomName.text);
        form.AddField("creator", gameController.userName);
        form.AddField("roomTitle", "123");
        form.AddField("roomPublic", "true");


        using (UnityWebRequest createRoom = UnityWebRequest.Post("http://174.119.178.27:65530/room/createRoom", form))
        {
            createRoom.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return createRoom.SendWebRequest();

            if (createRoom.isNetworkError || createRoom.isHttpError)
            {
                Debug.Log(createRoom.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in createRoom.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }
                JSONObject reqJSON = new JSONObject(createRoom.downloadHandler.text);
                VerifyCreate(reqJSON);

            }
        }
    }

    public void CreateBtnOnClick()
    {
        StartCoroutine(createRoom());
    }

    public void VerifyCreate(JSONObject loginInfo)
    {
        string judgeMsg = loginInfo.GetField("type").ToString();
        Debug.Log(loginInfo);
        Debug.Log("loginInfo" + loginInfo.GetField("data").GetField("_id").ToString().Trim('"'));

        if (judgeMsg == "true")
        {
            createPop.SetActive(false);
            CreateErr.SetActive(false);
            gameController.singleton.roomId = loginInfo.GetField("data").GetField("_id").ToString().Trim('"');
            gameController.roomOwner = loginInfo.GetField("data").GetField("creator").ToString().Trim('"');
            NetworkLobbyController.singleton.CreateRoom();
        }

        else
        {
            Debug.Log("CreateRoom failed");
            CreateErrMsg.text = "Create Room Failed. You've already owned a room";
            createPop.SetActive(false);
        }
    }


    IEnumerator browseRoom()
    {
        using (UnityWebRequest createRoom = UnityWebRequest.Get("http://174.119.178.27:65530/room/browseRooms"))
        {
            createRoom.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return createRoom.SendWebRequest();

            if (createRoom.isNetworkError || createRoom.isHttpError)
            {
                Debug.Log(createRoom.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in createRoom.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }
                JSONObject reqJSON = new JSONObject(createRoom.downloadHandler.text);
                VerifyBrowse(reqJSON);

            }
        }
    }

    public void VerifyBrowse(JSONObject response)
    {
        Debug.Log(response);
        GameObject newRoom = new GameObject("RoomList");
        Instantiate(newRoom, scrollContent.transform);
        for (int i = 0; i < response.Count; i++)
        {
           roomNames.Add(response[i].GetField("roomName").ToString());
           roomTitles.Add(response[i].GetField("roomTitle").ToString());
           roomCreators.Add(response[i].GetField("creator").ToString());
           roomIDs.Add(response[i].GetField("_id").ToString());


            //Text newRoomName = newRoom.AddComponent<Text>();
            //newRoomName.text = roomNames[i];
            roomsName.text = roomNames[i];
            roomsTitle.text = roomTitles[i];
            roomsCreator.text = roomCreators[i];
            Instantiate(roomLine, new Vector3(scrollContent.transform.position.x + 380, scrollContent.transform.position.y- 70 - i*50, scrollContent.transform.position.z), Quaternion.identity, scrollContent.transform);
            Instantiate(roomsName, new Vector3(scrollContent.transform.position.x + 110, scrollContent.transform.position.y - 70 - i * 50, scrollContent.transform.position.z), Quaternion.identity, scrollContent.transform);
            Instantiate(roomsTitle, new Vector3(scrollContent.transform.position.x + 390, scrollContent.transform.position.y - 70 - i * 50, scrollContent.transform.position.z), Quaternion.identity, scrollContent.transform);
            Instantiate(roomsCreator, new Vector3(scrollContent.transform.position.x + 650, scrollContent.transform.position.y - 70 - i * 50, scrollContent.transform.position.z), Quaternion.identity, scrollContent.transform);

            //Debug.Log(newRoomName.text);
            //new Vector3(scrollContent.transform.position.x, scrollContent.transform.position.y - i, scrollContent.transform.position.z)
        }



    }

    public void browseBtnOnclick()
    {
        StartCoroutine(browseRoom());
    }

    public void searchRoomOnclick()
    {
        string roomId = searchRoomId.text;
        if (roomId == "")
            return;
        else
            StartCoroutine(searchRoom(roomId));
    }

    IEnumerator searchRoom(string roomId)
    {
        WWWForm searchform = new WWWForm();
        searchform.AddField("roomId", roomId);
        
        using (UnityWebRequest searchRoom = UnityWebRequest.Post("http://174.119.178.27:65530/room/searchRoom", searchform))
        {
            searchRoom.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return searchRoom.SendWebRequest();

            if (searchRoom.isNetworkError || searchRoom.isHttpError)
            {
                Debug.Log(searchRoom.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in searchRoom.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }
                JSONObject reqJSON = new JSONObject(searchRoom.downloadHandler.text);
                VerifySearch(reqJSON);

            }
        }
    }

    public void VerifySearch(JSONObject response)
    {

        string resultTF = response.GetField("type").ToString();
        Debug.Log(response);

        if(resultTF == "true")
        {
            SearchRoomName.text = response.GetField("data").GetField("roomName").ToString().Trim('"');
            SearchRoomTitle.text = response.GetField("data").GetField("roomTitle").ToString().Trim('"');
            SearchRoomCreator.text = response.GetField("data").GetField("creator").ToString().Trim('"');
            Debug.Log(response.GetField("data").GetField("roomPublic").ToString().GetType());
            //if ("true".Equals(response.GetField("data").GetField("roomPublic").ToString()))
            SearchRoomPub.text = "Public";

            gameController.roomOwner = response.GetField("data").GetField("creator").ToString().Trim('"');

            //else
            //    SearchRoomPub.text = "Private";
        }

        else
        {
            SearchRoomName.text = "Nah";
            SearchRoomTitle.text = "Nah";
            SearchRoomCreator.text = "Nah";
            SearchRoomPub.text = "Nah";
            SearchErrMsg.text = "Sorry, Room Not found.";
        }
            
    }

    public void startSearch()
    {
        searchPanel.SetActive(false);
        searchResult.SetActive(true);
    }

    public void backToSearch()
    {
        SearchErrMsg.text = "";
        SearchRoomName.text = "";
        SearchRoomTitle.text = "";
        SearchRoomCreator.text = "";
        searchPanel.SetActive(true);
        searchResult.SetActive(false);
    }
}
