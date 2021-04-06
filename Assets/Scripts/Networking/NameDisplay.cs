using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NameDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text P1_Name;

    [SerializeField]
    private Text P2_Name;


    void Start()
    {
        if (photonView.IsMine && PhotonNetwork.IsMasterClient)
        {
            P1_Name.text = PhotonNetwork.NickName;
            P2_Name.text = photonView.Owner.NickName;
        }
        else
        {
            P1_Name.text = photonView.Owner.NickName;
            P2_Name.text = PhotonNetwork.NickName;
        }
       

    }


    
}
