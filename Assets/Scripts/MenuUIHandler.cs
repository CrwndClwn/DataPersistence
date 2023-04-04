using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Text InputPlayerName;
    [SerializeField] Text BestScore;
    
    private void Awake()
    {
        LoadData();
        if (MainManager.BestPlayer != null && MainManager.BestScore != 0)
        {
            BestScore.text = $"Hightest score of the players : {MainManager.BestPlayer} - {MainManager.BestScore}";
        }
        else BestScore.text = "";
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
        PlayerDataHandler.Instance.PlayerName = InputPlayerName.text;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int hightestScore;
    }
    void LoadData()
    {
        string path = Application.persistentDataPath + "/savescore.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            MainManager.BestPlayer = data.bestPlayerName;
            MainManager.BestScore = data.hightestScore;
        }
    }
}

