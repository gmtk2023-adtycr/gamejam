using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class disableIfProgress : MonoBehaviour
{
    public List<testData> testData = new List<testData>();

    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.SetActive(true);

        int i = 0;
        while(i < testData.Count && gameObject.active)
        {
            gameObject.SetActive(testData[i].test());
            i++;
        }
        
    }
}

public class testData
{
    public taskList task;
    public int id;
    public bool test()
    {
        int val = PlayerPrefs.GetInt(task.name + id, 0);
        int testval = task.list[id].taskvalue;

        return val == testval;
    }

}
