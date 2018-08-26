using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

    public Slider bar;
    public Image Fill;
    //public AudioSource sound;

    float timeTaken;
    bool IsPlaying;

    Color fillStartColor;
    Color FillEndColor;

	// Use this for initialization
	void Start () {
        bar.value = bar.maxValue;

        fillStartColor = Fill.color;
        FillEndColor = Color.red;
    }
	
	// Update is called once per frame
	void Update () {
		if (IsPlaying)
        {
            timeTaken += Time.deltaTime;

            bar.value = bar.maxValue - timeTaken;
            Fill.color = Color.Lerp(FillEndColor, fillStartColor, bar.value / bar.maxValue);

            if (timeTaken >= bar.maxValue)
            {
                //FindObjectOfType<GameController>().TimesUp();
                IsPlaying = false;
            }
        }
	}

    public void RoundStarted()
    {
        timeTaken = 0;
        IsPlaying = true;
        //sound.time = 1.0f;
        //sound.Play();
    }

    public void StartOfRoundAnimation()
    {
        bar.value = bar.maxValue;
        Fill.color = fillStartColor;
    }

    public void SelectionMade()
    {
        IsPlaying = false;
        //sound.Stop();
    }

    public float getValue()
    {
        return bar.value;
    }
}
