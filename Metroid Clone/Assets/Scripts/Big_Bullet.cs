using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Bullet : MonoBehaviour
{
    public float speed;
    public bool goingLeft;

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


    //if this object collides with a specified tagged object it is destroyed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
