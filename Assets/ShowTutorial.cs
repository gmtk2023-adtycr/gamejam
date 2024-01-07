using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{

    public List<Behaviour> Behaviours = new();
    
    private static bool shown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (shown){
            Destroy(gameObject);
            return;
        }
        shown = true;
        Behaviours.ForEach(b => b.enabled = false);
    }

    public void HideTutorial(){
        Behaviours.ForEach(b => b.enabled = true);
        Destroy(gameObject);
    }

}
