/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //public variable that sets the players health
    public int health = 100;

    //variable for controlling player's speed
    public float speed;
    
    //variable for determining if the big bullet pickup is active or not
    public bool bigBulletPickUp = false;

    //multiplier for jumping force
    public int jumpforce;

    //controllable parameter for jetpump jump force
    public int jetPack;

    //variable for checking if player is grounded
    public bool isGrounded;

    //varaible for health gain
    public int healthGain;

    public int maxHealth = 100;

    private int sceneNumber;

    //variable for counting how many times a max health pickup has been collided with
    private int maxHealthCounter = 0;

    //variable for coroutine to controll shooting frequency
    private bool canShoot = true;

    private Rigidbody rigid_body;

    //adjustable variable that designate how long the player must survive in scene 9 before they clear the level and are returned to the main menu
    public int survivalTime;

    //variables that allow for the attachment of game objects
    public GameObject regularBulletPrefab;
    public GameObject bigBulletPrefab;

    //UI variables
    public Text healthText;
    public Text death;
    public Text survival;

    // Start is called before the first frame update
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        //startPosition = new Vector3(-14.0f, -3.7f, 0f);
    }

    void FixedUpdate()
    {
        shootingBullet();
        move();
        jump();
        setText();
        healthCheck();
        Horde();
    }
    private void OnTriggerEnter(Collider other)
    {

        //if the player runs into a regular enemy, they lose 15 hp
        if (other.gameObject.tag == "Regular Enemy")
        {
            health -= 15;
            if (health <= 0)
            {
                health = 0;
            }
            
            Destroy(other.gameObject);
        }

        //if the player runs into a hard enemy, they lose 65 hp
        if (other.gameObject.tag == "Hard Enemy")
        {
            health -= 65;
            if (health <= 0)
            {
                health = 0;
            }
       
            Destroy(other.gameObject);
        }

        //if player collies with object tagged as health, it will gain a set amount of health
        if(other.gameObject.tag == "Health")
        {
            health += healthGain;
            if (health >= maxHealth)
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

        //player collides with designated tag, jumpforce value is changed to that of jetpack
        if (other.gameObject.tag == "Jet Pack")
        {
            jumpforce = jetPack;
            Destroy(other.gameObject);
        }

        //if player collides wioth tag, bigbullet is enabled
        if (other.gameObject.tag == "bigBullet")
        {
            bigBulletPickUp = true;
            Destroy(other.gameObject);
        }

        //will kill player if they collide with tag
        if (other.gameObject.tag == "Death Barrier")
        {
            health = 0;
            healthCheck();
        }

        //takes player to the next scene
        if (other.gameObject.tag == "Gateway")
        {
            SceneSwitch.instance.switchScene(sceneNumber++);
        }

        //player looses 15 hp everytime they contact a laser
        if (other.gameObject.tag == "Laser")
        {
            health -= 15;
            if (health <= 0)
            {
                health = 0;
            }
            healthCheck();
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
        if (Input.GetKey("w") && isGrounded)
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

    //function defines the keys for shooting left and right
    private void shootingBullet()
    {
        //when j is pressed, shoots left
        if (Input.GetKey("j"))
        {
            //if canShoot is true
            if (canShoot)
            {
                //start shooting, couroutine is used to limit the player to only shooting 2 times a second
                StartCoroutine(shooting(true));
            }
        }

        //when j is pressed, shoots right
        else if (Input.GetKey("l"))
        {
            if (canShoot)
            {
                StartCoroutine(shooting(false));
            }
        }
    }

    //detects wich direction the player is trying to shoot, also provents player from shooting too quickly by forcing them to wait a certain amount of time before shooting again
    IEnumerator shooting(bool left)
    {
        //checking for pick up
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

    //same as regGun but for the big bullet
    public void bigGun(bool left)
    {
        //if the bigBulletPickUp is true, then the shooting keys will control the bullet script, if the pickup returns true, the prefab is scaled up
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
        //updates UI
        setText();

        //if they are out of lives, the player is deactivated, and the the death message is displayed.
        if (health <= 0)
        {
            health = 0;
            setText();
            StartCoroutine(gameOver());
            this.enabled = false;
        }
    }

    //function detects for the final scene in game, if the statement returns true it carrys out the coroutine
    private void Horde()
    {
        int currentScene;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 9)
        {
            StartCoroutine(Survive());
        }
    }

    //a way of setting a survival timer, if player still has health after a specified time in the waitfor, then the player has survived the horde round
    IEnumerator Survive()
    {
        yield return new WaitForSeconds(survivalTime);

        //checks if player is still alive
        if (health > 0)
        {
            StartCoroutine(YouSurvived());
        }
    }

    //lets player know they survived and take player back to main menu after a few seconds
    IEnumerator YouSurvived()
    {
        {
            //ui element, diplays for 5 seconds
            survival.text = "YOU SURVIVED!";

            yield return new WaitForSeconds(5.0f);

            //ui element, displays for a few seconds
            survival.text = "Returning to Menu in 5 seconds";

            StartCoroutine(backToMenu());

        }
    }

    //reinitalizes the health text
    void setText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    //if the player runs out of health, they are returned to the main menu after a few seconds
    IEnumerator gameOver()
    {
        {
            death.text = "Game Over";
            yield return new WaitForSeconds(3.0f);
            death.text = "Returning to Menu in 5 seconds";
            StartCoroutine(backToMenu());
        }
    }

    //takes the player back to the main menu after five seconds when called
    IEnumerator backToMenu()
    {
        yield return new WaitForSeconds(5.0f);
        SceneSwitch.instance.gameOver(0);
        print("returned to menu");
    }
}
