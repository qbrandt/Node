using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class RoomListingContext : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    const string playerNamePrefKey = "PlayerName";


    public RoomInfo RoomInfo { get; private set;}

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.Name;
    }

    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}