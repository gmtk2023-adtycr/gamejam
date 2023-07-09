using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityTeleportTask : MonoBehaviour
{

    public void teleport(GameObject player, taskList task, int taskId)
    {
        player.transform.position = transform.position;

        task.ppTask(taskId);
    }

    
}
