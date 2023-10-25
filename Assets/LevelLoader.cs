using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;
    public void LoadNextLevel(string scene)
    {
      StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
      Time.timeScale = 1;
      transition.SetTrigger("Start");
      yield return new WaitForSeconds(transitionTime);
      Grid.ResetGrids();
      SceneManager.LoadScene(scene);
    }
}
