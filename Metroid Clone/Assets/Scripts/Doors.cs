using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject blueDoor;
    public GameObject redDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (blueDoor)
        {
            if (other.gameObject.tag == "regularBullet")
            {
                Destroy(this.gameObject);
            }

            else if (other.GetComponent<Regular_Bullet>().bigBullet == true)
            {
                this.gameObject.SetActive(false);
            }
        }

        else if (redDoor)
        {
            
            if (other.GetComponent<Regular_Bullet>().bigBullet == true)
            {
                this.gameObject.SetActive(false);
            }
        }

    }
}
