using UnityEngine;

public class DomMovement : MonoBehaviour
{
    //hoeveelheid force die op het character uit word gevoerd
    [SerializeField] float jumpForce = 2;

    //snelheid van de speler
    public int speed = 4;

    //Alle items die je kan op pakken
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

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        //koppel _rigidbody aan rigidbody
        rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 8, true);
    }

    // Update is called once per frame
    private void Update()
    {
        //krijg de horizontale axis voor dominous
        float movement = Input.GetAxis("DomHorizontal");

        //zet de velocity
        rigidbody.velocity = new Vector3(movement * speed, rigidbody.velocity.y, 0);

        //als je distance tot de grond minder dan 0,001 is kun je springen
        if (Input.GetKeyDown(KeyCode.Joystick2Button2) && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            //voeg de jumpforce toe
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        //anders, als joystick2 button 3 gedrukt is
        else if (Input.GetKeyDown(KeyCode.Joystick2Button3))
        {
            //disable de colider
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }

        //anders, als de de Y velocity onder -4 is 
        else if (rigidbody.velocity.y < -4)
        {
            //enable de colider
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }

        //anders, als de de Y velocity boven 2 is 
        else if (rigidbody.velocity.y > 2)
        {
            //disable de colider
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }

        //anders, als de de Y velocity boven 0 is 
        else if (rigidbody.velocity.y > 0)
        {
            //enable de colider
            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }

        //als de monster powerup actief is
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

        //als de battery powerup actief is
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

        //als de KFC powerup actief is
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

        //als de happymeal powerup actief is
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

        //als de stungun powerup actief is
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

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        //switch voor de collision tag
        switch (_collision.gameObject.tag)
        {
            case "Monster": //als de tag monser is
                {
                    Destroy(_collision.gameObject);
                    monsterBoost = true;
                    break;
                }
            case "Kfc": //als de tag KFC is
                {
                    Destroy(_collision.gameObject);
                    kfcBoost = true;
                    break;
                }
            case "HappyMeal": //als de tag HappyMeal is
                {
                    Destroy(_collision.gameObject);
                    happymealBoost = true;
                    break;
                }
            case "StunGun": //als de tag stungun is
                {
                    Destroy(_collision.gameObject);
                    stungunBoost = true;
                    break;
                }
            case "Battery": //als de tag battery is
                {
                    Destroy(_collision.gameObject);
                    batteryBoost = true;
                    break;
                }
        }
    }

    //wordt in elke phyics update opgeroepen
    private void FixedUpdate()
    {
        //laat speler naar links of rechts bewegen doormiddel van de velocity van rigidbody
        float movement = Input.GetAxis("DomHorizontal");
        rigidbody.velocity = new Vector3(movement * speed, rigidbody.velocity.y, 0);

        //als movement onder 0 is
        if (movement < 0)
        {
            //filp de x axis
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movement > 0)
        {
            //flip de x axis niet meer
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
