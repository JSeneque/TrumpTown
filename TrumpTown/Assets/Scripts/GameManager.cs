using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int bank;
    public int people;
    public int peopleMax;


    private void Awake()
    {
        Instance = this;
    }

    public int GetBalance()
    {
        return bank;
    }
	
}
