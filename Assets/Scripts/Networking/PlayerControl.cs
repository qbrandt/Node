using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl PC;

    public int selectedPieces;

    public GameObject[] allPieces;

    private void OnEnable()
    {
        if(PlayerControl.PC == null)
        {
            PlayerControl.PC = this;
        }
        else
        {
            if(PlayerControl.PC != this)
            {
                Destroy(PlayerControl.PC.gameObject);
                PlayerControl.PC = this;
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
