using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "task", menuName = "script/taskList", order = 1)]
public class taskList : ScriptableObject
{
    public string nameGroup = "";
    public List<taskData> list = new List<taskData>();
}

[System.Serializable]
public class taskData
{
    public string taskName;
    public int taskvalue;
}