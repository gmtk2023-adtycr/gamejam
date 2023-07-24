using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTask : MonoBehaviour
{
    private TaskManager _taskManager;
    private TextMeshProUGUI _text;
    void Awake()
    {
        _taskManager = GameObject.FindGameObjectWithTag("Phases").GetComponent<TaskManager>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        
        Debug.Log(_taskManager);
        Debug.Log(_text);

        _taskManager.OnNextTask += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        _text.text = _taskManager.CurrentTask.Description;
    }
}
