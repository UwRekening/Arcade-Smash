using UnityEditor; //is only included in editor build
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    private GameManager gameManager;        //de gememanager voor het vergelijken van munten
    private MenuController menuController;  //de menu controller voor de kleur van start naar rood te veranderen als er niet genoeg munten zijn

    //is opgeroepen wanneer het script geladen wrodt
    private void Awake()
    {
        //krijg de GameManager
        gameManager = FindObjectOfType<GameManager>();

        //krijg de menu controller
        menuController = FindObjectOfType<MenuController>();
    }

    //opgeroepen wanneer de start optie in het menu geselecteerd is
    public void ActionStart()
    {
        if (gameManager.coinAmount <= 0)
        {
            Debug.Log("Failed to start the game: insuffisient amount coins");

            //krijg het laatst geselecteerde item en zet de kleur naar rood
            menuController.lastSelectedItem.SetColor(Color.red);
            return; //stop de method
        }

        Debug.Log("Started Game!");

        //verweider 1 munt
        gameManager.coinAmount--;

        //laadt de transitie scene (trasitioneerd naar game)
        SceneManager.LoadScene("Transition");
    }

    //opgeroepen wanneer de insert coins optie in het menu geselecteerd is
    public void ActionInsertCoins()
    {
        Debug.Log("Inserted Coins!");

        //laadt de rekensommen scene
        SceneManager.LoadScene("Calculations");
    }

    //opgeroepen waneer de highscores optie in het menu geselecteerd is
    public void ActionHighscores()
    {
        //log naar de console
        Debug.Log("Entering Highscores!");

        //laadt de tutorial scene
        SceneManager.LoadScene("Highscores"); //is MainMenu tot toturial scene gemaakt is
    }

    //opgeroepen wanneer de quit optie in het menu geselecteerd is
    public void ActionQuit()
    {
        Debug.Log("Quit Game!");

#if DEBUG
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}