using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkConnect: MonoBehaviourPunCallbacks
{
    public GameObject ReconnectPanel;
    private RoomCanvases _roomCanvases;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Connecting to the server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
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
        ReconnectPanel.SetActive(true);
        
    }

    public void OnClick_AttemptReconnect()
    {
        ReconnectPanel.SetActive(false);

        if (PhotonNetwork.ReconnectAndRejoin())
        {
            //Client reconnected and rejoined room?
            Debug.Log("Successfully reconnected.");
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

   


}