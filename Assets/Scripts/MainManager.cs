using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string currentPlayer;
    public int highScore1;
    public int highScore2;
    public int highScore3;
    public string highScoreName1;
    public string highScoreName2;
    public string highScoreName3;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        data.highScore1 = highScore1;
        data.highScore2 = highScore2;
        data.highScore3 = highScore3;
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
            highScore1 = data.highScore1;
            highScore2 = data.highScore2;
            highScore3 = data.highScore3;
            highScoreName1 = data.highScoreName1;
            highScoreName2 = data.highScoreName2;
            highScoreName3 = data.highScoreName3;
        }
    }
}
