using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoinGameBtn : MonoBehaviour
{
    public GameObject MultiplayerPanel;
    public GameObject JoinRoomPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Onclick_JoinRoom()
    {
        //PhotonNetwork.LeaveLobby();
        PhotonNetwork.JoinLobby();
        MultiplayerPanel.SetActive(false);
        JoinRoomPanel.SetActive(true);
    }
}
