using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI nameText;

    void Start()
    {
        string pName = GameManager.Instance.GetBestScorePlayerName();
        int pScore = GameManager.Instance.GetBestScore();
        bestScoreText.text = $"Best Score : {pName} : {pScore}";
    }

    public void StartGame()
    {
        if(nameText.text == null)
        {
            GameManager.Instance.SetPlayerName("???");
        }
        else
        {
            GameManager.Instance.SetPlayerName(nameText.text);
        }
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}