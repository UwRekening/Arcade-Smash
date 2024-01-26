using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CalculationManager : MonoBehaviour
{
    private int earnedCoins = 0;            //het aantal verdiende munten

    private GameManager gameManager;        //voor het toevoegen van het aantal verdiende munten en het switchen van scene
    private CalculationDisplay display;     //het display voor de rekensommen te weergeven
    private EquationManager equation;       //genereert de som en het antwoord

    [SerializeField] private Text earnedCoinsText;      //de tekst dat de hoeveelheid verdiende munten weergeeft
    [SerializeField] private Text equationText;         //de tekst dat de som bevat
    [SerializeField] private Text[] answerOptions;      //de teksten waar de antwoorden ingezet moeten worden

    public int EarnedCoins { get => earnedCoins; }
    public int EquationAnswer { get => equation.CorrectAnswer; }
    public string Equation { get => equation.Equation; }
    public Text EarnedCoinsText { get => earnedCoinsText; }
    public Text EquationText { get => equationText; }
    public Text[] AnswerOptions { get => answerOptions; }

    /// <summary>
    /// voegt alle verdiende munten toe en laadt de main menu
    /// </summary>
    public void Exit()
    {
        //voeg munten toe
        gameManager.coinAmount += earnedCoins;

        //laad de main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// voegt <paramref name="_amount"/> aan <see cref="earnedCoins"/> toe
    /// </summary>
    public void ModifyCoins(int _amount)
    {
        //bereken wat de nieuwe waarde is
        int result = earnedCoins + _amount;

        //als het resultaat gelijk aan of boven nul is
        if (result >= 0)
            earnedCoins = result;
    }

    //opgeroepen zodra het script geladen wordt
    private void Awake()
    {
        //krijg componenten
        display = GetComponent<CalculationDisplay>();
        equation = GetComponent<EquationManager>();

        //krijg de gamemanager
        gameManager = FindObjectOfType<GameManager>();
    }

    //opgeroepen op de eerste frame
    private void Start()
    {
        equation.NewEquation();
        display.UpdateDisplay();
    }
}