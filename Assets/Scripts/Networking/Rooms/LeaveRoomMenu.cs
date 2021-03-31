using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LeaveRoomMenu : MonoBehaviour
{

    private RoomCanvases _roomCanvases;

    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom(true);
        _roomCanvases.CurrentRoom.Hide();
        _roomCanvases.MenuScene.Show();
    }

    //public void DisconnectPlayer()
    //{
    //    StartCoroutine(DisconnectAndLoad());
    //}

    //IEnumerator DisconnectAndLoad()
    //{
    //    PhotonNetwork.Disconnect();
    //    while (PhotonNetwork.IsConnected)
    //        yield return null;
    //    SceneManager.LoadScene(1);
    //}

}
