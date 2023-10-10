/*using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ParallaxBG : MonoBehaviour {
    Vector2 StartPos;
    [SerializeField] float moveModifier;

    private void Start(){
        StartPos = transform.position;
    }

    private void Update(){
        //Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 pz = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //Debug.Log(pz);
        //Debug.Log(pz.x);
        //Debug.Log(pz.y);
        float posX = Mathf.Lerp(transform.position.x, StartPos.x + (pz.x * moveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPos.y + (pz.y * moveModifier), 2f * Time.deltaTime);

        transform.position = new Vector3(
          posX,
          posY,
          0
        );
    }
}
*/
