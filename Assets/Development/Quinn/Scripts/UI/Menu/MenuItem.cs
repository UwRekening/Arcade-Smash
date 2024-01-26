using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    private MenuController menuController;  //voor het opslaan van het laatste geselecteerde item
    private Text text;                      //de text waarvan de kleur verandert moet worden
    private bool active;                    //of de cursor op het huidige menu item is

    //het event dat opgeroepen wordt als het menu item geselecteerd is, heeft MenuItem als type
    [SerializeField] private UnityEvent onSelect;

    //zet tekst kleuren
    [SerializeField] private Color defaultColor = Color.cyan;   //de standaard kleur van de tekst
    [SerializeField] private Color activeColor = Color.white;   //de kleur van de tekst als de cursor hen heeft geselecteerd
    [SerializeField] private Color selectColor = Color.magenta; //de kleur van de tekst als het geselecteerd is

    //opgeroepen zodra het script geladen wordt
    private void Awake()
    {
        //krijg de menu controller
        menuController = FindObjectOfType<MenuController>();

        //krijg het text component
        text = GetComponent<Text>();


        //zet de tekst kleur naar de standaard kleur
        text.color = defaultColor;

        //zet active naar false
        active = false;
    }

    /// <summary>
    /// zet de actieve status van het item
    /// </summary>
    public void SetActive(bool _value)
    {
        //zet de actieve status naar de waarde van de parameter
        active = _value;

        //als active true is, zet de kleur naar de actieve kleur
        if (active == true)
            text.color = activeColor;

        //zet anders de kleur naar de normale kleur
        else
            text.color = defaultColor;
    }

    /// <summary>
    /// selecteerd het MenuItem en roept het event <see cref="onSelect"/> aan
    /// </summary>
    public void SelectItem()
    {
        text.color = selectColor;

        //zet het laatst opgeroepte menu item naar dit
        menuController.lastSelectedItem = this;

        //roept het event op
        onSelect.Invoke();
    }

    /// <summary>
    /// zet de kleur van de item tekst
    /// </summary>
    public void SetColor(Color _color)
    {
        text.color = _color;
    }
}