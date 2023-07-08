using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.gameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = GetEnemyDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.position = enemy.transform.position;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private Vector3 GetEnemyDirection()
    {
        return enemy.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Debug.Log("Player detected");
    }

}
