using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoom : MonoBehaviour
{
   public GameObject joinRoom;


   public void OpenJoinRoomMenu()
    {
        if(joinRoom != null)
        {
            bool isActive = joinRoom.activeSelf;
            joinRoom.SetActive(!isActive);
        }
    }
}
