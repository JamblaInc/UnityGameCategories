﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour 
{
	private RoundData[] allRoundData;
    public Settings settings;
	private PlayerProgress playerProgress;
	private string gameDataFileName = "data.json";
    private int currentRoundTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()  
	{
        LoadGameData ();
        LoadPlayerProgress ();
        LoadGameSettings();
        SceneManager.LoadScene ("MainMenu");
	}
    void Update()
    {
        //Debug.Log("currentRoundTime is: " + currentRoundTime);
    }

    public void LoadGameSettings()
    {
        if (PlayerPrefs.HasKey("roundTime"))
        {
            currentRoundTime = PlayerPrefs.GetInt("roundTime");
            //Debug.Log("currentRoundTime is: " + currentRoundTime);
        }
    }

	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];
	}

	public void SubmitNewPlayerScore(int newScore)
	{
		if (newScore > playerProgress.highestScore) 
		{
			playerProgress.highestScore = newScore;
			SavePlayerProgress ();
		}
	}

    public int getCurrentRoundTime()
    {
        //Debug.Log("currentRoundTime is: " + currentRoundTime);
        return currentRoundTime;
    }

    public void changeRoundTime(float newTime)
    {
        currentRoundTime = Mathf.RoundToInt(newTime);
        SaveRoundTime();
    }

	public int GetHighestPlayerScore()
	{
		return playerProgress.highestScore;
	}

	private void LoadPlayerProgress()
	{
		playerProgress = new PlayerProgress ();

		if (PlayerPrefs.HasKey ("highestScore")) 
		{
			playerProgress.highestScore = PlayerPrefs.GetInt ("highestScore");
		}
	}

	private void SavePlayerProgress()
	{
		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);
	}

    private void SaveRoundTime()
    {
        PlayerPrefs.SetInt("roundTime", currentRoundTime);
    }

	private void LoadGameData()
	{
		#if UNITY_EDITOR
		string filePath = Path.Combine (Application.streamingAssetsPath, gameDataFileName);

			if (File.Exists (filePath)) {
				string dataAsJson = File.ReadAllText (filePath);
				GameData loadedData = JsonUtility.FromJson<GameData> (dataAsJson);
				allRoundData = loadedData.allRoundData;
			} else 
			{
				Debug.LogError ("Cannot load game data!");
			}
		#elif UNITY_ANDROID
			string path = Application.streamingAssetsPath + "/data.json";
			WWW www = new WWW(path); 
			while(!www.isDone){}
			string dataAsJson1 = www.text;
			GameData loadedData1 = JsonUtility.FromJson<GameData> (dataAsJson1);
			allRoundData = loadedData1.allRoundData;
		#endif
	}

}