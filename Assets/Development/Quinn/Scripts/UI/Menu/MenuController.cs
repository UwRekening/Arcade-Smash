using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public MenuItem lastSelectedItem;   //bevat het laats geselecteerde menu item, wordt gezet in MenuItem.SelectItem()
    private int activeItemIndex;        //de index van het actieve MenuItem
    private float delayTimer;           //de tijd dat

    [SerializeField] private MenuItem[] menuItems;      //krijg de menu items
    [SerializeField] private float inputDelay = 0.20f;  //zet de delay in seconden tussen het verplatsen van de "cursor"

    //wordt aangeroepen zodra het script geladen wordt
    private void Awake()
    {
        //zet het actieve item naar 0
        activeItemIndex = 0;

        if (menuItems.Length == 0)
            Debug.LogWarning("er zijn geen items toegevoegd aan het menu, dingen zullen niet correct werken");
    }

    //wordt aangeroepen op de eerste frame
    private void Start()
    {
        //zet het menu item naar actief
        menuItems[activeItemIndex].SetActive(true);

        //bereken de tijd dat de cursor mag bewegen
        delayTimer = Time.time + inputDelay;
    }

    //wordt elke frame aangeroepen
    private void Update()
    {
        //krijg de raw axes; geen oplopen/aflopen
        float verticalAxis = Input.GetAxisRaw("Vertical");  //krijgt de verticale axis
        float submitAxis = Input.GetAxisRaw("Submit");      //krijgt de submit axis


        //als de cursor niet verplaatst wordt
        if (verticalAxis == 0 && submitAxis == 0)
            delayTimer = 0; //zorg ervoor dat de input delay niet meer geblokeerd wordt

        //als de timer behaald en een van de inputs gebruikt zijn
        if (Time.time > delayTimer && (verticalAxis != 0 || submitAxis != 0))
        {
            //als de selecteer knop gedrukt is
            if (submitAxis == 1)
                Select();

            //als de omhoog knop gedrukt is
            if (verticalAxis == 1)
                MoveCursorUp();

            //als de omlaag knop gedrukt is
            if (verticalAxis == -1)
                MoveCursorDown();

            delayTimer = Time.time + inputDelay;
        }
    }

    /// <summary>
    /// verplaatst de cursor in <paramref name="_direction"/> waar directie 1 of -1 moet zijn
    /// </summary>
    private void MoveCursor(int _direction)
    {
        //als de directie niet 1 of -1 is
        if ((_direction == 1 || _direction == -1) == false)
            //werp een exceptie (genereer een error)
            throw new ArgumentOutOfRangeException("the direction has to be either 1 or -1");

        //bereken de nieuwe index
        int newIndex = activeItemIndex + _direction;

        //als de nieuwe index lager is dan de minimum index in menu items, zet het naar de maximum index
        if (newIndex < 0)
            newIndex = menuItems.Length - 1;

        //als de nieuwe index hoger of gelijk is is aan de maximum index in menu items, zet het naar de minimum index
        if (newIndex >= menuItems.Length)
            newIndex = 0;

        //update de hovering status
        menuItems[activeItemIndex].SetActive(false);
        menuItems[newIndex].SetActive(true);

        //zet de active index naar de nieuwe index
        activeItemIndex = newIndex;
    }

    //wordt aangeroepen wanner de gebruiker omhoog beweegt
    private void MoveCursorUp()
        => MoveCursor(-1);

    //wordt aangeroepen wanner de gebruiker omlaag beweegt
    private void MoveCursorDown()
        => MoveCursor(1);

    //wordt aangeroepen wanneer de gebruiker het huidige item selecteerd
    private void Select()
        => menuItems[activeItemIndex].SelectItem();
}
