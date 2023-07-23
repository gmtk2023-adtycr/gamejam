using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteragtTrigger : MonoBehaviour
{
    public UnityEvent interact;

    public GameObject Soundprefab;

    public bool autodestroy = false;

    bool trigerOn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            InteractUI.enterZone?.Invoke();
            trigerOn = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            InteractUI.exitZone?.Invoke();
            trigerOn = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && trigerOn)
        {
            interact?.Invoke();
            if (Soundprefab != null)
            {
                GameObject go = GameObject.Instantiate(Soundprefab);
                go.transform.position = Camera.main.transform.position;
            }

            if(autodestroy)
                GameObject.Destroy(gameObject);
        }
    }
}
