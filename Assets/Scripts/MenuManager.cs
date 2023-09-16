using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField nameInputField;

    void Start()
    {
        string pName = GameManager.Instance.GetBestScorePlayerName();
        if(string.IsNullOrEmpty(pName) )
        {
            pName = "???";
        }
        int pScore = GameManager.Instance.GetBestScore();
        bestScoreText.text = $"Best Score : {pName} : {pScore}";
    }

    public void StartGame()
    {
        if(string.IsNullOrWhiteSpace(nameInputField.text))
        {
            GameManager.Instance.SetPlayerName("???");
        }
        else
        {
            GameManager.Instance.SetPlayerName(nameInputField.text);
        }
        SceneManager.LoadScene(1);
    }

    public void Highscores()
    {
        SceneManager.LoadScene(2);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}