using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WindowBreak : MonoBehaviour
{
    bool InTrigger = false;
    bool mouseClick = false;
    bool delayTimer = false;

    [SerializeField] GameInstance gameInstance;
    [SerializeField] Sprite windowBroken;
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Animator animator;
    [SerializeField] CineShake cameraShake;
    [SerializeField] Slider delaySlider;
    [SerializeField] float delayDuration = 0.9f;
    [SerializeField] float clickDelay = 0.5f;

    private float timer = 0f;
    private bool isCountingDown = false;
    private Collider2D currentCollision; // Track the currently targeted glass

    private void Start()
    {
        animator.GetComponent<Animator>();
        cameraShake.GetComponent<CineShake>();
        gameInstance = FindObjectOfType<GameInstance>();
    }

    private void Update()
    {
        //Checkt voor als controler word ingeklikt samen met delay en trigger
        if (!delayTimer && Input.GetKeyDown(KeyCode.Joystick2Button5) && InTrigger && !isCountingDown && !mouseClick)
        {
            mouseClick = true;
            StartCoroutine(ClickDelay());
        }
        //Checkt voor countingdown
        if (isCountingDown)
        {
            timer -= Time.deltaTime;
            delaySlider.value = timer / delayDuration;

            //Reset timer.
            if (timer <= 0f)
                ResetDelay();
        }
        else
        {
            delaySlider.value = 1f;
        }
    }

    //set een muis click delay.
    private IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(clickDelay);
        mouseClick = false;
    }

    //Checkt voor trigger stay
    private void OnTriggerStay2D(Collider2D collision)
    {
        InTrigger = true;

        if (mouseClick && collision.CompareTag("windowBreakable") && collision == currentCollision && !isCountingDown)
        {
            animator.SetBool("isAttacking", true);
            gameInstance.scoreP1 += 2;
            if (gameInstance.scoreP2 - 1 >= 0)
            {
                gameInstance.scoreP2 -= 1;
            }
            scoreText.text = gameInstance.scoreP2.ToString();
            collision.tag = "windowRepair";
            particleSystem.transform.position = collision.transform.position;
            particleSystem.Play();
            cameraShake.ShakeCamera(.3f, .5f);
            collision.GetComponent<SpriteRenderer>().sprite = windowBroken;

            animator.SetBool("isAttacking", false);
            StartDelay();
        }
    }

    //Checkt voor trigger enter.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision; // Update the currently targeted glass
    }

    //Checkt voor trigger exit.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentCollision)
        {
            currentCollision = null; // Reset the currently targeted glass
        }
    }

    //Reset Delay timer.
    private void ResetDelay()
    {
        isCountingDown = false;
        timer = delayDuration;
        delayTimer = false;
    }

    //Start Delay timer.
    private void StartDelay()
    {
        delayTimer = true;
        timer = delayDuration;
        isCountingDown = true;
    }
}
