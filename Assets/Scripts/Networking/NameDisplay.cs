using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NameDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI P1_Name;

    [SerializeField]
    private TextMeshProUGUI P2_Name;


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            P1_Name.text = PlayerPrefs.GetString("PlayerName");
            //P2_Name.text = photonView.Owner?.NickName ?? "";
        }
        else
        {
            //P1_Name.text = photonView.Owner?.NickName ?? "";
            P2_Name.text = PlayerPrefs.GetString("PlayerName");
        }
       

    }


    
}
