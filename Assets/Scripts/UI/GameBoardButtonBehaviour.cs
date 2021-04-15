using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardButtonBehaviour : MonoBehaviour
{
    public GameObject shadowPanel;
    public GameObject contentPanel;

    public void OpenPanel()
    {
        shadowPanel.SetActive(true);
        contentPanel.SetActive(true);
    }

    public void ClosePanel ()
    {
        shadowPanel.SetActive(false);
        shadowPanel.SetActive(false);
    }

}
