using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideWalls : MonoBehaviour
{


    public List<GameObject> Walls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Walls.ForEach(w => {
                //w.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, .7f);
                w.SetActive(false);
            });
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Walls.ForEach(w => {
                //w.GetComponent<Tilemap>().color = new Color(1f, 1f, 1f, 1f);
                w.SetActive(true);
            });        
        }
    }
}
