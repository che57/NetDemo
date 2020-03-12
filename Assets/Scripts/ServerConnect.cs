using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;

public class ServerConnect : MonoBehaviour
{

    public InputField username;
    public InputField password;
    public Dropdown role;
    public Text StatsMsg;

    public static string creator;

    //public string ipAddress;

    // Start is called before the first frame update
    void Start()
    {
        //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //socket.Connect("127.0.0.1", 3000);
        //Debug.Log("The Unity is connecting to server! ");
        //string hostName = Dns.GetHostName();
        //ipAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
        //Debug.Log("ipAddewss is: " + ipAddress);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //login Button clicked
    IEnumerator Signup()
    {

        //using(UnityWebRequest get = UnityWebRequest.Get("http://127.0.0.1:8081/"))
        //{
        //    yield return get.SendWebRequest();

        //    if (get.isHttpError || get.isNetworkError)                         //如果其 请求失败，或是 网络错误
        //    {
        //        Debug.Log(get.error); //打印错误原因
        //    }
        //    else //请求成功
        //    {
        //        Debug.Log("Get:请求成功");
        //        Debug.Log(get.downloadedBytes);
        //    }


        //}

        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("role", role.options[role.value].text);
        Debug.Log(role.options[role.value].text.GetType());

        using (UnityWebRequest regis = UnityWebRequest.Post("http://174.119.178.27:65530/account/signup", form))
        {
            regis.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return regis.SendWebRequest();

            if (regis.isNetworkError || regis.isHttpError)
            {
                Debug.Log(regis.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in regis.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                // Print Headers
                //Debug.Log(sb.ToString());

                // Print Body
                JSONObject reqJSON = new JSONObject(regis.downloadHandler.text);
                VerifySignup(reqJSON);
            }
        }
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        Debug.Log(form);

        using (UnityWebRequest login = UnityWebRequest.Post("http://174.119.178.27:65530/account/authenticate", form))
        {
            login.SetRequestHeader("content-type", "application/x-www-form-urlencoded");

            yield return login.SendWebRequest();

            if (login.isNetworkError || login.isHttpError)
            {
                Debug.Log(login.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                StringBuilder sb = new StringBuilder();
                foreach (System.Collections.Generic.KeyValuePair<string, string> dict in login.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                // Print Headers
                //Debug.Log(sb.ToString());

                // Print Body
                Debug.Log(login.downloadHandler.text);
                JSONObject loginJSON = new JSONObject(login.downloadHandler.text);
                VerifyLogin(loginJSON);
            }
        }
    }

    public void LoginBtnOnClick()
    {
        StartCoroutine(Login());
    }
    public void SignupBtnOnclick()
    {
        StartCoroutine(Signup());
    }

    public void VerifyLogin(JSONObject loginInfo)
    {
        string trueOrFalse = loginInfo.GetField("type").ToString();
        if (trueOrFalse == "true")
        {
            gameController.singleton.userName = loginInfo.GetField("data").GetField("username").ToString().Trim('"');
            SceneManager.LoadScene("PhotonDemo");
        }
        else
        {
            StatsMsg.text = ("Bad username or password. Try again please.");
        }
    }

    public void VerifySignup(JSONObject signupInfo)
    {
        string trueOrFalse = signupInfo.GetField("type").ToString();
        if (trueOrFalse =="true")
            StatsMsg.text = "Congrats, you now have your own account!";
        else
            StatsMsg.text = "Regist failed. Please try again later";
    }

}
