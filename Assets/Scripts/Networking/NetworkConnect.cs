using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkConnect: MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    private RoomCanvases _roomCanvases;
    const string USER_ID = "USER_ID";
    public string previousRoom;
    public GameBoard gameboard;
    public static int rejoin = 0;
    private const byte REJOIN_EVENT = 20;
    private const byte DISCONNECT_EVENT = 21;



    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.All,
        TargetActors = null,
        InterestGroup = 0
    };

    RaiseEventOptions options2 = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.Others,
        TargetActors = null,
        InterestGroup = 0
    };

    public override void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == REJOIN_EVENT)
        {
            OnClick_AttemptReconnect();
        }
        else if (obj.Code == DISCONNECT_EVENT)
        {
            OnClick_Disconnect();
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Connecting to the server.");
        PhotonNetwork.AuthValues = new AuthenticationValues(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PlayerPrefs.SetString(USER_ID, PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        if (PhotonNetwork.AuthValues == null)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
        }
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
       // Debug.Log($"previousRoom = {PlayerPrefs.GetString("RoomName")}");
        ReconnectPanel.SetActive(true);
        //if (PhotonNetwork.Reconnect())
        //{
        //    while (PhotonNetwork.RejoinRoom(PlayerPrefs.GetString("RoomName")) == false)
        //    {

        //        Debug.Log("ReJoining previous room: " + PlayerPrefs.GetString("RoomName"));
        //    }
        //    Debug.Log("We are back in baby!");

        //object[] data = new object[] { 0 };

        ////PhotonNetwork.RaiseEvent(DISCONNECT_EVENT, data, options2, SendOptions.SendReliable);
        //PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);


        //    //this.previousRoom = null;
        //    ReconnectPanel.SetActive(false);
        //}

    }


    public void OnClick_AttemptReconnect()
    {
        ++rejoin;
        Debug.Log($"rejoin count = {rejoin}");
        if (PhotonNetwork.ReconnectAndRejoin())
        {
            Debug.Log("Successfully reconnected.");
            ReconnectPanel.SetActive(false);
            //Client reconnected and rejoined room?


            //if(rejoin % 2 != 0)
            //{
            //    System.Threading.Thread.Sleep(2000); 
            //    OnClick_Disconnect();

            //}
            //else
            //{
            //    Debug.Log("Successfully reconnected.");
            //    ReconnectPanel.SetActive(false);
            //}
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            //PhotonNetwork.CurrentRoom.IsVisible = false;
            //PhotonNetwork.LoadLevel(1);
            Debug.Log("Switched master client");
            Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
            //if (PlayerPrefs.GetInt("TurnID") == 1)
            //    gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        else
        {
            //Tell them not able to restore session and try again
            if (PhotonNetwork.IsConnectedAndReady)
            {
                Debug.LogError("Unable to reconnect.");

                ReconnectPanel.SetActive(true);

            }

        }
    }

    public void OnClick_Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void OnClick_QuitRoom()
    {
        ReconnectPanel.SetActive(false);
        _roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();

    }


    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public void LeaveLobbyOnClick()
    {
        if (PhotonNetwork.InLobby)
            PhotonNetwork.LeaveLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        this.previousRoom = PhotonNetwork.CurrentRoom.Name;
    }

   
}