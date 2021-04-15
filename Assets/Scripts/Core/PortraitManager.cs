using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitManager : MonoBehaviour
{
    public Sprite farmerBaird;
    public Sprite farmerFoust;
    public Sprite farmerRagsdale;
    public Sprite farmerSteil;
    public GameObject Portrait1;
    public GameObject Portrait2;
    private int P1Farmer = 0;
    private int P2Farmer = 1;
    private GameBoard gameboard;
    private Vector3 big = new Vector3(1.5F, 1.5F, 0);
    private Vector3 small = new Vector3(1f, 1f, 0);
    private List<Sprite> Sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        Sprites.Add(farmerBaird);
        Sprites.Add(farmerFoust);
        Sprites.Add(farmerRagsdale);
        Sprites.Add(farmerSteil);

        gameboard = GameObject.FindObjectOfType<GameBoard>();
        
        if(GameInformation.playerGoesFirst)
        {
            //Portrait1.transform.localScale = big;
            Portrait1.GetComponent<Animator>().Play("PhaseIn");
            Portrait2.transform.localScale = small;            
            Portrait2.GetComponent<Image>().color = new Color(1,1,1,.7f);
        }
        else
        {
            //Portrait2.transform.localScale = big;
            Portrait2.GetComponent<Animator>().Play("PhaseIn");
            Portrait1.transform.localScale = small;
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
        }

        Portrait1.GetComponent<Image>().sprite = Sprites[(int)GameInformation.Player1Farmer];
        Portrait2.GetComponent<Image>().sprite = Sprites[(int)GameInformation.Player2Farmer];
    }

    public void SwitchPortrait()
    {
        if(gameboard.Player1sTurn)
        {
            //Portrait1.transform.localScale = big;
            Portrait1.GetComponent<Animator>().Play("PhaseIn");
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //Portrait2.transform.localScale = small;
            Portrait2.GetComponent<Animator>().Play("PhaseOut");
            Portrait2.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
        }
        else if(gameboard.Player2sTurn)
        {
            //Portrait1.transform.localScale = small;
            Portrait1.GetComponent<Animator>().Play("PhaseOut");
            Portrait1.GetComponent<Image>().color = new Color(1, 1, 1, .7f);
            //Portrait2.transform.localScale = big;
            Portrait2.GetComponent<Animator>().Play("PhaseIn");
            Portrait2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
}
