using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;



public class Node : MonoBehaviourPun
{
    public int id;
    private Turns turn;
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

    //private Basket basket;
    
    public Sprite blueBasket;
    public Sprite orangeBasket;
    public Sprite greyBasket;
    public Sprite transparentBasket;

    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        turn = GameObject.FindObjectOfType<Turns>();
        gameboard.Nodes[id].renderer = spriteRenderer;
        PV = GetComponent<PhotonView>();

    }


    public void OnMouseDown()
    {
        turn.NodeClicked(id);
    }

    void OnMouseOver()
    {
        //if (player's turn, has basket resources, basket is not yet placed) {
        //  this.gameObject.GetComponent<SpriteRenderer>().sprite = TransparentBasket;
        //}
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //if (this.gameObject.GetComponent<SpriteRenderer>().sprite == TransparentBasket) {
        //    this.gameObject.GetComponent<SpriteRenderer>().sprite = none;
        //}
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
