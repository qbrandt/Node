using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public GameObject TextEntry;

    private void Start()
    {
        TextEntry.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            TextEntry.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TextEntry.SetActive(false);
        }
    }
}
