using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeRotateWithEnemy : MonoBehaviour
{
 
    private GameObject enemy;
    private Rigidbody2D enemyBody;
    private Vector3 base_pos;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        enemyBody = enemy.GetComponent<Rigidbody2D>();
        base_pos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyBody.velocity.x == 0f && enemyBody.velocity.y == 0f)
            return;
        
        Vector3 dir = enemyBody.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), .1f);

        //var lp = transform.localPosition;
        //transform.localPosition = new Vector3(Mathf.Abs(lp.x) * (enemy.GetComponent<SpriteRenderer>().flipX ? -1 : 1), lp.y, lp.z);
        //transform.localPosition = base_pos;
    }
    
    
}
