using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[ExecuteAlways]
public class FollowPlayer : MonoBehaviour
{

    public Transform PlayerTransform;

    public List<Rect> allowZone = new List<Rect>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rect innerRect = Rect.zero;
        int i = 0;

        while(i < allowZone.Count && innerRect == Rect.zero)
        {
            if (allowZone[i].Contains(PlayerTransform.position))
            {
                //Debug.Log("in " + i);
                innerRect = allowZone[i];
            }
            else i++;
        }

        Vector3 targetPosition = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z);

        if (innerRect != Rect.zero)
        {
            float xOffset = Camera.main.orthographicSize * Camera.main.aspect;
            float yOffset = Camera.main.orthographicSize;

            targetPosition.x = Mathf.Clamp(targetPosition.x, innerRect.xMin + xOffset, innerRect.xMax - xOffset);
            targetPosition.y = Mathf.Clamp(targetPosition.y, innerRect.yMin + yOffset, innerRect.yMax - yOffset);
        }


        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);



        foreach(Rect r in allowZone) 
        {
            Debug.DrawLine(new Vector3(r.xMin, r.yMin), new Vector3(r.xMin, r.yMax), Color.magenta);
            Debug.DrawLine(new Vector3(r.xMin, r.yMax), new Vector3(r.xMax, r.yMax), Color.magenta);
            Debug.DrawLine(new Vector3(r.xMax, r.yMax), new Vector3(r.xMax, r.yMin), Color.magenta);
            Debug.DrawLine(new Vector3(r.xMax, r.yMin), new Vector3(r.xMin, r.yMin), Color.magenta);
        }


    }
}
