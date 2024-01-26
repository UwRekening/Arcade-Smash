using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //SerializedFields, zijn zichtbaar/bewerkbaar in de unity editor
    [SerializeField] private int startingCoins = 3; //de hoeveelheid munten dat de speler mee start

    //NonSerialized, zijn niet zichtbaar/bewerkbaar in de unity editor
    [NonSerialized] public int coinAmount;          //de hoeveelheid munten dat de speler heeft

    //aangeroepen zodra het script geladen wordt
    private void Awake()
    {
        //zorg ervoor dat er maar 1 gamemanager bestaad
        if (FindObjectsOfType<GameManager>().Length > 1) //als er meer dan een GameManager is
        {
            Debug.Log("GameObject already exists, destroying this..");
            Destroy(gameObject); //vernietig het huidige gameObject
            return; //stop de method
        }

        //zorg ervoor dat dit gameObject niet vernietigt wordt wanneer een scene wordt geladen
        DontDestroyOnLoad(gameObject);

        //zet het aantal munten dat de speler heeft naar de hoeveelheid munten waar de speler mee start
        coinAmount = startingCoins;
    }
}
