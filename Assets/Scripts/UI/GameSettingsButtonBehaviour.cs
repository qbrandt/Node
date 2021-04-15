using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSettingsButtonBehaviour : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public InputField usernameInput;

    public Toggle playerGoesFirstInput;
    public Toggle simpleAIInput;

    public GameObject errorMessage;

    public void Start()
    {

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
    }

    private void RemoveErrors()
    {
        errorMessage.SetActive(false);
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
            sceneTransition.TransitionToScene("GameBoard");
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
            RemoveErrors();
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
        sceneTransition.TransitionToScene("MainMenu");
    }

    public void LoadMultiplayerGameSettings()
    {
        sceneTransition.TransitionToScene("MultiplayerGameSettings");
    }

    public void LoadSelectHost()
    {
        sceneTransition.TransitionToScene("SelectHost");
    }

    public void LoadNetworkingOptions()
    {
        sceneTransition.TransitionToScene("NetworkingOptions");
    }
}
