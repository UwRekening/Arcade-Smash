using UnityEngine;
using UnityEngine.UI;

public class Abilitys : MonoBehaviour
{
    [SerializeField] float launchForce = 10f;       // de kracht dat op de speler uitgeoefent om de speler te lanceren
    [SerializeField] float launchRadius = 5f;       // de straal waarin de spelers ge-"launched" kunnen worden
    [SerializeField] float cooldownDuration = 10f;  // de duratie van de cooldown in seconden
    [SerializeField] Slider cooldownSlider;         // voor het visualiseren van cooldwon
    [SerializeField] string applyTag;             // de tag waarop de ability is toegepast

    private bool isCooldownActive = false;          // of de cooldown actief is
    private float cooldownTimer = 0f;               // timer voor de cooldown duratie

    private void Start()
    {
        cooldownSlider.value = 1f; // maak de cooldown slider vol
    }

    // opgeroepen elke framne
    private void Update()
    {
        // check of de cooldown actief is
        if (isCooldownActive)
        {
            // Update de cooldown timer
            cooldownTimer -= Time.deltaTime;

            // update de waarde van de cooldown display
            cooldownSlider.value = cooldownTimer / cooldownDuration;

            // als de cooldown voorbij is
            if (cooldownTimer <= 0f)
            {
                isCooldownActive = false; // maak de cooldown niet meer actief
                cooldownSlider.value = 1f; // maak de slider weer vol
            }
        }
        else
        {
            // als "E" gedrukt is
            if (Input.GetKeyDown(KeyCode.Joystick2Button1))
            {
                LaunchNearbyPlayer(); // lanceer de dichtbeizeinde spelers
                isCooldownActive = true; // activeer de cooldown
                cooldownTimer = cooldownDuration; // zet de cooldown timer
            }
        }
    }

    //lanceert de speler
    private void LaunchNearbyPlayer()
    {
        // zet collisie tussen de spelers uit zolang ze gelanceerd worden
        Physics2D.IgnoreLayerCollision(7, 8, false);

        // verkrijg alle colliders binnen de straal dat spelers gelanceerd worden
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, launchRadius);

        foreach (Collider2D collider in colliders)
        {
            // als de collider de juiste tag bevat
            if (collider.CompareTag(applyTag))
            {
                // lanceer de speler
                Rigidbody2D playerRigidbody = collider.GetComponent<Rigidbody2D>();

                //als de rigidbody niet null is
                if (playerRigidbody != null)
                {
                    //zet de velocity om de speler omhoog te lanceren met de launchforce
                    playerRigidbody.velocity = new Vector2(0f, launchForce);

                    //voeg een impulse kracht toe in de X richting
                    playerRigidbody.AddForce(new Vector2(launchForce, 0), ForceMode2D.Impulse);

                    // zet de collisie tussen spelers weer aan
                    Physics2D.IgnoreLayerCollision(7, 8, true);
                }
            }
        }
    }
}