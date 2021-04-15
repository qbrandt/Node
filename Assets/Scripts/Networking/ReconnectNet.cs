using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ReconnectNet: MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    public GameObject QuitPanel;
    public GameObject OppQuitPanel;
    public GameObject WaitingForOppPanel;
    public GameObject ReconnectFailedPanel;

    public SceneTransition sceneTransition;

    private RoomCanvases _roomCanvases;
    const string USER_ID = "USER_ID";
    public string previousRoom;
    public GameBoard gameboard;
    private Turns turn;
    private int id;
    private int id2;
    private const byte REJOIN_EVENT = 25;
    private const byte REJOIN_EVENT2 = 26;
    private const byte REJOIN_EVENT3 = 27;

    private const string REJOIN_ID = "REJOIN_ID";
    private int interval = 10;
    private static bool leaveOther = false;

    private static int continueReconnect = 0;



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
            //// PlayerPrefs.SetInt(REJOIN_ID, 1);
            // PhotonNetwork.LeaveRoom();
            // System.Threading.Thread.Sleep(1000);
            // AttemptReconnect();

            object[] data = (object[])obj.CustomData;
            bool id = (bool)data[0];
            leaveOther = id;
        }
        else if (obj.Code == REJOIN_EVENT2)
        {
            //// PlayerPrefs.SetInt(REJOIN_ID, 1);
            // PhotonNetwork.LeaveRoom();
            // System.Threading.Thread.Sleep(1000);
            // AttemptReconnect();

            object[] data = (object[])obj.CustomData;
            bool id = (bool)data[0];
            WaitingForOppPanel.SetActive(id);
        }
        else if (obj.Code == REJOIN_EVENT3)
        {
            //// PlayerPrefs.SetInt(REJOIN_ID, 1);
            // PhotonNetwork.LeaveRoom();
            // System.Threading.Thread.Sleep(1000);
            // AttemptReconnect();

            object[] data = (object[])obj.CustomData;
            bool id = (bool)data[0];
            bool id2 = (bool)data[1];
            WaitingForOppPanel.SetActive(id);
            OppQuitPanel.SetActive(id2);


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
        if (leaveOther)
        {
            OppQuitPanel.SetActive(true);
        }
        else 
        {
            WaitingForOppPanel.SetActive(true);
            //System.Threading.Thread.Sleep(1000);

           // PhotonNetwork.Disconnect();
        }
       
    }

    private void OnApplicationQuit()
    {
        object[] data = new object[] { false, true };
        PhotonNetwork.RaiseEvent(REJOIN_EVENT3, data, options, SendOptions.SendReliable);
        PhotonNetwork.SendAllOutgoingCommands();
    }

    public void OnClick_CloseWaitingPanel()
    {
        WaitingForOppPanel.SetActive(false);
    }
        

    public override void OnDisconnected(DisconnectCause cause)
    {

        Debug.Log("Disconnected from server for reason " + cause.ToString());
        ReconnectPanel.SetActive(true);
        // Debug.Log($"previousRoom = {PlayerPrefs.GetString("RoomName")}");
        //AttemptReconnect();
        if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            turn = GameObject.FindObjectOfType<Turns>();
            id = PlayerPrefs.GetInt("prevNode");
            id2 = PlayerPrefs.GetInt("prevBranch");
            Debug.Log($"Rejoin id before active{id}");
            if (PlayerPrefs.GetString("GB_EventID") == "N")
                turn.Event_NodeClicked(id);
            else
                turn.Event_BranchClicked(id2);

            AttemptReconnect();

        }




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



    public void AttemptReconnect()
    {
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
            System.Threading.Thread.Sleep(5000);
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
        else if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogError("Unable to reconnect.");
            ReconnectFailedPanel.SetActive(true);
        }
    }


    //public void AttemptReconnect()
    //{
    //    if (PhotonNetwork.ReconnectAndRejoin())
    //    {
    //        //Client reconnected and rejoined room?
    //        Debug.Log("Successfully reconnected.");
    //       // ReconnectPanel.SetActive(false);
    //        turn = GameObject.FindObjectOfType<Turns>();
    //        id = PlayerPrefs.GetInt("prevNode");
    //        Debug.Log($"Rejoin id before active{id}");
    //        turn.Event_NodeClicked(id);
    //        //rejoinOther = false;

    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to reconnect.");
    //        //Tell them not able to restore session and try again
    //        if (PhotonNetwork.IsConnectedAndReady)
    //        {

    //            // ReconnectPanel.SetActive(true);

    //        }
    //    }

    //    //rejoinOther = false;
    //}

    public void OnClick_Disconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("We are disconnected");
    }


    public void OnClick_QuitRoomWarning()
    {
        // ReconnectPanel.SetActive(false);
        // _roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
        QuitPanel.SetActive(true);

    }

    public void OnClick_CancelQuit()
    {
        // ReconnectPanel.SetActive(false);
        // _roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
        QuitPanel.SetActive(false);

    }


    public void OnClick_QuitRoom()
    {
        // ReconnectPanel.SetActive(false);
        // _roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.EmptyRoomTtl = 10;
            object[] data = new object[] { true };
            PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);
            PhotonNetwork.SendAllOutgoingCommands();
        }

        StartCoroutine(DisconnectAndLoad());

    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        PhotonNetwork.LeaveLobby();
        // PhotonNetwork.LoadLevel(1);
        sceneTransition.TransitionToMainMenuWithPhoton();

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
        continueReconnect = 0;
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log($"Are we in room for reconnect of other player = {PhotonNetwork.InRoom}");
        Debug.Log($"My id = {PlayerPrefs.GetInt("TurnID")} and Room id = {PlayerPrefs.GetInt("TurnTrack")}");

        System.Threading.Thread.Sleep(5000);
        ReconnectPanel.SetActive(false);

        object[] data = new object[] {false};

        PhotonNetwork.RaiseEvent(REJOIN_EVENT2, data, options, SendOptions.SendReliable);



        //  base.photonView.RequestOwnership();

        //if (PlayerPrefs.GetInt("TurnID") == PlayerPrefs.GetInt("TurnTrack"))
        //{
        //}






    }




}