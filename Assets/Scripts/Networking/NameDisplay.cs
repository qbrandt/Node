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
        CachingOption = EventCaching.AddToRoomCache,
        Receivers = ReceiverGroup.All
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
            P1_Name.text = name;
        }
        else if(obj.Code == NAME2_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            P2_Name.text = name;
        }
        
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
           // P1_Name.text = PlayerPrefs.GetString("PlayerName");
            //P2_Name.text = photonView.Owner?.NickName ?? "";

            object[] data = new object[] { PlayerPrefs.GetString("PlayerName") };

            PhotonNetwork.RaiseEvent(NAME1_EVENT, data, options, SendOptions.SendReliable);
        }
        else
        {
            //P1_Name.text = photonView.Owner?.NickName ?? "";
            //P2_Name.text = PlayerPrefs.GetString("PlayerName");
            object[] data = new object[] { PlayerPrefs.GetString("PlayerName") };

            PhotonNetwork.RaiseEvent(NAME2_EVENT, data, options, SendOptions.SendReliable);
        }
       

    }


    
}
