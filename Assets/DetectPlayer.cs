using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = GetEnemyDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 plp = gameObject.transform.parent.localPosition;
        Debug.Log(plp.x + " " + enemy.GetComponent<SpriteRenderer>().flipX);
        //parent_local_position.x = Mathf.Abs(parent_local_position.x) * (enemy.GetComponent<SpriteRenderer>().flipX ? - 1 : 1);

        gameObject.transform.parent.localPosition = new Vector3(Mathf.Abs(plp.x) * (enemy.GetComponent<SpriteRenderer>().flipX ? -1 : 1), plp.y, plp.z);
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
