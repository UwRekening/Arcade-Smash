using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private Text coinText;              //wordt gebruikt om het aantal munten te weergeven
    private GameManager gameManager;    //wordt gebruikt om het aantal munten te verkrijgen

    //wordt aangeroepen zodra het script geladen wordt
    private void Awake()
    {
        //krijg de gamemanager
        gameManager = FindObjectOfType<GameManager>();

        //krijg de tekst
        coinText = GetComponent<Text>();
    }

    //wordt aangeroepen elke frame
    private void Update()
    {
        //krijg de hoeveelheid munten van de gamemanager
        int coinAmount = gameManager.coinAmount;

        //zet de tekst naar het aantal munten
        coinText.text = "coins: " + coinAmount.ToString();
    }
}