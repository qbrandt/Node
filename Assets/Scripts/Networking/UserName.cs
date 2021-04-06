using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class UserName : MonoBehaviour
{
    public InputField username;
    public GameObject setNameBtn;
    public GameObject MultiplayerPanel;
    public GameObject UsernamePanel;
    public static UserName instance;
    const string playerNamePrefKey = "PlayerName";
    const string playerSeedPrefKey = "StartSeed";

    public void OnInput()
    {
        if (username.text.Length > 0)
            setNameBtn.SetActive(true);
        else
            setNameBtn.SetActive(false);

    }

    public void OnClick_SetUserName()
    {
        PhotonNetwork.NickName = username.text;
        PlayerPrefs.SetString(playerNamePrefKey, username.text);
        UsernamePanel.SetActive(false);
        MultiplayerPanel.SetActive(true);
        Debug.Log("Player has created name");
    }

   
}

    
    




