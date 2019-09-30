using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    private MyText myText;
    private Text ui;

    public void Init()
    {
        myText = GetComponent<MyText>();
        ui = GetComponent<Text>();
        myText.Hide();
    }

    public void Show(string message)
    {
        ui.text = message;
        myText.Show();
        StartCoroutine(Hide(4.0f));
    }

    private IEnumerator Hide(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        myText.Hide();
    }
}
