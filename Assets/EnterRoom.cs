using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public string roomName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            TaskGroupAddRemove.addTaskGroup(roomName);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            TaskGroupAddRemove.removeTaskGroup(roomName);
    }
}
