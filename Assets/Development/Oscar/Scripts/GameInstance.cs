using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public int scoreP1 = 0;
    public int scoreP2 = 0;

    private float startTime; //tijd dat het spel start
    private float endTime; //tijd dat het spel stopt
    private bool winCheck = false;
    private bool checkUpdate = false;
    private ScoreManager scoreManager;

    [SerializeField] Animator animationGameOver;
    [SerializeField] GameObject score;

    //Pakt de score van player 1 en 2.
    [SerializeField] TextMeshProUGUI scoreP1Text;
    [SerializeField] TextMeshProUGUI scoreP2Text;

    //Pakt de movement van junker en dom
    [SerializeField] JunkerMovement junkerMovement;
    [SerializeField] DomMovement domMovement;

    private void Start()
    {
        //Zoekt naar score manager
        scoreManager = FindObjectOfType<ScoreManager>();
        //Set de starttime naar time.
        startTime = Time.time;
        animationGameOver.gameObject.SetActive(false);
        junkerMovement.enabled = false;
        domMovement.enabled = false;
        StartCoroutine(Delay());
    }
    private void Update()
    {
        //(voor debugging) zet score naar 100
        if (Input.GetKeyDown(KeyCode.Q))
            scoreP1 = 100;

        //als wincheck false is
        if (winCheck == false)
        {
            //check of het spel game-over is
            GameOver();
        }

        //Update de score,
        if (checkUpdate)
        {
            scoreP1Text.text = scoreP1.ToString();
            scoreP2Text.text = scoreP2.ToString();
        }
    }
    //Wacht voor dat hij de score update
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(6);
        checkUpdate = true;
        scoreP1Text.text = scoreP1.ToString();
        scoreP2Text.text = scoreP2.ToString();
        junkerMovement.enabled = true;
        domMovement.enabled = true;
    }
    //Wacht 8s dan switch naar loadscene
    IEnumerator SwitchMainmenu()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("MainMenu");
    }

    //Laat GameOver screen zien en switch naar mainmenu als het klaar is.
    private void GameOver()
    {
        //krijg de end time
        endTime = Time.time;

        //berken de highscore
        float score = endTime - startTime;

        //Checkt als score van player 1 100 is
        if (scoreP1 >= 100)
        {
            this.score.SetActive(false);
            //Laat gameover screen widget zien.
            animationGameOver.gameObject.SetActive(true);
            animationGameOver.Play("GameOverP2Won");
            print("test");
            //Disabled movement
            domMovement.gameObject.SetActive(false);
            junkerMovement.gameObject.SetActive(false);

            scoreManager.AddHighscore(score);
            scoreManager.SaveHighscores();

            winCheck = true;

            StartCoroutine(SwitchMainmenu());
        }
        //Checkt als score van player 2 100 is
        else if (scoreP2 >= 100)
        {
            animationGameOver.Play("GameOver");
            this.score.SetActive(false);
            animationGameOver.gameObject.SetActive(true);
            print("test");
            junkerMovement.gameObject.SetActive(false);
            domMovement.gameObject.SetActive(false);

            scoreManager.AddHighscore(score);
            scoreManager.SaveHighscores();

            winCheck = true;

            StartCoroutine(SwitchMainmenu());
        }
    }
}
