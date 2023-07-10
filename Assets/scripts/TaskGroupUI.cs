using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskGroupUI : MonoBehaviour
{

    public taskList tasks;
    public GameObject prefabTaskUI;

    public TMP_Text titleGroup;
    public RectTransform panel;

    // Start is called before the first frame update
    void Start()
    {
        if (tasks != null)
        {
            titleGroup.text = tasks.nameGroup;

            for (int i = 0; i < tasks.list.Count; i++)
            {
                GameObject go = GameObject.Instantiate(prefabTaskUI);
                TaskUI taskUI = go.GetComponent<TaskUI>();
                taskUI.tasks = tasks;
                taskUI.idTask = i;

                go.transform.parent = panel;
                go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
                go.transform.transform.localScale = Vector3.one;

            }
        ((RectTransform)transform).sizeDelta = new Vector2(((RectTransform)transform).sizeDelta.x, 35 + 30 * tasks.list.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
