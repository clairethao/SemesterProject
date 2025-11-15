using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HighScoreControl : MonoBehaviour
{
    private string secretKey = "mySecretKey";
    public string addScoreURL = "http://localhost/RoofJumperGame/addscore.php?";
    public string highscoreURL = "http://localhost/RoofJumperGame/display.php";
    public Text nameDisplay;

    void Start()
    {
        SendScoreBtn();
        GetScoreBtn();
    }

    public void GetScoreBtn()
    {
        nameDisplay.text = "";
        StartCoroutine(GetScores("highest"));
        StartCoroutine(GetScores("fastest"));
    }

    public void SendScoreBtn()
    {
        string name = PlayerPrefs.GetString("PlayerName", "Guest");
        int score = PlayerPrefs.GetInt("FinalScore", 0);
        int time = PlayerPrefs.GetInt("TimeTaken", 0);
        StartCoroutine(PostScores(name, score, time));
    }

    IEnumerator GetScores(string mode)
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(highscoreURL + "?mode=" + mode);
        yield return hs_get.SendWebRequest();

        if (hs_get.error != null)
        {
            Debug.Log("Error getting " + mode + " scores: " + hs_get.error);
        }
        else
        {
            string[] splitData = Regex.Split(hs_get.downloadHandler.text, @"_");

            nameDisplay.text += (mode == "highest" ? "Top Scores:\n" : "Fastest Times:\n");

            for (int i = 0; i < splitData.Length - 2; i += 3)
            {
                string name = splitData[i];
                string score = splitData[i + 1];
                string time = splitData[i + 2];

                nameDisplay.text += $"{name} {score}  Time: {time}s\n";
            }

            nameDisplay.text += "\n";
        }
    }

    IEnumerator PostScores(string name, int score, int time)
    {
        string hash = HashInput(name + score + time + secretKey);
        string post_url = addScoreURL + "name=" +
               UnityWebRequest.EscapeURL(name) + "&score=" +
               score + "&time=" + time + "&hash=" + hash;

        UnityWebRequest hs_post = UnityWebRequest.Get(post_url);
        yield return hs_post.SendWebRequest();

        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
    }

    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
                 BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }
}
