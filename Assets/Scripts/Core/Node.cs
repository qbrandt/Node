using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private Turns turns;

    // Start is called before the first frame update
    void Start()
    {
        turns = GameObject.FindObjectOfType<Turns>();
    }

    public void OnMouseDown()
    {
        turns.NodeClicked(spriteRenderer, id);
    }
}
