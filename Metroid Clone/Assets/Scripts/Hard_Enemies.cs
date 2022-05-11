using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hard_Enemies : MonoBehaviour
{
    //game objects will be used to control the direction in which the enemy moves
    public GameObject target;
   


    //will be used to store the transform postiions of the game object
    private Vector3 targetPos;

    //speed modifier for the enemy game object
    public int speed;

    //used to store the true/false in regards to the object moving left
    public bool goingLeft;

    //health modifier
    public int health;

    private void Start()
    {
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        checkHealth();
    }



    //function for making the enemy game object move back and forth between two points
    private void Move()
    {

        //sees if goingLeft is true
        if (goingLeft == true)
        {
            //if the x position of the enemey game object does not exceed the x position of the set left point, then going left is false
            if (transform.position.x <= targetPos.x)
            {
                goingLeft = false;
            }

            //if goingleft is true, then the object will move left
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }

        //if going left is false, then this is executed
        else
        {
            //if the x position of the enemey game object is greater than or equal the x position of the set right point, then going left is true
            if (transform.position.x >= targetPos.x)
            {
                goingLeft = true;
            }
            else
            {
                //if goingLeft is false, then object will move right
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        //checks collided object tag
        if (other.gameObject.tag == "regularBullet")
        {

            //if tag is regularBullet, enemy looses one health
            health--;
        }

        //checks collided object tag
        if (other.gameObject.tag == "bigBullet")
        {

            //if tag is bigBullet, enemy looses three health
            health -= 3;
        }
    }

    //function checks the current health, if health is less than or equal to 0, then it is destroyed
    private void checkHealth()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }
}
