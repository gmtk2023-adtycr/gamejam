using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteragtTrigger : MonoBehaviour
{
    public UnityEvent interact;

    public GameObject Soundprefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            InteractUI.enterZone?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Player"))
            InteractUI.exitZone?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("interact");
            interact.Invoke();
            GameObject go  = GameObject.Instantiate(Soundprefab);
            go.transform.position = Camera.main.transform.position;
        }
    }
}
