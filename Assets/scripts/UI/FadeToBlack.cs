using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class FadeToBlack : MonoBehaviour
{
    Image img;

    [Range(0f, 1f)]
    public float intencity = 0;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.color = Color.Lerp(new Color(0f, 0f, 0f, 0f), Color.black, intencity);
    }

    public static void setIntencity(float intencity)
    {
        FadeToBlack fade = GameObject.FindAnyObjectByType<FadeToBlack>();
        if (fade != null ) 
        fade.intencity = intencity;
    }
}
