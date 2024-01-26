using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class junkerAnimationSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator junker_anim;
    [SerializeField] float junkerMovementValue;
    private float junkersJumpHeight;
    [SerializeField] Rigidbody2D _rbJunker;
    void Start()
    {
        //zeg dat anim 
        junker_anim = GetComponent<Animator>();
        _rbJunker = GetComponent<Rigidbody2D>();

        junkersJumpHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        junkerMovementValue = Input.GetAxisRaw("JunkerHorizontal");

        //als de speler naar links of rechts gaat speelt de animation IsRunning
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetAxis("JunkerHorizontal") != 0)
        {
            junker_anim.SetBool("isWalking", true);
        }
        //zo niet gebeurt er niets en blijft de player idlen totdat er wel iets gebeurt
        else
        {
            junker_anim.SetBool("isWalking", false);
        }
        //als de speler op deze key drukt speelt de attack animation af
        if (_rbJunker.velocity.y < 0) //kijk hier dat als de velocity in de - gaat dat dan falling true is. anders niet.
        {
            //junkersJumpHeight = transform.position.y;
            junker_anim.SetBool("isFalling", true); //pas wanneer jumping true is kan falling false zijn.
        }
        else
        {
            junker_anim.SetBool("isFalling", false);
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            junkersJumpHeight = transform.position.y;
            junker_anim.SetBool("isJumping", true);
        }
        //zo niet blijft de spele idlen of rennen
        else
        {
            junkersJumpHeight = transform.position.y;
            
            junker_anim.SetBool("isJumping", false);
        }

        if (Input.GetKey(KeyCode.F))
        {
            junker_anim.SetBool("isShooting", true);
        }
        //zo niet blijft de spele idlen of rennen
        else
        {
            junker_anim.SetBool("isShooting", false);
        }
    }
}
