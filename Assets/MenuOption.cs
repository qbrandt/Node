using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    //public int id;
    //public SpriteRenderer spriteRenderer;
    private Turns turns;

    // Start is called before the first frame update
    void Start()
    {
        //turns = GameObject.FindObjectOfType<Turns>();
    }

    public void OnMouseDown()
    {
        Debug.Log("Button Pressed");
    }
}
