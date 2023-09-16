using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // swap best score for top highscore
    private string playerName;
    private HighScoreData[] highScores = new HighScoreData[10];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = new HighScoreData();
        }
        LoadHighScoreData();
    }

    [Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int score;
    }

    void LoadHighScoreData()
    {
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData[] data = JsonHelper.FromJson<HighScoreData>(json);

            if (data != null)
            {
                Array.Copy(data, highScores, highScores.Length);
            }
            for (int i = 0; i < highScores.Length; i++)
            {
                //Debug.Log(data.highScores[i].playerName + " : " + data.highScores[i].score);
            }
        }
        else
        {
            Debug.Log("File does not exist!");
        }
    }

    public void SaveHighScoreData()
    {
        HighScoreData[] data = new HighScoreData[highScores.Length];
        for (int i = 0; i < highScores.Length; i++)
        {
            data[i] = highScores[i];
            Debug.Log(data[i].playerName + " : " + data[i].score);
        }
        string json = JsonHelper.ToJson(data, true);
        //string json = JsonUtility.ToJson(data);
        Debug.Log(json);

        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);
    }

    public int GetBestScore()
    {
        int bestScore = highScores[0].score;
        return bestScore;
    }

    public string GetBestScorePlayerName()
    {
        string bestScorePlayerName = highScores[0].playerName;
        return bestScorePlayerName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string currentPlayerName)
    {
        playerName = currentPlayerName;
    }

    public HighScoreData[] GetHighScores()
    {
        return highScores;
    }

    public void SetHighScores(HighScoreData newHighScore)
    {
        HighScoreData[] tempHighScores = new HighScoreData[10];
        for (int i = 0; i < highScores.Length; i++)
        {
            tempHighScores[i] = highScores[i];
        }
        if (newHighScore.score >= tempHighScores[highScores.Length - 1].score)
        {
            tempHighScores[highScores.Length - 1] = newHighScore;
        }
        HighScoreData[] sortedHighScores = tempHighScores.OrderByDescending((HighScoreData i) => i.score).ToArray();
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = sortedHighScores[i];
        }
        SaveHighScoreData();
    }
}