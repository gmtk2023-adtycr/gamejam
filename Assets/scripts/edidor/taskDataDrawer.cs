using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(taskList))]
public class taskDataDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        taskList obj = this.target as taskList;
        EditorGUILayout.LabelField("save data");

        for(int i = 0; i < obj.list.Count; i++)
        {
            string key = obj.name + i;
            int datavalue = PlayerPrefs.GetInt(key, 0);
            float temp = EditorGUILayout.IntField(key + " : ", datavalue);
            if (temp != datavalue) { PlayerPrefs.SetFloat(key, temp); PlayerPrefs.Save(); }
        }

    }
}
