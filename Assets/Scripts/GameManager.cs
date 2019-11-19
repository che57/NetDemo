using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;
    public NetworkManager networkManager;
    public enum ClientTypes { Guest, Host }
    public ClientTypes ClientType
    {
        get;
        private set;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNetworkAddressAndPort(string iPAddress, int port)
    {
        networkManager.networkPort = port;
        networkManager.networkAddress = iPAddress;
    }

    public void SetHost()
    {
        ClientType = ClientTypes.Host;
    }

    public void SetGuest()
    {
        ClientType = ClientTypes.Guest;
    }

    public void SetupRoom()
    {
        switch (ClientType)
        {
            case ClientTypes.Guest:
                JoinRoomAsGuest();
                break;
            case ClientTypes.Host:
                CreateRoomAsHost();
                break;
            default:
                break;
        }
    }
    
    void CreateRoomAsHost()
    {
        networkManager.StartHost();
    }

    void JoinRoomAsGuest()
    {
        networkManager.StartClient();
    }
}
