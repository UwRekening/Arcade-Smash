using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkersBullet : MonoBehaviour
{
    public float laserSpeed = 20f; //dit is hoe snel de kogel beweegt
    public int laserDamage = 1; //dit is hoeveel schade de kogel aanricht aan de enemy (mocht de enemy geraakt worden).)
    public Rigidbody2D bulletRB; //hier roep ik de rigidBody van een bullet op.
    void Start()
    {
        bulletRB.velocity = transform.right * laserSpeed; //hier bepaal ik de snelheid van de bullet.
    }

}
