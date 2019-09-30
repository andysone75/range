using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LangSystem : MonoBehaviour
{
    public static Lang lng = new Lang();
    [SerializeField] private Sprite[] flags;
    private int index = 0;
    private string[] langs = { "ru_RU", "en_UK" };

    [Header("Texts")]
    [SerializeField] private Text start;
    [SerializeField] private Text restart;
    [SerializeField] private Text ad;
    [SerializeField] private Text highScore;
    [SerializeField] private Text howToPlay;
    [SerializeField] private Text[] tutorial;
    [SerializeField] private GameManager gm;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "ru_RU");
            else PlayerPrefs.SetString("Language", "en_UK");
        }
        Load();
    }

    private void Start()
    {
        Set();
    }

    private void Load()
    {
        string language = PlayerPrefs.GetString("Language");
        if (language != "ru_RU" && language != "en_UK") PlayerPrefs.SetString("Language", language = "en_UK");

        string json;
        string path = string.Format("{0}/Language/{1}.json", Application.streamingAssetsPath, language);
#if !UNITY_EDITOR
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(path);
#endif
        lng = JsonUtility.FromJson<Lang>(json);
        for (var i = 0; i < langs.Length; i++)
            if (langs[i] == language) index = i;
        GetComponent<Image>().sprite = flags[index];
    }

    private void Set()
    {
        start.text = lng.start;
        restart.text = lng.restart;
        ad.text = lng.ad;
        highScore.text = lng.highScore + ": " + (PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0).ToString();
        howToPlay.text = lng.howToPlay;
        for (var i = 0; i < tutorial.Length; i++)
            tutorial[i].text = lng.tutorial[i];
    }

    public void SwitchLanguage()
    {
        if (++index >= langs.Length) index = 0;
        PlayerPrefs.SetString("Language", langs[index]);
        Load();
        Set();
        gm.getSoundManager().getAudioSources()["button_2"].Play();
    }
}

public class Lang
{
    public string start;
    public string restart;
    public string ad;
    public string highScore;
    public string howToPlay;
    public string[] tutorial = new string[5];
}
