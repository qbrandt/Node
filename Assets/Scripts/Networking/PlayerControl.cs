using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviourPunCallbacks
{
    private RoomCanvases _roomCanvases;

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
   
        _roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public void AttemptReconnect()
    {
        if (PhotonNetwork.ReconnectAndRejoin())
        {
            //Client reconnected and rejoined room?
        }
        else
        {
            //Tell them not able to restore session and leave
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.LeaveRoom();
            }

        }
    }




}
