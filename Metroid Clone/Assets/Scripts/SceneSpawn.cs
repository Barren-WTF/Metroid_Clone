/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawn : MonoBehaviour
{
    void Start()
    {
        //When the player spawns in, they are put at the game object called: spawn point's coordinates
        GameObject.FindGameObjectWithTag("player").transform.position = this.gameObject.transform.position;
    }
}
