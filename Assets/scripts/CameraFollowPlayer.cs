using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{

    public Transform _playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        Vector3 targetPosition =
            new Vector3(_playerTransform.position.x, _playerTransform.position.y, transform.position.z);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

    }
}