/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool goingLeft;

    public GameObject enemyPrefab;
    public float spawnDelay;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", startDelay, spawnDelay);
    }

    //spawns projectile, gives tranform information, pulls the going left from Regular script and sets it equal to that of this script, allowing for the altering of the bool value in the prefab in sync with that of the spawner
    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
        if (projectile.GetComponent<Regular_Enemies>())
        {
            projectile.GetComponent<Regular_Enemies>().goingLeft = goingLeft;
        }
    }
}
