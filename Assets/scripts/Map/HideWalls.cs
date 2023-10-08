using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideWalls : MonoBehaviour
{


    public List<GameObject> Walls;

    public void Awake()
    {
        if(Application.isEditor) return;
        var playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        if(Physics2D.IsTouching(GetComponent<PolygonCollider2D>(), playerCollider))
            OnTriggerStay2D(playerCollider);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SubCollider") && collision.gameObject.transform.parent.CompareTag("Player"))
        {
            Walls.ForEach(w => {
                //w.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, .7f);
                w.SetActive(false);
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        this.OnTriggerStay2D(other);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SubCollider") && collision.gameObject.transform.parent.CompareTag("Player"))
        {
            Walls.ForEach(w => {
                //w.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
                w.SetActive(true);
            });        
        }
    }
}
