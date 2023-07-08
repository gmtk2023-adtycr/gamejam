using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DetectSalt : MonoBehaviour
{

    private GameObject ennemy;

    public List<GameObject> list = new List<GameObject>();
    bool returnWay = false;
    pathPoint prev;


    // Start is called before the first frame update
    void Start()
    {
        ennemy = gameObject.transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (list.Count > 0)
        {
            GameObject first = list[0];
            GameObject last = list[list.Count - 1];
            //Debug.Log(last.transform.position);
            if (Vector3.Distance(last.transform.position, ennemy.transform.position) < 0.1f && !returnWay)
            {
                List<GameObject> temp = new List<GameObject>();
                for (int i = 0; i < list.Count; i++)
                {
                    temp.Add(list[list.Count - 1 - i]);
                    if (temp.Count > 1)
                        temp[i- 1].GetComponent<pathPoint>().nextPoint = temp[i].GetComponent<pathPoint>();
                    
                }
                ennemy.GetComponent<followPath>().point = temp[0].GetComponent<pathPoint>();
                returnWay = true;
            }
            else if (Vector3.Distance(first.transform.position, ennemy.transform.position) < 0.1f && returnWay)
            {
                ennemy.GetComponent<followPath>().point = prev;

                foreach(GameObject temp in list)
                    GameObject.Destroy(temp);
                list.Clear();


                returnWay = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        saltTrap trap = collision.GetComponent<saltTrap>();
        if (trap != null)
            if(trap.isOn)
        {
            int count = trap.line.positionCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = new GameObject();
                go.transform.position = trap.transform.position + trap.line.GetPosition(i);
                pathPoint point = go.AddComponent<pathPoint>();
                if (list.Count > 0)
                    list[list.Count - 1].GetComponent<pathPoint>().nextPoint = point;
                list.Add(go);

            }
            returnWay = false;
            prev = ennemy.GetComponent<followPath>().point;
            ennemy.GetComponent<followPath>().point = list[0].GetComponent<pathPoint>();

        }

    }
}
