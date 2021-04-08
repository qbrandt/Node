using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameSettingsButtonBehaviour : MonoBehaviour
{
    
    public InputField usernameInput;

    public Toggle goesFirstInput;
    public Toggle simpleAIInput;

    public void Start()
    {
        //retain previous values, especially the username
        /*
        usernameInput.text = username;
        goesFirstInput.isOn = goesFirst;
        simpleAIInput.isOn = simpleAI;
        */
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
        return GameInformation.username != "" &&
            GameInformation.farmer != Farmer.NONE;
    }
    private void DisplayErrors()
    {
        // TODO: enable error message game objects
        // It's not possible to get here because I'm really good at my job, so the function is empty.
    }

    public void SingleplayerPlayButton()
    {
        GameInformation.farmer = GetSelectedFarmer();
        GameInformation.username = usernameInput.text;
        if (GameInformation.username == "") GameInformation.username = GenerateDefaultUsername(GameInformation.farmer);
        GameInformation.goesFirst = goesFirstInput.isOn;
        GameInformation.simpleAI = simpleAIInput.isOn;
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
            GameInformation.farmer = GetSelectedFarmer();
            GameInformation.username = usernameInput.text;
            if (GameInformation.username == "") GameInformation.username = GenerateDefaultUsername(GameInformation.farmer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("NetworkingOptions");
        }
        else
        {
            DisplayErrors();
        }
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 2.0");
    }

    public void LoadMultiplayerGameSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MultiplayerGameSettings");
    }

    public void LoadSelectHost()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectHost");
    }

    public void LoadNetworkingOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NetworkingOptions");
    }
}
