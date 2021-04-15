using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoomMenu : MonoBehaviour
{

    //private RoomCanvases _roomCanvases;
    public GameObject MultiplayerScene;
    public GameObject CurrentRoom;

    //public void Initialize(RoomCanvases canvases)
    //{
    //    _roomCanvases = canvases;
    //}

    public void OnClick_LeaveRoom()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        PhotonNetwork.LeaveLobby();
        CurrentRoom.SetActive(false);
        MultiplayerScene.SetActive(true);

    }
}
