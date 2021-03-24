using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsButtonBehaviour : MonoBehaviour
{
    public void FarmerSelection()
    {
        // disable all selection highlight game objects
        // enable selection highlight object for pressed button
        // store farmer value of the pressed button
    }
    
    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu 2.0");
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameBoard");
    }
}
