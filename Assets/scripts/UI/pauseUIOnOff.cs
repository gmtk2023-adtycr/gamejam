using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseUIOnOff : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            toggleMenu();
        }
    }

    public void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);

        if(Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    // }
    // public void returnMenu()
    // {
    //     SceneManager.LoadScene(0);
    // }
    //
    // public void QuitGame(){
    //     Application.Quit();
    }
}
