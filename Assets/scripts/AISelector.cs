using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AISelector : MonoBehaviour
{
    public aiMode mode;

    FollowPath path;
    earNoise ear;

    bool inreturn = false;

    // Start is called before the first frame update
    void Start()
    {
        path = this.GetComponent<FollowPath>();
        ear = this.GetComponent<earNoise>();

        mode = aiMode.path;
        path.enabled = true;
        ear.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inreturn)
        {
            if (Vector3.Distance(transform.position, ear.earsObject.transform.position) < 1f)
            {
                mode = aiMode.path;
                path.enabled = true;
                ear.enabled = false;
            }
        }
        if(mode == aiMode.search)
        {
            if(Vector3.Distance(transform.position, ear.earsObject.transform.position)< 0.1)
            {
                changeToPath();
            }
        }
    }

    public void changeToPath()
    {
        if (mode == aiMode.path)
            return;

        if (Vector3.Distance(transform.position, path.point.transform.position) < 1f)
        {
            mode = aiMode.path;
            path.enabled = true;
            ear.enabled = false;
        }
        else
        {
            ear.earsObject = path.point.gameObject;
        }
    }

    public void search( GameObject go)
    {
        ear.earsObject = go;
    }

    public void changeToSearch()
    {
        mode = aiMode.search;
        path.enabled = false;
        ear.enabled = true;
    }

}
public enum aiMode
{
    path, search
}