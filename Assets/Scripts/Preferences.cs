using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    public static string playerName => PlayerPrefs.GetString("PlayerName", "Guest");
    public static string speed => PlayerPrefs.GetString("Speed", "Slow");
    public static string difficultyLvl => PlayerPrefs.GetString("DiffcultyLvl", "Easy");

    public InputField nameInput;
    public Dropdown speedDropdown;
    public Dropdown difficultyDropdown;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        nameInput.onValueChanged.AddListener(UpdateName);
        speedDropdown.onValueChanged.AddListener(delegate { SetSpeedDropdown(); });
        difficultyDropdown.onValueChanged.AddListener(delegate { SetDifficultyDropdown(); });
    }

    void UpdateName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            PlayerPrefs.SetString("PlayerName", "Guest");
        else
            PlayerPrefs.SetString("PlayerName", newName);

        PlayerPrefs.Save();
    }

    void SetSpeedDropdown()
    {
        string selectedSpeed = speedDropdown.options[speedDropdown.value].text;
        if (selectedSpeed != "Game Speed")
            PlayerPrefs.SetString("Speed", selectedSpeed);
        else
            PlayerPrefs.DeleteKey("Speed");
    }

    public static float GetSpeedValue()
    {
        string speed = PlayerPrefs.GetString("Speed", "Slow");

        return speed switch
        {
            "Slow" => 2f,
            "Medium" => 3.5f,
            "Fast" => 5f,
            _ => 3.5f
        };
    }

    void SetDifficultyDropdown()
    {
        string selectedDifficulty = difficultyDropdown.options[difficultyDropdown.value].text;
        PlayerPrefs.SetString("DiffcultyLvl", selectedDifficulty);
    }

    public class MathQuestion
    {
        public string question;
        public string correctAnswer;
        public List<string> choices;

        public MathQuestion(string q, string a, List<string> c)
        {
            question = q;
            correctAnswer = a;
            choices = c;
        }
    }

    public static List<MathQuestion> GetQuestions()
    {
        string difficulty = PlayerPrefs.GetString("DiffcultyLvl", "Easy").ToLower();
        TextAsset questionFile = Resources.Load<TextAsset>("math_questions");
        List<MathQuestion> questions = new List<MathQuestion>();

        if (questionFile == null)
        {
            Debug.LogError("Question file not found in Resources!");
            return questions;
        }

        string[] lines = questionFile.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] parts = line.Trim().Split('|');
            if (parts.Length < 6) continue;

            string entryDifficulty = parts[0].ToLower();
            if (entryDifficulty != difficulty) continue;

            string question = parts[1];
            List<string> choices = new List<string> { parts[2], parts[3], parts[4] };
            int correctIndex = int.Parse(parts[5]);

            questions.Add(new MathQuestion(question, choices[correctIndex], choices));
        }
        return questions;
    }

}
