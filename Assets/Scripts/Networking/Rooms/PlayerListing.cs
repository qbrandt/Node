using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public Player Player { get; private set; }
    public bool Ready = false;

    public void SetPlayerInfo(Player player)
    {
        Player = player;

        string username = "Code Cropper";
        if(player.CustomProperties.ContainsKey("Username"))
            username = (string)player.CustomProperties["Username"];
        _text.text = username;
 

    }
}