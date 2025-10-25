using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    private void ResetGameState()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        PlayerPrefs.SetString("PlayerName", "Guest");
        PlayerPrefs.Save();
    }

    public void Play()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void Pref()
    {
        SceneManager.LoadScene("prefScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("introScene");

    }

    public void Quit()
    {
        ResetGameState();
        SceneManager.LoadScene("exitScene");
    }
}
