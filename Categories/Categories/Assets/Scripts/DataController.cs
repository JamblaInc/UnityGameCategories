using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class DataController : MonoBehaviour 
{
	private RoundData[] allRoundData;
    public Settings settings;
	private PlayerProgress playerProgress;
	private string gameDataFileName = "data.json";
    private int currentRoundTime;
    private int numberOfRounds;
    private List<int> roundScores = new List<int>();

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
        
    }

    public void LoadGameSettings()
    {
        if (PlayerPrefs.HasKey("roundTime"))
        {
            currentRoundTime = PlayerPrefs.GetInt("roundTime");
        }
        if (PlayerPrefs.HasKey("numberOfRounds"))
        {
            numberOfRounds = PlayerPrefs.GetInt("numberOfRounds");
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

    public void addRoundScore(int roundScore)
    {
        roundScores.Add(roundScore);
    }

    public void clearRoundScores()
    {
        roundScores.Clear();
    }

    public int returnRoundScores(int roundNumber)
    {
        return roundScores[roundNumber];
    }

    public int getTotalScore()
    {
        int sumOfScores = 0;
        for(int i = 0; i < getNumberOfRounds(); i++)
        {
            sumOfScores += roundScores[i];
        }
        return sumOfScores;
    }

    public int getCurrentRoundTime()
    {
        return currentRoundTime;
    }

    public int getNumberOfRounds()
    {
        return numberOfRounds;
    }

    public void changeRoundTime(float newTime)
    {
        currentRoundTime = Mathf.RoundToInt(newTime);
        SaveRoundTime();
    }

    public void changeNumberOfRounds(float newTime)
    {
        numberOfRounds = Mathf.RoundToInt(newTime);
        SaveNumberOfRounds();
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

    private void SaveNumberOfRounds ()
    {
        PlayerPrefs.SetInt("numberOfRounds", numberOfRounds);
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