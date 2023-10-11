using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class pauseUIOnOff : MonoBehaviour
{
    public GameObject menu;
    [SerializeField] private AudioMixer myAudioMixer;
    [Range(200,8000)][SerializeField] private float muffled = 1800;
    [Range(10000,22000)][SerializeField] private float not_muffled = 22000;


    [Range(200,8000)][SerializeField] private float highpassed = 100;
    [Range(10000,22000)][SerializeField] private float not_highpassed = 10;


        private bool isPaused = false;
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
        isPaused = !isPaused;
        menu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
            myAudioMixer.SetFloat("MUFFLED IN MENU", muffled);
            myAudioMixer.SetFloat("HIGHPASS IN MENU", highpassed);
        }
        else
        {
            Time.timeScale = 1;
            myAudioMixer.SetFloat("MUFFLED IN MENU", not_muffled);
            myAudioMixer.SetFloat("HIGHPASS IN MENU", not_highpassed);
        }
    // }
    // public void returnMenu()
    // {
    //     SceneManager.LoadScene(0);
    // }
    //
    // public void QuitGame(){
    //     Application.Quit();
    }

    public void ExitMenu()
    {

    myAudioMixer.SetFloat("MUFFLED IN MENU", not_muffled);
    myAudioMixer.SetFloat("HIGHPASS IN MENU", not_highpassed);    }
}
