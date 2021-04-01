using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkConnect: MonoBehaviourPunCallbacks
{

    private bool rejoin;

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

        if (PhotonNetwork.Server == ServerConnection.GameServer)
        {
            switch (cause)
            {
                case DisconnectCause.DisconnectByClientLogic:
                    Debug.Log("Disconnected from server for reason " + cause.ToString());
                    break;

                case DisconnectCause.ClientTimeout:
                    rejoin = true;
                    break;
                case DisconnectCause.DisconnectByServerReasonUnknown:
                    rejoin = true;
                    break;
                default:
                    rejoin = false;
                    break;
            }

            if (rejoin && !PhotonNetwork.ReconnectAndRejoin())
            {
                Debug.LogError("Error: Attempting to reconnect and rejoin");
            }

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







}