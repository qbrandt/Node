using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    public Sprite farmer1;
    public Sprite farmer2;
    public Sprite farmer3;
    public Sprite farmer4;
    public GameObject Portrait1;
    public GameObject Portrait2;
    private int P1Farmer = 0;
    private int P2Farmer = 1;
    private GameBoard gameboard;
    private Vector3 big = new Vector3(1, 1, 0);
    private Vector3 small = new Vector3(.6f, .6f, 0);
    private List<Sprite> Sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        Sprites.Add(farmer1);
        Sprites.Add(farmer2);
        Sprites.Add(farmer3);
        Sprites.Add(farmer4);

        gameboard = GameObject.FindObjectOfType<GameBoard>();
        
        if(GameInformation.goesFirst)
        {
            Portrait1.transform.localScale = big;
            Portrait2.transform.localScale = small;
            Portrait2.GetComponent<Image>().color = new Color(1,1,1,.7f);
        }
        else
        {
            Portrait2.transform.localScale = big;
            Portrait1.transform.localScale = small;
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
        }

        Portrait1.GetComponent<Image>().sprite = Sprites[P1Farmer];
        Portrait2.GetComponent<Image>().sprite = Sprites[P2Farmer];
    }

    public void SwitchPortrait()
    {
        if(gameboard.Player1sTurn)
        {
            Portrait1.transform.localScale = big;
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Portrait2.transform.localScale = small;
            Portrait2.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
        }
        else if(gameboard.Player2sTurn)
        {
            Portrait1.transform.localScale = small;
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
            Portrait2.transform.localScale = big;
            Portrait2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
