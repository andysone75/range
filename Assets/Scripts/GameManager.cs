using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    private GameObject ring;

    [SerializeField] private ScoreManager score;
    [SerializeField] private MyText ad;
    [SerializeField] private MyText restart;
    [SerializeField] private RingTimer ringTimer;
    [SerializeField] private GameObject pref_Ring;
    [SerializeField] private GameObject pref_RingWithoutMarker;
    [SerializeField] private MyText title;
    [SerializeField] private MyText start;
    [SerializeField] private Text ringTime;
    [SerializeField] private MyText tutorial;
    [SerializeField] private MyText howToPlayBtn;
    [SerializeField] private SoundManager sm;
    [SerializeField] private MyText language;
    [SerializeField] private LogManager log;
    [SerializeField] private ParticlesManager particles;

    public static bool isGame = false;
    public bool ad_used = false; // Has been ad shown
    public static bool newHighScore = false;

    private int heavyArmorLevel = 0;
    private int oneHandedLevel = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("HeavyArmorLevel")) heavyArmorLevel = PlayerPrefs.GetInt("HeavyArmorLevel");
        if (PlayerPrefs.HasKey("OneHandedLevel"))   oneHandedLevel = PlayerPrefs.GetInt("OneHandedLevel");
        ShowMenu();
        log.Init();
        log.Show(PlayerPrefs.GetString("Language") == "ru_RU" ? "Добро пожаловать" : "Welcome");
    }

    public PlayerManager getPlayer() => player;
    public ScoreManager getScoreManager() => score;
    public MyText getAdButton() => ad;
    public MyText getRestartButton() => restart;
    public RingTimer getRingTimer() => ringTimer;
    public MyText getHowToPlayButton() => howToPlayBtn;
    public SoundManager getSoundManager() => sm;
    public GameObject getRing(bool withMarker) => withMarker ? pref_Ring : pref_RingWithoutMarker;

    public void ShowMenu()
    {
        GetComponent<Animation>().Play("MainCamera_BlurOut");
        title.Show();
        start.Show();
        player.Show();
        howToPlayBtn.Show();
        language.Show();
    }

    public void StartGame()
    {
        if (ring != null) DestroyRing();
        player.StopRotating();
        isGame = true;
        title.Hide();
        start.Hide();
        howToPlayBtn.Hide();
        ringTimer.Show();
        CreateRing(pref_RingWithoutMarker);
        score.Res();
        score.getMyText().Show();
        sm.getAudioSources()["button_3"].Play();
        sm.drownOut(sm.getAudioSources()["background"]);
        language.Hide();
        particles.Hide();
    }

    // Ring
    public void CreateRing(GameObject prefab)
    {
        var newRing = (ring = Instantiate(prefab)).GetComponent<RingManager>();
        ringTime.text = newRing.time.ToString();
    }

    public void CreateRing(GameObject prefab, float lifetime)
    {
        var newRing = (ring = Instantiate(prefab)).GetComponent<RingManager>();
        newRing.time = lifetime;
        ringTime.text = lifetime.ToString();
    }

    public void DestroyRing()
    {
        if (ring != null)
        {
            Destroy(ring.gameObject);
            ring = null;
        }
    }

    // Tutorial
    public void ShowTutorial()
    {
        player.Hide();
        tutorial.Show();
        if (ring != null) ring.GetComponent<RingManager>().Hide();
        sm.getAudioSources()["button_1"].Play();
    }

    public void HideTutorial()
    {
        tutorial.Hide();
        player.Show();
        if (ring != null) ring.GetComponent<RingManager>().Show();
        sm.getAudioSources()["button_2"].Play();
    }

    public void Losing()
    {
        isGame = false;

        ringTimer.Hide();
        howToPlayBtn.Show();
        restart.Show();
        sm.getAudioSources()["losing_" + Random.Range(0, 6).ToString()].Play();
        sm.drownUp(sm.getAudioSources()["background"], .2f);

        particles.Show();
    }

    public void Restart()
    {
        isGame = false;
        ad_used = false;
        newHighScore = false;

        player.RotateToNormal();
        DestroyRing();
        restart.Hide();
        ad.Hide();
        score.getMyText().Hide();
        start.Show();
        title.Show();
        language.Show();

        sm.getAudioSources()["pickup"].pitch = 1.0f;
    }
}   