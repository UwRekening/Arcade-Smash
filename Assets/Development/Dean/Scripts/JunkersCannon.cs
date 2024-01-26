using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkersCannon : MonoBehaviour
{
    public Transform junkerFirePoint; //hier roep ik een vuur punt aan waarvan het geweer moet schieten.
    public GameObject laserBullet; //en hier roep ik de kogel aan.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4)) //Als ik deze knop indruk schiet ik.
        {
            ShootLaser();
        }
    }
    void ShootLaser()
    {
        Instantiate(laserBullet, junkerFirePoint.position, junkerFirePoint.rotation); //hier word een kogel gespawned.
    }
}
