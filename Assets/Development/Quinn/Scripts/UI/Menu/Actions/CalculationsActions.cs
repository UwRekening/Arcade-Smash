using UnityEngine;
using UnityEngine.UI;

public class CalculationsActions : MonoBehaviour
{
    private CalculationManager calculationManager;  //toevoegen van munten
    private CalculationDisplay calculationDisplay;  //updaten van speelveld
    private EquationManager equationManager;        //beantwoorden van de vraag
    private MenuController menuController;          //gebruikt om te kijken wel item geselecteerd is

    //opgeroepen zodra het script geladen wordt
    private void Awake()
    {
        //krijg de MenuController van het menu
        menuController = FindObjectOfType<MenuController>();

        //krijg componenten
        calculationManager = GetComponent<CalculationManager>();
        equationManager = GetComponent<EquationManager>();
        calculationDisplay = GetComponent<CalculationDisplay>();
    }

    //opgeroepen wanneer een antwoord geselecteerd is
    public void ReceivedAnswer()
    {
        MenuItem selectedMenuItem = menuController.lastSelectedItem;
        Text itemText = selectedMenuItem.GetComponent<Text>();

        //probeert de tekst naar een integer te pars-en
        if (int.TryParse(itemText.text, out int answer) == false)
        {
            //genereert een error
            throw new System.Exception("failed to parse the text to an integer!");
        }

        bool correct = equationManager.IsCorrect(answer);

        //als het antwoord correct is
        if (correct)
            //voeg een coin toe
            calculationManager.ModifyCoins(1);
        else
            //verweider een coin
            calculationManager.ModifyCoins(-1);

        //genereer een nieuwe vraag
        equationManager.NewEquation();
        calculationDisplay.UpdateDisplay();
    }
}
