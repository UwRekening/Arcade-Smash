using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //constante variablen; kunnen niet veranderd worden (zijn ook automatich static om die reden)
    private const string HighscorePrefsKey = "Highscores";  //de key dat de highscores gebruikt in PlayerPrefs
    private const int MaxStoredHighscores = 5;              //zet hoe veel highscores het spel opslaat

    private List<float> highscores;                         //de highscores dat het spel heeft


    /// <summary>
    /// zet <see cref="highscores"> naar de json van de playerprefs of maakt een lege list aan als de playerprefs leeg is
    /// </summary>
    private void Awake()
    {
        //als playerprefs de key befat
        if (PlayerPrefs.HasKey(HighscorePrefsKey))
        {
            //krijg de JSON data van de playerprefs
            string highscoreJsonData = PlayerPrefs.GetString(HighscorePrefsKey);

            //converteer de json naar een list en sla die op
            highscores = JsonUtility.FromJson<List<float>>(highscoreJsonData);
        }
        //zo niet
        else
        {
            //maak een leege list aan
            highscores = new List<float>();

            //sla de highscores op in de JSON
            SaveHighscores();
        }
    }

    /// <returns>
    /// the highscores
    /// </returns>
    public IReadOnlyCollection<float> GetHighscores()
    {
        return highscores;
    }

    /// <summary>
    /// voegt <paramref name="_score"/> toe aan <see cref="highscores"/>.<br/>
    /// Sorteert <see cref="highscores"/> oplopend en als als de count grooter<br/>
    /// is dan <see cref="MaxStoredHighscores"/> worden dan de laatste verweiderd.
    /// </summary>
    public void AddHighscore(float _score)
    {
        //voeg de score toe
        highscores.Add(_score);

        //sorteer de scores oplopend
        highscores.Sort();

        //log dat een score binnen gekomen is
        Debug.Log($"the score {_score} has been recieved");

        //als er meer highscores dan het maximum aantal highscores in de lijst staan
        if (highscores.Count > MaxStoredHighscores)
        {

            Debug.Log($"removed score {_score} from the list"); //log dat de score verweiderd is
            highscores.RemoveAt(highscores.Count - 1); //verweider het laatste item van de lijst
        }
    }

    /// <summary>
    /// slaat de highscores op in de playerprefs als JSON
    /// </summary>
    public void SaveHighscores()
    {
        //converteer de highscore lijst naar json
        string highscoreJsonData = JsonUtility.ToJson(highscores);

        //zet de playerprefs naar deze json
        PlayerPrefs.SetString(HighscorePrefsKey, highscoreJsonData);

        //sla de playerprefs op
        PlayerPrefs.Save();
    }
}