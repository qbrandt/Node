using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Farmer
{
    BAIRD = 0,
    FOUST = 1,
    RAGSDALE = 2,
    STEIL = 3,
    NONE = 4
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

    private Farmer GetSelectedFarmer()
    {
        var farmerRadioToggles = GameObject.FindGameObjectsWithTag("Character Radio Toggle");
        Farmer selectedFarmer = Farmer.NONE;
        for (int i = 0; i < farmerRadioToggles.Length; ++i)
        {
            if (farmerRadioToggles[i].GetComponent<Toggle>().isOn)
            {
                selectedFarmer = (Farmer)i;
            }
        }
        return selectedFarmer;
    }

    private string GenerateDefaultUsername(Farmer farmer)
    {
        string result;
        switch (farmer) {
            case Farmer.BAIRD:
                result = "Farmer Baird, PhD";
                break;
            case Farmer.FOUST:
                result = "Farmer Foust, PhD";
                break;
            case Farmer.RAGSDALE:
                result = "Farmer Ragsdale, PhD";
                break;
            case Farmer.STEIL:
                result = "Farmer Steil, PhD";
                break;
            default:
                result = "Final Boss: Bjarne Stroustrup";
                break;
        }
        return result;
    }

    private bool InputIsValid()
    {
        return username != "" &&
            farmer != Farmer.NONE;
    }
    private void DisplayErrors()
    {
        // TODO: enable error message game objects
        // It's not possible to get here because I'm really good at my job, so I might just use this function to store ascii art of clippy.
    }

    public void SingleplayerPlayButton()
    {
        farmer = GetSelectedFarmer();
        username = usernameInput.text;
        if (username == "") username = GenerateDefaultUsername(farmer);
        if (goesFirstInput.isOn) goesFirst = true;
        else goesFirst = false;
        if (simpleAIInput.isOn) simpleAI = true;
        else simpleAI = false;
        if (InputIsValid())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameBoard");
        }
        else
        {
            DisplayErrors();
        }
    }

    public void MultiplayerPlayButton()
    {
        if (InputIsValid())
        {
            farmer = GetSelectedFarmer();
            username = usernameInput.text;
            if (username == "") username = GenerateDefaultUsername(farmer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameBoard");
        }
        else
        {
            DisplayErrors();
        }
    }
}
