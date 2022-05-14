/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hard_Enemies : MonoBehaviour
{
    //stors transform information of a choosen object
    public Transform target;

    private Rigidbody rb;

    //speed modifier for the enemy game object
    public int speed;

    //health modifier
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //target = FindObjectOfType<Player>().transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyFollow();
        checkHealth();
    }

    //function allows for enemy to follow designated target based on the players and enemies transform information
    void enemyFollow()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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

                //checks the health everytime it is damaged
                checkHealth();
            }

            //if the above is false then the enemy loses one health
            else
            {
                health -= 1;

                //checks the health everytime it is damaged
                checkHealth();
            }
        }

        if (other.gameObject.tag == "Death Barrier")
        {
            Destroy(this.gameObject);
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
