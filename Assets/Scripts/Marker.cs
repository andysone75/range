using UnityEngine;

public class Marker : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private RingManager rm;

    public bool active = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Pointer") return;
        active = true;
        var ring = rm.getRing();
        rm.active = ring.active;
        sr.color = rm.c_Marker_Right;
        if (rm.active) ring.StartHolding();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Pointer") return;
        if (rm.active) rm.getRing().StopHolding();
        active = false;
        rm.active = false;
        sr.color = rm.c_Marker_Wrong;
    }
}
