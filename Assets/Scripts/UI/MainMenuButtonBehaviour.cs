using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehaviour : MonoBehaviour
{
    public SceneTransition sceneTransition;

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
        sceneTransition.TransitionToScene("SingleplayerGameSettings");
    }

    public void MultiplayerButton()
    {
        sceneTransition.TransitionToScene("MultiplayerGameSettings");
    }

    public void HelpButton()
    {
        sceneTransition.TransitionToScene("Help");
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
#else
            Application.Quit();
#endif
    }
}
