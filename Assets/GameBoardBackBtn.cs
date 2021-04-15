using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameBoardBackBtn : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public ReconnectNet reconnect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickLeave()
    {
        if(PhotonNetwork.InRoom)
        {
            reconnect.OnClick_QuitRoomWarning();
        }
        else
        {
            sceneTransition.TransitionToScene("MainMenu");
        }
    }

}
