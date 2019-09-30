using UnityEngine;

public class MyText : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    public void Disactive() { gameObject.SetActive(false); }
    public void Show()
    {
        gameObject.SetActive(true);
        anim.Play(name + "_show");
    }
    public void Hide()
    {
        if (gameObject.activeSelf)
            anim.Play(name + "_hide");
    }
}
