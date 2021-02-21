using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;


public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    private RoomCanvases _roomCanvases;
    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        Debug.Log("Attempting to creating a new Room");
        RoomOptions roomOps = new RoomOptions();
        roomOps.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, roomOps, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);
        _roomCanvases.CurrentRoom.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room." + message, this);

    }
}