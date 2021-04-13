using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenAnimation : MonoBehaviour
{

    private bool finished = false;

    public CanvasGroup teamLogoCanvasGroup;
    public CanvasGroup skipIntroCanvasGroup;
    public CanvasGroup backdropCanvasGroup;
    public CanvasGroup gameLogoCanvasGroup;

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
            finished = true;
        }
    }
    
    private void LateUpdate()
    {
        if (finished)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
    
    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator SetAlphaToZero(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        yield return null;
    }

    IEnumerator DoFade()
    {
        yield return FadeIn(teamLogoCanvasGroup);
        teamLogoMoo.Play();
        yield return new WaitForSeconds(2);
        yield return FadeOut(teamLogoCanvasGroup);
        //play music? (farm 2)
        yield return FadeIn(backdropCanvasGroup);
        yield return SetAlphaToZero(skipIntroCanvasGroup);
        yield return new WaitForSeconds(1);
        yield return FadeIn(gameLogoCanvasGroup);
        yield return new WaitForSeconds(2);
        yield return FadeOut(gameLogoCanvasGroup);
        yield return FadeOut(backdropCanvasGroup);

        yield return finished = true;
    }
}
