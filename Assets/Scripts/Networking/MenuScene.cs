using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    [SerializeField]
    private CreateRoom _createRoom;
    [SerializeField]
    private RoomListings _roomListings;
    
    private RoomCanvases _roomCanvases;
   public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        _createRoom.Initialize(canvases);
        _roomListings.Initialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
