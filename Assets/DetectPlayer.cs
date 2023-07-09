using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private GameObject enemy;

    public GameObject deathSound;

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
        transform.localPosition = Vector3.zero;

    }


    private Vector3 GetEnemyDirection()
    {
        return enemy.GetComponent<Rigidbody2D>().velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject go = GameObject.Instantiate(deathSound);
            go.transform.position= Camera.main.transform.position;
        }

    }

}
