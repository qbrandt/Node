using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MenuScreen Menu;
    private Menu2 menuController; 

    void Start()
    {
        menuController = GameObject.FindObjectOfType<Menu2>();
    }

    public void OnMouseDown()
    {
        menuController.ChangeToMenu(Menu);
    }
}
