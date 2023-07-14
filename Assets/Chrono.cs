using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Chrono : MonoBehaviour
{
    public TMP_Text txt;

    public float TimeTo;
    float TimeIn;
    public UnityEvent ChronoEnd;

    // Start is called before the first frame update
    void Start()
    {
        TimeIn = TimeTo;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeIn > 0)
        {
            txt.text = ((int)TimeIn).ToString();

            TimeIn -= Time.deltaTime;

            if (TimeIn < 0)
            {
                ChronoEnd.Invoke();
            }
        }
        else
        {

            txt.text = "0";
        }

    }
}
