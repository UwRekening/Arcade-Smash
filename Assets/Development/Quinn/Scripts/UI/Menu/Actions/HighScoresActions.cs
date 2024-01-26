using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresActions : MonoBehaviour
{
    public void ActionExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}