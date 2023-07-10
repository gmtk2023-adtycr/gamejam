using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alertHunter : MonoBehaviour
{
    aiSelector ai;

    // Start is called before the first frame update
    void Start()
    {
        aiSelector[] ais = GameObject.FindObjectsOfType<aiSelector>();

        if (ais.Length > 0)
        {
            ai = ais[0];
            
            int i = 1;
            while(i < ais.Length) 
            {
                if (Vector3.Distance(transform.position, ais[i].transform.position) < Vector3.Distance(transform.position, ai.transform.position))
                {
                    ai = ais[i];
                }
                i++;
            }

            ai.search(gameObject);
            ai.changeToSearch();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, ai.transform.position) < 0.1)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
