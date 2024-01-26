using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementDominus : MonoBehaviour
{
    [SerializeField] string bulletDirection;

    public float jumpForce = 15;
    public GameObject bullet;
    public Image hp;
    public Transform bulletOffsetRight;
    public Transform bulletOffsetLeft;
    public int speed = 4;
    public Animator animator;
    public GameManager gameManager;
    public bool stopWatch = false;

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
    private bool hasShot;
    private float currentTime;
    private Rigidbody2D rigidbody;
    private Rigidbody2D rigidBody2D;
   

    // Start is called before the first frame update
    private void Start()
    {
        //hier koppel ik de rigidbody aan _rigidbody
        rigidbody = GetComponent<Rigidbody2D>();
        hasShot = false;
    }

    // Update is called once per frame
    private void Update()
    {
        print(playerStunned);
        //met de Mathf.Abs zorg je ervoor dat het altijd een positief getal is
        //als je op je down arrow drukt en niet al in de lucht bent spring je
        if (Input.GetButtonDown("Vertical") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (stopWatch)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            currentTime -= Time.deltaTime;
            print(currentTime);
            //Zorgt ervoor dat er secondes zijn.

            if (currentTime < -2)
            {
                hasShot = false;
                stopWatch = false;
            }
        }
        if (monsterBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
             monsterTime -= Time.deltaTime;
            speed = 20;
            //Zorgt ervoor dat er secondes zijn.

            if (monsterTime < -10)
            {
                speed = 10;
                monsterBoost = false;
            }
        }
        if (batteryBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            batteryTime -= Time.deltaTime;
            speed = 15;
            //Zorgt ervoor dat er secondes zijn.

            if (batteryTime < -10)
            {
                speed = 10;
                monsterBoost = false;
            }
        }
        if (kfcBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            kfcTime -= Time.deltaTime;
            speed = 15;
            jumpForce = 20;
            //Zorgt ervoor dat er secondes zijn.

            if (kfcTime < -10)
            {
                speed = 10;
                jumpForce = 15;
                kfcBoost = false;
            }
        }
        if (happymealBoost)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            happyMealTime -= Time.deltaTime;
            speed = 14;
            jumpForce = 19;
            //Zorgt ervoor dat er secondes zijn.

            if (happyMealTime < -10)
            {
                speed = 10;
                jumpForce = 19;
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
    //private void OnTriggerEnter2D(Collision2D collision)
    private void OnTriggerEnter2D(Collider2D collision)

    {
       
        switch (collision.gameObject.tag)
        {
            case "bullet":
                {
                    print("test");
                    hp.fillAmount -= 1f / 30f;
                    break;
                }
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
        if (playerStunned == false)
        {
            print("move");
            float movement = Input.GetAxisRaw("Horizontal"); //slaat de movement op
            print(movement);
            rigidbody.velocity = new Vector3(movement * speed, rigidbody.velocity.y, 0);  // dit er er voor de speler snelheid
            if (movement == -1)
            {

                bulletDirection = "Left";
                GetComponent<SpriteRenderer>().flipX = true; //zorgt ervoor dat de speler gedraaid wordt want je flipt de x axis
            }
            else if (movement == 1) //als je momement groter is dan 0
            {

                bulletDirection = "Right";
                GetComponent<SpriteRenderer>().flipX = false; //hier word de player niet gedraaid want je movement is groter dan 0 
            }
        }
    }
}