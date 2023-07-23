using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{

    public Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 targetPosition =
            new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }
}