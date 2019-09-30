using UnityEngine;

public class Ring : MonoBehaviour
{
    private SpriteRenderer sr;
    private RingManager rm;

    private Coroutine holding;

    public bool active = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rm = GetComponent<RingManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        active = true;
        sr.color = rm.c_Ring_Right;

        Marker marker = rm.getMarker();
        rm.active = (marker != null) ? marker.active : true;
        if (rm.active) StartHolding();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (rm.active) StopHolding();
        active = false;
        rm.active = false;
        sr.color = rm.c_Ring_Wrong;
    }

    public void StartHolding()
    {
        holding = StartCoroutine(rm.Holding());
    }

    public void StopHolding()
    {
        StopCoroutine(holding);
    }
}
