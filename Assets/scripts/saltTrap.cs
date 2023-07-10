using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltTrap : MonoBehaviour
{
    public float timeLine = 0.5f;
    GameObject player;
    float followPlayer = 0;
    public float resLine = 0.1f;
    float upLine = 0;

    public LineRenderer line;

    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (upLine < resLine)
        {
            upLine += Time.deltaTime;
        }
        else
        {
            if (followPlayer > 0)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, player.transform.position - transform.position);
                followPlayer -= upLine;
            }
            else
            {
                line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - upLine);
                line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - upLine);
            }
            upLine -= resLine;
        }

        if(line.startColor.a <=0)
            isOn = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Contains("Player"))
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1f);
            line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1f);
            line.positionCount = 1;

            followPlayer = timeLine;
            isOn = true;
        }
    }
}
