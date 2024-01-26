using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollisionCheck : MonoBehaviour
{
    public bool stopWatch = false;
    float currentTime;
    JunkerMovement movementPlayers = null;
    

    private void Start()
    {
        movementPlayers = GetComponent<JunkerMovement>();
    }
    void Update()
    {
        //Checkt als stopWatch true is
        if (stopWatch)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            currentTime = currentTime -= Time.deltaTime;
            print(currentTime);
            //Zorgt ervoor dat er secondes zijn.

            if ( currentTime < -6) 
            {
                movementPlayers.speed = 4;
                stopWatch = false;
            }
        }
    }
        public void OnTriggerEnter2D(Collider2D _collision)
        {
        switch (_collision.gameObject.tag)

        {
            // print is voor testen!
            case "StunGun":
                print("StunGun");
                //Stun other player

                // bool voor animation in script dean naar true zetten?
                Destroy(_collision.gameObject);
                break;
            case "Battery":
                //Debuff other player
                //MovementJunker.cs is script!!!

                //Movement P1 speed omlaag gooien?
                //-1 hp voor P1?
                print("Battery");
                Destroy(_collision.gameObject);
                break;
            case "Kfc":
                print("Kfc");
                //Big buff
                // + movement speed
                Destroy(_collision.gameObject);
                break;
            case "HappyMeal":
                print("HappyMeal");
                //Big debuff
                // - movement speed
                // - jump height
                Destroy(_collision.gameObject);
                break;
                //Werkt
            case "Monster":
                movementPlayers.speed += 2;
                stopWatch = true;
                Destroy(_collision.gameObject);
                break;
        }
    }

}
