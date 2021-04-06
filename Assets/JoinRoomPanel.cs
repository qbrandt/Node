using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomPanel : MonoBehaviour
{
    public GameObject JoinRoom;
    public GameObject MultiplayerPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackButtonClick ()
    {
        JoinRoom.SetActive(false);
        MultiplayerPanel.SetActive(true);
    }
}
