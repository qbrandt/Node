using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;


public class CreateRoom : MonoBehaviourPunCallbacks
{
    //[SerializeField]
    //private Text _roomName;

    public GameObject CurrentRoomPanel;
    public GameObject MultiplayerPanel;




    private RoomCanvases _roomCanvases;
    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        var name = PhotonNetwork.NickName;
        Debug.Log($"Photon NickName = {name}");
        if (!PhotonNetwork.IsConnected)
            return;
        Debug.Log("Attempting to create a new Room");
        RoomOptions roomOps = new RoomOptions();
        roomOps.BroadcastPropsChangeToAll = true;
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add("PlayerTurn", 1);
        roomOps.PublishUserId = true;
        roomOps.MaxPlayers = 2;
        roomOps.PlayerTtl = 60000;
        roomOps.EmptyRoomTtl = 60000;
        roomOps.CleanupCacheOnLeave = false;
        PhotonNetwork.JoinOrCreateRoom(name, roomOps, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        MultiplayerPanel.SetActive(false);
        CurrentRoomPanel.SetActive(true);
        Debug.Log("Created room successfully.", this);
        //_roomCanvases.CurrentRoom.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room." + message, this);

    }
}
