using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private GameBoard gameboard;

    // Start is called before the first frame update
    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
    }

    public void OnMouseDown()
    {
        if(spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
        {
            spriteRenderer.color = gameboard.Orange;
            gameboard.Nodes[id].player = 1;
        }
        else if(spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
        {
            spriteRenderer.color = Color.gray;
            gameboard.Nodes[id].player = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
