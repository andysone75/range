using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    Vector2 startVector;

    private void Start()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        Vector3 unitsSizeVector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        float offset = unitsSizeVector.y * .2f / 2;
        collider.size = new Vector2(unitsSizeVector.x * 2, unitsSizeVector.y * 1.8f);
        collider.offset = new Vector2(.0f, offset);
    }

    private void OnMouseDown()
    {
        startVector = GetPlayerVector();
    }

    private void OnMouseDrag()
    {
        if (!GameManager.isGame) return;

        Vector2 newVector = GetPlayerVector();

        float newScale = newVector.magnitude / 96.0f;
        if (newScale < 1.35f) newScale = 1.35f;
        else if (newScale > 4.3f) newScale = 4.3f;
        player.localScale = new Vector3(newScale, newScale, 1);

        float angle = Vector2.SignedAngle(startVector, newVector);
        player.Rotate(.0f, .0f, angle);

        startVector = newVector;
    }

    private Vector2 GetPlayerVector() => new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
    public float getRotationKoef()
    {
        float rotataionKoef = Vector2.SignedAngle(Vector2.up, player.up);
        rotataionKoef /= 180.0f;
        if (rotataionKoef < 0) rotataionKoef = 1 + rotataionKoef;
        return rotataionKoef;
    }
}
