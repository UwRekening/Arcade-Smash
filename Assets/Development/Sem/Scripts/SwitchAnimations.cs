using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        //zeg dat anim 
        anim = GetComponent<Animator>();
        anim.SetBool("IsRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        //als de speler naar links of rechts gaat speelt de animation IsRunning
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("IsRunning", true);
        }
        //zo niet gebeurt er niets en blijft de player idlen totdat er wel iets gebeurt
        else
        {
            anim.SetBool("IsRunning", false);
        }
        //als de speler op deze key drukt speelt de attack animation af
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("IsAttacking", true);
        }
        //zo niet blijft de spele idlen of rennen
        else
        {
            anim.SetBool("IsAttacking", false);
        }
        
    }
}
