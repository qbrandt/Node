using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReconnectNet: MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    private RoomCanvases _roomCanvases;
    const string USER_ID = "USER_ID";
    public string previousRoom;
    public GameBoard gameboard;
    private Turns turn;
    private int id;
    private int id2;
    private const byte REJOIN_EVENT = 25;
    private const string REJOIN_ID = "REJOIN_ID";
    private int interval = 10;
    private static bool rejoinOther = false;

    //private static bool rejoinOther2 = true;

    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.Others,
        TargetActors = null,
        InterestGroup = 0
    };

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }



    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == REJOIN_EVENT)
        {
           // PlayerPrefs.SetInt(REJOIN_ID, 1);
            PhotonNetwork.LeaveRoom();
            System.Threading.Thread.Sleep(1000);
            AttemptReconnect();
        }

    }


    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.SetInt(REJOIN_ID, 0);

    }

    //private void Update()
    //{


    //    if (Time.frameCount % interval == 0)
    //    {
    //        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 1)
    //        {
    //            Debug.Log("Update in progress");
    //            PhotonNetwork.Disconnect();
    //            rejoinOther = true;
    //        }
    //    }


    //}

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        

    }

    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        System.Threading.Thread.Sleep(1000);

        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
        // Debug.Log($"previousRoom = {PlayerPrefs.GetString("RoomName")}");
        OnClick_AttemptReconnect();

        //if (PlayerPrefs.GetInt(REJOIN_ID) == 0)
        //{

        //}
        //else
        //{
        //    AttemptReconnect();
        //}

        //Debug.Log($"RejoinOther value = {rejoinOther}");

        //if (rejoinOther)
        //{
        //    Debug.Log("opp reconnect");

        //    AttemptReconnect();
        //}
        //else
        //{
        //    Debug.Log("my reconnect");
        //}

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
        ReconnectPanel.SetActive(true);

        if (PhotonNetwork.ReconnectAndRejoin())
        {
            //Client reconnected and rejoined room?
            Debug.Log("Successfully reconnected.");
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            //PhotonNetwork.CurrentRoom.IsVisible = false;
            //PhotonNetwork.LoadLevel(1);
            Debug.Log("Switched master client");
            // Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
            //if (PlayerPrefs.GetInt("TurnID") == 1)
            //    gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
            turn = GameObject.FindObjectOfType<Turns>();
            id = PlayerPrefs.GetInt("prevNode");
            id2 = PlayerPrefs.GetInt("prevBranch");
            Debug.Log($"Rejoin id before active{id}");
            turn.Event_NodeClicked(id);


            System.Threading.Thread.Sleep(3000);
            ReconnectPanel.SetActive(false);


            //if (PlayerPrefs.GetString("GB_EventID") == "N")
            //    turn.Event_NodeClicked(id);
            //else if (PlayerPrefs.GetString("GB_EventID") == "B")
            // turn.Event_BranchClicked(id2);


            //rejoinOther = true;

            //Debug.Log($"Are we in room for reconnect of other player = {PhotonNetwork.InRoom}");
            //object[] data = new object[] { 0 };

            //PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);
            Debug.Log($"Are we in room for reconnect of other player = {PhotonNetwork.InRoom}");
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


    public void AttemptReconnect()
    {
        if (PhotonNetwork.ReconnectAndRejoin())
        {
            //Client reconnected and rejoined room?
            Debug.Log("Successfully reconnected.");
           // ReconnectPanel.SetActive(false);
            turn = GameObject.FindObjectOfType<Turns>();
            id = PlayerPrefs.GetInt("prevNode");
            Debug.Log($"Rejoin id before active{id}");
            turn.Event_NodeClicked(id);
            //rejoinOther = false;

        }
        else
        {
            Debug.LogError("Unable to reconnect.");
            //Tell them not able to restore session and try again
            if (PhotonNetwork.IsConnectedAndReady)
            {

                // ReconnectPanel.SetActive(true);

            }
        }

        //rejoinOther = false;
    }

    public void OnClick_Disconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("We are disconnected");
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
        Debug.Log($"Are we in room for reconnect of other player = {PhotonNetwork.InRoom}");
        Debug.Log($"My id = {PlayerPrefs.GetInt("TurnID")} and Room id = {PlayerPrefs.GetInt("TurnTrack")}");
        //  base.photonView.RequestOwnership();

        //if (PlayerPrefs.GetInt("TurnID") == PlayerPrefs.GetInt("TurnTrack"))
        //{
        //}

       
        //object[] data = new object[] {0};

        //PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);

    
        
    }


   }