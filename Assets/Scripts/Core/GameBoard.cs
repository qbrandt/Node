using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameBoard : MonoBehaviour
{

    private SpriteRenderer TileRenderer;
    private SpriteRenderer NodeRenderer;
    private SpriteRenderer BranchRenderer;
    private Turns turns;
    private Branch branches;

    public Color Orange = new Color(1f, 0.5f, 0f, 1f);
    public Color Purple = new Color(0.5f, 0f, 0.5f, 1f);

    public Sprite Red1;
    public Sprite Red2;
    public Sprite Red3;
    public Sprite Green1;
    public Sprite Green2;
    public Sprite Green3;
    public Sprite Yellow1;
    public Sprite Yellow2;
    public Sprite Yellow3;
    public Sprite Blue1;
    public Sprite Blue2;
    public Sprite Blue3;
    public Sprite Gray;

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

    public GameObject branch1;
    public GameObject branch2;
    public GameObject branch3;
    public GameObject branch4;
    public GameObject branch5;
    public GameObject branch6;
    public GameObject branch7;
    public GameObject branch8;
    public GameObject branch9;
    public GameObject branch10;
    public GameObject branch11;
    public GameObject branch12;
    public GameObject branch13;
    public GameObject branch14;
    public GameObject branch15;
    public GameObject branch16;
    public GameObject branch17;
    public GameObject branch18;
    public GameObject branch19;
    public GameObject branch20;
    public GameObject branch21;
    public GameObject branch22;
    public GameObject branch23;
    public GameObject branch24;
    public GameObject branch25;
    public GameObject branch26;
    public GameObject branch27;
    public GameObject branch28;
    public GameObject branch29;
    public GameObject branch30;
    public GameObject branch31;
    public GameObject branch32;
    public GameObject branch33;
    public GameObject branch34;
    public GameObject branch35;
    public GameObject branch36;

    public TextMeshProUGUI P1_ScoreText;
    public TextMeshProUGUI P1_RedText;
    public TextMeshProUGUI P1_GreenText;
    public TextMeshProUGUI P1_YellowText;
    public TextMeshProUGUI P1_BlueText;
    public TextMeshProUGUI P2_ScoreText;
    public TextMeshProUGUI P2_RedText;
    public TextMeshProUGUI P2_GreenText;
    public TextMeshProUGUI P2_YellowText;
    public TextMeshProUGUI P2_BlueText;
    public TextMeshProUGUI P1_networkText;
    public TextMeshProUGUI P2_networkText;

    public static List<int> player1Network1 = new List<int>();
    public static List<int> player1Network2 = new List<int>();
    public static List<int> player2Network1 = new List<int>();
    public static List<int> player2Network2 = new List<int>();

    public player Player1 = new player();
    public player Player2 = new player();
    public List<tile> Gameboard = new List<tile>();
    List<GameObject> TileObjects = new List<GameObject>();
    public List<node> Nodes = new List<node>();
    List<GameObject> NodeObjects = new List<GameObject>();
    public List<GameObject> BranchObjects = new List<GameObject>();
    public List<branch> Branches = new List<branch>();
    public List<branch> curBranches = new List<branch>();
    public List<node> curNodes = new List<node>();
    public TextMeshProUGUI TurnKeeper;
    public string AiCode = "";
    public int oneNode = 1;
    public int oneBranch = 1;
    public bool Player1sTurn = true;
    public bool Player2sTurn = false;
    public bool firstTurnsOver = false;
    public bool gameWon = false;
    public int P1_LongestNetwork = 0;
    public int P2_LongestNetwork = 0;

    public class player
    {
        public int score = 0;
        public int longestNetwork = 0;
        public int red = 0;
        public int green = 0;
        public int yellow = 0;
        public int blue = 0;
    }
    public class tile
    {
        public Color color;
        public int maxNodes;
        public int curNodes = 0;
        public string code;
        public bool owned = false;
        public bool isBlocked = false;
        public bool captured = false;

        public tile(Color newColor, int newDots, string newCode)
        {
            color = newColor;
            maxNodes = newDots;
            code = newCode;
        }
    }
    public struct _branch
    {
        public int id;
        public bool inBounds;

        public _branch(int _id, bool _inBounds)
        {
            id = _id;
            inBounds = _inBounds;
        }
    }
    public class node
    {
        public bool owned = false;
        public bool newNode = false;
        public int player = 0;
        public tile tile1;
        public tile tile2;
        public tile tile3;
        public tile tile4;
        public _branch branch1;
        public _branch branch2;
        public _branch branch3;
        public _branch branch4;

        public node(tile newTile1, tile newTile2, tile newTile3, tile newTile4, _branch newBranch1, _branch newBranch2, _branch newBranch3, _branch newBranch4)
        {
            tile1 = newTile1;
            tile2 = newTile2;
            tile3 = newTile3;
            tile4 = newTile4;
            branch1 = newBranch1;
            branch2 = newBranch2;
            branch3 = newBranch3;
            branch4 = newBranch4;
        }
    }

    public class branch
    {
        public bool owned = false;
        public bool nextToOwned = false;
        public bool newBranch = false;
        public int player = 0;
        public int id;
        public int network = 0;
        public node node1;
        public node node2;
        public int branch1;
        public int branch2;
        public int branch3;
        public int branch4;
        public int branch5;
        public int branch6;

        public void resetBranch()
        {
            player = 0;
            network = 0;
            owned = false;
            nextToOwned = false;
            newBranch = false;
        }

        public branch(int newId, node newNode1, node newNode2, int newBranch1, int newBranch2, int newBranch3, int newBranch4, int newBranch5, int newBranch6)
        {
            id = newId;
            node1 = newNode1;
            node2 = newNode2;
            branch1 = newBranch1;
            branch2 = newBranch2;
            branch3 = newBranch3;
            branch4 = newBranch4;
            branch5 = newBranch5;
            branch6 = newBranch6;
        }
    }

    void Awake()
    {
        turns = GameObject.FindObjectOfType<Turns>();
        branches = GameObject.FindObjectOfType<Branch>();

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

        BranchObjects.Add(branch1);
        BranchObjects.Add(branch2);
        BranchObjects.Add(branch3);
        BranchObjects.Add(branch4);
        BranchObjects.Add(branch5);
        BranchObjects.Add(branch6);
        BranchObjects.Add(branch7);
        BranchObjects.Add(branch8);
        BranchObjects.Add(branch9);
        BranchObjects.Add(branch10);
        BranchObjects.Add(branch11);
        BranchObjects.Add(branch12);
        BranchObjects.Add(branch13);
        BranchObjects.Add(branch14);
        BranchObjects.Add(branch15);
        BranchObjects.Add(branch16);
        BranchObjects.Add(branch17);
        BranchObjects.Add(branch18);
        BranchObjects.Add(branch19);
        BranchObjects.Add(branch20);
        BranchObjects.Add(branch21);
        BranchObjects.Add(branch22);
        BranchObjects.Add(branch23);
        BranchObjects.Add(branch24);
        BranchObjects.Add(branch25);
        BranchObjects.Add(branch26);
        BranchObjects.Add(branch27);
        BranchObjects.Add(branch28);
        BranchObjects.Add(branch29);
        BranchObjects.Add(branch30);
        BranchObjects.Add(branch31);
        BranchObjects.Add(branch32);
        BranchObjects.Add(branch33);
        BranchObjects.Add(branch34);
        BranchObjects.Add(branch35);
        BranchObjects.Add(branch36);
    }
    void Start()
    {
        SetUpBoard();
        CheckNodes();       
        SetUpBranches();
        updateBranches();
        SetText();
        SetScore();
    }
    void CheckNodes()
    {
        if(!gameWon)
        {
            isTileBlocked();
            updateBranches();
            //Check to see if a tile is not null, owned/who owns it, and what resource to give if it isn't blocked.

            for (int i = 0; i < 24; i++)
            {
                if (Nodes[i].tile1 != null)
                {
                    if (Nodes[i].player == 1)
                    {
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile1.isBlocked && firstTurnsOver)
                        {
                            if (Nodes[i].tile1.color == Color.red)
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
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile1.isBlocked && firstTurnsOver)
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
                if (Nodes[i].tile2 != null)
                {
                    if (Nodes[i].player == 1)
                    {
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile2.isBlocked && firstTurnsOver)
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
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile2.isBlocked && firstTurnsOver)
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
                if (Nodes[i].tile3 != null)
                {
                    if (Nodes[i].player == 1)
                    {
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile3.isBlocked && firstTurnsOver)
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
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile3.isBlocked && firstTurnsOver)
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
                if (Nodes[i].tile4 != null)
                {
                    if (Nodes[i].player == 1)
                    {
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile4.isBlocked && firstTurnsOver)
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
                        isTileBlocked();
                        Nodes[i].newNode = false;
                        if (!Nodes[i].tile4.isBlocked && firstTurnsOver)
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
            //Change node color to reflect which player owns it
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
            curNodes.Clear();
        }
    }
    void isTileBlocked()
    {
        // Checks to see if the given tile belongs to a player & if it does, change the owned variable to true and increment the curNodes
        for (int i = 0; i < 24; i++)
        {
            if (Nodes[i].owned == false)
            {
                if (Nodes[i].tile1 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile1.curNodes += 1;
                        Branches[turns.curBranch].owned = true;
                    }
                }
                if (Nodes[i].tile2 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile2.curNodes += 1;
                        Branches[turns.curBranch].owned = true;
                    }
                }
                if (Nodes[i].tile3 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile3.curNodes += 1;
                        Branches[turns.curBranch].owned = true;
                    }
                }
                if (Nodes[i].tile4 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile4.curNodes += 1;
                        Branches[turns.curBranch].owned = true;
                    }
                }
            }

        //Check if number of nodes on tile exceeds amount allowed and block the tile if it has
        if (Nodes[i].tile1 != null)
        {
            if (Nodes[i].tile1.curNodes > Nodes[i].tile1.maxNodes)
            {
                    Nodes[i].tile1.isBlocked = true;
            }
        }
        if (Nodes[i].tile2 != null)
        {
            if (Nodes[i].tile2.curNodes > Nodes[i].tile2.maxNodes)
            {
                    Nodes[i].tile2.isBlocked = true;
            }
        }
        if (Nodes[i].tile3 != null)
        {
            if (Nodes[i].tile3.curNodes > Nodes[i].tile3.maxNodes)
            {
                    Nodes[i].tile3.isBlocked = true;
            }
        }
        if (Nodes[i].tile4 != null)
        {
            if (Nodes[i].tile4.curNodes > Nodes[i].tile4.maxNodes)
            {
                    Nodes[i].tile4.isBlocked = true;
            }
        }
        }

    }
    public void updateBranches()
    {
        // Loops through and sets any branch that is colored to 'owned' only AFTER 'Make Move' is pressed
        for (int i = 0; i < 36; i++)
        {
            BranchRenderer = BranchObjects[i].GetComponent<SpriteRenderer>();
            if (BranchRenderer.color == Orange || BranchRenderer.color == Purple)
            {
                Branches[i].owned = true;
                Branches[i].newBranch = false;
            }
        }
        curBranches.Clear();


        SetText();
    }
    public void SetText()
    {
        if(!gameWon)
        {
            //Sets GUI text to reflect current scores and resource count
            P1_RedText.text = Player1.red.ToString();
            P1_GreenText.text = Player1.green.ToString();
            P1_YellowText.text = Player1.yellow.ToString();
            P1_BlueText.text = Player1.blue.ToString();

            P2_RedText.text = Player2.red.ToString();
            P2_GreenText.text = Player2.green.ToString();
            P2_YellowText.text = Player2.yellow.ToString();
            P2_BlueText.text = Player2.blue.ToString();

            P1_networkText.text = P1_LongestNetwork.ToString();
            P2_networkText.text = P2_LongestNetwork.ToString();
        }
    }
    public void SetScore()
    {
        if(!gameWon)
        {
            if(P1_LongestNetwork > P2_LongestNetwork)
            {
                Player1.longestNetwork = 2;
                Player2.longestNetwork = 0;
            }
            else if(P2_LongestNetwork > P1_LongestNetwork)
            {
                Player1.longestNetwork = 0;
                Player2.longestNetwork = 2;
            }
            else
            {
                Player1.longestNetwork = 0;
                Player2.longestNetwork = 0;
            }
            P1_ScoreText.text = (Player1.longestNetwork + Player1.score).ToString();
            P2_ScoreText.text = (Player2.longestNetwork + Player2.score).ToString();
            SetText();
            
            if(Player1.score >= 10)
            {
                WinGame(1);
            }
            else if(Player2.score >= 10)
            {
                WinGame(2);
            }
        }
    }
    void SetUpBoard()
    {
        Gameboard.Add(new tile(Color.red, 1, "R1"));
        Gameboard.Add(new tile(Color.red, 2, "R2"));
        Gameboard.Add(new tile(Color.red, 3, "R3"));
        Gameboard.Add(new tile(Color.green, 1, "G1"));
        Gameboard.Add(new tile(Color.green, 2, "G2"));
        Gameboard.Add(new tile(Color.green, 3, "G3"));
        Gameboard.Add(new tile(Color.yellow, 1, "Y1"));
        Gameboard.Add(new tile(Color.yellow, 2, "Y2"));
        Gameboard.Add(new tile(Color.yellow, 3, "Y3"));
        Gameboard.Add(new tile(Color.blue, 1, "B1"));
        Gameboard.Add(new tile(Color.blue, 2, "B2"));
        Gameboard.Add(new tile(Color.blue, 3, "B3"));
        Gameboard.Add(new tile(Color.gray, 0, "XX"));

        Gameboard = RandomizeBoard(Gameboard);
        SetUpNodes();
        //Generates string code representing the board for the AI
        GenerateCode();

        for (int i = 0; i < 13; i++)
        {
            TileRenderer = TileObjects[i].GetComponent<SpriteRenderer>();
            
            //Determine which sprite should be applied to each Tile
            if(Gameboard[i].color == Color.red)
            {
                if(Gameboard[i].maxNodes == 1)
                {
                    TileRenderer.sprite = Red1;
                }
                else if(Gameboard[i].maxNodes == 2)
                {
                    TileRenderer.sprite = Red2;
                }
                else
                {
                    TileRenderer.sprite = Red3;
                }
            }
            else if(Gameboard[i].color == Color.green)
            {
                if (Gameboard[i].maxNodes == 1)
                {
                    TileRenderer.sprite = Green1;
                }
                else if (Gameboard[i].maxNodes == 2)
                {
                    TileRenderer.sprite = Green2;
                }
                else
                {
                    TileRenderer.sprite = Green3;
                }
            }
            else if(Gameboard[i].color == Color.yellow)
            {
                if (Gameboard[i].maxNodes == 1)
                {
                    TileRenderer.sprite = Yellow1;
                }
                else if (Gameboard[i].maxNodes == 2)
                {
                    TileRenderer.sprite = Yellow2;
                }
                else
                {
                    TileRenderer.sprite = Yellow3;
                }
            }
            else if (Gameboard[i].color == Color.blue)
            {
                if (Gameboard[i].maxNodes == 1)
                {
                    TileRenderer.sprite = Blue1;
                }
                else if (Gameboard[i].maxNodes == 2)
                {
                    TileRenderer.sprite = Blue2;
                }
                else
                {
                    TileRenderer.sprite = Blue3;
                }
            }
            else
            {
                TileRenderer.sprite = Gray;
            }
        }
    }
    void SetUpNodes()
    {
        Nodes.Add(new node(null, null, null, Gameboard[0], new _branch(36, false), new _branch(36, false), new _branch(0, true), new _branch(1, true)));
        Nodes.Add(new node(null, null, Gameboard[0], null, new _branch(36, false), new _branch(0, true), new _branch(36, false), new _branch(2, true)));
        Nodes.Add(new node(null, null, null, Gameboard[1], new _branch(36, false), new _branch(36, false), new _branch(3, true), new _branch(6, true)));
        Nodes.Add(new node(null, Gameboard[0], Gameboard[1], Gameboard[2], new _branch(1, true), new _branch(3, true), new _branch(4, true), new _branch(7, true)));
        Nodes.Add(new node(Gameboard[0], null, Gameboard[2], Gameboard[3], new _branch(2, true), new _branch(4, true), new _branch(5, true), new _branch(8, true)));
        Nodes.Add(new node(null, null, Gameboard[3], null, new _branch(36, false), new _branch(5, true), new _branch(36, false), new _branch(9, true)));
        Nodes.Add(new node(null, null, null, Gameboard[4], new _branch(36, false), new _branch(36, false), new _branch(10, true), new _branch(15, true)));
        Nodes.Add(new node(null, Gameboard[1], Gameboard[4], Gameboard[5], new _branch(6, true), new _branch(10, true), new _branch(11, true), new _branch(16, true)));
        Nodes.Add(new node(Gameboard[1], Gameboard[2], Gameboard[5], Gameboard[6], new _branch(7, true), new _branch(11, true), new _branch(12, true), new _branch(17, true)));
        Nodes.Add(new node(Gameboard[2], Gameboard[3], Gameboard[6], Gameboard[7], new _branch(8, true), new _branch(12, true), new _branch(13, true), new _branch(18, true)));
        Nodes.Add(new node(Gameboard[3], null, Gameboard[7], Gameboard[8], new _branch(9, true), new _branch(13, true), new _branch(14, true), new _branch(19, true)));
        Nodes.Add(new node(null, null, Gameboard[8], null, new _branch(36, false), new _branch(14, true), new _branch(36, false), new _branch(20, true)));
        Nodes.Add(new node(null, Gameboard[4], null, null, new _branch(15, true), new _branch(36, false), new _branch(21, true), new _branch(36, false)));
        Nodes.Add(new node(Gameboard[4], Gameboard[5], null, Gameboard[9], new _branch(16, true), new _branch(21, true), new _branch(22, true), new _branch(26, true)));
        Nodes.Add(new node(Gameboard[5], Gameboard[6], Gameboard[9], Gameboard[10], new _branch(17, true), new _branch(22, true), new _branch(23, true), new _branch(27, true)));
        Nodes.Add(new node(Gameboard[6], Gameboard[7], Gameboard[10], Gameboard[11], new _branch(18, true), new _branch(23, true), new _branch(24, true), new _branch(28, true)));
        Nodes.Add(new node(Gameboard[7], Gameboard[8], Gameboard[11], null, new _branch(19, true), new _branch(24, true), new _branch(25, true), new _branch(29, true)));
        Nodes.Add(new node(Gameboard[8], null, null, null, new _branch(20, true), new _branch(25, true), new _branch(36, false), new _branch(36, false)));
        Nodes.Add(new node(null, Gameboard[9], null, null, new _branch(26, true), new _branch(36, false), new _branch(30, true), new _branch(36, false)));
        Nodes.Add(new node(Gameboard[9], Gameboard[10], null, Gameboard[12], new _branch(27, true), new _branch(30, true), new _branch(31, true), new _branch(33, true)));
        Nodes.Add(new node(Gameboard[10], Gameboard[11], Gameboard[12], null, new _branch(28, true), new _branch(31, true), new _branch(32, true), new _branch(34, true)));
        Nodes.Add(new node(Gameboard[11], null, null, null, new _branch(29, true), new _branch(32, true), new _branch(36, false), new _branch(36, false)));
        Nodes.Add(new node(null, Gameboard[12], null, null, new _branch(33, true), new _branch(36, false), new _branch(35, true), new _branch(36, false)));
        Nodes.Add(new node(Gameboard[12], null, null, null, new _branch(34, true), new _branch(35, true), new _branch(36, false), new _branch(36, false)));
    }
    void SetUpBranches()
    {
        Branches.Add(new branch(0, Nodes[0], Nodes[1], 36, 36, 36, 36, 1, 2));
        Branches.Add(new branch(1, Nodes[0], Nodes[3], 36, 36, 0, 3, 4, 7));
        Branches.Add(new branch(2, Nodes[1], Nodes[4], 36, 0, 36, 4, 5, 8));
        Branches.Add(new branch(3, Nodes[2], Nodes[3], 36, 1, 36, 4, 6, 7));
        Branches.Add(new branch(4, Nodes[3], Nodes[4], 1, 2, 3, 5, 7, 8));
        Branches.Add(new branch(5, Nodes[4], Nodes[5], 2, 36, 4, 36, 8, 9));
        Branches.Add(new branch(6, Nodes[2], Nodes[7], 36, 36, 3, 10, 11, 16));
        Branches.Add(new branch(7, Nodes[3], Nodes[8], 1, 3, 4, 11, 12, 17));
        Branches.Add(new branch(8, Nodes[4], Nodes[9], 2, 4, 5, 12, 13, 18));
        Branches.Add(new branch(9, Nodes[5], Nodes[10], 36, 5, 36, 13, 14, 19));
        Branches.Add(new branch(10, Nodes[6], Nodes[7], 36, 6, 36, 11, 15, 16));
        Branches.Add(new branch(11, Nodes[7], Nodes[8], 6, 7, 10, 12, 16, 17));
        Branches.Add(new branch(12, Nodes[8], Nodes[9], 7, 8, 11, 13, 17, 18));
        Branches.Add(new branch(13, Nodes[9], Nodes[10], 8, 9, 12, 14, 18, 19));
        Branches.Add(new branch(14, Nodes[10], Nodes[11], 9, 36, 13, 36, 19, 20));
        Branches.Add(new branch(15, Nodes[6], Nodes[12], 36, 36, 10, 36, 21, 36));
        Branches.Add(new branch(16, Nodes[7], Nodes[13], 6, 10, 11, 21, 22, 26));
        Branches.Add(new branch(17, Nodes[8], Nodes[14], 7, 11, 12, 22, 23, 27));
        Branches.Add(new branch(18, Nodes[9], Nodes[15], 8, 12, 13, 23, 24, 28));
        Branches.Add(new branch(19, Nodes[10], Nodes[16], 9, 13, 14, 24, 25, 29));
        Branches.Add(new branch(20, Nodes[11], Nodes[17], 36, 14, 36, 25, 36, 36));
        Branches.Add(new branch(21, Nodes[12], Nodes[13], 15, 16, 36, 22, 36, 26));
        Branches.Add(new branch(22, Nodes[13], Nodes[14], 16, 17, 21, 23, 26, 27));
        Branches.Add(new branch(23, Nodes[14], Nodes[15], 17, 18, 22, 24, 27, 28));
        Branches.Add(new branch(24, Nodes[15], Nodes[16], 18, 19, 23, 25, 28, 29));
        Branches.Add(new branch(25, Nodes[16], Nodes[17], 19, 20, 24, 36, 29, 36));
        Branches.Add(new branch(26, Nodes[13], Nodes[18], 16, 21, 22, 36, 30, 36));
        Branches.Add(new branch(27, Nodes[14], Nodes[19], 17, 22, 23, 30, 31, 33));
        Branches.Add(new branch(28, Nodes[15], Nodes[20], 18, 23, 24, 31, 32, 34));
        Branches.Add(new branch(29, Nodes[16], Nodes[21], 19, 24, 25, 32, 36, 36));
        Branches.Add(new branch(30, Nodes[18], Nodes[19], 26, 27, 36, 31, 36, 33));
        Branches.Add(new branch(31, Nodes[19], Nodes[20], 27, 28, 30, 32, 33, 34));
        Branches.Add(new branch(32, Nodes[20], Nodes[21], 28, 29, 31, 36, 34, 36));
        Branches.Add(new branch(33, Nodes[19], Nodes[22], 27, 30, 31, 36, 35, 36));
        Branches.Add(new branch(34, Nodes[20], Nodes[23], 28, 31, 32, 35, 36, 36));
        Branches.Add(new branch(35, Nodes[22], Nodes[23], 33, 34, 36, 36, 36, 36));
        Branches.Add(new branch(36, null, null, 36, 36, 36, 36, 36, 36));
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
    public void MakeMove()
    {
        if((turns.NodePlaced && turns.BranchPlaced && !gameWon) || firstTurnsOver)
        {
            SetScore();
            if(turns.turns % 2 == 0)
            {
                CheckNodes();
            }
            SetText();
            oneNode = 1;
            oneBranch = 1;
        }
    }
    public void WinGame(int i)
    {
        gameWon = true;
        TurnKeeper.text = ($"P{i} Wins!");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("GameBoard");
    }
}