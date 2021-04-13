using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource buttonClick;

    public void playClickSound()
    {
        buttonClick.Play();
    }
}
