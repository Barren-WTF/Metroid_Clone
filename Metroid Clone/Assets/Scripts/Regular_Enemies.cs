/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Enemies : MonoBehaviour
{
    //speed modifier for the enemy game object
    public int speed;

    //used to store the true/false in regards to the object moving left
    public bool goingLeft;

    //health modifier
    public int health;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        checkHealth();
    }

    //function for making the enemy game object move back and forth
    private void Move()
    {

        //sees if goingLeft is true
        if (goingLeft == true)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }

        //if going left is false, then this is executed
        else if (goingLeft == false)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //checks if collided object is tagged as Wall
        if (other.gameObject.tag == "Wall")
        {
            //if going left is true, it will be changed to false
            if (goingLeft == true)
            {
                goingLeft = false;
            }

            //if going left is false, it will be changed to true
            else if (goingLeft == false)
            {
                goingLeft = true;
            }
        }

        if (other.gameObject.tag == "Regular Enemy")
        {
            //if going left is true, it will be changed to false
            if (goingLeft == true)
            {
                goingLeft = false;
            }

            //if going left is false, it will be changed to true
            else if (goingLeft == false)
            {
                goingLeft = true;
            }
        }

        if (other.gameObject.tag == "Hard Enemy")
        {
            //if going left is true, it will be changed to false
            if (goingLeft == true)
            {
                goingLeft = false;
            }

            //if going left is false, it will be changed to true
            else if (goingLeft == false)
            {
                goingLeft = true;
            }
            
        }

        //destroys enemy if it makes contact with death barrier
        if (other.gameObject.tag == "Death Barrier")
        {
            Destroy(this.gameObject);
        }

        //checks collided object tag
        if (other.gameObject.tag == "regularBullet")
        {

            //if tag is regularBullet, and the function isBig from the Regular bullet script returns true, enemy looses three health
            if (other.gameObject.GetComponent<Regular_Bullet>().isBig() == true)
            {
                health -= 3;

                //checks the health everytime it is damaged
                checkHealth();
            }

            //eif the above is false then the enemy loses one health
            else
            {
                health-= 1;

                //checks the health everytime it is damaged
                checkHealth();
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
