using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteButtonMask : MonoBehaviour
{
    public Image Img;
    // Start is called before the first frame update
    void Start()
    {
        Img.alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
