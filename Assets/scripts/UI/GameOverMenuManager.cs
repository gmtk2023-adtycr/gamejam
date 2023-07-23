using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

}
