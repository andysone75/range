using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rotation rotater;
    private Animation anim;
    public Coroutine rotation;

    private void Awake()
    {
        rotater = GameObject.FindGameObjectWithTag("Rotater").GetComponent<Rotation>();
        anim = gameObject.GetComponent<Animation>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        anim.Play("Player_start");
    }

    public void Hide() { anim.Play("Player_hide"); }
    public void Disactive() { gameObject.SetActive(false); }
    public void StopRotating() { if (rotation != null) StopCoroutine(rotation); }
    public void RotateToNormal() { rotation = StartCoroutine(CRotateToNormal()); }

    private IEnumerator CRotateToNormal()
    {
        float z = transform.rotation.eulerAngles.z;
        float newz;
        for (var i = .0f; i <= 1.0f; i += .05f)
        {
            newz = Mathf.LerpAngle(z, .0f, i);
            transform.rotation = Quaternion.Euler(.0f, .0f, newz);
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = new Quaternion(.0f, .0f, .0f, .0f);
    }

    public Rotation getRotater() => rotater;
    public float getNormalizedScale() => (transform.localScale.x - 1.35f) / 2.95f;
}
