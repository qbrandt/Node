using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Farmer
{
    BAIRD = 0,
    FOUST= 1,
    RAGSDALE = 2,
    STEIL = 3
}

public class GameSettingsButtonBehaviour : MonoBehaviour
{
    public static Farmer farmer;
    public InputField usernameInput;
    public static string username;
    public Toggle goesFirstInput;
    public static bool goesFirst;
    public Toggle simpleAIInput;
    public static bool simpleAI;
        
    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 2.0");
    }

    public void PlayButton()
    {
        if (username == "") // no username
        {
            // produce error message!
        }
        else // valid input
        {
            var farmerRadioToggles = GameObject.FindGameObjectsWithTag("Character Radio Toggle");
            for (int i = 0; i < farmerRadioToggles.Length; ++i)
            {
                if (farmerRadioToggles[i].GetComponent<Toggle>().isOn)
                {
                    farmer = (Farmer)i;
                }
            }
            username = usernameInput.text;
            if (goesFirstInput.isOn) goesFirst = true;
            else goesFirst = false;
            if (simpleAIInput.isOn) simpleAI = true;
            else simpleAI = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameBoard");
        }
    }
}
