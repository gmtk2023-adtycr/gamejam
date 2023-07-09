using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        Debug.Log("Start Game");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Credit()
    {
        Debug.Log("Credit");
        SceneManager.LoadScene("Credit");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
