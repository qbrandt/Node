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
        PhotonNetwork.JoinLobby();
        string name = PhotonNetwork.NickName;
        if(string.IsNullOrEmpty(name))
        {
            System.Random rnd = new System.Random();
            int result = rnd.Next(0, 100);

            name = "Farmer" + result.ToString();
            PhotonNetwork.LocalPlayer.NickName = name;
        }

        Debug.Log($"Photon NickName = {name}");
        if (!PhotonNetwork.IsConnected)
            return;
        Debug.Log("Attempting to create a new Room");
        RoomOptions roomOps = new RoomOptions();
        roomOps.BroadcastPropsChangeToAll = true;
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add("PlayerTurn", PhotonNetwork.LocalPlayer.UserId);
        roomOps.CustomRoomProperties.Add("Player1", PhotonNetwork.LocalPlayer.UserId);
        roomOps.PublishUserId = true;
        roomOps.MaxPlayers = 2;
        roomOps.PlayerTtl = 50;
        roomOps.EmptyRoomTtl = 50;
        //roomOps.CleanupCacheOnLeave = false;
        StartCoroutine(LobbyAndLoad(name, roomOps));

    }

    IEnumerator LobbyAndLoad(string roomName, RoomOptions options)
    {
        while (!PhotonNetwork.InLobby)
            yield return null;
        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
        PhotonNetwork.JoinRoom(roomName);
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
        string name = PhotonNetwork.NickName;
        if (string.IsNullOrEmpty(name))
        {
            System.Random rnd = new System.Random();
            int result = rnd.Next(0, 100);

            name = "Farmer" + result.ToString();
            PhotonNetwork.LocalPlayer.NickName = name;
        }

        Debug.Log($"Photon NickName = {name}");
        if (!PhotonNetwork.IsConnected)
            return;
        Debug.Log("Attempting to create a new Room");
        RoomOptions roomOps = new RoomOptions();
        roomOps.BroadcastPropsChangeToAll = true;
        roomOps.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOps.CustomRoomProperties.Add("PlayerTurn", PhotonNetwork.LocalPlayer.UserId);
        roomOps.CustomRoomProperties.Add("Player1", PhotonNetwork.LocalPlayer.UserId);
        roomOps.PublishUserId = true;
        roomOps.MaxPlayers = 2;
        roomOps.PlayerTtl = -1;
        roomOps.EmptyRoomTtl = 60000;
        //roomOps.CleanupCacheOnLeave = false;
        name = name + Random.Range(0, 100);
        PlayerPrefs.SetString("RoomName", name);
        PhotonNetwork.CreateRoom(name, roomOps, TypedLobby.Default);
        PhotonNetwork.JoinRoom(name);

    }
}
