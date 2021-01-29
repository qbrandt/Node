using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameBoard : MonoBehaviour
{

    private SpriteRenderer TileRenderer;
    private SpriteRenderer NodeRenderer;
    private Color Orange = new Color(1f, 0.5f, 0f, 1f);
    private Color Purple = new Color(0.5f, 0f, 0.5f, 1f);

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

    public GameObject Node1;
    public GameObject Node2;
    public GameObject Node3;
    public GameObject Node4;
    public GameObject Node5;
    public GameObject Node6;
    public GameObject Node7;
    public GameObject Node8;
    public GameObject Node9;
    public GameObject Node10;
    public GameObject Node11;
    public GameObject Node12;
    public GameObject Node13;
    public GameObject Node14;
    public GameObject Node15;
    public GameObject Node16;
    public GameObject Node17;
    public GameObject Node18;
    public GameObject Node19;
    public GameObject Node20;
    public GameObject Node21;
    public GameObject Node22;
    public GameObject Node23;
    public GameObject Node24;

    player Player1 = new player();
    player Player2 = new player();
    List<tile> Gameboard = new List<tile>();
    List<GameObject> TileObjects = new List<GameObject>();
    List<node> Nodes = new List<node>();
    List<GameObject> NodeObjects = new List<GameObject>();
    List<GameObject> DotList = new List<GameObject>();
    public string AiCode = "";

    public class player
    {
        public int score = 0;
        public int red = 0;
        public int green = 0;
        public int yellow = 0;
        public int blue = 0;
    }

    public class tile
    {
        public Color color;
        public int dots;
        public string code;
        public bool one;
        public bool two;
        public bool three;
        public bool owned = false;
        public bool isBlocked = false;

        public tile(Color newColor, int newDots, string newCode, bool newOne, bool newTwo, bool newThree)
        {
            color = newColor;
            dots = newDots;
            code = newCode;
            one = newOne;
            two = newTwo;
            three = newThree;
        }
    }

    public class node
    {
        public int player = 0;
        public tile tile1;
        public tile tile2;
        public tile tile3;
        public tile tile4;

        public node(tile newTile1, tile newTile2, tile newTile3, tile newTile4)
        {
            tile1 = newTile1;
            tile2 = newTile2;
            tile3 = newTile3;
            tile4 = newTile4;
        }
    }

    void Awake()
    {
        TileObjects.Add(tile1);
        TileObjects.Add(tile2);
        TileObjects.Add(tile3);
        TileObjects.Add(tile4);
        TileObjects.Add(tile5);
        TileObjects.Add(tile6);
        TileObjects.Add(tile7);
        TileObjects.Add(tile8);
        TileObjects.Add(tile9);
        TileObjects.Add(tile10);
        TileObjects.Add(tile11);
        TileObjects.Add(tile12);
        TileObjects.Add(tile13);

        NodeObjects.Add(Node1);
        NodeObjects.Add(Node2);
        NodeObjects.Add(Node3);
        NodeObjects.Add(Node4);
        NodeObjects.Add(Node5);
        NodeObjects.Add(Node6);
        NodeObjects.Add(Node7);
        NodeObjects.Add(Node8);
        NodeObjects.Add(Node9);
        NodeObjects.Add(Node10);
        NodeObjects.Add(Node11);
        NodeObjects.Add(Node12);
        NodeObjects.Add(Node13);
        NodeObjects.Add(Node14);
        NodeObjects.Add(Node15);
        NodeObjects.Add(Node16);
        NodeObjects.Add(Node17);
        NodeObjects.Add(Node18);
        NodeObjects.Add(Node19);
        NodeObjects.Add(Node20);
        NodeObjects.Add(Node21);
        NodeObjects.Add(Node22);
        NodeObjects.Add(Node23);
        NodeObjects.Add(Node24);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpBoard();
        Nodes[3].player = 1;
        Nodes[4].player = 1;
        CheckNodes();       
    }

    void CheckNodes()
    {
        for(int i = 0; i < 24; i++)
        {
            if(Nodes[i].tile1 != null)
            {
                if(Nodes[i].player == 1)
                {
                    if(!Nodes[i].tile1.isBlocked)
                    {
                        if(Nodes[i].tile1.color == Color.red)
                        {
                            Player1.red += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.green)
                        {
                            Player1.green += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.yellow)
                        {
                            Player1.yellow += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.blue)
                        {
                            Player1.blue += 1;
                        }
                    }
                }
                else if (Nodes[i].player == 2)
                {
                    if (!Nodes[i].tile1.isBlocked)
                    {
                        if (Nodes[i].tile1.color == Color.red)
                        {
                            Player2.red += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.green)
                        {
                            Player2.green += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.yellow)
                        {
                            Player2.yellow += 1;
                        }
                        else if (Nodes[i].tile1.color == Color.blue)
                        {
                            Player2.blue += 1;
                        }
                    }
                }
            }
            if(Nodes[i].tile2 != null)
            {
                if(Nodes[i].player == 1)
                {
                    if (!Nodes[i].tile2.isBlocked)
                    {
                        if (Nodes[i].tile2.color == Color.red)
                        {
                            Player1.red += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.green)
                        {
                            Player1.green += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.yellow)
                        {
                            Player1.yellow += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.blue)
                        {
                            Player1.blue += 1;
                        }
                    }
                }
                else if (Nodes[i].player == 2)
                {
                    if (!Nodes[i].tile2.isBlocked)
                    {
                        if (Nodes[i].tile2.color == Color.red)
                        {
                            Player2.red += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.green)
                        {
                            Player2.green += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.yellow)
                        {
                            Player2.yellow += 1;
                        }
                        else if (Nodes[i].tile2.color == Color.blue)
                        {
                            Player2.blue += 1;
                        }
                    }
                }
            }
            if(Nodes[i].tile3 != null)
            {
                if (Nodes[i].player == 1)
                {
                    if (!Nodes[i].tile3.isBlocked)
                    {
                        if (Nodes[i].tile3.color == Color.red)
                        {
                            Player1.red += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.green)
                        {
                            Player1.green += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.yellow)
                        {
                            Player1.yellow += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.blue)
                        {
                            Player1.blue += 1;
                        }
                    }
                }
                else if (Nodes[i].player == 2)
                {
                    if (!Nodes[i].tile3.isBlocked)
                    {
                        if (Nodes[i].tile3.color == Color.red)
                        {
                            Player2.red += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.green)
                        {
                            Player2.green += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.yellow)
                        {
                            Player2.yellow += 1;
                        }
                        else if (Nodes[i].tile3.color == Color.blue)
                        {
                            Player2.blue += 1;
                        }
                    }
                }
            }
            if(Nodes[i].tile4 != null)
            {
                if (Nodes[i].player == 1)
                {
                    if (!Nodes[i].tile4.isBlocked)
                    {
                        if (Nodes[i].tile4.color == Color.red)
                        {
                            Player1.red += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.green)
                        {
                            Player1.green += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.yellow)
                        {
                            Player1.yellow += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.blue)
                        {
                            Player1.blue += 1;
                        }
                    }
                }
                else if (Nodes[i].player == 2)
                {
                    if (!Nodes[i].tile4.isBlocked)
                    {
                        if (Nodes[i].tile4.color == Color.red)
                        {
                            Player2.red += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.green)
                        {
                            Player2.green += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.yellow)
                        {
                            Player2.yellow += 1;
                        }
                        else if (Nodes[i].tile4.color == Color.blue)
                        {
                            Player2.blue += 1;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < 24; i++)
        {
            NodeRenderer = NodeObjects[i].GetComponent<SpriteRenderer>();
            if (Nodes[i].player == 1)
            {
                NodeRenderer.color = Orange;
            }
            else if (Nodes[i].player == 2)
            {
                NodeRenderer.color = Purple;
            }
            else
            {
                NodeRenderer.color = Color.gray;
            }
        }

        Debug.Log("Score: " + Player1.score);
        Debug.Log("Red: " + Player1.red);
        Debug.Log("Green: " + Player1.green);
        Debug.Log("Yellow: " + Player1.yellow);
        Debug.Log("Blue: " + Player1.blue);
    }
    void SetUpBoard()
    {
        Gameboard.Add(new tile(Color.red, 1, "R1", true, false, false));
        Gameboard.Add(new tile(Color.red, 2, "R2", false, true, false));
        Gameboard.Add(new tile(Color.red, 3, "R3", false, false, true));
        Gameboard.Add(new tile(Color.green, 1, "G1", true, false, false));
        Gameboard.Add(new tile(Color.green, 2, "G2", false, true, false));
        Gameboard.Add(new tile(Color.green, 3, "G3", false, false, true));
        Gameboard.Add(new tile(Color.yellow, 1, "Y1", true, false, false));
        Gameboard.Add(new tile(Color.yellow, 2, "Y2", false, true, false));
        Gameboard.Add(new tile(Color.yellow, 3, "Y3", false, false, true));
        Gameboard.Add(new tile(Color.blue, 1, "B1", true, false, false));
        Gameboard.Add(new tile(Color.blue, 2, "B2", false, true, false));
        Gameboard.Add(new tile(Color.blue, 3, "B3", false, false, true));
        Gameboard.Add(new tile(Color.gray, 0, "XX", false, false, false));

        Gameboard = RandomizeBoard(Gameboard);
        SetUpNodes();
        GenerateCode();

        for (int i = 0; i < 13; i++)
        {
            TileRenderer = TileObjects[i].GetComponent<SpriteRenderer>();
            TileRenderer.color = Gameboard[i].color;
        }
    }
    
    void SetUpNodes()
    {
        Nodes.Add(new node(null, null, null, Gameboard[0]));
        Nodes.Add(new node(null, null, Gameboard[0], null));
        Nodes.Add(new node(null, null, null, Gameboard[1]));
        Nodes.Add(new node(null, Gameboard[0], Gameboard[1], Gameboard[2]));
        Nodes.Add(new node(Gameboard[0], null, Gameboard[2], Gameboard[3]));
        Nodes.Add(new node(null, null, Gameboard[3], null));
        Nodes.Add(new node(null, null, null, Gameboard[4]));
        Nodes.Add(new node(null, Gameboard[1], Gameboard[4], Gameboard[5]));
        Nodes.Add(new node(Gameboard[1], Gameboard[2], Gameboard[5], Gameboard[6]));
        Nodes.Add(new node(Gameboard[2], Gameboard[3], Gameboard[6], Gameboard[7]));
        Nodes.Add(new node(Gameboard[3], null, Gameboard[7], Gameboard[8]));
        Nodes.Add(new node(null, null, Gameboard[8], null));
        Nodes.Add(new node(null, Gameboard[4], null, null));
        Nodes.Add(new node(Gameboard[4], Gameboard[5], null, Gameboard[9]));
        Nodes.Add(new node(Gameboard[5], Gameboard[6], Gameboard[9], Gameboard[10]));
        Nodes.Add(new node(Gameboard[6], Gameboard[7], Gameboard[10], Gameboard[11]));
        Nodes.Add(new node(Gameboard[7], Gameboard[8], Gameboard[11], null));
        Nodes.Add(new node(Gameboard[8], null, null, null));
        Nodes.Add(new node(null, Gameboard[9], null, null));
        Nodes.Add(new node(Gameboard[9], Gameboard[10], null, Gameboard[12]));
        Nodes.Add(new node(Gameboard[10], Gameboard[11], Gameboard[12], null));
        Nodes.Add(new node(Gameboard[11], null, null, null));
        Nodes.Add(new node(null, Gameboard[12], null, null));
        Nodes.Add(new node(Gameboard[12], null, null, null));

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateCode()
    {
        for(int i = 0; i < 13; i++)
        {
            AiCode += Gameboard[i].code;
        }
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

    public void UpdateBoard()
    {
        Player1 = ResetPlayer(Player1);
        Player2 = ResetPlayer(Player2);
        Nodes.Clear();
        SetUpNodes();
        CheckNodes();
    }

    player ResetPlayer(player Player)
    {
        Player.score = 0;
        Player.red = 0;
        Player.green = 0;
        Player.yellow = 0;
        Player.blue = 0;

        return Player;
    }
    public void ResetGame()
    {
        Gameboard.Clear();
        Nodes.Clear();
        AiCode = "";

        Player1 = ResetPlayer(Player1);
        Player2 = ResetPlayer(Player2);

        Start();
    }
}