using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;

    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;


    private RoomCanvases _roomCanvases;
    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
        _playerListingsMenu.Initialize(canvases);
        _leaveRoomMenu.Initialize(canvases);
      
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
