using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuText;
    public GameObject SinglePlayerBtn;
    public GameObject MultiPlayerBtn;
    public GameObject SettingsBtn;
    public GameObject SettingsImage;
    public GameObject BackBtn;
    void Start()
    {
        SettingsImage.SetActive(false);
        BackBtn.SetActive(false);
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("GameBoard");
    }

    public void MultiPlayer()
    {
        Debug.Log("Nothing to see here!");
    }

    public void Settings()
    {
        MenuText.SetActive(false);
        SinglePlayerBtn.SetActive(false);
        MultiPlayerBtn.SetActive(false);
        SettingsBtn.SetActive(false);

        SettingsImage.SetActive(true);
        BackBtn.SetActive(true);
    }

    public void Back()
    {
        MenuText.SetActive(true);
        SinglePlayerBtn.SetActive(true);
        MultiPlayerBtn.SetActive(true);
        SettingsBtn.SetActive(true);

        SettingsImage.SetActive(false);
        BackBtn.SetActive(false);
    }
}
