using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trade : MonoBehaviour
{
    private GameBoard gameboard;

    public int red = 0;
    public int green = 0;
    public int blue = 0;
    public int yellow = 0;
    public int total = 0;
    public bool isTrading = false;

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

    private int p1Red;
    private int p1Green;
    private int p1Blue;
    private int p1Yellow;
    // Start is called before the first frame update
    void Start()
    {
        gameboard = GameObject.FindObjectOfType<GameBoard>();
        TradeMenu.SetActive(false);
    }

    public void OpenTradeMenu()
    {
        resetTradeMenu();

        p1Red = gameboard.Player1.red;
        p1Green = gameboard.Player1.green;
        p1Blue = gameboard.Player1.blue;
        p1Yellow = gameboard.Player1.yellow;

        if (gameboard.Player1sTurn && gameboard.firstTurnsOver)
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

    public void clickOnYellow()
    {
        if(p1Yellow >= 1)
        {
            YellowOutput.SetActive(false);
            yellow += 1;
            p1Yellow -= 1;
            total += 1;
            YellowInpText.text = yellow.ToString();
            checkTotal();
        }
    }
    public void clickOnGreen()
    {
        if (p1Green >= 1)
        {
            GreenOutput.SetActive(false);
            green += 1;
            p1Green -= 1;
            total += 1;
            GreenInpText.text = green.ToString();
            checkTotal();
        }
    }
    public void clickOnRed()
    {
        if (p1Red >= 1)
        {
            RedOutput.SetActive(false);
            red += 1;
            p1Red -= 1;
            total += 1;
            RedInpText.text = red.ToString();
            checkTotal();
        }
    }
    public void clickOnBlue()
    {
        if (p1Blue >= 1)
        {
            BlueOutput.SetActive(false);
            blue += 1;
            p1Blue -= 1;
            total += 1;
            BlueInpText.text = blue.ToString();
            checkTotal();
        }
    }

    public void buyResource(string str)
    {
        if(total == 3)
        {
            if(str == "red")
            {
                gameboard.Player1.red += 1;
                gameboard.Player1.green -= green;
                gameboard.Player1.blue -= blue;
                gameboard.Player1.yellow -= yellow;
            }
            else if(str == "green")
            {
                gameboard.Player1.green += 1;
                gameboard.Player1.red -= red;
                gameboard.Player1.blue -= blue;
                gameboard.Player1.yellow -= yellow;
            }
            else if (str == "blue")
            {
                gameboard.Player1.blue += 1;
                gameboard.Player1.green -= green;
                gameboard.Player1.red -= red;
                gameboard.Player1.yellow -= yellow;
            }
            else if (str == "yellow")
            {
                gameboard.Player1.yellow += 1;
                gameboard.Player1.green -= green;
                gameboard.Player1.blue -= blue;
                gameboard.Player1.red -= red;
            }
            checkTotal();
            isTrading = false;
            TradeMenu.SetActive(false);
            gameboard.SetText();
        }
    }

    public void checkTotal()
    {
        if(total > 3)
        {
            resetTradeMenu();
            total = 0;
        }
        gameboard.SetText();
    }

    public void resetTradeMenu()
    {
        red = 0;
        RedInpText.text = red.ToString();
        green = 0;
        GreenInpText.text = green.ToString();
        blue = 0;
        BlueInpText.text = blue.ToString();
        yellow = 0;
        YellowInpText.text = yellow.ToString();

        p1Red = gameboard.Player1.red;
        p1Green = gameboard.Player1.green;
        p1Blue = gameboard.Player1.blue;
        p1Yellow = gameboard.Player1.yellow;

        RedOutput.SetActive(true);
        GreenOutput.SetActive(true);
        BlueOutput.SetActive(true);
        YellowOutput.SetActive(true);
    }
}
