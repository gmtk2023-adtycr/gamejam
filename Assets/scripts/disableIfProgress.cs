using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class disableIfProgress : MonoBehaviour
{
    public List<testData> testData = new List<testData>();

    public GameObject gObject;
    public bool inverse = false;
    public bool autoDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inverse)
        {
            gObject.SetActive(true);

            int i = 0;
            while (i < testData.Count && gObject.activeSelf)
            {
                gObject.SetActive(testData[i].test());
                i++;
            }

            if(!gObject.activeSelf)
                GameObject.Destroy(gameObject);
        }
        else
        {
            gObject.SetActive(false);

            int i = 0;
            while (i < testData.Count && !gObject.activeSelf)
            {
                gObject.SetActive(testData[i].test());
                i++;
            }

            if (gObject.activeSelf)
                GameObject.Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class testData
{
    public taskList task;
    public int id;
    public bool test()
    {
        return task.test(id);
    }

}
