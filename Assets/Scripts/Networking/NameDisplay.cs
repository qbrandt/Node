using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class NameDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI P1_Name;

    [SerializeField]
    private TextMeshProUGUI P2_Name;
    private const byte NAME1_EVENT = 10;
    private const byte NAME2_EVENT = 11;


    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.All,
        TargetActors = null,
        InterestGroup = 0
    };


    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == NAME1_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            Farmer farmer = (Farmer)data[1];
            Debug.Log($"Player 1 - {name} - {farmer}");
            P1_Name.text = name;
            GameInformation.Player1Username = name;
            GameInformation.Player1Farmer = farmer == GameInformation.farmer ? (Farmer)((int)farmer + 3 % 4) : farmer;
        }
        else if(obj.Code == NAME2_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            Farmer farmer = (Farmer)data[1];
            Debug.Log($"Player 2 - {name} - {farmer}");
            P2_Name.text = name;
            GameInformation.Player2Username = name;
            GameInformation.Player2Farmer = farmer == GameInformation.farmer ? (Farmer)((int)farmer + 1 % 4) : farmer;

        }

    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
           // P1_Name.text = PlayerPrefs.GetString("PlayerName");
            //P2_Name.text = photonView.Owner?.NickName ?? "";

            object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, GameInformation.farmer};

            PhotonNetwork.RaiseEvent(NAME1_EVENT, data, options, SendOptions.SendReliable);
        }
        else
        {
            //P1_Name.text = photonView.Owner?.NickName ?? "";
            //P2_Name.text = PlayerPrefs.GetString("PlayerName");
            object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, GameInformation.farmer };

            PhotonNetwork.RaiseEvent(NAME2_EVENT, data, options, SendOptions.SendReliable);
        }
       

    }


    
}
