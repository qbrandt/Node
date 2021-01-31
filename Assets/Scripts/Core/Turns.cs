using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turns : MonoBehaviour
{
    private GameBoard gameboard;
    public TextMeshProUGUI TurnKeeper;
    public int cost = 2;
    public int turns = 0;
    public bool EndOfStartPhase = false;
    public bool JustStarting = true;
    public bool TurnTaken;

    // Start is called before the first frame update
    public void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        TurnKeeper.text = "P1";
        TurnKeeper.color = gameboard.Orange;
        TurnTaken = false;
    }

    public void NodeClicked(SpriteRenderer spriteRenderer, int id)
    {
        if (gameboard.firstTurnsOver)
        {
            if (gameboard.Player1sTurn)
            {
                if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                {
                    if (gameboard.Player1.green >= cost && gameboard.Player1.yellow >= cost)
                    {
                        spriteRenderer.color = gameboard.Orange;
                        gameboard.Nodes[id].player = 1;
                        gameboard.Player1.green -= cost;
                        gameboard.Player1.yellow -= cost;
                        gameboard.SetText();
                    }
                }
                else if (spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
                {
                    spriteRenderer.color = Color.gray;
                    gameboard.Nodes[id].player = 0;
                    gameboard.Player1.green += cost;
                    gameboard.Player1.yellow += cost;
                    gameboard.SetText();
                }
            }
            else if (gameboard.Player2sTurn)
            {
                if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                {
                    if (gameboard.Player2.green >= cost && gameboard.Player2.yellow >= cost)
                    {
                        spriteRenderer.color = gameboard.Purple;
                        gameboard.Nodes[id].player = 2;
                        gameboard.Player2.green -= cost;
                        gameboard.Player2.yellow -= cost;
                        gameboard.SetText();
                    }
                }
                else if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                {
                    spriteRenderer.color = Color.gray;
                    gameboard.Nodes[id].player = 0;
                    gameboard.Player2.green += cost;
                    gameboard.Player2.yellow += cost;
                    gameboard.SetText();
                }
            }
        }
        else
        {
            if(gameboard.Player1sTurn)
            {
                if (gameboard.oneNode == 1)
                {
                    if (spriteRenderer.color != gameboard.Orange && spriteRenderer.color != gameboard.Purple)
                    {
                        spriteRenderer.color = gameboard.Orange;
                        gameboard.Nodes[id].player = 1;
                        TurnTaken = true;
                        gameboard.oneNode = 0;
                    }
                }
                else if (gameboard.oneNode == 0)
                {
                    if (spriteRenderer.color == gameboard.Orange && gameboard.Nodes[id].owned == false)
                    {
                        spriteRenderer.color = Color.gray;
                        gameboard.Nodes[id].player = 0;
                        TurnTaken = false;
                        gameboard.oneNode = 1;
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
                        TurnTaken = true;
                        gameboard.oneNode = 0;
                    }
                }
                else if (gameboard.oneNode == 0)
                {
                    if (spriteRenderer.color == gameboard.Purple && gameboard.Nodes[id].owned == false)
                    {
                        spriteRenderer.color = Color.gray;
                        gameboard.Nodes[id].player = 0;
                        TurnTaken = false;
                        gameboard.oneNode = 1;
                    }
                }
            }
        }
    }

    public void MoveMade()
    {        
        if(TurnTaken || gameboard.firstTurnsOver)
        {
            turns++;
            if (!EndOfStartPhase)
            {
                if (turns >= 4)
                {
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
        TurnTaken = false;
    }      
}