using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
    public GameObject Tile_1;
    public  class tile
    {
        public bool inBounds = true;
        public string color;
        public int dots;
    }

    public tile[] Tileset;
    public tile[,] GameBoard;

    private void Awake()
    {
        tile[] TileSet = new tile[13];
        GameBoard = new tile[5, 5];

        TileSet[0].color = "red";
        TileSet[0].dots = 1;
        TileSet[1].color = "red";
        TileSet[1].dots = 2;
        TileSet[2].color = "red";
        TileSet[2].dots = 3;

        TileSet[3].color = "green";
        TileSet[3].dots = 1;
        TileSet[4].color = "green";
        TileSet[4].dots = 2;
        TileSet[5].color = "green";
        TileSet[5].dots = 3;

        TileSet[6].color = "yellow";
        TileSet[6].dots = 1;
        TileSet[7].color = "yellow";
        TileSet[7].dots = 2;
        TileSet[8].color = "yellow";
        TileSet[8].dots = 3;

        TileSet[9].color = "blue";
        TileSet[9].dots = 1;
        TileSet[10].color = "blue";
        TileSet[10].dots = 2;
        TileSet[11].color = "blue";
        TileSet[11].dots = 3;

        TileSet[12].color = "gray";
        TileSet[12].dots = 0;

        Tileset = RandomizeBoard(TileSet);

        // Set out-of-bounds tiles to false
        //GameBoard[0, 0].inBounds = false;
        //GameBoard[0, 1].inBounds = false;
        //GameBoard[0, 3].inBounds = false;
        //GameBoard[0, 4].inBounds = false;
        //GameBoard[1, 0].inBounds = false;
        //GameBoard[1, 4].inBounds = false;
        //GameBoard[3, 0].inBounds = false;
        //GameBoard[3, 4].inBounds = false;
        //GameBoard[4, 0].inBounds = false;
        //GameBoard[4, 1].inBounds = false;
        //GameBoard[4, 3].inBounds = false;
        //GameBoard[4, 4].inBounds = false;

        //GameBoard[0, 2].color = "red";
        //GameBoard[0, 2].dots = 1;
        //GameBoard[1, 1].color = "red";
        //GameBoard[1, 1].dots = 2;
        //GameBoard[1, 2].color = "red";
        //GameBoard[1, 2].dots = 3;

        //GameBoard[1, 3].color = "green";
        //GameBoard[1, 3].dots = 1;
        //GameBoard[2, 0].color = "green";
        //GameBoard[2, 0].dots = 2;
        //GameBoard[2, 1].color = "green";
        //GameBoard[2, 1].dots = 3;

        //GameBoard[2, 2].color = "yellow";
        //GameBoard[2, 2].dots = 1;
        //GameBoard[2, 3].color = "yellow";
        //GameBoard[2, 3].dots = 2;
        //GameBoard[2, 4].color = "yellow";
        //GameBoard[2, 4].dots = 3;

        //GameBoard[3, 1].color = "blue";
        //GameBoard[3, 1].dots = 1;
        //GameBoard[3, 2].color = "blue";
        //GameBoard[3, 2].dots = 2;
        //GameBoard[3, 3].color = "blue";
        //GameBoard[3, 3].dots = 3;

        //GameBoard[4, 2].color = "gray";
        //GameBoard[4, 2].dots = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 13; i++)
        {
            Debug.Log(Tileset[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    tile[] RandomizeBoard(tile[] TileSet)
    {
        int n = TileSet.Length;
        int rand = Random.Range(0, 12);

        for(int i = 0; i < n; i++)
        {
            swap(TileSet, i, rand);
        }

        return TileSet;
    }

    void swap(tile[] TileSet, int a, int b)
    {
        tile temp = TileSet[a];
        TileSet[a] = TileSet[b];
        TileSet[b] = temp;
    }
}