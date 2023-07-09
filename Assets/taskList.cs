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
        if(val < list[id].taskvalue)
        val++; 
        setTask(id, val);
    }

    public void setTask(int id, int val)
    {
        GameObject go = GameObject.Find("Player");

        PlayerPrefs.SetFloat("PlayerX", go.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", go.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", go.transform.position.z);

        PlayerPrefs.SetInt(name+id,val);
        PlayerPrefs.Save();

        TaskUI.taskUpdate(name, id);
    }
}

[System.Serializable]
public class taskData
{
    public string taskName;
    public int taskvalue;
}