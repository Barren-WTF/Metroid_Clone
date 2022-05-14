/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    //contols how long the spawner is active
    public float SpawnerDuration;

    //attachable game object
    public GameObject enemyPrefab;

    //controls how long before the spawner activates and the rate at which it spawns
    public float spawnDelay;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnDelay);
        StartCoroutine(StopSpawning());
    }

    //spawns the attached prefab
    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
    }

    //when timer is up, the spawner is destroyed
    IEnumerator StopSpawning()
    {
        yield return new WaitForSeconds(SpawnerDuration);
        Destroy(this.gameObject);
    }
}
