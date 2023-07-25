using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    private List<Task> _tasksOfCurrentPhase = new();
    private int _currentPhaseId = -1, _currentTaskId;
    
    public Task CurrentTask => _tasksOfCurrentPhase[_currentTaskId];
    public event Action OnNextTask;
    
    void Start()
    {
        // disabled all tasks
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetChild(i).gameObject.SetActive(false);
        NextTask(true);
    }

    /// <summary>
    /// Passe à la prochaine tâche
    /// Charge la phase suivante si nécessaire
    /// Si la tâche actuelle est la dernière, on détruit le manager
    /// </summary>
    /// <param name="first">true first time it's called</param>
    private void NextTask(bool first = false)
    {
        if(!first) 
            CurrentTask.gameObject.SetActive(false);
        _currentTaskId++;
        if (first || _currentTaskId == _tasksOfCurrentPhase.Count)
        {
            bool phaseLoaded = LoadNextPhase();
            if(!phaseLoaded)
                return;
        }
        CurrentTask.gameObject.SetActive(true);
        CurrentTask.OnDone.AddListener(e => NextTask());
        OnNextTask?.Invoke();
    }

    /// <summary>
    /// Chargement de la prochaine phase
    /// Reset des l'id task à 0
    /// </summary>
    /// <returns>Vrai si les tâches ont bien été chargées, faux sinon (si plus de tâches)</returns>
    private bool LoadNextPhase()
    {
        _currentPhaseId++;
        if (transform.childCount == _currentPhaseId)
        {
            Destroy(this);
            return false;
        }
        _tasksOfCurrentPhase.Clear();
        var phaseTransform = transform.GetChild(_currentPhaseId);
        for(int i = 0; i < phaseTransform.childCount; i++)
            _tasksOfCurrentPhase.Add(phaseTransform.GetChild(i).gameObject.GetComponent<Task>());
        _currentTaskId = 0;
        return true;
    }
    
}