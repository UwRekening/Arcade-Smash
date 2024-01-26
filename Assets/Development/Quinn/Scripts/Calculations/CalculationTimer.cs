
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CalculationTimer : MonoBehaviour
{
    private CalculationManager calculationManager;

    [SerializeField] private int maxTimeSeconds;    //aantal seconden dat de timer heeft
    [SerializeField] private Text timerDisplay;     //waar de timer wordt weergeven

    //opgeroepen zodra het script geladen is
    private void Awake()
    {
        //verkrijg de calculation manager
        calculationManager = FindObjectOfType<CalculationManager>();
    }

    //opgeroepen op de eerste frame
    private void Start()
    {
        //start de coroutine (in start zodat de timer niet begint voordat de speler iets kan zien)
        StartCoroutine(ProgressTimer());
    }

    //opgeroepen elke frame
    private void Update()
    {
        //(voor debugging) skipt de timer
        if (Input.GetKeyDown(KeyCode.Q))
            maxTimeSeconds = -1;
    }

    private IEnumerator ProgressTimer()
    {
        //zo lang de timer lager is dan de maximum tijd
        for (int elapsedSeconds = 0; elapsedSeconds < maxTimeSeconds; elapsedSeconds++)
        {
            //update de display tekst
            UpdateTimerDisplay(maxTimeSeconds - elapsedSeconds);

            //geef de executie terug & pauzeer voor 1 seconde (doordat dit een coroutine is)
            yield return new WaitForSeconds(1);
        }

        //beindig de sessie van rekensommen en ga terug naar het hoofdmenu
        calculationManager.Exit();
    }

    private void UpdateTimerDisplay(int _timeInSeconds)
    {
        //krijg hoe vaak 60 in _timer zit
        int minutes = _timeInSeconds / 60;

        //bereken hoeveel seconden er over zijn
        int seconds = _timeInSeconds - minutes * 60;

        //laat de tekst zien
        timerDisplay.text = $"{minutes:00}:{seconds:00}";
    }
}