using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string currentPlayer;
    public string highScoreName1;
    public string highScoreName2;
    public string highScoreName3;

    private int m_highScore1;
    public int HighScore1
    {
        get { return m_highScore1; }
        set { m_highScore1 = value; }
    }

    private int m_highScore2;
    public int HighScore2
    {
        get { return m_highScore2; }
        set { m_highScore2 = value; }
    }

    private int m_highScore3;
    public int HighScore3
    {
        get { return m_highScore3; }
        set { m_highScore3 = value; }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadFile();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore1;
        public int highScore2;
        public int highScore3;
        public string highScoreName1;
        public string highScoreName2;
        public string highScoreName3;
        
    }

    public void SaveFile()
    {
        SaveData data = new SaveData();
        data.highScore1 = HighScore1;
        data.highScore2 = HighScore2;
        data.highScore3 = HighScore3;
        data.highScoreName1 = highScoreName1;
        data.highScoreName2 = highScoreName2;
        data.highScoreName3 = highScoreName3;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            HighScore1 = data.highScore1;
            HighScore2 = data.highScore2;
            HighScore3 = data.highScore3;
            highScoreName1 = data.highScoreName1;
            highScoreName2 = data.highScoreName2;
            highScoreName3 = data.highScoreName3;
        }
    }
}
