using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using CustomDLL;
using Photon.Pun;

//using Photon.Pun;

public class GameBoard : MonoBehaviourPunCallbacks
{

    private AI AI_Script;
    private string PlayerMove;
    private SpriteRenderer TileRenderer;
    private SpriteRenderer NodeRenderer;
    private SpriteRenderer BranchRenderer;
    private Turns turns;
    private Trade trade;

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
    public GameObject branch37;

    public GameObject BlueFence0;

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
    public List<node> curNodes = new List<node>();
    public TextMeshProUGUI TurnKeeper;
    public string AiCode = "";
    public string MoveCode = "";
    public string TradeCode = "";
    public string GameCode = "";
    public int oneNode = 1;
    public int oneBranch = 1;
    public bool Player1sTurn = true;
    public bool Player2sTurn = false;
    public bool firstTurnsOver = false;
    public bool gameWon = false;
    public int P1_LongestNetwork = 0;
    public int P2_LongestNetwork = 0;
    public PhotonView PV;

    public static int Seed { get; set; }

    public bool IsTurn { get { return Player1sTurn == (!PhotonNetwork.InRoom || PV.IsMine); } }


    //private PhotonView PV;


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
        public bool visited = false;
        public int player = 0;
        public int id;

        public int tile1 = 100;
        public int tile2 = 100;
        public int tile3 = 100;
        public int tile4 = 100;

        public int branch1;
        public int branch2;
        public int branch3;
        public int branch4;

        public tile(int newId, Color newColor, int newDots, string newCode)
        {
            id = newId;
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
        public SpriteRenderer renderer;
        public bool owned = false;
        public bool newNode = false;
        public int player = 0;
        public int id;
        public tile tile1;
        public tile tile2;
        public tile tile3;
        public tile tile4;
        public _branch branch1;
        public _branch branch2;
        public _branch branch3;
        public _branch branch4;

        public node(int newId, tile newTile1, tile newTile2, tile newTile3, tile newTile4, _branch newBranch1, _branch newBranch2, _branch newBranch3, _branch newBranch4)
        {
            id = newId;
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
        public SpriteRenderer renderer;
        public bool owned = false;
        public bool nextToOwned = false;
        public bool newBranch = false;
        public bool edge;
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

        public branch(int newId, node newNode1, node newNode2, int newBranch1, int newBranch2, int newBranch3, int newBranch4, int newBranch5, int newBranch6, bool newEdge)
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
            edge = newEdge;
        }
    }

    void Awake()
    {
        turns = GameObject.FindObjectOfType<Turns>();
        trade = GameObject.FindObjectOfType<Trade>();

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
        BranchObjects.Add(branch37);

        SetUpBoard();
        CheckNodes();
        SetUpBranches();
        updateBranches();
        SetText();
        SetScore();
    }
    void Start()
    {
        AI_Script = GameObject.FindObjectOfType<AI>();
        PV = GetComponent<PhotonView>();
        Debug.Log($"PV in GB = {PV}");
        
        SetUpAI();
    }
    public void SetUpAI()
    {
        Debug.Log(GameCode);
        AI_Script.GameSetup(GameCode, false, true);
    }
    public void CheckNodes()
    {
        Debug.Log("Check Nodes");
        if (!gameWon)
        {
            isTileBlocked();
            updateBranches();

            if (turns.turns == 3)
            {
                firstTurnsOver = true;
            }

            //Check to see if a tile is not null, owned/who owns it, and what resource to give if it isn't blocked.
            for (int i = 0; i < 24; i++)
            {
                if (Nodes[i].tile1 != null)
                {
                    if (Nodes[i].player == 1)
                    {
                        isTileBlocked();
                        Nodes[i].newNode = false;

                        if (!Nodes[i].tile1.isBlocked && firstTurnsOver && Player2sTurn)
                        {
                            Debug.Log("GIVE THEM POINTS");
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
                        if (!Nodes[i].tile1.isBlocked && firstTurnsOver && Player1sTurn)
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
                        if (!Nodes[i].tile2.isBlocked && firstTurnsOver && Player2sTurn)
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
                        if (!Nodes[i].tile2.isBlocked && firstTurnsOver && Player1sTurn)
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
                        if (!Nodes[i].tile3.isBlocked && firstTurnsOver && Player2sTurn)
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
                        if (!Nodes[i].tile3.isBlocked && firstTurnsOver && Player1sTurn)
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
                        if (!Nodes[i].tile4.isBlocked && firstTurnsOver && Player2sTurn)
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
                        if (!Nodes[i].tile4.isBlocked && firstTurnsOver && Player1sTurn)
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

            //if (turns.turns == 3)
            //{
            //    firstResources = false;
            //}

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
                    }
                }
                if (Nodes[i].tile2 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile2.curNodes += 1;
                    }
                }
                if (Nodes[i].tile3 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile3.curNodes += 1;
                    }
                }
                if (Nodes[i].tile4 != null)
                {
                    if (Nodes[i].player == 1 || Nodes[i].player == 2)
                    {
                        Nodes[i].owned = true;
                        Nodes[i].tile4.curNodes += 1;
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


        SetText();
    }
    public void SetText()
    {
        if (!gameWon)
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
        if (!gameWon)
        {
            if (P1_LongestNetwork > P2_LongestNetwork)
            {
                Player1.longestNetwork = 2;
                Player2.longestNetwork = 0;
            }
            else if (P2_LongestNetwork > P1_LongestNetwork)
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

            if (Player1.score + Player1.longestNetwork >= 10)
            {
                GenerateMoveCode();
                WinGame(1);
            }
            else if (Player2.score + Player2.longestNetwork >= 10)
            {
                GenerateMoveCode();
                WinGame(2);
            }
        }
    }

    void SetUpBoard()
    {
        Gameboard.Add(new tile(0, Color.red, 1, "R1"));
        Gameboard.Add(new tile(1, Color.red, 2, "R2"));
        Gameboard.Add(new tile(2, Color.red, 3, "R3"));
        Gameboard.Add(new tile(3, Color.green, 1, "G1"));
        Gameboard.Add(new tile(4, Color.green, 2, "G2"));
        Gameboard.Add(new tile(5, Color.green, 3, "G3"));
        Gameboard.Add(new tile(6, Color.yellow, 1, "Y1"));
        Gameboard.Add(new tile(7, Color.yellow, 2, "Y2"));
        Gameboard.Add(new tile(8, Color.yellow, 3, "Y3"));
        Gameboard.Add(new tile(9, Color.blue, 1, "B1"));
        Gameboard.Add(new tile(10, Color.blue, 2, "B2"));
        Gameboard.Add(new tile(11, Color.blue, 3, "B3"));
        Gameboard.Add(new tile(12, Color.gray, 0, "XX"));

        //Randomize board
        Gameboard = RandomizeBoard(Gameboard);
        //Setup Tile relations for capturing logic
        {
            Gameboard[0].tile4 = 2;
            Gameboard[0].branch1 = 0;
            Gameboard[0].branch2 = 1;
            Gameboard[0].branch3 = 2;
            Gameboard[0].branch4 = 4;

            Gameboard[1].tile3 = 2;
            Gameboard[1].tile4 = 5;
            Gameboard[1].branch1 = 3;
            Gameboard[1].branch2 = 6;
            Gameboard[1].branch3 = 7;
            Gameboard[1].branch4 = 11;

            Gameboard[2].tile1 = 0;
            Gameboard[2].tile2 = 1;
            Gameboard[2].tile3 = 3;
            Gameboard[2].tile4 = 6;
            Gameboard[2].branch1 = 4;
            Gameboard[2].branch2 = 7;
            Gameboard[2].branch3 = 8;
            Gameboard[2].branch4 = 12;

            Gameboard[3].tile2 = 2;
            Gameboard[3].tile4 = 7;
            Gameboard[3].branch1 = 5;
            Gameboard[3].branch2 = 8;
            Gameboard[3].branch3 = 9;
            Gameboard[3].branch4 = 13;

            Gameboard[4].tile3 = 5;
            Gameboard[4].branch1 = 10;
            Gameboard[4].branch2 = 15;
            Gameboard[4].branch3 = 16;
            Gameboard[4].branch4 = 21;

            Gameboard[5].tile1 = 1;
            Gameboard[5].tile2 = 4;
            Gameboard[5].tile3 = 6;
            Gameboard[5].tile4 = 9;
            Gameboard[5].branch1 = 11;
            Gameboard[5].branch2 = 16;
            Gameboard[5].branch3 = 17;
            Gameboard[5].branch4 = 22;

            Gameboard[6].tile1 = 2;
            Gameboard[6].tile2 = 5;
            Gameboard[6].tile3 = 7;
            Gameboard[6].tile4 = 10;
            Gameboard[6].branch1 = 12;
            Gameboard[6].branch2 = 17;
            Gameboard[6].branch3 = 18;
            Gameboard[6].branch4 = 23;

            Gameboard[7].tile1 = 3;
            Gameboard[7].tile2 = 6;
            Gameboard[7].tile3 = 8;
            Gameboard[7].tile4 = 11;
            Gameboard[7].branch1 = 13;
            Gameboard[7].branch2 = 18;
            Gameboard[7].branch3 = 19;
            Gameboard[7].branch4 = 24;

            Gameboard[8].tile2 = 7;
            Gameboard[8].branch1 = 14;
            Gameboard[8].branch2 = 18;
            Gameboard[8].branch3 = 20;
            Gameboard[8].branch4 = 25;

            Gameboard[9].tile1 = 5;
            Gameboard[9].tile3 = 10;
            Gameboard[9].branch1 = 22;
            Gameboard[9].branch2 = 26;
            Gameboard[9].branch3 = 27;
            Gameboard[9].branch4 = 30;

            Gameboard[10].tile1 = 6;
            Gameboard[10].tile2 = 9;
            Gameboard[10].tile3 = 11;
            Gameboard[10].tile4 = 12;
            Gameboard[10].branch1 = 23;
            Gameboard[10].branch2 = 27;
            Gameboard[10].branch3 = 28;
            Gameboard[10].branch4 = 31;

            Gameboard[11].tile1 = 7;
            Gameboard[11].tile2 = 10;
            Gameboard[11].branch1 = 24;
            Gameboard[11].branch2 = 28;
            Gameboard[11].branch3 = 29;
            Gameboard[11].branch4 = 32;

            Gameboard[12].tile1 = 10;
            Gameboard[12].branch1 = 31;
            Gameboard[12].branch2 = 33;
            Gameboard[12].branch3 = 34;
            Gameboard[12].branch4 = 35;
        }

        SetUpNodes();
        //Generates string code representing the board for the AI
        GenerateCode();

        for (int i = 0; i < 13; i++)
        {
            TileRenderer = TileObjects[i].GetComponent<SpriteRenderer>();

            //Determine which sprite should be applied to each Tile
            if (Gameboard[i].color == Color.red)
            {
                if (Gameboard[i].maxNodes == 1)
                {
                    TileRenderer.sprite = Red1;
                }
                else if (Gameboard[i].maxNodes == 2)
                {
                    TileRenderer.sprite = Red2;
                }
                else
                {
                    TileRenderer.sprite = Red3;
                }
            }
            else if (Gameboard[i].color == Color.green)
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
            else if (Gameboard[i].color == Color.yellow)
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
        Nodes.Add(new node(00, null, null, null, Gameboard[0], new _branch(36, false), new _branch(36, false), new _branch(0, true), new _branch(1, true)));
        Nodes.Add(new node(01, null, null, Gameboard[0], null, new _branch(36, false), new _branch(0, true), new _branch(36, false), new _branch(2, true)));
        Nodes.Add(new node(02, null, null, null, Gameboard[1], new _branch(36, false), new _branch(36, false), new _branch(3, true), new _branch(6, true)));
        Nodes.Add(new node(03, null, Gameboard[0], Gameboard[1], Gameboard[2], new _branch(1, true), new _branch(3, true), new _branch(4, true), new _branch(7, true)));
        Nodes.Add(new node(04, Gameboard[0], null, Gameboard[2], Gameboard[3], new _branch(2, true), new _branch(4, true), new _branch(5, true), new _branch(8, true)));
        Nodes.Add(new node(05, null, null, Gameboard[3], null, new _branch(36, false), new _branch(5, true), new _branch(36, false), new _branch(9, true)));
        Nodes.Add(new node(06, null, null, null, Gameboard[4], new _branch(36, false), new _branch(36, false), new _branch(10, true), new _branch(15, true)));
        Nodes.Add(new node(07, null, Gameboard[1], Gameboard[4], Gameboard[5], new _branch(6, true), new _branch(10, true), new _branch(11, true), new _branch(16, true)));
        Nodes.Add(new node(08, Gameboard[1], Gameboard[2], Gameboard[5], Gameboard[6], new _branch(7, true), new _branch(11, true), new _branch(12, true), new _branch(17, true)));
        Nodes.Add(new node(09, Gameboard[2], Gameboard[3], Gameboard[6], Gameboard[7], new _branch(8, true), new _branch(12, true), new _branch(13, true), new _branch(18, true)));
        Nodes.Add(new node(10, Gameboard[3], null, Gameboard[7], Gameboard[8], new _branch(9, true), new _branch(13, true), new _branch(14, true), new _branch(19, true)));
        Nodes.Add(new node(11, null, null, Gameboard[8], null, new _branch(36, false), new _branch(14, true), new _branch(36, false), new _branch(20, true)));
        Nodes.Add(new node(12, null, Gameboard[4], null, null, new _branch(15, true), new _branch(36, false), new _branch(21, true), new _branch(36, false)));
        Nodes.Add(new node(13, Gameboard[4], Gameboard[5], null, Gameboard[9], new _branch(16, true), new _branch(21, true), new _branch(22, true), new _branch(26, true)));
        Nodes.Add(new node(14, Gameboard[5], Gameboard[6], Gameboard[9], Gameboard[10], new _branch(17, true), new _branch(22, true), new _branch(23, true), new _branch(27, true)));
        Nodes.Add(new node(15, Gameboard[6], Gameboard[7], Gameboard[10], Gameboard[11], new _branch(18, true), new _branch(23, true), new _branch(24, true), new _branch(28, true)));
        Nodes.Add(new node(16, Gameboard[7], Gameboard[8], Gameboard[11], null, new _branch(19, true), new _branch(24, true), new _branch(25, true), new _branch(29, true)));
        Nodes.Add(new node(17, Gameboard[8], null, null, null, new _branch(20, true), new _branch(25, true), new _branch(36, false), new _branch(36, false)));
        Nodes.Add(new node(18, null, Gameboard[9], null, null, new _branch(26, true), new _branch(36, false), new _branch(30, true), new _branch(36, false)));
        Nodes.Add(new node(19, Gameboard[9], Gameboard[10], null, Gameboard[12], new _branch(27, true), new _branch(30, true), new _branch(31, true), new _branch(33, true)));
        Nodes.Add(new node(20, Gameboard[10], Gameboard[11], Gameboard[12], null, new _branch(28, true), new _branch(31, true), new _branch(32, true), new _branch(34, true)));
        Nodes.Add(new node(21, Gameboard[11], null, null, null, new _branch(29, true), new _branch(32, true), new _branch(36, false), new _branch(36, false)));
        Nodes.Add(new node(22, null, Gameboard[12], null, null, new _branch(33, true), new _branch(36, false), new _branch(35, true), new _branch(36, false)));
        Nodes.Add(new node(23, Gameboard[12], null, null, null, new _branch(34, true), new _branch(35, true), new _branch(36, false), new _branch(36, false)));
    }
    void SetUpBranches()
    {
        Branches.Add(new branch(00, Nodes[0], Nodes[1], 36, 36, 36, 36, 1, 2, true));
        Branches.Add(new branch(01, Nodes[0], Nodes[3], 36, 36, 0, 3, 4, 7, true));
        Branches.Add(new branch(02, Nodes[1], Nodes[4], 36, 0, 36, 4, 5, 8, true));
        Branches.Add(new branch(03, Nodes[2], Nodes[3], 36, 1, 36, 4, 6, 7, true));
        Branches.Add(new branch(04, Nodes[3], Nodes[4], 1, 2, 3, 5, 7, 8, false));
        Branches.Add(new branch(05, Nodes[4], Nodes[5], 2, 36, 4, 36, 8, 9, true));
        Branches.Add(new branch(06, Nodes[2], Nodes[7], 36, 36, 3, 10, 11, 16, true));
        Branches.Add(new branch(07, Nodes[3], Nodes[8], 1, 3, 4, 11, 12, 17, false));
        Branches.Add(new branch(08, Nodes[4], Nodes[9], 2, 4, 5, 12, 13, 18, false));
        Branches.Add(new branch(09, Nodes[5], Nodes[10], 36, 5, 36, 13, 14, 19, true));
        Branches.Add(new branch(10, Nodes[6], Nodes[7], 36, 6, 36, 11, 15, 16, true));
        Branches.Add(new branch(11, Nodes[7], Nodes[8], 6, 7, 10, 12, 16, 17, false));
        Branches.Add(new branch(12, Nodes[8], Nodes[9], 7, 8, 11, 13, 17, 18, false));
        Branches.Add(new branch(13, Nodes[9], Nodes[10], 8, 9, 12, 14, 18, 19, false));
        Branches.Add(new branch(14, Nodes[10], Nodes[11], 9, 36, 13, 36, 19, 20, true));
        Branches.Add(new branch(15, Nodes[6], Nodes[12], 36, 36, 10, 36, 21, 36, true));
        Branches.Add(new branch(16, Nodes[7], Nodes[13], 6, 10, 11, 21, 22, 26, false));
        Branches.Add(new branch(17, Nodes[8], Nodes[14], 7, 11, 12, 22, 23, 27, false));
        Branches.Add(new branch(18, Nodes[9], Nodes[15], 8, 12, 13, 23, 24, 28, false));
        Branches.Add(new branch(19, Nodes[10], Nodes[16], 9, 13, 14, 24, 25, 29, false));
        Branches.Add(new branch(20, Nodes[11], Nodes[17], 36, 14, 36, 25, 36, 36, true));
        Branches.Add(new branch(21, Nodes[12], Nodes[13], 15, 16, 36, 22, 36, 26, true));
        Branches.Add(new branch(22, Nodes[13], Nodes[14], 16, 17, 21, 23, 26, 27, false));
        Branches.Add(new branch(23, Nodes[14], Nodes[15], 17, 18, 22, 24, 27, 28, false));
        Branches.Add(new branch(24, Nodes[15], Nodes[16], 18, 19, 23, 25, 28, 29, false));
        Branches.Add(new branch(25, Nodes[16], Nodes[17], 19, 20, 24, 36, 29, 36, true));
        Branches.Add(new branch(26, Nodes[13], Nodes[18], 16, 21, 22, 36, 30, 36, true));
        Branches.Add(new branch(27, Nodes[14], Nodes[19], 17, 22, 23, 30, 31, 33, false));
        Branches.Add(new branch(28, Nodes[15], Nodes[20], 18, 23, 24, 31, 32, 34, false));
        Branches.Add(new branch(29, Nodes[16], Nodes[21], 19, 24, 25, 32, 36, 36, true));
        Branches.Add(new branch(30, Nodes[18], Nodes[19], 26, 27, 36, 31, 36, 33, true));
        Branches.Add(new branch(31, Nodes[19], Nodes[20], 27, 28, 30, 32, 33, 34, false));
        Branches.Add(new branch(32, Nodes[20], Nodes[21], 28, 29, 31, 36, 34, 36, true));
        Branches.Add(new branch(33, Nodes[19], Nodes[22], 27, 30, 31, 36, 35, 36, true));
        Branches.Add(new branch(34, Nodes[20], Nodes[23], 28, 31, 32, 35, 36, 36, true));
        Branches.Add(new branch(35, Nodes[22], Nodes[23], 33, 34, 36, 36, 36, 36, true));
        Branches.Add(new branch(36, null, null, 36, 36, 36, 36, 36, 36, false));
    }
    void GenerateCode()
    {
        for (int i = 0; i < 13; i++)
        {
            AiCode += Gameboard[i].code;
        }
        //AiCode += ";";
        GameCode += AiCode;
    }
    public void GenerateMoveCode()
    {
        //Assumes player1 always goes first for now
        if (!firstTurnsOver)
        {

            if (Player1sTurn)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (Nodes[i].player == 1 && Nodes[i].owned == false)
                    {
                        MoveCode += "N" + Nodes[i].id.ToString("D2");
                    }
                }

                for (int i = 0; i < 36; i++)
                {
                    if (Branches[i].player == 1 && Branches[i].owned == false)
                    {
                        MoveCode += "B" + Branches[i].id.ToString("D2");
                    }
                }
                PlayerMove = MoveCode;
            }
            else
            {
                // AI
                if (turns.turns == 1)
                {
                    string TestAiMove = AI_Script.GetMove(PlayerMove);
                    TestAiMove += ";";
                    TranslateAiMove(TestAiMove);
                }
                else if (turns.turns == 2)
                {
                    string TestAiMove = AI_Script.GetMove("X00");
                    TestAiMove += ";";
                    TranslateAiMove(TestAiMove);
                }
            }
        }
        else
        {
            if (Player1sTurn)
            {
                if (TradeCode.CompareTo("") != 0)
                {
                    MoveCode += "+" + TradeCode;
                    TradeCode = "";
                }
                Debug.Log(MoveCode);

                for (int i = 0; i < 36; i++)
                {
                    if (Branches[i].player == 1 && Branches[i].owned == false)
                    {
                        MoveCode += "B" + Branches[i].id.ToString("D2");
                    }
                }
                for (int i = 0; i < 24; i++)
                {
                    if (Nodes[i].player == 1 && Nodes[i].owned == false)
                    {
                        MoveCode += "N" + Nodes[i].id.ToString("D2");
                    }
                }
                if (MoveCode.CompareTo("") == 0)
                {
                    MoveCode = "X00";
                }
                Debug.Log(MoveCode);
                PlayerMove = MoveCode;
            }
            else
            {
                // AI
                Debug.Log(turns.turns);
                Debug.Log(PlayerMove);
                //if (turns.turns == 3)
                //{
                //    string TestAiMove = AI_Script.GetMove("X00");
                //    TestAiMove += ";";
                //    TranslateAiMove(TestAiMove);
                //}
                //else
                //{
                //    string TestAiMove = AI_Script.GetMove(PlayerMove);
                //    TestAiMove += ";";
                //    TranslateAiMove(TestAiMove);
                //}

                string TestAiMove = AI_Script.GetMove(PlayerMove);
                TestAiMove += ";";
                TranslateAiMove(TestAiMove);
            }
        }

        if (Player1sTurn)
        {
            GameCode += MoveCode;
        }
    }
    void TranslateAiMove(string move)
    {
        string tradecode = "";
        string piece = "";
        string id = "";
        int intId;
        int index = 0;

        //While true, loop until reaching the ';' of the move
        while (true)
        {
            if (piece == ";" || piece == "X00;")
            {
                break;
            }
            else if (move[index] == '+')
            {
                //Trade
                tradecode = move.Substring(1, 3);
                var tradeFor = move[4];
                for (int i = 0; i < 3; i++)
                {
                    switch (tradecode[i])
                    {
                        case 'R':
                            Player2.red--;
                            break;
                        case 'G':
                            Player2.green--;
                            break;
                        case 'Y':
                            Player2.yellow--;
                            break;
                        case 'B':
                            Player2.blue--;
                            break;
                    };
                }
                switch (tradeFor)
                {
                    case 'R':
                        Player2.red++;
                        break;
                    case 'G':
                        Player2.green++;
                        break;
                    case 'Y':
                        Player2.yellow++;
                        break;
                    case 'B':
                        Player2.blue++;
                        break;
                };

                index += 5;
                Debug.Log($"Trade {tradecode} for {tradeFor}");
            }
            //If there is a trade
            //else if (move[0] == 'R' || move[0] == 'G' || move[0] == 'B' || move[0] == 'Y')
            //{
            //    tradecode = move.Substring(0, 3);
            //    index += 3;
            //    Debug.Log(TradeCode);
            //}
            //Not yet working perfectly
            else
            {
                for (int i = index; i < move.Length; i++)
                {
                    id = "";
                    piece = move[i].ToString();
                    if (piece == ";")
                    {
                        break;
                    }

                    id += move[i + 1];
                    id += move[i + 2];
                    i += 2;
                    if (piece == "N")
                    {
                        if (id[0].ToString() == "0")
                        {
                            id = id[1].ToString();
                        }

                        intId = System.Int16.Parse(id);
                        for (int j = 0; j < 24; j++)
                        {
                            if (intId == Nodes[j].id)
                            {
                                turns.SetNodeAi(intId);
                                break;
                            }
                        }
                    }
                    else if (piece == "B")
                    {
                        if (id[0].ToString() == "0")
                        {
                            id = id[1].ToString();
                        }

                        intId = System.Int16.Parse(id);
                        for (int j = 0; j < 36; j++)
                        {
                            if (intId == Branches[j].id)
                            {
                                turns.SetBranchAi(intId);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    List<tile> RandomizeBoard(List<tile> Gameboard)
    {
        List<tile> newGameboard = new List<tile>();
        int n = Gameboard.Count;
        int rand;
        Random.InitState(Seed);
        Seed = (int)System.DateTime.Now.Ticks;
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
        Debug.Log("IsTurn = " + IsTurn);

        if (IsTurn && PhotonNetwork.InRoom)
        {    
            PV.RPC("RPC_MakeMove", RpcTarget.All);
        }
        else
        {
            RPC_MakeMove();
        }

    }

    [PunRPC]
    public void RPC_MakeMove()
    {
        Debug.Log("MakeMove_RPC");
        if ((turns.NodePlaced && turns.BranchPlaced && !gameWon) || firstTurnsOver || Player2sTurn)
        {
            SetScore();
            MoveCode = "";
            if (!PhotonNetwork.InRoom)
            {
                GenerateMoveCode();
            }

            CheckNodes();
            SetText();
            oneNode = 1;
            oneBranch = 1;
            trade.canTrade = true;
            CheckCapture();

            turns.MoveMade();
        }

    }

    public void CheckCapture()
    {
        bool captured = false;
        for(int i = 0; i < 13; i++)
        {
            if(!Gameboard[i].captured)
            {
                //Check single capture
                if(!SingleCapture(i))
                {
                    //Check multi-capture
                    captured = MultiCapture(i);
                    if (captured)
                    {
                        for (int j = 0; j < 13; j++)
                        {

                            if (Gameboard[j].visited)
                            {
                                Debug.Log($"Captured Tile {j}");
                                Gameboard[j].captured = true;
                                Gameboard[j].isBlocked = false;
                                Gameboard[j].owned = true;
                                Gameboard[j].player = Player1sTurn ? 1 : 2;
                            }
                        }
                    }
                    for (int j = 0; j < 13; j++)
                    {
                        Gameboard[j].visited = false;
                    }
                }
                else
                {
                    Debug.Log($"Captured Tile {i}");
                    Gameboard[i].captured = true;
                    Gameboard[i].isBlocked = false;
                    Gameboard[i].owned = true;
                    Gameboard[i].player = Player1sTurn ? 1 : 2;
                }
            }
        }
    }

    public bool SingleCapture(int i)
    {
        bool captured = false;

        //if captured by player 1
        if(Branches[Gameboard[i].branch1].player == 1 && Branches[Gameboard[i].branch2].player == 1 &&
            Branches[Gameboard[i].branch3].player == 1 && Branches[Gameboard[i].branch4].player == 1)
        {
            Gameboard[i].captured = true;
            captured = true;
        }

        //if captured by player 2
        if (Branches[Gameboard[i].branch1].player == 2 && Branches[Gameboard[i].branch2].player == 2 &&
            Branches[Gameboard[i].branch3].player == 2 && Branches[Gameboard[i].branch4].player == 2)
        {
            Gameboard[i].captured = true;
            captured = true;
        }

        return captured;
    }

    public bool MultiCapture(int i)
    {
        Debug.Log($"Multicapture on Tile {i}");
        //assume true
        bool captured = true;
        int enemy = Player1sTurn ? 2 : 1;
        

        ////Check if any branch is enemy branch
        if (Branches[Gameboard[i].branch1].player == enemy || Branches[Gameboard[i].branch2].player == enemy ||
            Branches[Gameboard[i].branch4].player == enemy || Branches[Gameboard[i].branch4].player == enemy)
        {
            captured = false;
        }
        //Check if any empty spots are edge pieces
        else if ((Branches[Gameboard[i].branch1].player == 0 && Branches[Gameboard[i].branch1].edge) || (Branches[Gameboard[i].branch2].player == 0 && Branches[Gameboard[i].branch2].edge) ||
            (Branches[Gameboard[i].branch3].player == 0 && Branches[Gameboard[i].branch3].edge) || (Branches[Gameboard[i].branch4].player == 0 && Branches[Gameboard[i].branch4].edge))
        {
            captured = false;
        }
        //Check if next tile is captured
        else
        {
            Gameboard[i].visited = true;
            //if branch is empty, check the neighboring tile
            if (Branches[Gameboard[i].branch1].owned == false && Gameboard[i].tile1 != 100 && !Gameboard[Gameboard[i].tile1].visited)
            {
                if (!MultiCapture(Gameboard[i].tile1))
                {
                    captured = false;
                }
            }
            if (Branches[Gameboard[i].branch2].owned == false && Gameboard[i].tile2 != 100 && !Gameboard[Gameboard[i].tile2].visited)
            {
                if (!MultiCapture(Gameboard[i].tile2))
                {
                    captured = false;
                }
            }
            if (Branches[Gameboard[i].branch3].owned == false && Gameboard[i].tile3 != 100 && !Gameboard[Gameboard[i].tile3].visited)
            {
                if (!MultiCapture(Gameboard[i].tile3))
                {
                    captured = false;
                }
            }
            if (Branches[Gameboard[i].branch4].owned == false && Gameboard[i].tile4 != 100 && !Gameboard[Gameboard[i].tile4].visited)
            {
                if (!MultiCapture(Gameboard[i].tile4))
                {
                    captured = false;
                }
            }
        }

        return captured;
    }

    public bool RecursiveCapture(int i)
    {
        bool captured = true;

        if(i > 13)
        {
            Gameboard[i].visited = true;

            if(Gameboard[Gameboard[i].tile1].visited == false)
            {

            }
        }

        return captured;
    }
    public void WinGame(int i)
    {
        gameWon = true;
        Debug.Log(GameCode);
        TurnKeeper.text = ($"P{i} Wins!");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("GameBoard");

    }
}