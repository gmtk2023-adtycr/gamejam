using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskGroupAddRemove : MonoBehaviour
{

    public List<taskList> tasks = new List<taskList>();
    public GameObject taskGroupUIPrefab;


    public static Action<string> addTaskGroup;
    public static Action<string> removeTaskGroup;

    Dictionary<string,GameObject> taskIn = new Dictionary<string,GameObject>();

    private void OnEnable()
    {
        addTaskGroup += addTaskGroupUI;
        removeTaskGroup += removeTaskGroupUI;
    }
    private void OnDisable()
    {
        addTaskGroup -= addTaskGroupUI;
        removeTaskGroup -= removeTaskGroupUI;
    }

    taskList findTask(string name)
    {
        taskList task = null;
        int i = 0;

        while (i < tasks.Count && task == null)
        {
            if (tasks[i].name == name) 
            {
                task = tasks[i];
            }
            else
                i++;
        }
        return task;

    }
    void addTaskGroupUI(string name)
    {
        Debug.Log(name);
        if (!taskIn.Keys.Contains(name) || taskIn.Count == 0)
        {
            taskList task = findTask(name);
            Debug.Log(task);
            GameObject taskGroup = GameObject.Instantiate(taskGroupUIPrefab);
            taskGroup.GetComponent<TaskGroupUI>().tasks = task;
            taskGroup.transform.parent = transform;
            taskGroup.transform.localPosition = new Vector3(taskGroup.transform.localPosition.x, taskGroup.transform.localPosition.y, 0);
            taskGroup.transform.transform.localScale = Vector3.one;

            taskIn.Add(name, taskGroup);
        }
    }
    void removeTaskGroupUI(string name)
    {
        if (taskIn.Keys.Contains(name))
        {
            GameObject.Destroy(taskIn[name]);
            taskIn.Remove(name);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
