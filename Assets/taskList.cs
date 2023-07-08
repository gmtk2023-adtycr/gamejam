using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "task", menuName = "script/taskList", order = 1)]
public class taskList : ScriptableObject
{
    public string nameGroup = "";
    public List<taskData> list = new List<taskData>();

    public void ppTask(int id)
    {

        int val = PlayerPrefs.GetInt(name + id,0);
        val++; 
        setTask(id, val);
    }

    public void setTask(int id, int val)
    {
        PlayerPrefs.SetInt(name+id,val);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class taskData
{
    public string taskName;
    public int taskvalue;
}