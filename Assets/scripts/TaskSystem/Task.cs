using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Task : MonoBehaviour
{
    
    public string Description;
    public List<GameObject> Requirements = new();
    public EventTrigger.TriggerEvent OnDone;

    private int _subTaskDoneCount;

    public void Initialize()
    {
        foreach (var gameObject in Requirements)
        {
            var kyb = gameObject.GetComponent<UsableItem>();
            if (kyb == null) {
                Debug.LogError($"GameObject {gameObject.name} must have a KeyItemBehaviour script");
            }
            else{
                if (!kyb.Active) //if item is not active before the task, disable it after the task is done
                    kyb.OnUse += () => kyb.Active = false;
                kyb.OnUse += TriggerOnDone;
                kyb.Active = true;
            }
        }
    }

    private void TriggerOnDone()
    {
        if (++_subTaskDoneCount == Requirements.Count){
            OnDone.Invoke(new BaseEventData(EventSystem.current));
            PlayerPrefs.SetString("Last_Task", name);
        }
            
    }

}