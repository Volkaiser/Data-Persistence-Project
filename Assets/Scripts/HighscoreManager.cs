using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] playerTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;

    private void Start()
    {
        GameManager.HighScoreData[] tempHighScores = new GameManager.HighScoreData[10];
        tempHighScores = GameManager.Instance.GetHighScores();
        for (int i = 0; i < tempHighScores.Length; i++)
        {
            if (string.IsNullOrEmpty(tempHighScores[i].playerName) || tempHighScores[i].score == 0)
            {
                playerTexts[i].text = "???";
                scoreTexts[i].text = "0";
            }
            else
            {
                playerTexts[i].text = tempHighScores[i].playerName;
                scoreTexts[i].text = tempHighScores[i].score.ToString();
            }
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}