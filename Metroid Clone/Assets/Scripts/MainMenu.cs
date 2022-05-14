/*
 * Author: Marco Ramirez-Buckles
 * Date: 5/13/2022
 * Last Updated: 5/13/2022 Marco Ramirez-Buckles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //initializing the instance
    public static MainMenu instance;

    private void Start()
    {
        instance = this;
    }

    public void PlayGame()
    {
        //for main menu scene, button will procede to scene named scene 1
        SceneManager.LoadScene("Scene 1");
    }

    public void ReadMe()
    {
        //for main menu scene, button will procede to scene named READ ME
        SceneManager.LoadScene("READ ME");
    }

    public void Back()
    {
        //for main menu scene, button will return player to main menu
        SceneManager.LoadScene("Menu");
    }

   
    public void QuitGame()
    {
        //for main menu scene, button will exit the the application
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
