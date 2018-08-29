using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    public Image splashBG; // Splash Background
    public string loadMainMenu; // Move to next scene

    IEnumerator Start()
    {
        splashBG.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(loadMainMenu);
    }

    void FadeIn()
    {
        splashBG.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    void FadeOut()
    {
        splashBG.CrossFadeAlpha(0.0f, 2.5f, false);
    }

    
}
