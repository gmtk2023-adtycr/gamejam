using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TaskCutscene : MonoBehaviour
{

    public void Play(){
        if(!TaskManager.Loading)
            GetComponent<PlayableDirector>().Play();
    }
    
}
