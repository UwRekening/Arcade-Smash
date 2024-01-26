using UnityEngine;

/// <summary>
/// genereert de som en het antwoord
/// </summary>
public class EquationManager : MonoBehaviour
{
    private int correctAnswer;          //
    private string equation;            //de rekensom dat gegenereed is (uit CalculationManager)

    [SerializeField] private int equationDigits;    //het aantal nummers dat waaruit de som bestaad

    //voor het krijgen van het correcte antwoord
    public int CorrectAnswer
    {
        get => correctAnswer;
    }

    //voor het krijgen van de som
    public string Equation
    {
        get => equation;
    }


    /// <returns>
    /// <see langword="true"/> als <paramref name="_answer"/> gelijk is aan <see cref="correctAnswer"/>, anders <see langword="false"/>
    /// </returns>
    public bool IsCorrect(int _answer)
    {
        if (correctAnswer == _answer)
        {
            Debug.Log("answer was correct!");
            return true;
        }

        Debug.Log("answer was incorrect!");
        return false;
    }

    public void NewEquation()
    {
        //reset de som
        equation = string.Empty;
        correctAnswer = 0;

        //loep equationDigits keer
        for (int i = 0; i < equationDigits; i++)
        {
            int digit = Random.Range(100, 500); //zet een willekeurig nummer van 100-500 op de index
            char operation;

            //als i 0 is
            if (i == 0)
            {
                //zet het correcte antwoord naar het nummer
                correctAnswer = digit;

                //voeg het nummer toe aan de som
                equation += digit.ToString();

                //skip naar de volgende iteratie van de loep
                continue;
            }

            if (Random.value < 0.5f)
            {
                correctAnswer += digit;
                operation = '+';
            }
            else
            {
                correctAnswer -= digit;
                operation = '-';
            }

            //voeg de operatie en het nummer toe aan de som
            equation += operation + digit.ToString();
        }
    }
}