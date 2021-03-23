using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private Turns turns;
    private GameBoard gameboard;

    //private Basket basket;
    
    public Sprite blueBasket;
    public Sprite orangeBasket;
    public Sprite greyBasket;
    public Sprite transparentBasket;

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

    void OnMouseOver()
    {
        //if (player's turn, has basket resources, basket is not yet placed) {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = TransparentBasket;
        //}
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //if (this.gameObject.GetComponent<SpriteRenderer>().sprite == TransparentBasket) {
        //    this.gameObject.GetComponent<SpriteRenderer>().sprite = none;
        //}
    }
}
