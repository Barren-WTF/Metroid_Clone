using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public bool goingLeft;

    public GameObject projectilePrefab;
    public float spawnDelay;
    public float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnProjectile", startDelay, spawnDelay);
    }

    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        if (projectile.GetComponent<Laser>())
        {
            projectile.GetComponent<Laser>().goingLeft = goingLeft;
        }
    }
}
