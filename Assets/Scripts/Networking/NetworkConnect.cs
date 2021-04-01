using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkConnect: MonoBehaviourPunCallbacks
{


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
        switch (cause)
        {
            case DisconnectCause.DisconnectByClientLogic:
                //Do nothing, disconnect was intentional
                break;
            default:
                AttemptReconnect();
                break;

        }
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

    public void AttemptReconnect()
    {
        if (PhotonNetwork.ReconnectAndRejoin())
        {
          //Client reconnected and rejoined room?
        }
        else
        {
            //Tell them not able to and leave
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.LeaveRoom();
            }
           
        }
    }


}