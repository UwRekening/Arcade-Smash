using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowRepair : MonoBehaviour
{
    bool InTrigger = false;
    bool mouseClick = false;
    bool delayTimer = false;

    [SerializeField] GameInstance gameInstance;
    [SerializeField] Sprite windowRepair;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] CineShake cameraShake;
    [SerializeField] Animator animator;
    [SerializeField] Slider delaySlider;
    [SerializeField] float delayDuration = 0.7f;
    [SerializeField] float clickDelay = 0.5f; // Delay between consecutive clicks

    private float timer = 0f;
    private bool isCountingDown = false;
    private Collider2D currentCollision; // Track the currently targeted glass

    private void Start()
    {
        cameraShake.GetComponent<CineShake>();
        animator = GetComponent<Animator>();
        gameInstance = FindObjectOfType<GameInstance>();
    }

    private void Update()
    {
        if (!delayTimer && Input.GetKeyDown(KeyCode.Joystick1Button5) && InTrigger && !isCountingDown)
        {
            if (!mouseClick)
            {
                mouseClick = true;
                StartCoroutine(ClickDelay());
            }
        }

        if (isCountingDown)
        {
            timer -= Time.deltaTime;
            delaySlider.value = timer / delayDuration;

            if (timer <= 0f)
            {
                ResetDelay();
            }
        }
        else
        {
            delaySlider.value = 1f;
        }
    }

    private IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(clickDelay);
        mouseClick = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        InTrigger = true;

        if (mouseClick && collision.CompareTag("windowRepair") && collision == currentCollision && !isCountingDown)
        {
            animator.SetBool("isRepairing", true);
            if (gameInstance.scoreP1 - 1 >= 0)
            {
                gameInstance.scoreP1 -= 1;
            }
            gameInstance.scoreP2 += 2;
            scoreText.text = gameInstance.scoreP2.ToString();
            collision.tag = "windowBreakable";
            particleSystem.transform.position = collision.transform.position;
            particleSystem.Play();
            cameraShake.ShakeCamera(.1f, .8f);
            collision.GetComponent<SpriteRenderer>().sprite = windowRepair;

            animator.SetBool("isRepairing", false);
            StartDelay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision; // Update the currently targeted glass
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentCollision)
        {
            currentCollision = null; // Reset the currently targeted glass
        }
    }

    private void ResetDelay()
    {
        isCountingDown = false;
        timer = delayDuration;
        delayTimer = false;
    }

    private void StartDelay()
    {
        delayTimer = true;
        timer = delayDuration;
        isCountingDown = true;
    }
}
