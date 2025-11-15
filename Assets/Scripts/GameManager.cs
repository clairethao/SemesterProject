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
    public Text countDownTxt;
    public static bool gameStarted = false;


    void Start()
    {
        StartCoroutine(CountdownBeforeStart());
    }

    IEnumerator GameTimer()
    {
        while (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            timerText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
            yield return null;
        }
    }

    IEnumerator CountdownBeforeStart()
    {
        int countdown = 3;

        while (countdown > 0)
        {
            countDownTxt.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countDownTxt.text = "Go!";
        yield return new WaitForSeconds(1f);
        countDownTxt.text = "";

        gameStarted = true;
        StartCoroutine(GameTimer());
        FindObjectOfType<RoofTopSpawner>().UnpauseSpawner();
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
