using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public string PlayerName;
    public int PlayerScore;
    public int PlayerHighscore;
    public HighScoreData HighScore;
    string m_dataSavePath;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        } 
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        m_dataSavePath = Application.persistentDataPath;
        LoadHighScoreData();
    }

    public void SetPlayerScore(int score) {
        PlayerScore = score;
        PlayerHighscore = Mathf.Max(score, PlayerHighscore);
        if (PlayerHighscore > HighScore.PlayerScore) {
            HighScore.PlayerName = PlayerName;
            HighScore.PlayerScore = PlayerHighscore;
        }
    }

    void OnDestroy() {
        SaveHighScoreData();
    }

    public void SaveHighScoreData()
    {
        string json = JsonUtility.ToJson(HighScore);
        File.WriteAllText(m_dataSavePath + "/highscore.json", json);
    }

    public void LoadHighScoreData()
    {
        string filename = m_dataSavePath + "/highscore.json";
        Debug.Log("Try to load highscoredata from " + filename);
        if (!File.Exists(filename)) {
            HighScore = new HighScoreData();
            return;
        }
        string json = File.ReadAllText(filename);
        HighScore = JsonUtility.FromJson<HighScoreData>(json);
    }

    [System.Serializable]
    public class HighScoreData {
        public string PlayerName;
        public int PlayerScore;
    }
}
