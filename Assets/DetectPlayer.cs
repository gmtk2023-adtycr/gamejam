using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private GameObject ennemy;

    // Start is called before the first frame update
    void Start()
    {
        ennemy = gameObject.transform.parent.parent.gameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = GetEnnemyDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localPosition = Vector3.zero;

    }


    private Vector3 GetEnnemyDirection()
    {
        return ennemy.GetComponent<followPath>().direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Debug.Log("Player detected");

    }

}
