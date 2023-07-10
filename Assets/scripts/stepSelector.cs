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
        Step = PlayerPrefs.GetInt("globalStep", 0);
    }

    public void SetStep(int id)
    {
        Step = id;

        PlayerPrefs.SetInt("globalStep", id);
        PlayerPrefs.Save();
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
