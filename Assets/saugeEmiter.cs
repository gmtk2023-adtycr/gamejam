using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saugeEmiter : MonoBehaviour
{
    public GameObject prefab;
    public float sizeRand = 1.0f;

    public float timedelay = 0.1f;
    public float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        transform.localPosition = Vector3.zero;

        if(timeLeft > timedelay) 
        {
            GameObject go = GameObject.Instantiate( prefab);
            go.transform.position= transform.position + Vector3.up * Random.Range(-sizeRand, sizeRand) + Vector3.right * Random.Range(-sizeRand, sizeRand);
            timeLeft = 0;
        }


    }
}
