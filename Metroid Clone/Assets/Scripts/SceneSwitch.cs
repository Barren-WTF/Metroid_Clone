/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject UI;

    public static SceneSwitch instance;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(player);
            Destroy(mainCamera);
            Destroy(UI);
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(this.gameObject);

    }

    //move to the scene number indicated
    public void switchScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);    
    }

    public void gameOver(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Destroy(player);
        Destroy(mainCamera);
        Destroy(UI);
        Destroy(this.gameObject);
    }
}
