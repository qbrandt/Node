using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SingleplayerButton()
    {
        SceneManager.LoadScene("SingleplayerGameSettings");
    }

    public void MultiplayerButton()
    {
        SceneManager.LoadScene("MultiplayerGameSettings");
    }

    public void HelpButton()
    {
        SceneManager.LoadScene("Help");
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
#else
            Application.Quit();
#endif
    }
}
