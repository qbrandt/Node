using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public void backToMenu()
    {
        SceneManager.LoadScene("Menu 2.0");
    }
}
