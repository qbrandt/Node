using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MenuScreen
{
    MAIN = 0,
    SINGLE = 1,
    MULTI = 2,
    SETTINGS = 3,
    TUTORIAL = 4,
    QUIT = 5,
    CREATE = 6,
    JOIN = 7,
    USER = 8
}

public class Menu2 : MonoBehaviour
{
    public GameObject MenuText;
    public GameObject MainPanel;
    public GameObject MultiplayerPanel;
    public GameObject SettingsPanel;
    public GameObject CreateRoomPanel;
    public GameObject JoinRoomPanel;
    public GameObject UsernamePanel;

    void Start()
    {
        ChangeToMenu(MenuScreen.MAIN);
    }

    public void ChangeToMenu(MenuScreen menu)
    {
        MainPanel.SetActive(false);
        MultiplayerPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        JoinRoomPanel.SetActive(false);
        UsernamePanel.SetActive(false);

        //BackBtn.SetActive(menu != MenuScreen.MAIN);

        switch (menu)
        {
            case MenuScreen.MAIN:
                MainPanel.SetActive(true);
                break;
            case MenuScreen.SINGLE:
                SceneManager.LoadScene("SingleplayerGameSettings");
                break;
            case MenuScreen.MULTI:
                MultiplayerPanel.SetActive(true);
                break;
            case MenuScreen.TUTORIAL:
                SceneManager.LoadScene("GameBoard");
                break;
            case MenuScreen.SETTINGS:
                SettingsPanel.SetActive(true);
                break;

            case MenuScreen.CREATE:
                CreateRoomPanel.SetActive(true);
                break;
            case MenuScreen.JOIN:
                JoinRoomPanel.SetActive(true);
                break;
            case MenuScreen.USER:
                UsernamePanel.SetActive(true);
                break;
            case MenuScreen.QUIT:
                MainPanel.SetActive(true);
                // save any game data here
#if UNITY_EDITOR
#else
                Application.Quit(); 
#endif
                break;
            
        }

    }

    public void Main()
    {
        ChangeToMenu(MenuScreen.MAIN);
    }

    public void Single()
    {
        ChangeToMenu(MenuScreen.SINGLE);
    }

    public void Multiplayer()
    {
        ChangeToMenu(MenuScreen.MULTI);
    }

    public void Tutorial()
    {
        ChangeToMenu(MenuScreen.TUTORIAL);
    }

    public void Settings()
    {
        ChangeToMenu(MenuScreen.SETTINGS);
    }
    public void CreateRoom()
    {
        ChangeToMenu(MenuScreen.CREATE);
    }
    public void JoinRoom()
    {
        ChangeToMenu(MenuScreen.JOIN);
    }
    public void SetUsername()
    {
        ChangeToMenu(MenuScreen.USER);
    }

    public void Quit()
    {
        ChangeToMenu(MenuScreen.QUIT);
    }

   
}
