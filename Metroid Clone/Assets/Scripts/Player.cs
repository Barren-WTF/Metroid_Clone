using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public int sceneNum;

    //public variable that sets the players health
    public int health = 100;

    //variable for controlling player's speed
    public float speed;
    
    //variable for determining if the big bullet pickup is active or not
    public bool bigBulletPickUp = false;

    //multiplier for jumping force
    public int jumpforce;

    //variable for checking if player is grounded
    public bool isGrounded;

    //varaible for health gain
    public int healthGain;

    public int maxHealth = 100;

    //variable for counting how many times a max health pickup has been collided with
    private int maxHealthCounter = 0;

    private bool canShoot = true;

    private Rigidbody rigid_body;

    //variables that allow for the attachment of game objects
    public GameObject regularBulletPrefab;
    public GameObject bigBulletPrefab;


    private Vector3 startPosition;

    //UI variables
    public Text win;
    public Text healthText;
    public Text death;

    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        shootingBullet();
        move();
        jump();
        setText();
        healthCheck();
    }
    private void OnTriggerEnter(Collider other)
    {

        //if the player runs into a regular enemy, they lose 15 hp
        if (other.gameObject.tag == "Regular Enemy")
        {
            health = health - 15;
            healthCheck();
            Destroy(other.gameObject);
        }

        //if the player runs into a hard enemy, they lose 65 hp
        if (other.gameObject.tag == "Hard Enemy")
        {
            health = health - 65;
            healthCheck();
            Destroy(other.gameObject);
        }

        //if player collies with object tagged as health, it will gain a set amount of health
        if(other.gameObject.tag == "Health")
        {
            health += healthGain;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            Destroy(other.gameObject);
        }

        //if player collides with object tagged as max health, it will be fully heald and max health increased by 100
        if (other.gameObject.tag == "Max Health")
        {
            maxHealthCounter++;
            health = 100;
            health += 100 * maxHealthCounter;
            maxHealth += 100 * maxHealthCounter;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Jet Pack")
        {
            jumpforce += jumpforce * 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "bigBullet")
        {
            bigBulletPickUp = true;
            Destroy(other.gameObject);
        }


        
    }

    //allows the player to move left and right
    private void move()
    {
        Vector3 add_position = Vector3.zero;


        //a key allows the player to move left
        if (Input.GetKey("a"))
        {
            add_position += Vector3.left * Time.deltaTime * speed;
        }

        //d key allows the player to move right
        if (Input.GetKey("d"))
        {
            add_position += Vector3.right * Time.deltaTime * speed;
        }

        //updates player position
        GetComponent<Transform>().position += add_position;

    }

    //function that allows for player to jump up
    void jump()
    {
        //function checks if the player is grounded
        isPlayerGrounded();

        //if the player is grounded, they it can jump via the space key
        if (Input.GetKey("space") && isGrounded)
        {
            //hitting the space key adds a jump force in the upwards direction to the player
            rigid_body.AddForce(Vector3.up * jumpforce);
        }
    }

    //function that checks to see if player is grounded
    void isPlayerGrounded()
    {

        RaycastHit hit;

        //shoots a racast down from player object down to the floor collider and outputs it to the raycast hit variable
        //if hit distance is less than 1.5, the player object is grounded, if not, then it is not grounded
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

        //draws a red line showing the raycast
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

        //sends value of hit.distance to log
        Debug.Log(hit.distance);
    }

    private void shootingBullet()
    {
        if (Input.GetKey("j"))
        {
            if (canShoot)
            {
                StartCoroutine(shooting(true));
            }
        }

        else if (Input.GetKey("l"))
        {
            if (canShoot)
            {
                StartCoroutine(shooting(false));
            }
        }
    }

    //detects with direction the player is trying to shoot and calls the script to make them shoot that direction
    IEnumerator shooting(bool left)
    {
        if (bigBulletPickUp == false)
        {
            regGun(left);
            canShoot = false;
            yield return new WaitForSeconds(.5f);
            canShoot = true;
        }
        else if (bigBulletPickUp == true)
        {
            bigGun(left);
            canShoot = false;
            yield return new WaitForSeconds(.5f);
            canShoot = true;
        }
    }


    //funtion controls the true or false value of goingLeft in the bullet scripts
    public void regGun(bool left)
    {
        //if the bigBulletPickUp is false, then the shooting keys will controll the Regular Bullet script
        if (bigBulletPickUp == false)
        {
            GameObject regularBullet = Instantiate(regularBulletPrefab, transform.position, regularBulletPrefab.transform.rotation);
            if (regularBullet.GetComponent<Regular_Bullet>())
            {
                regularBullet.GetComponent<Regular_Bullet>().goingLeft = left;
            }
        }
    }

    public void bigGun(bool left)
    {
        //if the bigBulletPickUp is true, then the shooting keys will control the Big Bullet script
        if (bigBulletPickUp == true)
        {
            print("big bullet true");
            GameObject bigBullet = Instantiate(regularBulletPrefab, transform.position, regularBulletPrefab.transform.rotation);
            bigBullet.GetComponent<Regular_Bullet>().scaleUp();
            if (bigBullet.GetComponent<Regular_Bullet>())
            {
                bigBullet.GetComponent<Regular_Bullet>().goingLeft = left;
            }
        }
    }

    //respawns the player at the starting point and checks if they have no more lives
    private void healthCheck()
    {
        //if they are out of lives, the player is deactivated, and the the death message is displayed.
        if (health <= 0)
        {
            this.enabled = false;
            death.text = "Game Over";
        }
    }

    //reinitalizes the health text
    void setText()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
