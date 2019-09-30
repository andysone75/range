using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text ui;
    [SerializeField] private Text hsui; // High Score UI
    public int value { get; private set; }
    public static int highScore { get; private set; }

    private void Start()
    {
        ui = GetComponent<Text>();

        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        else
            PlayerPrefs.SetInt("HighScore", highScore = 0);
        hsui.text = LangSystem.lng.highScore + ": " + highScore.ToString();

        gameObject.SetActive(false);
    }

    public void inc()
    {
        if (++value > highScore)
        {
            PlayerPrefs.SetInt("HighScore", highScore = value);
            hsui.text = LangSystem.lng.highScore + ": " + highScore.ToString();
            GameManager.newHighScore = true;
        }
        ui.text = value.ToString();
    }

    public void Res()
    {
        ui.text = (value = 0).ToString();
    }

    public MyText getMyText() => GetComponent<MyText>();
}