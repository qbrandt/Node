using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PVScript : MonoBehaviourPun
{
    public PhotonView PV { get; set; }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log($"PV = {PV}");
    }


}
