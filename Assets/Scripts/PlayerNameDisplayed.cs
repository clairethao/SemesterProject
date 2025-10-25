using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplayed : MonoBehaviour
{
    public Text nameText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        nameText.text = playerName;
    }
}