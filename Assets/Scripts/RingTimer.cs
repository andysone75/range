using UnityEngine;
using UnityEngine.UI;

public class RingTimer : MonoBehaviour
{
    private Animation anim;
    private Slider slider;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        slider = GetComponent<Slider>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        anim.Play("RingSlider_show");
    }

    public void Hide()
    {
        anim.Play("RingSlider_hide");
    }

    public Slider getSlider() => slider;
}
