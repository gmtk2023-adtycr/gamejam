using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathPoint : MonoBehaviour, IPointPath
{
    public float waitingTime = 0f; // seconds
    public float WaitingTime => waitingTime;
    public Vector3 Position => transform.position;

    public EventTrigger.TriggerEvent OnArrival;
    public EventTrigger.TriggerEvent OnDeparture;
    
    
    public void Arrival(){
        OnArrival.Invoke(new BaseEventData(EventSystem.current));
    }

    public void Departure(){        
        OnDeparture.Invoke(new BaseEventData(EventSystem.current));
    }
    
}