using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] private Material material;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Hide()
    {
        ps.gravityModifier = 1;
    }

    public void Show()
    {
        ps.gravityModifier = 0;
    }
}