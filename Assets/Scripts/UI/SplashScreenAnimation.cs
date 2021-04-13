using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenAnimation : MonoBehaviour
{

    private bool finished = false;

    public CanvasGroup teamLogoCanvasGroup;
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

    IEnumerator DoFade()
    {
        yield return fadeIn(teamLogoCanvasGroup);
        teamLogoMoo.Play();
        yield return new WaitForSeconds(2);
        yield return fadeOut(teamLogoCanvasGroup);
        //play music? (farm 2)
        yield return fadeIn(backdropCanvasGroup);
        yield return new WaitForSeconds(1);
        yield return fadeIn(gameLogoCanvasGroup);
        yield return new WaitForSeconds(1);
        yield return fadeOut(gameLogoCanvasGroup);
        yield return fadeOut(backdropCanvasGroup);

        yield return finished = true;
    }
}
