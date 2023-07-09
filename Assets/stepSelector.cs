using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class stepSelector : MonoBehaviour
{
    public int Step = 0;

    public List<GameObject> steps = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetStep(int id)
    {
        Step = id;
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < steps.Count; i++) 
        {
            steps[i].SetActive(i== Step);
        }
    }
}
