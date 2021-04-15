using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkConnect : MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    private RoomCanvases _roomCanvases;
    const string USER_ID = "USER_ID";
    public string previousRoom;
    public GameBoard gameboard;
    private Turns turn;
    private int id;
    private const byte REJOIN_EVENT = 25;
    private static bool rejoinOther = false;


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

        

    }

    


    public void JoinLobbyOnClick()
    {
        Debug.Log($"Click Join Lobby");
        //if (!PhotonNetwork.InLobby)
        //   PhotonNetwork.JoinLobby();
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