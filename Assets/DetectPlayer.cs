using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private GameObject enemy;
    private Vector3 base_pos;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.parent.gameObject;
        base_pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = GetEnemyDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("detected");
            collision.gameObject.GetComponent<Movement>().enabled = false;
            deathControl.isdetect = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            deathControl.isdetect = false;
        }

    }

}
