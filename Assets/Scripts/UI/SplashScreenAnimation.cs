using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenAnimation : MonoBehaviour
{
    public SceneTransition sceneTransition;

    private bool finished = false;

    public CanvasGroup teamLogoCanvasGroup;
    public CanvasGroup backdropCanvasGroup;
    public CanvasGroup gameLogoCanvasGroup;
    public CanvasGroup skipMessageCanvasGroup;

    public CanvasGroup sceneTransitionCanvasGroup;

    public AudioSource teamLogoMoo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (finished)
        {
            sceneTransition.TransitionToScene("MainMenu");
        }
    }
    
    IEnumerator fadeIn(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator fadeOut(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator setIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        yield return null;
    }

    IEnumerator setOut(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        yield return null;
    }

    IEnumerator DoFade()
    {
        yield return fadeIn(teamLogoCanvasGroup);
        teamLogoMoo.Play();
        yield return new WaitForSeconds(2);
        yield return fadeOut(teamLogoCanvasGroup);
        //play music? (farm 2)
        yield return fadeIn(backdropCanvasGroup);
        yield return setOut(skipMessageCanvasGroup);
        yield return setIn(sceneTransitionCanvasGroup);
        yield return new WaitForSeconds(1);
        yield return fadeIn(gameLogoCanvasGroup);
        yield return new WaitForSeconds(2);
        yield return fadeOut(gameLogoCanvasGroup);
        yield return fadeOut(backdropCanvasGroup);

        yield return finished = true;
    }
}
