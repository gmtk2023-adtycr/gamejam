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

    private int SubTaskDoneCount;

    public void Initialize()
    {
        foreach (var gameObject in Requirements)
        {
            var kyb = gameObject.GetComponent<KeyItemBehaviour>();
            if (kyb == null) {
                Debug.LogError($"GameObject {gameObject.name} must have a KeyItemBehaviour script");
                return;
            }

            kyb.Active = true;
            kyb.OnGet += TriggerOnDone;
        }
    }

    private void TriggerOnDone()
    {
        if(++SubTaskDoneCount == Requirements.Count)
            OnDone.Invoke(new BaseEventData(EventSystem.current));
    }

}
