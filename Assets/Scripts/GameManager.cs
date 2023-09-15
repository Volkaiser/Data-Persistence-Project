using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int bestScore;
    private string bestScorePlayerName;
    private string playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScoreData();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestScorePlayerName;
    }

    void LoadBestScoreData()
    {
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestScorePlayerName = data.bestScorePlayerName;
        }
    }

    public void SaveBestScoreData()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestScorePlayerName = bestScorePlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int newBestScore)
    {
        bestScore = newBestScore;
    }

    public string GetBestScorePlayerName()
    {
        return bestScorePlayerName;
    }

    public void SetBestScorePlayerName(string newBestPlayerName)
    {
        bestScorePlayerName = newBestPlayerName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string currentPlayerName)
    {
        playerName = currentPlayerName;
    }
}