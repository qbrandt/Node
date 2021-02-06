using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private Turns turns;

    void Start()
    {
        turns = GameObject.FindObjectOfType<Turns>();
    }

    public void OnMouseDown()
    {
        turns.BranchClicked(spriteRenderer, id);
    }
}
