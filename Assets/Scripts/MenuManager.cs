using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inputField;

    public static MenuManager Instance;

    public string PlayerName;

    public int HighScore;
    public string HighScoreName;


    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        var sm = GameObject.Find("StartScreen");
        sm.SetActive(false);
        LoadScore();
        SceneManager.LoadScene(1);
    }

    public void GetText()
    {
        PlayerName = inputField.GetComponent<TMP_InputField>().text;


       Debug.Log(PlayerName);
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.PlayerScore = HighScore;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.PlayerScore;
            HighScoreName = data.PlayerName;

        }
    }
    
    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public int PlayerScore;
    }
}
