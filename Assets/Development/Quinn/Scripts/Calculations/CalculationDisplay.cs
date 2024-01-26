using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// het display voor de rekensommen te weergeven
/// </summary>
public class CalculationDisplay : MonoBehaviour
{
    private Text equationText;
    private Text earnedCoinsText;
    private Text[] answerOptions;
    private CalculationManager manager;

    [SerializeField] private int maxIncorrectDistance;  //de maximum waarde hoe ver het foute antwoord van het juiste antwoord mag zijn


    /// <summary>
    /// update de som en <inheritdoc cref="UpdateOptions"/>
    /// </summary>
    public void UpdateDisplay()
    {
        //zet de som tekst naar de som
        equationText.text = manager.Equation;

        //zet de coin tekst naar het aantal verdiende munten
        earnedCoinsText.text = $"earned {manager.EarnedCoins} coins";

        //update de opties
        UpdateOptions();
    }

    /// <summary>
    /// update de antwoorden
    /// </summary>
    private void UpdateOptions()
    {
        float correctAnswerChance = 1f / answerOptions.Length; //berken de nodige kans dat het correcte antwoord gekozen wordt
        bool wroteCorrectAnswer = false; //of het correcte antwoord al gekozen is
        List<int> incorrectAnswers = new(); //de al gekozen incorrecte antwoorden

        for (int i = 0; i < answerOptions.Length; i++)
        {
            int shiftAnswer; //hoe ver het foute antwoord van het juiste antwoord is
            int optionAnswer; //het antwoord dat deze optie zal weergeven

            //als het antwoord nog niet gescheven is en het laatste item of de willekeurige kans geactiveerd is
            if (wroteCorrectAnswer == false && (i == answerOptions.Length - 1 || Random.value < correctAnswerChance))
            {
                //zet het optie antwoord naar de correcte waarde
                optionAnswer = manager.EquationAnswer;

                //zet het correcte antwoord naar true
                wroteCorrectAnswer = true;
            }
            else
            {
                do
                {
                    //krijg hoe ver het foute antwoord van het juiste antwoord is met een willekeurige kans
                    shiftAnswer = Random.Range(0, maxIncorrectDistance + 1);

                    //50% om de afstand van het antwoord om te draaien
                    if (Random.value < 0.5f)
                        shiftAnswer *= -1;

                    optionAnswer = manager.EquationAnswer + shiftAnswer;
                }
                while (incorrectAnswers.Contains(optionAnswer));

                incorrectAnswers.Add(optionAnswer);
            }

            //set the tekst naar het optie antwoord
            answerOptions[i].text = optionAnswer.ToString();
        }
    }

    //opgeroepen zodra het script geladen wordt
    private void Awake()
    {
        //krijg componenten
        manager = GetComponent<CalculationManager>();

        //krijg de nodige variablen
        answerOptions = manager.AnswerOptions;
        earnedCoinsText = manager.EarnedCoinsText;
        equationText = manager.EquationText;
    }
}