using PrimeTween;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<LightConeCollider>().OnDetectPlayer += OnDetectPlayer;
    }

    private void OnDetectPlayer(GameObject player)
    {
        Debug.Log("Detected by " + transform.parent.parent.gameObject.name);
        Tween.ShakeCamera(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), strengthFactor: 2.0f);
        player.GetComponent<Movement>()?.Die();
    }


}
