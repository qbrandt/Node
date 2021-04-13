using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class CustomNames : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    
    private void SetCustomName()
    {
        
        _myCustomProperties["Username"] = PhotonNetwork.NickName;
        PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }
    public void OnClick_NameButton()
    {
        SetCustomName();

    }
}
