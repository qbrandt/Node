using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MenuScript : MonoBehaviour
{
    public GameObject MenuText;
    public GameObject MainPanel;
    public GameObject MultiplayerPanel;
    public GameObject SettingsPanel;
    public GameObject BackBtn;

    void Start()
    {
        ChangeToMenu(MenuScreen.MAIN);
    }

    private void ChangeToMenu(MenuScreen menu)
    {
        MainPanel.SetActive(false);
        MultiplayerPanel.SetActive(false);
        SettingsPanel.SetActive(false);

        BackBtn.SetActive(menu != MenuScreen.MAIN);

        switch (menu)
        {
            case MenuScreen.MAIN:
                MainPanel.SetActive(true);
                break;
            case MenuScreen.SINGLE:
                SceneManager.LoadScene("GameBoard");
                break;
            case MenuScreen.MULTI:
                MultiplayerPanel.SetActive(true);
                break;
            case MenuScreen.SETTINGS:
                SettingsPanel.SetActive(true);
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

    public void Settings()
    {
        ChangeToMenu(MenuScreen.SETTINGS);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var btn = eventData.pointerEnter;
        btn.GetComponent<Text>().fontSize += 5;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var btn = eventData.pointerEnter;
        btn.GetComponent<Text>().fontSize -= 5;
    }
}
