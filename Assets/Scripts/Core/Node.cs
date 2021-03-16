using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;



public class Node : MonoBehaviourPun
{
    public int id;
    public SpriteRenderer spriteRenderer;
    private GameBoard gameboard;
    PhotonView PV;
    public TextMeshProUGUI TurnKeeper;
    public int nodeCost = 2;
    public int branchCost = 1;
    public int turns = 0;
    public bool EndOfStartPhase = false;
    public bool JustStarting = true;
    public bool NodePlaced;
    public bool BranchPlaced;
    public SpriteRenderer BranchRenderer;


    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        //turns = GameObject.FindObjectOfType<Turns>();
        gameboard.Nodes[id].renderer = spriteRenderer;
        PV = GetComponent<PhotonView>();

    }


    //public void SetupRenderer()
    //{
    //}


    public void OnMouseDown()
    {
        //turns.NodeClicked(spriteRenderer, id);
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_NodeClicked", RpcTarget.All);
            // turns.GetComponent<PhotonView>().RPC("NodeClicked", RpcTarget.All, spriteRenderer, id);
        }
        //if (PV.IsMine)
        //    PV.RPC("RPC_NodeClicked", RpcTarget.Others, spriteRenderer, id);
    }


    //[PunRPC]
    //private void RPC_NodeClicked (SpriteRenderer spriteRenderer, int id)
    //{
    //    Turns turns = GameObject.FindObjectOfType<Turns>();
    //    turns.NodeClicked(spriteRenderer, id);

    //           // NodeClicked(spriteRenderer, id);

    //}

    [PunRPC]
    public void RPC_NodeClicked()
    {
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



    public void CheckBranches()
    {
        for (int i = 0; i < 36; i++)
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
            // if neither node is player 1 but is orange, turn black and give back resources
            else if (gameboard.Branches[i].node1.player != 1 && gameboard.Branches[i].node2.player != 1 && newRenderer.color == gameboard.Orange)
            {
                gameboard.Player1.red += branchCost;
                gameboard.Player1.blue += branchCost;
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

}
