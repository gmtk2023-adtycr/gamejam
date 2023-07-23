using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu]
public class Task : ScriptableObject
{

    public List<GameObject> Requirements = new();

    public EventTrigger.TriggerEvent OnDone;

}
