using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameBoard : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    public GameObject tile4;
    public GameObject tile5;
    public GameObject tile6;
    public GameObject tile7;
    public GameObject tile8;
    public GameObject tile9;
    public GameObject tile10;
    public GameObject tile11;
    public GameObject tile12;
    public GameObject tile13;

    public List<tile> Gameboard = new List<tile>();
    public List<GameObject> TileList = new List<GameObject>();
    public List<GameObject> DotList = new List<GameObject>();

    public class tile
    {
        public Color color;
        public int dots;
        public bool one;
        public bool two;
        public bool three;

        public tile(Color newColor, int newDots, bool newOne, bool newTwo, bool newThree)
        {
            color = newColor;
            dots = newDots;
            one = newOne;
            two = newTwo;
            three = newThree;
        }
    }
    private void Awake()
    {
        TileList.Add(tile1);
        TileList.Add(tile2);
        TileList.Add(tile3);
        TileList.Add(tile4);
        TileList.Add(tile5);
        TileList.Add(tile6);
        TileList.Add(tile7);
        TileList.Add(tile8);
        TileList.Add(tile9);
        TileList.Add(tile10);
        TileList.Add(tile11);
        TileList.Add(tile12);
        TileList.Add(tile13);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoard();
    }

    public void SetUpBoard()
    {
        Gameboard.Clear();

        Gameboard.Add(new tile(Color.red, 1, true, false, false));
        Gameboard.Add(new tile(Color.red, 2, false, true, false));
        Gameboard.Add(new tile(Color.red, 3, false, false, true));
        Gameboard.Add(new tile(Color.green, 1, true, false, false));
        Gameboard.Add(new tile(Color.green, 2, false, true, false));
        Gameboard.Add(new tile(Color.green, 3, false, false, true));
        Gameboard.Add(new tile(Color.yellow, 1, true, false, false));
        Gameboard.Add(new tile(Color.yellow, 2, false, true, false));
        Gameboard.Add(new tile(Color.yellow, 3, false, false, true));
        Gameboard.Add(new tile(Color.blue, 1, true, false, false));
        Gameboard.Add(new tile(Color.blue, 2, false, true, false));
        Gameboard.Add(new tile(Color.blue, 3, false, false, true));
        Gameboard.Add(new tile(Color.gray, 0, false, false, false));

        Gameboard = RandomizeBoard(Gameboard);

        for (int i = 0; i < 13; i++)
        {
            spriteRenderer = TileList[i].GetComponent<SpriteRenderer>();
            spriteRenderer.color = Gameboard[i].color;

            if(Gameboard[i].one)
            {

            }
            else if(Gameboard[i].two)
            {
                
            }
            else if (Gameboard[i].three)
            {

            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<tile> RandomizeBoard(List<tile> Gameboard)
    {
        List<tile> newGameboard = new List<tile>();
        int n = Gameboard.Count;
        int rand;

        for (int i = 0; i < n; i++)
        {
            rand = Random.Range(0, Gameboard.Count);
            newGameboard.Add(Gameboard[rand]);
            Gameboard.RemoveAt(rand);
        }

        return newGameboard;
    }
}