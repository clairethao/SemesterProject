using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    private float elapsedTime = 0f;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
        }
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        PlayerPrefs.SetFloat("FinalTime", elapsedTime);
        PlayerPrefs.SetInt("FinalScore", ScoreManager.Instance.GetScore());
        PlayerPrefs.SetString("PlayerName", Preferences.playerName);
        PlayerPrefs.Save();

        SceneManager.LoadScene("gameOverScene");
    }

    public void TriggerWinScene()
    {
        isGameOver = true;
        PlayerPrefs.SetFloat("FinalTime", elapsedTime);
        PlayerPrefs.SetInt("FinalScore", ScoreManager.Instance.GetScore());
        PlayerPrefs.SetString("PlayerName", Preferences.playerName);
        PlayerPrefs.Save();

        SceneManager.LoadScene("winScene");
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

}
