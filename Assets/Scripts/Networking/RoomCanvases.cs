using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField]
    private MenuScene _menuScene;
    public MenuScene MenuScene { get { return _menuScene; } }

    [SerializeField]
    private CurrentRoom _currentRoom;
    public CurrentRoom CurrentRoom{ get { return _currentRoom; } }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        //MenuScene.Initialize(this);
        CurrentRoom.Initialize(this);
    }
}
