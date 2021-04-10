using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    private int _panelId = 0;
    private enum PanelDirection
    { 
        Left = 0,
        Right = 1
    }


    public void ClickLeft()
    {
        SwitchPanelDirection(PanelDirection.Left);
    }

    public void ClickRight()
    {
        SwitchPanelDirection(PanelDirection.Right);
    }

    private void SwitchPanelDirection(PanelDirection direction)
    {
        var offset = direction == PanelDirection.Left ? -1 : 1;
        var panelTransform = GameObject.FindGameObjectWithTag("HelpPanel").transform;
        var numPanels = panelTransform.childCount;
        var panelIndex = (_panelId + offset + numPanels) % numPanels;
        panelTransform.GetChild(_panelId).gameObject.SetActive(false);
        panelTransform.GetChild(panelIndex).gameObject.SetActive(true);
        _panelId = panelIndex;
    }
}
