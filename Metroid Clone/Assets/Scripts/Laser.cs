/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //variable that controlls speed
    public float speed;

    //variable that controls if the laser goes left or right
    public bool goingLeft;

    // Update is called once per frame
    void Update()
    {
        if (goingLeft == true)
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        else
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }

        StartCoroutine(despawn());
    }

    //after 5 seconds the bullet despawns
    IEnumerator despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
