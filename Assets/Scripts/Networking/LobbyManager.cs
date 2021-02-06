using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    #region Properties

    public string roomName { get; set; }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            //Special logic
        }

        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            EmptyRoomTtl = 1,
            PlayerTtl = 1,
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        roomName = "Test";
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    #region UI

    public void ClickCreateRoom()
    {
        CreateRoom();
    }

    #endregion

    #region PUN2 Callbacks

    public override void OnConnectedToMaster()
    {

    }

    #endregion

    #region Lobby Callbacks

    public override void OnCreatedRoom()
    {
        Debug.Log("Room was created successfully");
    }

    public override void OnCreateRoomFailed(short returnCode,string message)
    {
        Debug.Log($"Room creation failed.\nReturn Code: {returnCode}\nMessage: {message}");
    }

    #endregion

}
