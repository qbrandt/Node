using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private Turns turns;
    private GameBoard gameboard;

    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        turns = GameObject.FindObjectOfType<Turns>();
        gameboard.Nodes[id].renderer = spriteRenderer;
    }

    public void SetupRenderer()
    {
    }

    public void OnMouseDown()
    {
        turns.NodeClicked(spriteRenderer, id);
    }
}
