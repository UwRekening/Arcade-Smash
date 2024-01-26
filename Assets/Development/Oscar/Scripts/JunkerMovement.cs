using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class JunkerMovement : MonoBehaviour
{
    //hoeveelheid force die op het character uit word gevoerd
    public float jumpForce = 2;
    //snelheid
    public float speed = 4;

    private float kfcTime;
    private float batteryTime;
    private float happyMealTime;
    private float monsterTime;
    private float stunGunTime;
    private bool batteryBoost = false;
    private bool playerStunned = false;
    private bool monsterBoost = false;
    private bool happymealBoost = false;
    private bool stungunBoost = false;
    private bool kfcBoost = false;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        //koppel _rigidbody aan rigidbody
        _rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }

    // Update is called once per frame
    private void Update()
    {
        print(Input.GetAxis("JunkerVertical"));
        //als je distance tot de grond minder dan 0,001 is kun je springen
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (_rigidbody.velocity.y < -4)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (_rigidbody.velocity.y > 2)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (_rigidbody.velocity.y > 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (monsterBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            monsterTime -= Time.deltaTime;
            speed = 4;
            //Zorgt ervoor dat er secondes zijn.

            if (monsterTime < -10)
            {
                speed = 3;
                monsterBoost = false;
            }
        }
        if (batteryBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            batteryTime -= Time.deltaTime;
            speed = 4;
            //Zorgt ervoor dat er secondes zijn.

            if (batteryTime < -20)
            {
                speed = 3;
                monsterBoost = false;
            }
        }
        if (kfcBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            kfcTime -= Time.deltaTime;
            speed = 3;
            jumpForce = 8;
            //Zorgt ervoor dat er secondes zijn.

            if (kfcTime < -10)
            {
                speed = 3;
                jumpForce = 6;
                kfcBoost = false;
            }
        }
        if (happymealBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            happyMealTime -= Time.deltaTime;
            speed = 4;
            jumpForce = 8;
            //Zorgt ervoor dat er secondes zijn.

            if (happyMealTime < -15)
            {
                speed = 3;
                jumpForce = 6;
                happymealBoost = false;
            }
        }
        if (stungunBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            stunGunTime -= Time.deltaTime;
            playerStunned = true;
            //Zorgt ervoor dat er secondes zijn.

            if (stunGunTime < -5)
            {
                playerStunned = false;
                stungunBoost = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {

        switch (collision.gameObject.tag)
        {
            case "Monster":
                {
                    Destroy(collision.gameObject);
                    monsterBoost = true;
                    break;
                }
            case "Kfc":
                {
                    Destroy(collision.gameObject);
                    kfcBoost = true;
                    break;
                }
            case "HappyMeal":
                {
                    Destroy(collision.gameObject);
                    happymealBoost = true;
                    break;
                }
            case "StunGun":
                {
                    Destroy(collision.gameObject);
                    stungunBoost = true;
                    break;
                }
            case "Battery":
                {
                    Destroy(collision.gameObject);
                    batteryBoost = true;
                    break;
                }
        }
    }
    private void FixedUpdate()
    {
        //laat speler naar links of rechts bewegen doormiddel van de velocity van rigidbody
        float movement = Input.GetAxis("JunkerHorizontal");
        _rigidbody.velocity = new Vector3(movement * speed, _rigidbody.velocity.y, 0);
        if (movement < 0)
        {
            //flip als movement <0
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement > 0)
        {
            //flip niet/terug als movement > 0
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

}
