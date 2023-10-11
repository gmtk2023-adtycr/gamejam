using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    Vector2 StartPos;
    [SerializeField] float moveModifier;

    private void Start()
    {
        StartPos = transform.position;
    }

    private void Update()
    {
        // Utilisez Input.mousePosition pour obtenir la position de la souris
        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Calculez la nouvelle position en fonction de la position de la souris
        float posX = Mathf.Lerp(transform.position.x, StartPos.x + (mousePosition.x * moveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPos.y + (mousePosition.y * moveModifier), 2f * Time.deltaTime);

        // Mettez à jour la position du sprite
        transform.position = new Vector3(posX, posY, 0);
    }
}
