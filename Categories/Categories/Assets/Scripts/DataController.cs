using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
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
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        string path = Application.streamingAssetsPath + "/data.json";
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        Debug.Log("Loaded Q's");
        string dataAsJson1 = www.downloadHandler.text;
        GameData loadedData1 = JsonUtility.FromJson<GameData>(dataAsJson1);
        allRoundData = loadedData1.allRoundData;
        
    }
}