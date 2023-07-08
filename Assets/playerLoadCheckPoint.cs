using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLoadCheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerX"))
        {
            float PlayerX = PlayerPrefs.GetFloat("PlayerX");
            float PlayerY = PlayerPrefs.GetFloat("PlayerY");
            float PlayerZ = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(PlayerX, PlayerY, PlayerZ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
