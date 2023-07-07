using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{

    public taskList tasks;
    public int idTask;

    public TMP_Text title;
    public TMP_Text taskValue;

    public GameObject checkMark;

    // Start is called before the first frame update
    void Start()
    {
        int val = PlayerPrefs.GetInt(tasks.name + idTask, 0);
        title.text = tasks.list[idTask].taskName;
        taskValue.text = PlayerPrefs.GetInt(tasks.name+idTask,0)+ "/" + tasks.list[idTask].taskvalue;
        checkMark.SetActive(val == tasks.list[idTask].taskvalue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
