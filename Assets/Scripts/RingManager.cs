using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] private Marker marker;

    private Animation anim;
    [HideInInspector] public GameManager gm;

    public float time = 7.0f;
    public bool active = false;

    #region Color states
    public Color
        c_Ring_Wrong = new Color(1.0f, .3686275f, .3411765f, .5019608f),
        c_Ring_Right = new Color(.04313726f, .9098039f, .5058824f, .7843137f),
        c_Marker_Wrong = new Color(1.0f, .2470588f, .2039216f, .5019608f),
        c_Marker_Right = new Color(.01960784f, 1.0f, .4196078f, .5019608f);
    #endregion

    private void Start()
    {
        anim = GetComponent<Animation>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        StartTimer();

        if (getMarker() != null)
        {
            Quaternion newRotation = Random.rotation;
            newRotation.x = .0f;
            newRotation.y = .0f;
            transform.rotation = newRotation;
        }

        float scale = Random.Range(.3f, .95f);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }

    public Ring getRing() => GetComponent<Ring>();
    public Marker getMarker() => marker;

    public void StartTimer()
    {
        StartCoroutine(Hiding());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Disactive()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator Hiding()
    {
        Stopwatch sw = new Stopwatch();
        float seconds = time * 1000;
        sw.Start();
        while (sw.ElapsedMilliseconds < seconds)
        {
            if (active)
            {
                gm.getRingTimer().getSlider().value = 1;
                sw.Restart();
            }
            gm.getRingTimer().getSlider().value = 1 - sw.ElapsedMilliseconds / seconds;
            yield return new WaitForEndOfFrame();
        }
        sw.Stop();
        gm.Losing();
    }
    
    public IEnumerator Holding()
    {
        yield return new WaitForSeconds(1.0f);
        if (active)
        {
            var score = gm.getScoreManager();
            score.inc();
            gm.DestroyRing();
            var sound = gm.getSoundManager().getAudioSources()["pickup"];
            sound.Play();
            float newTime = time / 1.1f;
            if (score.value <= 10)
            {
                gm.CreateRing(gm.getRing(false), newTime);
                sound.pitch += .04f;
            }
            else
            {
                if (score.value == 11) newTime = 7.0f;
                gm.CreateRing(gm.getRing(true), newTime);
                sound.pitch = 1;
            }
        }
    }
}
