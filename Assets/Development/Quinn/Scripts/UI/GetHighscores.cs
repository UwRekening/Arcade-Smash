using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHighscores : MonoBehaviour
{
    [SerializeField] private int decimals = 2;
    [SerializeField] private float showSecondsDelay;    //delay tussen elke score dat wordt laten zien
    [SerializeField] private string scoreTarget;        //het doelwit van de score

    private Text scoreText;                         //de tekst dat de scores zal laten zien
    private IReadOnlyCollection<float> highscores;  //de highscores


    private void Awake()
    {
        scoreText = GetComponent<Text>();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        highscores = scoreManager.GetHighscores();
    }

    //start is op het eerste frame opgeroepen
    private void Start()
    {
        //start de coroutine dat de highscores laat zien
        StartCoroutine(ShowHighscoreTimer());
    }

    private IEnumerator ShowHighscoreTimer()
    {
        List<string> showedHighscores = new();

        foreach (float highscore in highscores)
        {
            yield return new WaitForSeconds(showSecondsDelay);

            //rond de highscore af met een bepaalde hoeveelheid decimalen round(highscore * (10^decimals)) / (10^decimals)
            float increaseBy = Mathf.Pow(10, decimals);
            float roundedScore = Mathf.Round(highscore * increaseBy) / increaseBy;

            //voeg de afgeronde highscore tekst aan de lijst toe
            showedHighscores.Add(roundedScore.ToString());


            //zet de tekst naar de score teksten die tot nu toe in de lijst staan
            scoreText.text = string.Join('\n', showedHighscores);
        }
    }
}
