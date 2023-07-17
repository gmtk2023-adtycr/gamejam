using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Contains("Player"))
        {
            Debug.Log("Detected by " + transform.parent.parent.gameObject.name);
            collision.gameObject.GetComponent<Movement>().Die();
            deathControl.isdetect = true;
        }

    }
    
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            deathControl.isdetect = false;
        }

    }*/

}
