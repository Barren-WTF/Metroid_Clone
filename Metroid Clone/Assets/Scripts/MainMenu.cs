using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public static MainMenu instance;

    private void Start()
    {
        instance = this;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
