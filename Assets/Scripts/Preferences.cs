using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preferences : MonoBehaviour
{
    public static string speed => PlayerPrefs.GetString("Speed", "Slow");
    public static string playerName => PlayerPrefs.GetString("PlayerName", "Guest");
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
        if (nameInput != null)
        {
            nameInput.text = PlayerPrefs.GetString("PlayerName", "Guest");
            nameInput.onValueChanged.AddListener(UpdateName);
        }

        if (speedDropdown != null)
        {
            string speed = PlayerPrefs.GetString("Speed", "Slow");
            int index = speedDropdown.options.FindIndex(option => option.text == speed);
            speedDropdown.value = index >= 0 ? index : 1;
            speedDropdown.onValueChanged.AddListener(delegate { SetSpeedDropdown(); });
        }

        if (difficultyDropdown != null)
        {
            string difficulty = PlayerPrefs.GetString("DiffcultyLvl", "Easy");
            int index = difficultyDropdown.options.FindIndex(option => option.text == difficulty);
            difficultyDropdown.value = index >= 0 ? index : 0; // Default to Easy
            difficultyDropdown.onValueChanged.AddListener(delegate { SetDifficultyDropdown(); });
        }
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
        PlayerPrefs.SetString("Speed", selectedSpeed);
    }

    public static float GetSpeedValue()
    {
        string speed = PlayerPrefs.GetString("Speed", "Medium");

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
        string difficulty = PlayerPrefs.GetString("DiffcultyLvl", "Easy");
        List<MathQuestion> questions = new List<MathQuestion>();

        if (difficulty == "Easy")
        {
            questions.Add(new MathQuestion("2 + 2 = ?", "4", new List<string> { "3", "4" , "6"}));
            questions.Add(new MathQuestion("5 - 3 = ?", "2", new List<string> { "2", "1", "3" }));
            questions.Add(new MathQuestion("3 * 1 = ?", "3", new List<string> { "3", "1", "6" }));
            questions.Add(new MathQuestion("10 / 2 = ?", "5", new List<string> { "4", "5", "6" }));
            questions.Add(new MathQuestion("6 + 1 = ?", "7", new List<string> { "7", "8", "9" }));
        }
        else if (difficulty == "Medium")
        {
            questions.Add(new MathQuestion("12 % 5 = ?", "2", new List<string> { "2", "3", "1" }));
            questions.Add(new MathQuestion("3^2 = ?", "9", new List<string> { "6", "9", "12" }));
            questions.Add(new MathQuestion("15 / 3 = ?", "5", new List<string> { "3", "5", "6" }));
            questions.Add(new MathQuestion("7 * 2 = ?", "14", new List<string> { "12", "14", "16" }));
            questions.Add(new MathQuestion("18 - 9 = ?", "9", new List<string> { "8", "9", "10" }));
        }
        else if (difficulty == "Hard")
        {
            questions.Add(new MathQuestion("Sum of first 5 odd numbers?", "25", new List<string> { "15", "25", "35" }));
            questions.Add(new MathQuestion("Subsets of a 3-element set?", "8", new List<string> { "6", "8", "9" }));
            questions.Add(new MathQuestion("Binary of 13?", "1101", new List<string> { "1100", "1101", "1110" }));
            questions.Add(new MathQuestion("What is 2^5?", "32", new List<string> { "16", "32", "64" }));
            questions.Add(new MathQuestion("GCD of 24 and 36?", "12", new List<string> { "6", "12", "18" }));
        }

        return questions;
    }

}
