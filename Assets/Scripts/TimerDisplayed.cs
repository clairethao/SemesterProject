using UnityEngine;
using UnityEngine.UI;

public class FinalStatsDisplay : MonoBehaviour
{
    public Text TimerText;

    void Start()
    {
        float time = PlayerPrefs.GetFloat("FinalTime", 0f);

        TimerText.text = "Time: " + Mathf.FloorToInt(time).ToString() + "s";
    }
}