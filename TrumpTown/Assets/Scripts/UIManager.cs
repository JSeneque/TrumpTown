using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    public Text txtBank;
    public Text txtPeople;
    

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateTopBar()
    {
        txtBank.text = GameManager.Instance.bank.ToString();
        txtPeople.text = GameManager.Instance.people.ToString() + " / " +
            GameManager.Instance.peopleMax.ToString();

    }


    private void Update()
    {
        UpdateTopBar();
    }
}
