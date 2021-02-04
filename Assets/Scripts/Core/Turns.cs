using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turns : MonoBehaviour
{
    private GameBoard gameboard;
    public TextMeshProUGUI TurnKeeper;
    public int nodeCost = 2;
    public int branchCost = 1;
    public int turns = 0;
    public int curBranch;
    public bool EndOfStartPhase = false;
    public bool JustStarting = true;
    public bool NodePlaced;
    public bool BranchPlaced;
    public SpriteRenderer BranchRenderer;

    // Start is called before the first frame update
    public void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        TurnKeeper.text = "P1";
        TurnKeeper.color = gameboard.Orange;
        NodePlaced = false;
        BranchPlaced = false;
        curBranch = 0;
    }

    public void NodeClicked(SpriteRenderer spriteRenderer, int id)
    {
        if(!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
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
                        gameboard.Player1.score -= 1;
                        gameboard.Player1.green += nodeCost;
                        gameboard.Player1.yellow += nodeCost;
                        gameboard.SetText();
                        gameboard.Nodes[id].newNode = false;
                        gameboard.curNodes.Remove(gameboard.Nodes[id]);
                        gameboard.updateBranches();
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
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
                        gameboard.Player2.score -= 1;
                        gameboard.Player2.green += nodeCost;
                        gameboard.Player2.yellow += nodeCost;
                        gameboard.SetText();
                        gameboard.Nodes[id].newNode = false;
                        gameboard.curNodes.Remove(gameboard.Nodes[id]);
                        gameboard.updateBranches();
                    }
                }
            }
            else
            {
                // FIRST MOVES - NODES
                if(gameboard.Player1sTurn)
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
                            gameboard.Player1.score -= 1;
                            NodePlaced = false;
                            gameboard.oneNode = 1;
                            gameboard.Nodes[id].newNode = false;
                            gameboard.curNodes.Remove(gameboard.Nodes[id]);
                            gameboard.updateBranches();
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
                            gameboard.Player2.score -= 1;
                            NodePlaced = false;
                            gameboard.oneNode = 1;
                            gameboard.Nodes[id].newNode = false;
                            gameboard.curNodes.Remove(gameboard.Nodes[id]);
                            gameboard.updateBranches();
                        }
                    }
                }
            }
        }
    }

    public void MoveMade()
    {        
        if(!gameboard.gameWon)
        {
            if((NodePlaced && BranchPlaced)|| gameboard.firstTurnsOver)
            {
                turns++;
                if (!EndOfStartPhase)
                {
                    if (turns >= 4)
                    {
                        Debug.Log("End of start");
                        gameboard.firstTurnsOver = true;
                        gameboard.MakeMove();
                        EndOfStartPhase = true;
                    }
                }

                if (gameboard.firstTurnsOver)
                {
                    if (JustStarting)
                    {
                        turns--;
                        TurnKeeper.text = "P1";
                        TurnKeeper.color = gameboard.Orange;
                        gameboard.Player1sTurn = true;
                        gameboard.Player2sTurn = false;
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
                    Debug.Log(turns);
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
                        gameboard.MakeMove();
                    }
                }
            }

            if(NodePlaced)
            {
                BranchPlaced = false;
            }
            if(BranchPlaced)
            {
                NodePlaced = false;
            }
        }
    }

    public void BranchClicked(SpriteRenderer spriteRenderer, int id)
    {
        if (!gameboard.gameWon)
        {
            if (gameboard.firstTurnsOver)
            {
                if (gameboard.Player1sTurn)
                {
                    if (gameboard.Branches[id].node1.player == 1 || gameboard.Branches[id].node2.player == 1)
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple && gameboard.Player1.red >= branchCost && gameboard.Player1.blue >= branchCost)
                        {
                            spriteRenderer.color = gameboard.Orange;
                            gameboard.Player1.red -= branchCost;
                            gameboard.Player1.blue -= branchCost;
                            gameboard.Branches[id].newBranch = true;
                            gameboard.curBranches.Add(gameboard.Branches[id]);
                            gameboard.SetText();
                        }
                        else if (spriteRenderer.color == gameboard.Orange && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = Color.black;
                            gameboard.Player1.red += branchCost;
                            gameboard.Player1.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.curBranches.Remove(gameboard.Branches[id]);
                            gameboard.SetText();
                        }
                    }
                }
                else if (gameboard.Player2sTurn)
                {
                    if (gameboard.Branches[id].node1.player == 2 || gameboard.Branches[id].node2.player == 2)
                    {
                        if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple && gameboard.Player2.red >= branchCost && gameboard.Player2.blue >= branchCost)
                        {
                            spriteRenderer.color = gameboard.Purple;
                            gameboard.Player1.red -= branchCost;
                            gameboard.Player1.blue -= branchCost;
                            gameboard.Branches[id].newBranch = true;
                            gameboard.curBranches.Add(gameboard.Branches[id]);
                            gameboard.SetText();
                        }
                        else if (spriteRenderer.color == gameboard.Purple && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = gameboard.Purple;
                            gameboard.Player1.red += branchCost;
                            gameboard.Player1.blue += branchCost;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.curBranches.Remove(gameboard.Branches[id]);
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
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                curBranch = id;
                                gameboard.Branches[id].newBranch = true;
                                gameboard.curBranches.Add(gameboard.Branches[id]);
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
                                BranchPlaced = false;
                                gameboard.oneBranch = 1;
                                gameboard.Branches[id].newBranch = false;
                                gameboard.curBranches.Remove(gameboard.Branches[id]);
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
                                BranchPlaced = true;
                                gameboard.oneBranch = 0;
                                curBranch = id;
                                gameboard.Branches[id].newBranch = true;
                                gameboard.curBranches.Add(gameboard.Branches[id]);
                            }
                        }
                    }
                    else if(gameboard.oneBranch == 0)
                    {
                        if (spriteRenderer.color == gameboard.Purple && gameboard.Branches[id].owned == false)
                        {
                            spriteRenderer.color = Color.black;
                            BranchPlaced = false;
                            gameboard.oneBranch = 1;
                            gameboard.Branches[id].newBranch = false;
                            gameboard.curBranches.Remove(gameboard.Branches[id]);
                        }
                    }
                }
            }

        }
    }
}