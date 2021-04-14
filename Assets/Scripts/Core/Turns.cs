using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Turns : MonoBehaviourPunCallbacks
{
    public static Turns Instance { get; set; }
    private GameBoard gameboard;
    public TextMeshProUGUI TurnKeeper;
    public int nodeCost = 2;
    public int branchCost = 1;
    public int turns = 0;
    public bool EndOfStartPhase = false;
    public bool JustStarting = true;
    public bool NodePlaced;
    public bool BranchPlaced;
    public SpriteRenderer BranchRenderer;
    PhotonView PV;
    private const byte NODE_EVENT = 0;
    private const byte BRANCH_EVENT = 1;
    private const byte REJOIN_EVENT = 29;
    private const string LAST_NODE = "prevNode";
    private const string LAST_BRANCH = "prevBranch";
    private const string TURN_ID = "TurnTrack";
    private const string GB_EVENT = "GB_EventID";

    private static int turnIncre = 0;
    //private ExitGames.Client.Photon.Hashtable _RoomTurn = new ExitGames.Client.Photon.Hashtable();

    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.All,
        TargetActors = null,
        InterestGroup = 0
    };

    public override void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;

    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == NODE_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            int id = (int)data[0];
            Event_NodeClicked(id);
        }
        else if (obj.Code == BRANCH_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            int id = (int)data[0];
            Event_BranchClicked(id);
        }
        else if (obj.Code == REJOIN_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            int id = (int)data[0];

            PlayerPrefs.SetInt(TURN_ID, id);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        if(GameInformation.playerGoesFirst)
        {
            TurnKeeper.color = gameboard.Orange;
            TurnKeeper.text = GameInformation.Player1Username;
            gameboard.Player1sTurn = true;
            gameboard.Player2sTurn = false;
        }
        else
        {
            TurnKeeper.color = gameboard.Purple;
            TurnKeeper.text = GameInformation.Player2Username;
            gameboard.Player1sTurn = false;
            gameboard.Player2sTurn = true;
        }
        NodePlaced = false;
        BranchPlaced = false;
        PlayerPrefs.SetInt(TURN_ID, 1);

        PlayerPrefs.SetString("RoomName", PhotonNetwork.CurrentRoom.Name);

        //PV = GetComponent<PhotonView>();
    }



    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
    //    Debug.Log("Switched master client");
    //    Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
    //    if (PlayerPrefs.GetInt("TurnID") == 1)
    //        gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
    //}

    //public override void OnJoinedRoom()
    //{
    //    Debug.Log("Switched master client");
    //    Debug.Log($"TurnID Number = {PlayerPrefs.GetInt("TurnID")}");
    //    if (PlayerPrefs.GetInt("TurnID") == 1)
    //        gameboard.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
    //}

    public void NodeClicked(int id)
    {
        Debug.Log($"It is Farmer ID:{PlayerPrefs.GetInt("TurnID")} and {PlayerPrefs.GetInt("TurnTrack")}");
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { id };

                PhotonNetwork.RaiseEvent(NODE_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_NodeClicked(id);
            }

            PlayerPrefs.SetString(GB_EVENT, "N");
        }

    }
    public void Event_NodeClicked(int id)
    {
        var node = GameObject.FindGameObjectWithTag("Nodes").transform.GetChild(id).gameObject;
        var spriteRenderer = node.GetComponent<SpriteRenderer>();
        int player = 0;

        if (!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    player = 1;
                    if ((gameboard.Nodes[id].player == 0) &&//spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple) &&
                        ((gameboard.Nodes[id].branch1.inBounds && gameboard.Branches[gameboard.Nodes[id].branch1.id].player == 1) ||
                         (gameboard.Nodes[id].branch2.inBounds && gameboard.Branches[gameboard.Nodes[id].branch2.id].player == 1) ||
                         (gameboard.Nodes[id].branch3.inBounds && gameboard.Branches[gameboard.Nodes[id].branch3.id].player == 1) ||
                         (gameboard.Nodes[id].branch4.inBounds && gameboard.Branches[gameboard.Nodes[id].branch4.id].player == 1)))
                    {
                        if (gameboard.Player1.green >= nodeCost && gameboard.Player1.yellow >= nodeCost)
                        {
                            //spriteRenderer.color = gameboard.Orange;
                            gameboard.OrangeBaskets[id].SetActive(true);
                            gameboard.Nodes[id].player = 1;
                            gameboard.Player1.score += 1;
                            gameboard.Player1.green -= nodeCost;
                            gameboard.Player1.yellow -= nodeCost;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            gameboard.SetText();
                            PlayerPrefs.SetInt(LAST_NODE, id);
                        }
                    }
                    else if (gameboard.Nodes[id].player == 1 && gameboard.Nodes[id].owned == false)//spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
                    {
                        //spriteRenderer.color = Color.gray;
                        gameboard.OrangeBaskets[id].SetActive(false);
                        gameboard.Nodes[id].player = 0;
                        gameboard.Nodes[id].owned = false;
                        gameboard.Player1.score -= 1;
                        gameboard.Player1.green += nodeCost;
                        gameboard.Player1.yellow += nodeCost;
                        gameboard.SetText();
                        gameboard.Nodes[id].newNode = false;
                        gameboard.curNodes.Remove(gameboard.Nodes[id]);
                        CheckBranches();
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    player = 2;
                    if ((spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple) &&
                        ((gameboard.Nodes[id].branch1.inBounds && gameboard.Branches[gameboard.Nodes[id].branch1.id].player == 2) ||
                         (gameboard.Nodes[id].branch2.inBounds && gameboard.Branches[gameboard.Nodes[id].branch2.id].player == 2) ||
                         (gameboard.Nodes[id].branch3.inBounds && gameboard.Branches[gameboard.Nodes[id].branch3.id].player == 2) ||
                         (gameboard.Nodes[id].branch4.inBounds && gameboard.Branches[gameboard.Nodes[id].branch4.id].player == 2)))
                    {
                        if (gameboard.Player2.green >= nodeCost && gameboard.Player2.yellow >= nodeCost)
                        {
                            //spriteRenderer.color = gameboard.Purple;
                            gameboard.BlueBaskets[id].SetActive(true);
                            gameboard.Nodes[id].player = 2;
                            gameboard.Player2.score += 1;
                            gameboard.Player2.green -= nodeCost;
                            gameboard.Player2.yellow -= nodeCost;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            gameboard.SetText();
                            PlayerPrefs.SetInt(LAST_NODE, id);

                        }
                    }
                    else if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                    {
                        //spriteRenderer.color = Color.gray;
                        gameboard.BlueBaskets[id].SetActive(false);
                        gameboard.Nodes[id].player = 0;
                        gameboard.Nodes[id].owned = false;
                        gameboard.Player2.score -= 1;
                        gameboard.Player2.green += nodeCost;
                        gameboard.Player2.yellow += nodeCost;
                        gameboard.SetText();
                        gameboard.Nodes[id].newNode = false;
                        gameboard.curNodes.Remove(gameboard.Nodes[id]);
                        CheckBranches();
                    }
                }
            }
            else
            {
                Debug.Log("Node After Opening Moves");
                // FIRST MOVES - NODES
                if (gameboard.Player1sTurn)
                {
                    player = 1;
                    if (gameboard.oneNode == 1)
                    {
                        if (gameboard.Nodes[id].player == 0)
                        {
                            gameboard.OrangeBaskets[id].SetActive(true);
                            gameboard.Nodes[id].player = 1;
                            gameboard.Player1.score += 1;
                            NodePlaced = true;

                            if(GameInformation.playerGoesFirst)
                            {
                                if(turns == 0)
                                { 
                                    gameboard.Nodes[id].network = 1;
                                }
                                else if(turns == 3)
                                {
                                    gameboard.Nodes[id].network = 2;
                                }
                            }
                            else
                            {
                                if (turns == 1)
                                {
                                    gameboard.Nodes[id].network = 1;
                                }
                                else if (turns == 2)
                                {
                                    gameboard.Nodes[id].network = 2;
                                }
                            }
                            gameboard.oneNode = 0;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            PlayerPrefs.SetInt(LAST_NODE, id);

                        }
                    }
                    else if (gameboard.oneNode == 0)
                    {
                        if (gameboard.Nodes[id].player == 1 && gameboard.Nodes[id].owned == false)
                        {
                            gameboard.OrangeBaskets[id].SetActive(false);
                            gameboard.Nodes[id].player = 0;
                            gameboard.Nodes[id].owned = false;
                            gameboard.Player1.score -= 1;
                            NodePlaced = false;
                            gameboard.Nodes[id].network = 0;
                            gameboard.oneNode = 1;
                            gameboard.oneBranch = 1;
                            gameboard.Nodes[id].newNode = false;
                            CheckBranches();
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    player = 2;
                    if (gameboard.oneNode == 1)
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                        {
                            //spriteRenderer.color = gameboard.Purple;
                            gameboard.BlueBaskets[id].SetActive(true);
                            gameboard.Nodes[id].player = 2;
                            gameboard.Player2.score += 1;
                            NodePlaced = true;
                            gameboard.oneNode = 0;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            PlayerPrefs.SetInt(LAST_NODE, id);

                        }
                    }
                    else if (gameboard.oneNode == 0)
                    {
                        if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                        {
                            //spriteRenderer.color = Color.gray;
                            gameboard.BlueBaskets[id].SetActive(false);
                            gameboard.Nodes[id].player = 0;
                            gameboard.Nodes[id].owned = false;
                            gameboard.Player2.score -= 1;
                            NodePlaced = false;
                            gameboard.oneNode = 1;
                            gameboard.oneBranch = 1;
                            gameboard.Nodes[id].newNode = false;
                            gameboard.curNodes.Remove(gameboard.Nodes[id]);
                            CheckBranches();
                        }
                    }
                }
            }
            sendSignal(player);
        }
    }

    //No RPC needed because it is called from the gameboard MakeMove RPC
    public void MoveMade()
    {
        if (!gameboard.gameWon)
        {
            int player = 0;
            if(gameboard.Player1sTurn)
            {
                player = 1;
            }
            else
            {
                player = 2;
            }

            sendSignal(player);

            Debug.Log("Before Merge");
            checkMergeNetworks(1);
            checkMergeNetworks(2);
            setLongestNetwork();
            if ((NodePlaced && BranchPlaced) || gameboard.firstTurnsOver || gameboard.Player2sTurn)
            {
                turns++;
                Debug.Log(turns);
                if (!EndOfStartPhase)
                {
                    if (turns >= 4)
                    {
                        gameboard.firstTurnsOver = true;
                        EndOfStartPhase = true;
                    }
                }

                if (gameboard.firstTurnsOver)
                {
                    if (JustStarting)
                    {
                        //if (PhotonNetwork.InRoom)
                        //{
                        //    _RoomTurn["PlayerTurn"] = 2;
                        //    PhotonNetwork.CurrentRoom.SetCustomProperties(_RoomTurn);
                        //}
                        if(GameInformation.playerGoesFirst)
                        {
                            turns--;
                            TurnKeeper.text = GameInformation.Player2Username;
                            TurnKeeper.color = gameboard.Purple;                           
                        }
                        else
                        {
                            TurnKeeper.text = GameInformation.Player1Username;
                            TurnKeeper.color = gameboard.Orange;
                        }

                        gameboard.Player1sTurn = !GameInformation.playerGoesFirst;
                        gameboard.Player2sTurn = GameInformation.playerGoesFirst;
                        gameboard.SetText();
                        JustStarting = false;
                    }
                    else if (gameboard.Player1sTurn && !gameboard.gameWon)
                    {
                        //if (PhotonNetwork.InRoom)
                        //{
                        //    _RoomTurn["PlayerTurn"] = 2;
                        //    PhotonNetwork.CurrentRoom.SetCustomProperties(_RoomTurn);
                        //}

                       // PlayerPrefs.SetInt(TURN_ID, 2);

                        object[] data = new object[] { 2 };

                        PhotonNetwork.RaiseEvent(REJOIN_EVENT, data, options, SendOptions.SendReliable);

                        TurnKeeper.text = "P2";
                        TurnKeeper.color = gameboard.Purple;
                        gameboard.Player1sTurn = false;
                        gameboard.Player2sTurn = true;
                    }
                    else if (gameboard.Player2sTurn && !gameboard.gameWon)
                    {
                        //if (PhotonNetwork.InRoom)
                        //{
                        //    _RoomTurn["PlayerTurn"] = 1;
                        //    PhotonNetwork.CurrentRoom.SetCustomProperties(_RoomTurn);

                        //}
                        TurnKeeper.text = GameInformation.Player1Username;
                        TurnKeeper.color = gameboard.Orange;
                        gameboard.Player1sTurn = true;
                        gameboard.Player2sTurn = false;
                    }
                }
                else
                {
                    // Makes sure the first turns go as follows: P1, P2, P2, P1
                    if(GameInformation.playerGoesFirst)
                    {
                        if (turns == 1 || turns == 2)
                        {
                            //if (PhotonNetwork.InRoom)
                            //{
                            //    _RoomTurn["PlayerTurn"] = 2;
                            //    PhotonNetwork.CurrentRoom.SetCustomProperties(_RoomTurn);

                            //}

                            TurnKeeper.text = GameInformation.Player2Username;
                            TurnKeeper.color = gameboard.Purple;
                            gameboard.Player1sTurn = false;
                            gameboard.Player2sTurn = true;
                        }
                        else if (turns == 0 || turns == 3)
                        {
                            //if (PhotonNetwork.InRoom)
                            //{
                            //    _RoomTurn["PlayerTurn"] = 1;
                            //    PhotonNetwork.CurrentRoom.SetCustomProperties(_RoomTurn);

                            //}
                            TurnKeeper.text = GameInformation.Player1Username;
                            TurnKeeper.color = gameboard.Orange;
                            gameboard.Player1sTurn = true;
                            gameboard.Player2sTurn = false;
                        }
                    }
                    else
                    {
                        if (turns == 0 ||turns == 3)
                        {
                            TurnKeeper.text = GameInformation.Player2Username;
                            TurnKeeper.color = gameboard.Purple;
                            gameboard.Player1sTurn = false;
                            gameboard.Player2sTurn = true;
                        }
                        else if (turns == 1 || turns == 2)
                        {
                            TurnKeeper.text = GameInformation.Player1Username;
                            TurnKeeper.color = gameboard.Orange;
                            gameboard.Player1sTurn = true;
                            gameboard.Player2sTurn = false;
                        }
                    }
                }
                //This function is called from MakeMove and this would create a loop
                //gameboard.MakeMove();
            }

            if (NodePlaced)
            {
                BranchPlaced = false;
            }
            else if (BranchPlaced)
            {
                NodePlaced = false;
            }
        }
    }

    public void BranchClicked(int id)
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { id };

                PhotonNetwork.RaiseEvent(BRANCH_EVENT, data, options, SendOptions.SendReliable);
                // PV.RPC("RPC_BranchClicked", RpcTarget.AllBuffered, id);
                // turns.GetComponent<PhotonView>().RPC("NodeClicked", RpcTarget.All, spriteRenderer, id);
            }
            else
            {
                Event_BranchClicked(id);

            }

            PlayerPrefs.SetString(GB_EVENT, "B");

        }

    }

    public void Event_BranchClicked(int id)
    {
        //The 2 gets the branches child of gameboard
        var branch = GameObject.FindGameObjectWithTag("Branches").transform.GetChild(id).gameObject;
        int player = 0;
        var spriteRenderer = branch.GetComponent<SpriteRenderer>();
        //var fence = branch.GetComponent<GameObject>();
        // Add list of fences with id and player

        if (!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    player = 1;
                    // If branch is next to a player's node OR a branch of the same player
                    if (gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1 || adjacentBranches(gameboard.Branches[id], 1))
                    {
                        if (gameboard.Branches[id].player == 0 && gameboard.Player1.red >= branchCost && gameboard.Player1.blue >= branchCost)
                        {
                            PlayerPrefs.SetInt(LAST_BRANCH, id);
                            gameboard.OrangeFences[id].SetActive(true);
                            gameboard.Player1.red -= branchCost;
                            gameboard.Player1.blue -= branchCost;
                            gameboard.Branches[id].newBranch = true;
                            gameboard.Branches[id].player = 1;
                            gameboard.SetText();
                            //If branch is in Network1 or 2, change it's network id to match
                            if(nextToNetwork1(gameboard.Branches[id], 1))
                            {
                                //Debug.Log("Player 1 Network 1 - ADD");
                                gameboard.Branches[id].network = 1;
                            }
                            else
                            {
                                //Debug.Log("Player 1 Network 2 - ADD");
                                gameboard.Branches[id].network = 2;
                            }
                        }
                        else if (gameboard.Branches[id].player == 1 && gameboard.Branches[id].owned == false)
                        {

                            Debug.Log("Branch ID: " + id);
                            gameboard.OrangeFences[id].SetActive(false);
                            gameboard.Player1.red += branchCost;
                            gameboard.Player1.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.Branches[id].player = 0;

                            gameboard.Branches[id].network = 0;
                            CheckBranches();
                            gameboard.SetText();
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    player = 2;
                    if (gameboard.Branches[id].node1.player == 2 || gameboard.Branches[id].node2.player == 2 || adjacentBranches(gameboard.Branches[id], 2))
                    {
                        if (gameboard.Branches[id].player == 0 && gameboard.Player2.red >= branchCost && gameboard.Player2.blue >= branchCost)
                        {
                            PlayerPrefs.SetInt(LAST_BRANCH, id);
                            gameboard.BlueFences[id].SetActive(true);
                            gameboard.Player2.red -= branchCost;
                            gameboard.Player2.blue -= branchCost;
                            gameboard.Branches[id].newBranch = true;
                            gameboard.Branches[id].player = 2;
                            gameboard.SetText();
                            //If branch is in Network1 or 2, change it's network id to match
                            if (nextToNetwork1(gameboard.Branches[id], 2))
                            {
                                gameboard.Branches[id].network = 1;
                            }
                            else
                            {
                                gameboard.Branches[id].network = 2;
                            }
                        }
                        else if (gameboard.Branches[id].player == 2 && gameboard.Branches[id].owned == false)
                        {
                            gameboard.BlueFences[id].SetActive(false);
                            gameboard.Player2.red += branchCost;
                            gameboard.Player2.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.Branches[id].player = 0;

                            gameboard.Branches[id].network = 0;
                            CheckBranches();
                            gameboard.SetText();
                        }
                    }
                }

            }
            // FIRST MOVES - BRANCHES
            else
            {
                BranchRenderer = null;
                if (gameboard.Player1sTurn)
                {
                    player = 1;
                    if (gameboard.oneBranch == 1)
                    {
                        if ((gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1) && (gameboard.Branches[id].node1.newNode || gameboard.Branches[id].node2.newNode))
                        {
                            if (gameboard.Branches[id].player == 0)
                            {
                                PlayerPrefs.SetInt(LAST_BRANCH, id);
                                gameboard.OrangeFences[id].SetActive(true);
                                gameboard.Branches[id].player = 1;
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                gameboard.Branches[id].newBranch = true;

                                if(GameInformation.playerGoesFirst)
                                {
                                    if (turns == 0)
                                    {
                                        gameboard.Branches[id].network = 1;
                                    }
                                    else if (turns == 3)
                                    {
                                        gameboard.Branches[id].network = 2;
                                    }
                                }
                                else
                                {
                                    if (turns == 1)
                                    {
                                        gameboard.Branches[id].network = 1;
                                    }
                                    else if (turns == 2)
                                    {
                                        gameboard.Branches[id].network = 2;
                                    }
                                }
                            }
                        }
                    }
                    else if (gameboard.oneBranch == 0)
                    {
                        if (gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1)
                        {
                            if (gameboard.Branches[id].player == 1 && gameboard.Branches[id].owned == false)
                            {
                                gameboard.OrangeFences[id].SetActive(false);
                                gameboard.Branches[id].player = 0;
                                BranchPlaced = false;
                                gameboard.oneBranch = 1;
                                gameboard.Branches[id].newBranch = false;
                                CheckBranches();
                                gameboard.Branches[id].network = 0;
                            }
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    player = 2;
                    if (gameboard.oneBranch == 1)
                    {
                        if ((gameboard.Branches[id].node1.player == 2 || gameboard.Branches[id].node2.player == 2) && (gameboard.Branches[id].node1.newNode || gameboard.Branches[id].node2.newNode))
                        {
                            if (gameboard.Branches[id].player == 0)
                            {
                                PlayerPrefs.SetInt(LAST_BRANCH, id);
                                gameboard.BlueFences[id].SetActive(true);
                                gameboard.Branches[id].player = 2;
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                gameboard.Branches[id].newBranch = true;

                                //Add first nodes to networks
                                if(GameInformation.playerGoesFirst)
                                {
                                    if (turns == 1)
                                    {
                                        gameboard.Branches[id].network = 1;
                                    }
                                    else if (turns == 2)
                                    {
                                        gameboard.Branches[id].network = 2;
                                    }
                                }
                                else
                                {
                                    if (turns == 0)
                                    {
                                        gameboard.Branches[id].network = 1;
                                    }
                                    else if (turns == 3)
                                    {
                                        gameboard.Branches[id].network = 2;
                                    }
                                }
                            }
                        }
                    }
                    else if(gameboard.oneBranch == 0)
                    {
                        if (gameboard.Branches[id].player == 2 && gameboard.Branches[id].owned == false)//spriteRenderer.color == gameboard.Purple && gameboard.Branches[id].owned == false)
                        {
                            //spriteRenderer.color = Color.black;
                            gameboard.BlueFences[id].SetActive(false);
                            gameboard.Branches[id].player = 0;
                            BranchPlaced = false;
                            gameboard.oneBranch = 1;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.Branches[id].network = 0;
                            CheckBranches();
                        }
                    }
                }
            }
            if(gameboard.firstTurnsOver)
                sendSignal(player);
        }
    }

    public void CheckBranches()
    {
        for(int i = 0; i < 36; i++)
        {
            SpriteRenderer newRenderer = gameboard.BranchObjects[i].GetComponent<SpriteRenderer>();
            //If either of the branch's two nodes are player 1, set that variable
            if ((gameboard.Branches[i].node1.player == 1 && newRenderer.color == gameboard.Orange) ||
               (gameboard.Branches[i].node2.player == 1 && newRenderer.color == gameboard.Orange))
            {
                gameboard.Branches[i].player = 1;
            }
            //If either of the branch's two nodes are player 2, set that variable
            else if ((gameboard.Branches[i].node1.player == 2 && newRenderer.color == gameboard.Purple) ||
                   (gameboard.Branches[i].node1.player == 2 && newRenderer.color == gameboard.Purple))
            {
                gameboard.Branches[i].player = 2;
            }
            //If neither node is player 1 but is adjacent to another branch, set that variable
            else if (gameboard.Branches[i].node1.player != 1 && gameboard.Branches[i].node2.player != 1 && newRenderer.color == gameboard.Orange &&
                     adjacentBranches(gameboard.Branches[i], 1))
            {
                gameboard.Branches[i].player = 1;
            }
            //If neither node is player 2 but is adjacent to another branch, set that variable
            else if (gameboard.Branches[i].node1.player != 2 && gameboard.Branches[i].node2.player != 2 && newRenderer.color == gameboard.Purple &&
                     adjacentBranches(gameboard.Branches[i], 2))
            {
                gameboard.Branches[i].player = 2;
            }
            // if neither node is player 1 but is orange, turn black and give back resources IF after starting phase
            else if(gameboard.Branches[i].node1.player != 1 && gameboard.Branches[i].node2.player != 1 && newRenderer.color == gameboard.Orange)
            {
                if(EndOfStartPhase)
                {
                    gameboard.Player1.red += branchCost;
                    gameboard.Player1.blue += branchCost;
                }
                gameboard.Branches[i].owned = false;
                gameboard.Branches[i].player = 0;
                newRenderer.color = Color.black;
            }
            // if neither node is player 2 but is purple, turn black and give back resources
            else if (gameboard.Branches[i].node1.player != 2 && gameboard.Branches[i].node2.player != 2 && newRenderer.color == gameboard.Purple)
            {
                gameboard.Player2.red += branchCost;
                gameboard.Player2.blue += branchCost;
                gameboard.Branches[i].owned = false;
                gameboard.Branches[i].player = 0;
                newRenderer.color = Color.black;
            }
        }
    }

    public bool adjacentBranches(GameBoard.branch branch, int player)
    {
        bool nextToNode = false;

        if ((gameboard.Branches[branch.branch1] != null && branch.branch1 != -1 && gameboard.Branches[branch.branch1].player == player) ||
            (gameboard.Branches[branch.branch2] != null && branch.branch2 != -1 && gameboard.Branches[branch.branch2].player == player) ||
            (gameboard.Branches[branch.branch3] != null && branch.branch3 != -1 && gameboard.Branches[branch.branch3].player == player) ||
            (gameboard.Branches[branch.branch4] != null && branch.branch4 != -1 && gameboard.Branches[branch.branch4].player == player) ||
            (gameboard.Branches[branch.branch5] != null && branch.branch5 != -1 && gameboard.Branches[branch.branch5].player == player) ||
            (gameboard.Branches[branch.branch6] != null && branch.branch6 != -1 && gameboard.Branches[branch.branch6].player == player))
        {
            nextToNode = true;
        }

        return nextToNode;
    }

    public bool nextToNetwork1(GameBoard.branch branch, int player)
    {
        bool exists = false;
        //if any of the surrounding branches of this branch is part of a network for P1, add it to it.
        if (gameboard.Branches[branch.branch1] != null && gameboard.Branches[branch.branch1].network == 1 && gameboard.Branches[branch.branch1].player == player ||
            gameboard.Branches[branch.branch2] != null && gameboard.Branches[branch.branch2].network == 1 && gameboard.Branches[branch.branch2].player == player ||
            gameboard.Branches[branch.branch3] != null && gameboard.Branches[branch.branch3].network == 1 && gameboard.Branches[branch.branch3].player == player ||
            gameboard.Branches[branch.branch4] != null && gameboard.Branches[branch.branch4].network == 1 && gameboard.Branches[branch.branch4].player == player ||
            gameboard.Branches[branch.branch5] != null && gameboard.Branches[branch.branch5].network == 1 && gameboard.Branches[branch.branch5].player == player ||
            gameboard.Branches[branch.branch6] != null && gameboard.Branches[branch.branch6].network == 1 && gameboard.Branches[branch.branch6].player == player)
        {
            exists = true;
        }

        return exists;
    }

    public void checkMergeNetworks(int player)
    {
        for(int i = 0; i < 36; i++)
        {
            if(gameboard.Branches[i].player == player && gameboard.Branches[i].network == 1)
            {
                if(gameboard.Branches[gameboard.Branches[i].branch1] != null && gameboard.Branches[gameboard.Branches[i].branch1].network == 2 && gameboard.Branches[gameboard.Branches[i].branch1].player == player ||
                   gameboard.Branches[gameboard.Branches[i].branch2] != null && gameboard.Branches[gameboard.Branches[i].branch2].network == 2 && gameboard.Branches[gameboard.Branches[i].branch2].player == player ||
                   gameboard.Branches[gameboard.Branches[i].branch3] != null && gameboard.Branches[gameboard.Branches[i].branch3].network == 2 && gameboard.Branches[gameboard.Branches[i].branch3].player == player ||
                   gameboard.Branches[gameboard.Branches[i].branch4] != null && gameboard.Branches[gameboard.Branches[i].branch4].network == 2 && gameboard.Branches[gameboard.Branches[i].branch4].player == player ||
                   gameboard.Branches[gameboard.Branches[i].branch5] != null && gameboard.Branches[gameboard.Branches[i].branch5].network == 2 && gameboard.Branches[gameboard.Branches[i].branch5].player == player ||
                   gameboard.Branches[gameboard.Branches[i].branch6] != null && gameboard.Branches[gameboard.Branches[i].branch6].network == 2 && gameboard.Branches[gameboard.Branches[i].branch6].player == player)
                {
                    mergeNetworks(player);
                    break;
                }
            }
        }
    }

    public void mergeNetworks(int player)
    {
        for(int i = 0; i < 36; i++)
        {
            if(gameboard.Branches[i] != null && gameboard.Branches[i].network == 2 && gameboard.Branches[i].player == player)
            {
                gameboard.Branches[i].network = 1;
            }
        }
    }

    public void setLongestNetwork()
    {
        int p1n1 = 0;
        int p1n2 = 0;
        int p2n1 = 0;
        int p2n2 = 0;
        for(int i = 0; i < 36; i++)
        {
            if(gameboard.Branches[i].network == 1 && gameboard.Branches[i].player == 1)
            {
                p1n1 += 1;
            }
            else if (gameboard.Branches[i].network == 2 && gameboard.Branches[i].player == 1)
            {
                p1n2 += 1;
            }
            else if (gameboard.Branches[i].network == 1 && gameboard.Branches[i].player == 2)
            {
                p2n1 += 1;
            }
            else if (gameboard.Branches[i].network == 2 && gameboard.Branches[i].player == 2)
            {
                p2n2 += 1;
            }
        }

        //Debug.Log(p1n1);
        //Debug.Log(p1n2);
        //Debug.Log(p2n1);
        //Debug.Log(p2n2);

        if(p1n1 >= p1n2)
        {
            gameboard.P1_LongestNetwork = p1n1;
        }
        else
        {
            gameboard.P1_LongestNetwork = p1n2;
        }

        if(p2n1 >= p2n2)
        {
            gameboard.P2_LongestNetwork = p2n1;
        }
        else
        {
            gameboard.P2_LongestNetwork = p2n2;
        }
        gameboard.SetScore();
    }
    public void sendSignal(int player)
    {
        for (int i = 0; i < 24; i++)
        {
            if (gameboard.Nodes[i].player == player && gameboard.Branches[gameboard.Nodes[i].branch1.id].player == player && gameboard.Branches[gameboard.Nodes[i].branch1.id].nextToOwned == false
                && gameboard.Nodes[i].owned == true && gameboard.Branches[gameboard.Nodes[i].branch1.id].network == gameboard.Nodes[i].network)
            {
                Debug.Log("Node Network: " + gameboard.Nodes[i].network);
                gameboard.Branches[gameboard.Nodes[i].branch1.id].nextToOwned = true;
            }
            if (gameboard.Nodes[i].player == player&& gameboard.Branches[gameboard.Nodes[i].branch2.id].player == player && gameboard.Branches[gameboard.Nodes[i].branch2.id].nextToOwned == false
                && gameboard.Nodes[i].owned == true && gameboard.Branches[gameboard.Nodes[i].branch2.id].network == gameboard.Nodes[i].network)
            {
                gameboard.Branches[gameboard.Nodes[i].branch2.id].nextToOwned = true;
            }
            if (gameboard.Nodes[i].player == player && gameboard.Branches[gameboard.Nodes[i].branch3.id].player == player && gameboard.Branches[gameboard.Nodes[i].branch3.id].nextToOwned == false
                && gameboard.Nodes[i].owned == true && gameboard.Branches[gameboard.Nodes[i].branch3.id].network == gameboard.Nodes[i].network)
            {
                gameboard.Branches[gameboard.Nodes[i].branch3.id].nextToOwned = true;
            }
            if (gameboard.Nodes[i].player == player && gameboard.Branches[gameboard.Nodes[i].branch4.id].player == player && gameboard.Branches[gameboard.Nodes[i].branch4.id].nextToOwned == false
                && gameboard.Nodes[i].owned == true && gameboard.Branches[gameboard.Nodes[i].branch4.id].network == gameboard.Nodes[i].network)
            {
                gameboard.Branches[gameboard.Nodes[i].branch4.id].nextToOwned = true;
            }
        }

        FindOtherBranches(player);

        for (int j = 0; j < 36; j++)
        {
            if (gameboard.Branches[j].player == player && gameboard.Branches[j].nextToOwned == false)
            {
                Debug.Log("Delete Branch");
                gameboard.OrangeFences[j].SetActive(false);
                gameboard.Branches[j].newBranch = false;
                gameboard.Branches[j].player = 0;
                
                if(gameboard.firstTurnsOver && player == 1)
                {
                    gameboard.Player1.red += 1;
                    gameboard.Player1.blue += 1;
                }
                else if (gameboard.firstTurnsOver && player == 2)
                {
                    gameboard.Player2.red += 1;
                    gameboard.Player2.blue += 1;
                }
            }
        }

        if(gameboard.firstTurnsOver)
        {
            for (int i = 0; i < 24; i++)
            {

                if (gameboard.Nodes[i].player == player && gameboard.Nodes[i].owned == false
                && gameboard.Branches[gameboard.Nodes[i].branch1.id].player == 0
                && gameboard.Branches[gameboard.Nodes[i].branch2.id].player == 0
                && gameboard.Branches[gameboard.Nodes[i].branch3.id].player == 0
                && gameboard.Branches[gameboard.Nodes[i].branch4.id].player == 0)
                {
                    gameboard.OrangeBaskets[i].SetActive(false);
                    gameboard.Nodes[i].player = 0;
                    gameboard.Nodes[i].owned = false;
                    gameboard.Player1.score -= 1;
                    gameboard.Nodes[i].newNode = false;
                    gameboard.curNodes.Remove(gameboard.Nodes[i]);

                    if (gameboard.firstTurnsOver && player == 1)
                    {
                        gameboard.Player1.green += nodeCost;
                        gameboard.Player1.yellow += nodeCost;
                    }
                    else
                    {
                        gameboard.Player2.green += nodeCost;
                        gameboard.Player2.yellow += nodeCost;
                    }

                    gameboard.SetText();
                    CheckBranches();
                }
            }
        }

        gameboard.SetText();
        ResetNextToOwned();
    }

    public void FindOtherBranches(int player)
    {
        bool changeOccured = false;
        for (int i = 0; i < 36; i++)
        {
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch1].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch1].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch1].nextToOwned = true;
                    changeOccured = true;
                }
            }
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch2].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch2].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch2].nextToOwned = true;
                    changeOccured = true;
                }
            }
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch3].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch3].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch3].nextToOwned = true;
                    changeOccured = true;
                }
            }
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch4].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch4].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch4].nextToOwned = true;
                    changeOccured = true;
                }
            }
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch5].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch5].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch5].nextToOwned = true;
                    changeOccured = true;
                }
            }
            if (gameboard.Branches[i].nextToOwned == true && gameboard.Branches[gameboard.Branches[i].branch6].nextToOwned == false && gameboard.Branches[gameboard.Branches[i].branch6].player == player)
            {
                if (gameboard.firstTurnsOver)
                {
                    gameboard.Branches[gameboard.Branches[i].branch6].nextToOwned = true;
                    changeOccured = true;
                }
            }
        }

        if (changeOccured)
        {
            FindOtherBranches(player);
        }
    }

    public void ResetNextToOwned()
    {
        for(int i = 0; i < 36; i++)
        {
            gameboard.Branches[i].nextToOwned = false;
        }
    }

    public void SetNodeAi(int id)
    {
        //gameboard.Nodes[id].renderer.color = gameboard.Purple;
        gameboard.BlueBaskets[id].SetActive(true);
        gameboard.Nodes[id].player = 2;
        gameboard.Player2.score += 1;
        gameboard.Nodes[id].newNode = true;
        gameboard.curNodes.Add(gameboard.Nodes[id]);
        gameboard.SetText();
    }

    public void SetBranchAi(int id)
    {
        //gameboard.Branches[id].renderer.color = gameboard.Purple;
        gameboard.BlueFences[id].SetActive(true);
        if (!gameboard.firstTurnsOver)
        {
            gameboard.Branches[id].player = 2;
            BranchPlaced = true;
            gameboard.oneBranch = 0;
            gameboard.Branches[id].newBranch = true;

            if(GameInformation.playerGoesFirst)
            {
                if (turns == 0)
                {
                    gameboard.Branches[id].network = 1;
                }
                else if (turns == 3)
                {
                    gameboard.Branches[id].network = 2;
                }
            }
            else
            {
                if (turns == 1)
                {
                    gameboard.Branches[id].network = 1;
                }
                else if (turns == 2)
                {
                    gameboard.Branches[id].network = 2;
                }
            }
        }
        else
        {
            gameboard.Player2.red -= branchCost;
            gameboard.Player2.blue -= branchCost;
            gameboard.Branches[id].newBranch = true;
            gameboard.Branches[id].player = 2;
            gameboard.SetText();
            if (nextToNetwork1(gameboard.Branches[id], 2))
            {
                gameboard.Branches[id].network = 1;
            }
            else
            {
                gameboard.Branches[id].network = 2;
            }
        }
    }
}
