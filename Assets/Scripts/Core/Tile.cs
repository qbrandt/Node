using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
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
        //if(gameboard.Gameboard[id] != null)
        //{
        //    print(gameboard.Gameboard[id].curNodes);
        //    print(gameboard.Gameboard[id].maxNodes);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
