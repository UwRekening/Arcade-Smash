using System.Collections;
using UnityEngine;

public class pickUpManager : MonoBehaviour
{
    public JunkerMovement junkerMovement;
    [SerializeField] float originalMovementSpeed;
    [SerializeField] float originalJumpForce;
    public bool isBoosted = false;

    private IEnumerator BoostDuration(float duration) //hier maak ik een class aan waar ik de tijd van de boosts ga laten werken.
    {
        //Slaat de orginele waardes op zodat na de boost de oude waardes weer terug gezet kunnen worden.
        originalMovementSpeed = junkerMovement.speed;
        originalJumpForce = junkerMovement.jumpForce;

        // Wacht voor duration tot hij iets terug stuurt.
        yield return new WaitForSeconds(duration);

        //hier worden de oude waardes weer terug gezet zodat je niet voor altijd geboost blijft.
        junkerMovement.speed = originalMovementSpeed;
        junkerMovement.jumpForce = originalJumpForce;

        //Boost word false zodat je weer nieuwe kan oppakken.
        isBoosted = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) //deze code kan runnen als er een trigger word gedetecteerd.
    {
        if (!isBoosted) //als boosted niet true is dan kan deze code gebruikt worden.
        {
            switch (collision.gameObject.tag)
            {
                case "Speed": //als de collision tag "speed" is speelt hij deze code af.
                    StartCoroutine(BoostDuration(2f)); //speelt de code van deze class voor 2 seconden en gaat dan weer naar de normale toestand.
                    junkerMovement.speed *= 1.75f; //in die tijd word de speed boost 1.75x verbeterd
                    Destroy(collision.gameObject); //als ik hem oppak maak ik het object kapot (anders zou het niet logisch zijn).
                    isBoosted = true; //hier is de boolean true zodat je niet meer boosts kan oppakken
                    break; //hier break ik uit de switch case.

                case "Jump":
                    StartCoroutine(BoostDuration(2f));
                    junkerMovement.jumpForce *= 2.25f;
                    Destroy(collision.gameObject);
                    isBoosted = true;
                    break;

                case "Taser":
                    StartCoroutine(BoostDuration(2f));
                    junkerMovement.speed =0.1f;
                    junkerMovement.jumpForce = 0.1f;
                    Destroy(collision.gameObject);
                    isBoosted = true;
                    break;
            }
        }
    }
}
