using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMask : MonoBehaviour
{
    public Image theButton;

    // Start is called before the first frame update
    void Start()
    {
        theButton.alphaHitTestMinimumThreshold = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
