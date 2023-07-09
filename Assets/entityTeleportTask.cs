using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityTeleportTask : MonoBehaviour
{

    public GameObject player;
        public taskList task;
        public int taskId;
    public void teleport()
    {

        player.transform.position = transform.position;

        task.ppTask(taskId);
    }

    
}
