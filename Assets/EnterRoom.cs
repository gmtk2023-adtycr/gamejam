using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public taskList room;

    private void OnEnable()
    {
        TaskGroupAddRemove.addTaskGroup?.Invoke(room.name);
    }

    private void OnDisable()
    {
        TaskGroupAddRemove.removeTaskGroup?.Invoke(room.name);
    }
}
