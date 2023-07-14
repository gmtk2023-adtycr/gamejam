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
        enemy = gameObject.transform.parent.parent.gameObject;
        enemyBody = enemy.GetComponent<Rigidbody2D>();
        base_pos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyBody.velocity.x == 0f && enemyBody.velocity.y == 0f){
            return;
        }
        Vector3 dir = GetEnemyDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        //transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.AngleAxis(angle, Vector3.forward), .1f);
        
        

        Vector3 plp = gameObject.transform.parent.localPosition;
        gameObject.transform.parent.localPosition = new Vector3(Mathf.Abs(plp.x) * (enemy.GetComponent<SpriteRenderer>().flipX ? -1 : 1), plp.y, plp.z);
        gameObject.transform.localPosition = base_pos;
    }


    private Vector3 GetEnemyDirection()
    {
        if(enemy == null)
            return Vector3.zero;

        return enemy.GetComponent<Rigidbody2D>().velocity;
    }
    
}
