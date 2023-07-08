using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public taskList room;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            TaskGroupAddRemove.addTaskGroup?.Invoke(room.name);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            TaskGroupAddRemove.removeTaskGroup?.Invoke(room.name);
    }
}
