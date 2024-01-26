using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    private bool stopWatch = true;
    private bool load;
    float currentTime;
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Update()
    {
        if (stopWatch)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            currentTime = currentTime - Time.deltaTime;
            print(currentTime);
            //Zorgt ervoor dat er secondes zijn.
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);

            if (currentTime < -7)
            {
                stopWatch = false;
                load = true;
            }

            if (load == true)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
