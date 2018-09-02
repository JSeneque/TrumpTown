using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int bank;
    public int people;
    public int peopleMax;
    public string period;
    public GameObject personPrefab;
    public Vector3 personStartPos = new Vector3(-5.0f, 0.25f, 18.0f);

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
        CheckForNewTownPerson();


    }

    void ResetPeriod()
    {
        GameManager.Instance.period = monthsList[currentPeriod];
    }

    public void CheckForNewTownPerson()
    {
        if (people < peopleMax) {
            // bring in a new town person
            //Vector3 personPos = new Vector3(-5.0f, 0.25f, 18.0f);
            
            people++;
            GameObject newPerson = (GameObject)Instantiate(personPrefab, personStartPos, Quaternion.AngleAxis(-90, transform.up));
            // for now we will send the person to the one house
            GameObject house = GameObject.FindGameObjectWithTag("Building");

            newPerson.GetComponent<People>().setTargetPosition(house.transform);

        }
    }


        // TODO: Method to increment the months
        // 1. increase timer, when it reaches a set time
        // 2. go to the next month
        // 3. if it reaches the end of the array, go back to the beginning

}
