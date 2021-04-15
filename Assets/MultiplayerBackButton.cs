using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerBackButton : MonoBehaviour
{
    public GameObject UserPanel;
    public GameObject MultiplayerPanel;

    public void goBack()
    {
        MultiplayerPanel.SetActive(false);
        UserPanel.SetActive(true);
    }

}
