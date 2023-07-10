using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{

    public taskList tasks;
    public int idTask;

    public TMP_Text title;
    public TMP_Text taskValue;

    public GameObject checkMark;

    public static Action<string, int> taskUpdate;

    private void OnEnable()
    {
        taskUpdate += CheckTask;
    }
    private void OnDisable()
    {
        taskUpdate -= CheckTask;
    }

    void CheckTask(string name, int id)
    {
        if (tasks.name == name && idTask == id)
            taskUpdateUI();
    }
    void taskUpdateUI()
    {
        string strId = tasks.name + idTask;
        int val = 0;
        if (PlayerPrefs.HasKey(strId))
            val = PlayerPrefs.GetInt(strId, 0);
        title.text = tasks.list[idTask].taskName;
        taskValue.text = val + "/" + tasks.list[idTask].taskvalue;
        checkMark.SetActive(val == tasks.list[idTask].taskvalue);
    }
    // Start is called before the first frame update
    void Start()
    {
        taskUpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
