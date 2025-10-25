using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MathZone : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;
    public Text attemptsText;

    private System.Action onCorrectAnswer;
    private string correctAnswer;
    private int remainingAttempts;

    public void Initialize(System.Action resumeCallback)
    {
        string difficulty = PlayerPrefs.GetString("DiffcultyLvl", "Easy");

        remainingAttempts = difficulty switch
        {
            "Easy" => 3,
            "Medium" => 2,
            "Hard" => 1,
            _ => 3
        };

        attemptsText.text = $"Attempts left: {remainingAttempts}";

        onCorrectAnswer = resumeCallback;
        Time.timeScale = 0f;

        List<Preferences.MathQuestion> questions = Preferences.GetQuestions();
        Preferences.MathQuestion current = questions[Random.Range(0, questions.Count)];
        correctAnswer = current.correctAnswer;

        questionText.text = current.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < current.choices.Count)
            {
                string choice = current.choices[i];
                answerButtons[i].GetComponentInChildren<Text>().text = choice;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => CheckAnswer(choice));
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void CheckAnswer(string selected)
    {
        if (selected == correctAnswer)
        {
            ScoreManager.Instance.AddPoint();
            Time.timeScale = 1f;
            onCorrectAnswer?.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            remainingAttempts--;
            attemptsText.text = $"Attempts left: {remainingAttempts}";

            if (remainingAttempts <= 0)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("gameOverScene");
            }
        }
    }
}