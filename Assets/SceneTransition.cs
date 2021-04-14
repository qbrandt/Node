using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//photon includes!
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.IO;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    private static float transitionTime = 1.75f;

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(UseTransition(transition, sceneName));
    }

    public void TransitionToGameBoardWithPhoton()
    {
        StartCoroutine(UsePhotonTransition(transition, "GameBoard"));
    }

    public static IEnumerator UseTransition(Animator transition, string sceneName)
    {
        transition.SetTrigger("StartBarnWipe");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    public static IEnumerator UsePhotonTransition(Animator transition, string sceneName)
    {
        transition.SetTrigger("StartBarnWipe");

        yield return new WaitForSeconds(transitionTime);

        PhotonNetwork.LoadLevel(2);
    }

}
