using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hard_Enemies : MonoBehaviour
{
    //game objects will be used to control the direction in which the enemy moves
    public GameObject target;

    private Rigidbody rb;

    //speed modifier for the enemy game object
    public int speed;

    //health modifier
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        stopMove();
        checkHealth();
    }



    //function for making the enemy game object move back and forth between two points
    private void Move()
    {
        if (transform.position.x < target.transform.position.x)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }

        else if (transform.position.x > target.transform.position.x)
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }
    }

    public void stopMove()
    {
        if (GetComponent<Player>().health <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //checks collided object tag
        if (other.gameObject.tag == "regularBullet")
        {

            //if tag is regularBullet, and the function isBig from the Regular bullet script returns true, enemy looses three health
            if (other.gameObject.GetComponent<Regular_Bullet>().isBig() == true)
            {
                health -= 3;
            }

            //eif the above is false then the enemy loses one health
            else
            {
                health--;
            }
        }
    }

    //function checks the current health, if health is less than or equal to 0, then it is destroyed
    private void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
