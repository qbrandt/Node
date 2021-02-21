using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDLL;

public class AI_Button : MonoBehaviour
{
    private AI AI_Script;
    public GameObject Button;


    // Start is called before the first frame update
    void Start()
    {
        AI_Script = GameObject.FindObjectOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //AI_Script.GetBoard("Something");
        //Button.GetComponentInChildren<Text>().text =  "Got Board";

        //Button.GetComponentInChildren<Text>().text = AI_Script.SmartMove("Something");
    }
}
