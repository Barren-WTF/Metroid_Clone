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
        //when the new scene is loaded these items are not destroyed.
        instance = this;
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(UI);
    }

    //move to the scene number indicated
    public void switchScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
