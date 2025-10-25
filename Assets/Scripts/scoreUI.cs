using UnityEngine;
using UnityEngine.UI;

public class LiveScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
        }
    }
}