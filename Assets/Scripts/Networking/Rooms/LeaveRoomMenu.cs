using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoomMenu : MonoBehaviour
{

    private RoomCanvases _roomCanvases;

    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _roomCanvases.CurrentRoom.Hide();
        _roomCanvases.MenuScene.Show();
    }
}
