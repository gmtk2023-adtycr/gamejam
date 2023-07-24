using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Task : MonoBehaviour
{

    public List<GameObject> Requirements = new();
    public string Description;

    public EventTrigger.TriggerEvent OnDone;

    private int SubTaskDoneCount;

    void Start()
    {
        foreach (var gameObject in Requirements)
        {
            var kyb = gameObject.GetComponent<KeyItemBehaviour>();
            if(kyb == null)
                Debug.LogError($"GameObject {gameObject.name} must have a KeyItemBehaviour script");
            else
                kyb.OnGet += TriggerOnDone;
        }
    }

    private void TriggerOnDone()
    {
        if(++SubTaskDoneCount == Requirements.Count)
            OnDone.Invoke(new BaseEventData(EventSystem.current));
    }

}
