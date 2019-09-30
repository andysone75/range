using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sf_audioSources;
    private Dictionary<string, AudioSource> audioSources;

    private void Start()
    {
        audioSources = new Dictionary<string, AudioSource>();
        for (int i = 0; i < sf_audioSources.Length; i++)
            audioSources.Add(sf_audioSources[i].name, sf_audioSources[i]);
    }
    
    public Dictionary<string, AudioSource> getAudioSources() => audioSources;

    public void drownOut(AudioSource audio) { StartCoroutine(IDrownOut(audio)); }

    private IEnumerator IDrownOut(AudioSource audio)
    {
        float startVol = audio.volume;
        for(float i = .0f; i < 1.0f; i += .01f)
        {
            audio.volume = Mathf.Lerp(startVol, .0f, i);
            yield return new WaitForFixedUpdate();
        }
    }

    public void drownUp(AudioSource audio, float needVol) { StartCoroutine(IDrownUp(audio, needVol)); }

    private IEnumerator IDrownUp(AudioSource audio, float needVol)
    {
        for (float i = .0f; i < 1.0f; i += .01f)
        {
            audio.volume = Mathf.Lerp(.0f, needVol, i);
            yield return new WaitForFixedUpdate();
        }
    }
}
