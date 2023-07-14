using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DisableIfProgress : MonoBehaviour
{
    public List<testData> testData = new List<testData>();

    public GameObject gObject;
    public bool inverse = false;
    public bool autoDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        if(gObject == null)
            Debug.Log("Missing property gObject on object " + name);
    }

    // Update is called once per frame
    void Update()
    {
        if(gObject == null)
            return;
        
        if (!inverse)
        {
            gObject.SetActive(true);

            int i = 0;
            while (i < testData.Count && gObject.activeSelf)
            {
                gObject.SetActive(testData[i].test());
                i++;
            }

            if(gObject.activeSelf && autoDestroy)
                GameObject.Destroy(gameObject);
        }
        else
        {
            gObject.SetActive(false);

            int i = 0;
            while (i < testData.Count && !gObject.activeSelf)
            {
                gObject.SetActive(!testData[i].test());
                i++;
            }

            if (!gObject.activeSelf && autoDestroy)
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
