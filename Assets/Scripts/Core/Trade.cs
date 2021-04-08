using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Trade : MonoBehaviour
{
    private GameBoard gameboard;

    public int red = 0;
    public int green = 0;
    public int blue = 0;
    public int yellow = 0;
    public int total = 0;
    public bool isTrading = false;
    public bool canTrade = true;

    public TextMeshProUGUI TradeText;
    public GameObject TradeMenu;
    public Button MakeTrade;

    public Button RedInput;
    public Button GreenInput;
    public Button BlueInput;
    public Button YellowInput;

    public TextMeshProUGUI RedInpText;
    public TextMeshProUGUI GreenInpText;
    public TextMeshProUGUI BlueInpText;
    public TextMeshProUGUI YellowInpText;

    public GameObject RedOutput;
    public GameObject GreenOutput;
    public GameObject BlueOutput;
    public GameObject YellowOutput;

    private int playerRed;
    private int playerGreen;
    private int playerBlue;
    private int playerYellow;
    private const byte Op_TRADE_EVENT = 3;
    private const byte YELLOW_EVENT = 4;
    private const byte BLUE_EVENT = 5;
    private const byte GREEN_EVENT = 6;
    private const byte RED_EVENT = 7;
    private const byte BUY_EVENT = 8;



    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCache,
        Receivers = ReceiverGroup.All
    };


    // private PhotonView PV;



    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Op_TRADE_EVENT)
        {
            Event_OpenTradeMenu();
        }
        else if (obj.Code == YELLOW_EVENT)
        {
            Event_clickOnYellow();
        }
        else if (obj.Code == GREEN_EVENT)
        {
            Event_clickOnGreen();
        }
        else if (obj.Code == RED_EVENT)
        {
            Event_clickOnRed();
        }
        else if (obj.Code == BLUE_EVENT)
        {
            Event_clickOnBlue();
        }
        else if(obj.Code == BUY_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string str = (string)data[0];
            Event_buyResource(str);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        TradeMenu.SetActive(false);
        //PV = GetComponent<PhotonView>();
    }

    public void OpenTradeMenu()
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] {0};

                PhotonNetwork.RaiseEvent(Op_TRADE_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_OpenTradeMenu();
            }
        }

    }

    public void Event_OpenTradeMenu()
    {
        if(canTrade)
        {
            resetTradeMenu();

            if(gameboard.Player1sTurn)
            {
                playerRed = gameboard.Player1.red;
                playerGreen = gameboard.Player1.green;
                playerBlue = gameboard.Player1.blue;
                playerYellow = gameboard.Player1.yellow;
            }
            else
            {
                playerRed = gameboard.Player2.red;
                playerGreen = gameboard.Player2.green;
                playerBlue = gameboard.Player2.blue;
                playerYellow = gameboard.Player2.yellow;
            }

            if (gameboard.firstTurnsOver)
            {
                if(!isTrading)
                {
                    TradeMenu.SetActive(true);
                    isTrading = true;
                }
                else
                {
                    TradeMenu.SetActive(false);
                    isTrading = false;
                }
            }
        }
        else
        {
            //Pop up?
            Debug.Log("You can only trade once per round!");
        }

    }

    public void clickOnYellow()
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { 0 };

                PhotonNetwork.RaiseEvent(YELLOW_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_clickOnYellow();
            }
        }

    }

    public void Event_clickOnYellow()
    {
        if(playerYellow >= 1)
        {
            YellowOutput.SetActive(false);
            yellow += 1;
            playerYellow -= 1;
            total += 1;
            YellowInpText.text = yellow.ToString();
            gameboard.TradeCode += "Y";
            checkTotal();
        }
    }

    public void clickOnGreen()
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { 0 };

                PhotonNetwork.RaiseEvent(GREEN_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_clickOnGreen();
            }
        }

    }
    public void Event_clickOnGreen()
    {
        if (playerGreen >= 1)
        {
            GreenOutput.SetActive(false);
            green += 1;
            playerGreen -= 1;
            total += 1;
            GreenInpText.text = green.ToString();
            gameboard.TradeCode += "G";
            checkTotal();
        }
    }

    public void clickOnRed()
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { 0 };

                PhotonNetwork.RaiseEvent(RED_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_clickOnRed();
            }
        }

    }

    public void Event_clickOnRed()
    {
        if (playerRed >= 1)
        {
            RedOutput.SetActive(false);
            red += 1;
            playerRed -= 1;
            total += 1;
            RedInpText.text = red.ToString();
            gameboard.TradeCode += "R";
            checkTotal();
        }
    }

    public void clickOnBlue()
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] { 0 };

                PhotonNetwork.RaiseEvent(BLUE_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_clickOnYellow();
            }
        }

    }

    public void Event_clickOnBlue()
    {
        if (playerBlue >= 1)
        {
            BlueOutput.SetActive(false);
            blue += 1;
            playerBlue -= 1;
            total += 1;
            BlueInpText.text = blue.ToString();
            gameboard.TradeCode += "B";
            checkTotal();
        }
    }

    public void buyResource(string str)
    {
        if (gameboard.IsTurn)
        {
            if (PhotonNetwork.InRoom)
            {
                object[] data = new object[] {str};

                PhotonNetwork.RaiseEvent(BUY_EVENT, data, options, SendOptions.SendReliable);
                //PV.RPC("RPC_NodeClicked", RpcTarget.AllBuffered, id);
            }
            else
            {
                Event_buyResource(str);
            }
        }

    }

    public void Event_buyResource(string str)
    {
        if(total == 3)
        {
            if(gameboard.Player1sTurn)
            {
                if(str == "red")
                {
                    gameboard.Player1.red += 1;
                    gameboard.Player1.green -= green;
                    gameboard.Player1.blue -= blue;
                    gameboard.Player1.yellow -= yellow;
                    gameboard.TradeCode += "R";
                }
                else if(str == "green")
                {
                    gameboard.Player1.green += 1;
                    gameboard.Player1.red -= red;
                    gameboard.Player1.blue -= blue;
                    gameboard.Player1.yellow -= yellow;
                    gameboard.TradeCode += "G";
                }
                else if (str == "blue")
                {
                    gameboard.Player1.blue += 1;
                    gameboard.Player1.green -= green;
                    gameboard.Player1.red -= red;
                    gameboard.Player1.yellow -= yellow;
                    gameboard.TradeCode += "B";
                }
                else if (str == "yellow")
                {
                    gameboard.Player1.yellow += 1;
                    gameboard.Player1.green -= green;
                    gameboard.Player1.blue -= blue;
                    gameboard.Player1.red -= red;
                    gameboard.TradeCode += "Y";
                }
            }
            else
            {
                if (str == "red")
                {
                    gameboard.Player2.red += 1;
                    gameboard.Player2.green -= green;
                    gameboard.Player2.blue -= blue;
                    gameboard.Player2.yellow -= yellow;
                    //gameboard.TradeCode += "R";
                }
                else if (str == "green")
                {
                    gameboard.Player2.green += 1;
                    gameboard.Player2.red -= red;
                    gameboard.Player2.blue -= blue;
                    gameboard.Player2.yellow -= yellow;
                    //gameboard.TradeCode += "G";
                }
                else if (str == "blue")
                {
                    gameboard.Player2.blue += 1;
                    gameboard.Player2.green -= green;
                    gameboard.Player2.red -= red;
                    gameboard.Player2.yellow -= yellow;
                    //gameboard.TradeCode += "B";
                }
                else if (str == "yellow")
                {
                    gameboard.Player2.yellow += 1;
                    gameboard.Player2.green -= green;
                    gameboard.Player2.blue -= blue;
                    gameboard.Player2.red -= red;
                    //gameboard.TradeCode += "Y";
                }
            }
            checkTotal();
            isTrading = false;
            TradeMenu.SetActive(false);
            gameboard.SetText();
            canTrade = false;
        }
    }

    public void checkTotal()
    {
        if(total > 3)
        {
            resetTradeMenu();
            total = 0;
            gameboard.TradeCode = "";
        }
        gameboard.SetText();
    }

    public void resetTradeMenu()
    {
        gameboard.TradeCode = "";

        red = 0;
        RedInpText.text = red.ToString();
        green = 0;
        GreenInpText.text = green.ToString();
        blue = 0;
        BlueInpText.text = blue.ToString();
        yellow = 0;
        YellowInpText.text = yellow.ToString();

        if(gameboard.Player1sTurn)
        {
            playerRed = gameboard.Player1.red;
            playerGreen = gameboard.Player1.green;
            playerBlue = gameboard.Player1.blue;
            playerYellow = gameboard.Player1.yellow;
        }
        else
        {
            playerRed = gameboard.Player2.red;
            playerGreen = gameboard.Player2.green;
            playerBlue = gameboard.Player2.blue;
            playerYellow = gameboard.Player2.yellow;
        }

        RedOutput.SetActive(true);
        GreenOutput.SetActive(true);
        BlueOutput.SetActive(true);
        YellowOutput.SetActive(true);
    }
}
