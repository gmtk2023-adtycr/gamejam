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
        // mise à jour du texte affiché à chaque nouvelle tâche
        _taskManager.OnNextTask += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        _text.text = _taskManager.CurrentTask.Description;
    }
}
