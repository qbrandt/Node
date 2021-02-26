using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private Turns turns;
    private GameBoard gameboard;

    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        turns = GameObject.FindObjectOfType<Turns>();
        SetupRenderer();
    }

    public void SetupRenderer()
    {
        gameboard.Branches[id].renderer = gameboard.BranchObjects[id].GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        turns.BranchClicked(spriteRenderer, id);
    }
}
