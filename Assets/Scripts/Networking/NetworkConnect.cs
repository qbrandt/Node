using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkConnect: MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    public GameObject NotificationPanel;
    public GameObject ConfirmQuitPanel;
    private RoomCanvases _roomCanvases;
    const string USER_ID = "USER_ID";
    public string previousRoom;
    public GameBoard gameboard;
    private Turns turn;
    private int id;
    private const byte REJOIN_EVENT = 25;


    RaiseEventOptions options = new RaiseEventOptions()
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

        //if(cause == DisconnectCause.DisconnectByServerReasonUnknown)
        //{

        //    if (PhotonNetwork.ReconnectAndRejoin())
        //    {
        //        //Client reconnected and rejoined room?
        //        Debug.Log("Successfully reconnected.");
        //        // ReconnectPanel.SetActive(false);
        //        //PhotonNetwork.CurrentRoom.IsOpen = false;
        //        //PhotonNetwork.CurrentRoom.IsVisible = false;
        //        //PhotonNetwork.LoadLevel(1);
        //        Debug.Log("Switched master client");
        //        // Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
        //        //if (PlayerPrefs.GetInt("TurnID") == 1)
        //        //    gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        //        turn = GameObject.FindObjectOfType<Turns>();
        //        id = PlayerPrefs.GetInt("prevNode");
        //        Debug.Log($"Rejoin id before active{id}");
        //        turn.Event_NodeClicked(id);

        //        NotificationPanel.SetActive(false);


        //        //if (PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"] != null)
        //        //{
        //        //    Debug.Log($"Orange Node ID upon rejoin: {(int)PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"]}");
        //        //    if ((int)PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"] >= 0)
        //        //    {

        //        //    }
        //        //    else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BLUE"] >= 0)
        //        //    {
        //        //        gameboard.BlueBaskets[(int)PhotonNetwork.CurrentRoom.CustomProperties["BLUE"]].SetActive(true);

        //        //    }
        //        //}

        //    }
        //}

        ReconnectPanel.SetActive(true);

       
        // Debug.Log($"previousRoom = {PlayerPrefs.GetString("RoomName")}");



        //if (PhotonNetwork.Reconnect())
        //{
        //    while (PhotonNetwork.RejoinRoom(PlayerPrefs.GetString("RoomName")) == false)
        //    {

        //        Debug.Log("ReJoining previous room: " + PlayerPrefs.GetString("RoomName"));
        //    }
        //    Debug.Log("We are back in baby!");


        //    //this.previousRoom = null;
        //    ReconnectPanel.SetActive(false);
        //}

    }

    public void OnClick_AttemptReconnect()
    {

        if (PhotonNetwork.ReconnectAndRejoin())
        {
            //Client reconnected and rejoined room?
            Debug.Log("Successfully reconnected.");
            ReconnectPanel.SetActive(false);
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            //PhotonNetwork.CurrentRoom.IsVisible = false;
            //PhotonNetwork.LoadLevel(1);
            Debug.Log("Switched master client");
           // Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
            //if (PlayerPrefs.GetInt("TurnID") == 1)
            //    gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            turn = GameObject.FindObjectOfType<Turns>();
            id = PlayerPrefs.GetInt("prevNode");
            Debug.Log($"Rejoin id before active{id}");
            turn.Event_NodeClicked(id);



            //if (PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"] != null)
            //{
            //    Debug.Log($"Orange Node ID upon rejoin: {(int)PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"]}");
            //    if ((int)PhotonNetwork.CurrentRoom.CustomProperties["ORANGE"] >= 0)
            //    {

            //    }
            //    else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BLUE"] >= 0)
            //    {
            //        gameboard.BlueBaskets[(int)PhotonNetwork.CurrentRoom.CustomProperties["BLUE"]].SetActive(true);

            //    }
            //}

            object[] data = new object[] { 0 };

            PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);

        }
        else
        {
            Debug.LogError("Unable to reconnect.");
            //Tell them not able to restore session and try again
            if (PhotonNetwork.IsConnectedAndReady)
            {

                ReconnectPanel.SetActive(true);

            }

        }
    }

    public void OnClick_Disconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log($"Orange Node ID upon rejoin: {PlayerPrefs.GetInt("prevNode")}");

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

    public void OnClick_ConfirmQuit()
    {
        ConfirmQuitPanel.SetActive(true);
    }

    public void OnClick_CancelQuit()
    {
        ConfirmQuitPanel.SetActive(false);
    }

    public void OnClick_QuitGame()
    {
        ConfirmQuitPanel.SetActive(false);
        System.Threading.Thread.Sleep(1000);
        StartCoroutine(DisconnectAndLoad());
    }


    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(3);
    }


    

   
}