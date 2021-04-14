using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSettingsButtonBehaviour : MonoBehaviour
{
    
    public InputField usernameInput;

    public Toggle playerGoesFirstInput;
    public Toggle simpleAIInput;

    public GameObject errorMessage;

    public void Start()
    {
        //retain previous values, especially the username
        /*
        usernameInput.text = username;
        playerGoesFirstInput.isOn = playerGoesFirst;
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
        return !string.IsNullOrEmpty(GameInformation.username) &&
            GameInformation.farmer != Farmer.NONE;
    }
    private void DisplayErrors()
    {
        errorMessage.SetActive(true);
        // TODO: enable error message game objects
        // It's not possible to get here because I'm really good at my job, so the function is empty.
    }

    public void SingleplayerPlayButton()
    {
        GameInformation.farmer = GetSelectedFarmer();
        GameInformation.username = usernameInput.text;
        GameInformation.username = string.IsNullOrEmpty(GameInformation.username) ?  GenerateDefaultUsername(GameInformation.farmer) : GameInformation.username;
        GameInformation.Player1Username = GameInformation.username;
        GameInformation.Player1Farmer = GameInformation.farmer;
        GameInformation.Player2Username = "AI";
        GameInformation.Player2Farmer = GameInformation.farmer == Farmer.RAGSDALE ? Farmer.BAIRD : Farmer.RAGSDALE;
        GameInformation.playerGoesFirst = playerGoesFirstInput.isOn;
        GameInformation.simpleAI = simpleAIInput.isOn;
        if (InputIsValid())
        {
            SceneManager.LoadScene("GameBoard");
        }
        else
        {
            DisplayErrors();
        }
    }

    public void MultiplayerPlayButton()
    {
        GameInformation.farmer = GetSelectedFarmer();
        GameInformation.username = usernameInput.text;

        if (InputIsValid())
        {
            errorMessage.SetActive(false);
            GameObject.FindObjectOfType<UserName>().OnClick_SetUserName();
            GameObject.FindObjectOfType<CustomNames>().OnClick_NameButton();
            //SceneManager.LoadScene("NetworkingOptions");
        }
        else
        {
            DisplayErrors();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMultiplayerGameSettings()
    {
        SceneManager.LoadScene("MultiplayerGameSettings");
    }

    public void LoadSelectHost()
    {
        SceneManager.LoadScene("SelectHost");
    }

    public void LoadNetworkingOptions()
    {
        SceneManager.LoadScene("NetworkingOptions");
    }
}
