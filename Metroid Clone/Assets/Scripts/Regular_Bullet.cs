/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Bullet : MonoBehaviour
{
    public float speed;
    public bool goingLeft;

    public bool bigBullet = false;

    // Update is called once per frame
    void Update()
    {
        //checks if the bullet is moving left or right, and sets it's speed to the defined number

        bulletDirection();
        StartCoroutine(despawn());
    }

    void bulletDirection()
    {
        bulletLeft();
    }


    //tests if the parameter goinLeft is true or false to dtermine which way the bullet should travel when spawned
    void bulletLeft()
    {
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }

        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
    }


    //after 3 seconds the bullet despawns
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    //scales the bullet prefab up
    public void scaleUp()
    {
        transform.localScale += new Vector3(.7f, .7f, .7f);
        bigBullet = true;
    }

    //for player script, allows player script to determine if bigbullet is true
    public bool isBig()
    {
        return bigBullet;
    }

    //if this object collides with a specified tagged object it is destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hard Enemy")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Regular Enemy")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Blue Door")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Red Door")
        {
            Destroy(this.gameObject);
            if (bigBullet == true)
            {
                Destroy(other.gameObject);
            }
            
        }
    }
}
