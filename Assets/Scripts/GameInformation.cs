using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Farmer
{
    BAIRD = 0,
    FOUST = 1,
    RAGSDALE = 2,
    STEIL = 3,
    NONE = 4
}

public static class GameInformation
{
    public static Farmer farmer;
    public static string username;
    public static string alternatePlayerUsername;
    public static bool playerGoesFirst;
    public static bool simpleAI;

    public static bool gameIsSingleplayer;


}
