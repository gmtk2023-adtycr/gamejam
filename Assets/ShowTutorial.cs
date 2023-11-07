using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{

    private static bool shown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(shown)
            Destroy(gameObject);
        shown = true;
    }

}
