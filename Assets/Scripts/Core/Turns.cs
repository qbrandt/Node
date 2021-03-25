using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Turns : MonoBehaviour
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

    // Start is called before the first frame update
    public void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        TurnKeeper.text = "P1";
        TurnKeeper.color = gameboard.Orange;
        NodePlaced = false;
        BranchPlaced = false;
        PV = GetComponent<PhotonView>();
    }

    public void NodeClicked(int id)
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                PV.RPC("RPC_NodeClicked", RpcTarget.All, id);
            }
            else
            {
                RPC_NodeClicked(id);
            }
        }
    }

    [PunRPC]
    public void RPC_NodeClicked(int id)
    {
        //The 3 gets the nodes child of gameboard
        //can change, just need to get the nodes gameobject
        var node = gameboard.gameObject.transform.GetChild(3).GetChild(id).gameObject;
        var spriteRenderer = node.GetComponent<SpriteRenderer>();

        if (!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    if ((spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple) &&
                        ((gameboard.Nodes[id].branch1.inBounds && gameboard.Branches[gameboard.Nodes[id].branch1.id].player == 1) ||
                         (gameboard.Nodes[id].branch2.inBounds && gameboard.Branches[gameboard.Nodes[id].branch2.id].player == 1) ||
                         (gameboard.Nodes[id].branch3.inBounds && gameboard.Branches[gameboard.Nodes[id].branch3.id].player == 1) ||
                         (gameboard.Nodes[id].branch4.inBounds && gameboard.Branches[gameboard.Nodes[id].branch4.id].player == 1)))
                    {
                        if (gameboard.Player1.green >= nodeCost && gameboard.Player1.yellow >= nodeCost)
                        {
                            spriteRenderer.color = gameboard.Orange;
                            gameboard.Nodes[id].player = 1;
                            gameboard.Player1.score += 1;
                            gameboard.Player1.green -= nodeCost;
                            gameboard.Player1.yellow -= nodeCost;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            gameboard.SetText();
                        }
                    }
                    else if (spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
                    {
                        spriteRenderer.color = Color.gray;
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
                    if ((spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple) &&
                        ((gameboard.Nodes[id].branch1.inBounds && gameboard.Branches[gameboard.Nodes[id].branch1.id].player == 2) ||
                         (gameboard.Nodes[id].branch2.inBounds && gameboard.Branches[gameboard.Nodes[id].branch2.id].player == 2) ||
                         (gameboard.Nodes[id].branch3.inBounds && gameboard.Branches[gameboard.Nodes[id].branch3.id].player == 2) ||
                         (gameboard.Nodes[id].branch4.inBounds && gameboard.Branches[gameboard.Nodes[id].branch4.id].player == 2)))
                    {
                        if (gameboard.Player2.green >= nodeCost && gameboard.Player2.yellow >= nodeCost)
                        {
                            spriteRenderer.color = gameboard.Purple;
                            gameboard.Nodes[id].player = 2;
                            gameboard.Player2.score += 1;
                            gameboard.Player2.green -= nodeCost;
                            gameboard.Player2.yellow -= nodeCost;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                            gameboard.SetText();
                        }
                    }
                    else if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                    {
                        spriteRenderer.color = Color.gray;
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
                // FIRST MOVES - NODES
                if (gameboard.Player1sTurn)
                {
                    if (gameboard.oneNode == 1)
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                        {
                            spriteRenderer.color = gameboard.Orange;
                            gameboard.Nodes[id].player = 1;
                            gameboard.Player1.score += 1;
                            NodePlaced = true;
                            gameboard.oneNode = 0;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                        }
                    }
                    else if (gameboard.oneNode == 0)
                    {
                        if (spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
                        {
                            spriteRenderer.color = Color.gray;
                            gameboard.Nodes[id].player = 0;
                            gameboard.Nodes[id].owned = false;
                            gameboard.Player1.score -= 1;
                            NodePlaced = false;
                            gameboard.oneNode = 1;
                            gameboard.oneBranch = 1;
                            gameboard.Nodes[id].newNode = false;
                            CheckBranches();
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    if (gameboard.oneNode == 1)
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                        {
                            spriteRenderer.color = gameboard.Purple;
                            gameboard.Nodes[id].player = 2;
                            gameboard.Player2.score += 1;
                            NodePlaced = true;
                            gameboard.oneNode = 0;
                            gameboard.Nodes[id].newNode = true;
                            gameboard.curNodes.Add(gameboard.Nodes[id]);
                        }
                    }
                    else if (gameboard.oneNode == 0)
                    {
                        if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                        {
                            spriteRenderer.color = Color.gray;
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
        }
    }



    //No RPC needed because it is called from the gameboard MakeMove RPC
    public void MoveMade()
    {
        if (!gameboard.gameWon)
        {
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
                        turns--;
                        TurnKeeper.color = gameboard.Purple;
                        gameboard.Player1sTurn = false;
                        gameboard.Player2sTurn = true;
                        gameboard.SetText();
                        JustStarting = false;
                    }
                    else if (gameboard.Player1sTurn)
                    {
                        TurnKeeper.text = "P2";
                        TurnKeeper.color = gameboard.Purple;
                        gameboard.Player1sTurn = false;
                        gameboard.Player2sTurn = true;
                    }
                    else if (gameboard.Player2sTurn)
                    {
                        TurnKeeper.text = "P1";
                        TurnKeeper.color = gameboard.Orange;
                        gameboard.Player1sTurn = true;
                        gameboard.Player2sTurn = false;
                    }
                }
                else
                {
                    // Makes sure the first turns go as follows: P1, P2, P2, P1
                    if (turns == 1 || turns == 2)
                    {
                        TurnKeeper.text = "P2";
                        TurnKeeper.color = gameboard.Purple;
                        gameboard.Player1sTurn = false;
                        gameboard.Player2sTurn = true;
                    }
                    else if (turns == 3)
                    {
                        TurnKeeper.text = "P1";
                        TurnKeeper.color = gameboard.Orange;
                        gameboard.Player1sTurn = true;
                        gameboard.Player2sTurn = false;
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
                PV.RPC("RPC_BranchClicked", RpcTarget.All, id);
                // turns.GetComponent<PhotonView>().RPC("NodeClicked", RpcTarget.All, spriteRenderer, id);
            }
            else
            {
                RPC_BranchClicked(id);
            }
        }
    }

    [PunRPC]
    public void RPC_BranchClicked(int id)
    {
        //The 2 gets the branches child of gameboard
        var branch = gameboard.gameObject.transform.GetChild(2).GetChild(id).gameObject;
        var spriteRenderer = branch.GetComponent<SpriteRenderer>();
        // Add list of fences with id and player
        //var fence = gameboard.Fences[id];

        if (!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    // If branch is next to a player's node OR a branch of the same player
                    if (gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1 || adjacentBranches(gameboard.Branches[id], 1))
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple && gameboard.Player1.red >= branchCost && gameboard.Player1.blue >= branchCost)
                        {
                            //Change sprite renderer to proper fence
                            spriteRenderer.color = gameboard.Orange;
                            //fence.SetActive(true);
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
                        else if (spriteRenderer.color == gameboard.Orange && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = Color.black;
                            gameboard.Player1.red += branchCost;
                            gameboard.Player1.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.Branches[id].player = 0;

                            checkValidBranch();
                            gameboard.Branches[id].network = 0;
                            CheckBranches();
                            gameboard.SetText();
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    if (gameboard.Branches[id].node1.player == 2 || gameboard.Branches[id].node2.player == 2 || adjacentBranches(gameboard.Branches[id], 2))
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple && gameboard.Player2.red >= branchCost && gameboard.Player2.blue >= branchCost)
                        {
                            spriteRenderer.color = gameboard.Purple;
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
                        else if (spriteRenderer.color == gameboard.Purple && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = Color.black;
                            gameboard.Player2.red += branchCost;
                            gameboard.Player2.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.Branches[id].player = 0;

                            checkValidBranch();
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
                    if (gameboard.oneBranch == 1)
                    {
                        if ((gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1) && (gameboard.Branches[id].node1.newNode || gameboard.Branches[id].node2.newNode))
                        {
                            if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                            {
                                spriteRenderer.color = gameboard.Orange;
                                gameboard.Branches[id].player = 1;
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                gameboard.Branches[id].newBranch = true;

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
                    else if (gameboard.oneBranch == 0)
                    {
                        if (gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1)
                        {
                            if (spriteRenderer.color == gameboard.Orange && gameboard.Branches[id].owned == false)
                            {
                                spriteRenderer.color = Color.black;
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
                    if (gameboard.oneBranch == 1)
                    {
                        if ((gameboard.Branches[id].node1.player == 2 || gameboard.Branches[id].node2.player == 2) && (gameboard.Branches[id].node1.newNode || gameboard.Branches[id].node2.newNode))
                        {
                            if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                            {
                                spriteRenderer.color = gameboard.Purple;
                                gameboard.Branches[id].player = 2;
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                gameboard.Branches[id].newBranch = true;

                                //Add first nodes to networks
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
                    else if(gameboard.oneBranch == 0)
                    {
                        if (spriteRenderer.color == gameboard.Purple && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = Color.black;
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

    public bool adjacentBranches(GameBoard.branch branch, int _player)
    {
        bool nextToNode = false;
        int player = _player;

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

    public void checkValidBranch()
    {
        //bool isChange = false;
        //for (int i = 0; i < 36; i++)
        //{
        //    int b1 = gameboard.Branches[i].branch1;
        //    int b2 = gameboard.Branches[i].branch2;
        //    int b3 = gameboard.Branches[i].branch3;
        //    int b4 = gameboard.Branches[i].branch4;
        //    int b5 = gameboard.Branches[i].branch5;
        //    int b6 = gameboard.Branches[i].branch6;

        //    if(gameboard.Player1sTurn)
        //    {
        //        if(!gameboard.Branches[b1].owned && gameboard.Branches[b1].player == 1 &&
        //           !gameboard.Branches[b2].owned && gameboard.Branches[b2].player == 1 &&
        //           !gameboard.Branches[b3].owned && gameboard.Branches[b3].player == 1 &&
        //           !gameboard.Branches[b4].owned && gameboard.Branches[b4].player == 1 &&
        //           !gameboard.Branches[b5].owned && gameboard.Branches[b5].player == 1 &&
        //           !gameboard.Branches[b6].owned && gameboard.Branches[b6].player == 1)
        //        {
        //            gameboard.Branches[i].resetBranch();
        //            CheckBranches();
        //            isChange = true;
        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        //if(isChange)
        //{
        //    checkValidBranch();
        //}
    }

    public void sendSignal()
    {
        for(int i = 0; i < 24; i++)
        {
            if(gameboard.Nodes[i].player == 1)
            {
            }
        }
    }

    public void resetIsOwned()
    {
        for(int i = 0; i < 36; i++)
        {
            gameboard.Branches[i].nextToOwned = false;
        }
    }

    public void SetNodeAi(int id)
    {
        gameboard.Nodes[id].renderer.color = gameboard.Purple;
        gameboard.Nodes[id].player = 2;
        gameboard.Player2.score += 1;
        gameboard.Nodes[id].newNode = true;
        gameboard.curNodes.Add(gameboard.Nodes[id]);
        gameboard.SetText();
    }
    public void SetBranchAi(int id)
    {
        gameboard.Branches[id].renderer.color = gameboard.Purple;
        if(!gameboard.firstTurnsOver)
        {
            gameboard.Branches[id].player = 2;
            BranchPlaced = true;
            gameboard.oneBranch = 0;
            gameboard.Branches[id].newBranch = true;
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
