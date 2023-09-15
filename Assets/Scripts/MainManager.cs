using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    [SerializeField] Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string playerName;

    
    // Start is called before the first frame update
    void Start()
    {
        playerName = GameManager.Instance.GetPlayerName();
        UpdateBestScore();
        ScoreText.text = $"Score : {playerName} : 0";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {playerName} : {m_Points}";
    }

    void UpdateBestScore()
    {
        int currentBest = GameManager.Instance.GetBestScore();
        if(m_Points > currentBest)
        {
            bestScoreText.text = $"Best Score : {playerName} : {m_Points}";
            GameManager.Instance.SetBestScore(m_Points);
            GameManager.Instance.SetBestScorePlayerName(playerName);
        }
        else
        {
            bestScoreText.text = $"Best Score : {GameManager.Instance.GetBestScorePlayerName()} : {currentBest}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        UpdateBestScore();
        GameManager.Instance.SaveBestScoreData();
        GameOverText.SetActive(true);
    }
}