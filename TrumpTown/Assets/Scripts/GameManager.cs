using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int bank;
    public int people;
    public int peopleMax;
    public string period;

    public float targetTimer = 5.0f;
    private float currentTime;

    private string[] monthsList = {"Jan", "Feb", "Mar", "Apr",
                                    "May", "Jun", "Jul", "Aug",
                                    "Sep", "Oct", "Nov", "Dec"};
    int currentPeriod; 

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentPeriod = 0;
        ResetPeriod();


    }

    public int GetBalance()
    {
        return bank;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= targetTimer) {
            NextMonth();
            currentTime = 0;
        }
    }

    void NextMonth()
    {
        if (currentPeriod++ >= monthsList.Length) {
            currentPeriod = 0;
        };

        GameManager.Instance.period = monthsList[currentPeriod];

        
    }

    void ResetPeriod()
    {
        GameManager.Instance.period = monthsList[currentPeriod];
    }

    // TODO: Method to increment the months
    // 1. increase timer, when it reaches a set time
    // 2. go to the next month
    // 3. if it reaches the end of the array, go back to the beginning

}
