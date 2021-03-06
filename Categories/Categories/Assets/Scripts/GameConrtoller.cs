﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;

public class GameConrtoller : MonoBehaviour {

	public Text questionDisplayText;
	public Text scoreDisplayText;
    public Text roundNumberText;
	public Text timeRemainingDisplayText;
    public Text timeRemainingHeader;
	public SimpleObjectPool answerButtonObjectPool;
	public Transform answerButtonParent;

	public GameObject questionDisplay;
	public GameObject roundEndDisplay;
    public GameObject summaryDisplay;
	public GameObject winDisplay;
	public GameObject nextRoundBtn;
    public GameObject nextRoundBtnLost;
    public GameObject menuButton;
    public GameObject menuButtonLost;
    public GameObject toSummaryBtn;
    public GameObject toSummaryBtnLost;
    public GameObject continueButton;
    public GameObject continueButtonLost;
    public GameObject scoreDisplayContainer;
    public GameObject scoreDisplayPrefab;
    public GameObject zeroScoreText;
    public GameObject scrollBackgroundSummary;
    public GameObject loadingPanel;

    public Text winScoreDisplay;
	public Text loseScoreDisplay;
    public Text missedAnswerDisplay;
    public Text finalScoreText;

	private DataController dataController;
	private RoundData currentRoundData;
    private AnswerButton currentAnswerButton;
    private AdManager adManager;
    private ScreenshotManager screenshotManager;

	private Question[] questionPool;
    
	private bool isRoundActive;
	private float timeRemaining;
	private int questionIndex;
	private int playerScore;
	private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    //List to store the current answers
    public List<string> currentAnswers = new List<string>();

    private int roundCounter = 0;
    public int roundLimit;
	private int maxScore = 75;
	public bool next;
    private int roundScore;

	List<int> unusedQuestions = new List<int>();
    
    // Use this for initialization
    void Start () 
	{
		
		//Allow us to access the current round data
		dataController = FindObjectOfType<DataController> ();
		currentRoundData = dataController.GetCurrentRoundData ();
        adManager = FindObjectOfType<AdManager>();
        screenshotManager = FindObjectOfType<ScreenshotManager>();

        roundLimit = dataController.getNumberOfRounds();
        dataController.clearRoundScores();

		//Load the questions into the questionPool
		questionPool = currentRoundData.questions;

		//Load all the quesiton numbers into our array
		for (int j = 0; j < questionPool.Length; j++) 
		{
			unusedQuestions.Add (j);
			//Debug.Log ("Element " + j + " is equal to " + unusedQuestions[j]);
		}
			
		//Show the questions
		ShowQuestion ();

		//Set round to active
		isRoundActive = true;
	}

	public void ShowQuestion()
	{
        //Reset the missed answers text
        missedAnswerDisplay.text = "";

        continueButton.SetActive(false);
        continueButtonLost.SetActive(false);
		roundCounter++;
        summaryDisplay.SetActive(false);
		winDisplay.SetActive (false);
		roundEndDisplay.SetActive (false);
		isRoundActive = true;
		questionDisplay.SetActive (true);

		//Start the timer
		timeRemaining = dataController.getCurrentRoundTime();
		UpdateTimeRemainingDisplay ();

		//Set the starting score
		playerScore = 0;
		scoreDisplayText.text = "Score: " + playerScore.ToString ();

        //Set the round number at the top of screen to current round
        roundNumberText.text = "Round " + roundCounter;

		RemoveAnswerButtons ();

		//Set the current question to a random question
		questionIndex = unusedQuestions[Random.Range(0, unusedQuestions.Count)];
		//Debug.Log ("Current q index = " + questionIndex);
		Question questionData = questionPool [questionIndex];
		questionDisplayText.text = questionData.questions;

		//Display each answer button
		if (!next) 
		{
			for (int i = 0; i < questionData.answers.Length; i++) 
			{
				GameObject answerButtonGameObject = answerButtonObjectPool.GetObject ();

				answerButtonGameObjects.Add (answerButtonGameObject);
				answerButtonGameObject.transform.SetParent (answerButtonParent, false);

				AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton> ();

                //Setup the answerButtons on screen
                answerButton.SetUp (questionData.answers [i]);

                //Adding the current answers to a list
                currentAnswers.Add(answerButton.returnAnswer());
                next = true;
			} 
		} else 
		{
			for (int i = 0; i < questionData.answers.Length; i++) 
			{
				GameObject answerButtonGameObject = answerButtonObjectPool.GetObject ();

				answerButtonGameObjects.Add (answerButtonGameObject);
				answerButtonGameObject.transform.SetParent (answerButtonParent, false);

				AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton> ();

                //Setup the answerButtons on screen
                answerButton.SetUp (questionData.answers [6-i]);

                //Adding the current answers to a list
                currentAnswers.Add(answerButton.returnAnswer());
                next = false;
			} 
		}

		//Remove the question that has been used from the list
		unusedQuestions.Remove (questionIndex);
		
	}

	private void RemoveAnswerButtons()
	{
		while (answerButtonGameObjects.Count > 0) 
		{
			answerButtonObjectPool.ReturnObject (answerButtonGameObjects [0]);
			answerButtonGameObjects.RemoveAt (0);
		}
	}

	public void AddPoints(bool isSpecial)
	{
		if (isSpecial) 
		{
			//Add special amount of points to score
			playerScore += currentRoundData.pointsAddedForSpecialAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();
			//Debug.Log ("Player scored special points");
		} else 
		{
			//Add normal amount of points
			playerScore += currentRoundData.pointsAddedForNormalAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();
			//Debug.Log ("Player scored normal points");
		}
	}

	public void RemovePoints(bool isSpecial)
	{
		if (isSpecial) 
		{
			//Remove special amount of points to score
			playerScore -= currentRoundData.pointsAddedForSpecialAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();
		} else 
		{
			//Remove normal amount of points
			playerScore -= currentRoundData.pointsAddedForNormalAnswer;
			scoreDisplayText.text = "Score: " + playerScore.ToString ();
		}
	}


	public void EndRound(bool win)
	{
        if (win) //Player got all the answers
		{
            roundScore = (int)(Mathf.Round(timeRemaining) + playerScore);
            dataController.addRoundScore(roundScore);
			winScoreDisplay.text = "Score: " + playerScore.ToString () + " + " + Mathf.Round(timeRemaining).ToString () + " = " + roundScore.ToString();
            toSummaryBtn.SetActive(false);

            RemoveAnswerButtons();

            //End the round
            if (roundCounter == roundLimit)
            {
                //Debug.Log("User has reached the round limit");
                nextRoundBtn.SetActive(false);
                menuButton.SetActive(false);
                toSummaryBtn.SetActive(true);
            }

            winDisplay.SetActive(true);
            
            //Reset the current answers list
            currentAnswers.Clear();
        } else if (!win) //Player didn't get all the answers
		{
            if (playerScore == 0)
            {
                zeroScoreText.SetActive(true);
            }
            dataController.addRoundScore (playerScore);
			loseScoreDisplay.text = "Score: " + playerScore.ToString ();
            toSummaryBtnLost.SetActive(false);

            RemoveAnswerButtons();

            
            
            //Sort the missed answers alphabetically before displaying them
            currentAnswers.Sort();

            //Display the answers that the player missed
            missedAnswerDisplay.text += "You missed:";
            for (int i = 0; i < currentAnswers.Count; i++)
            {
                missedAnswerDisplay.text += "\n" + currentAnswers[i].ToString();
            }

            //Reset the current answers list
            currentAnswers.Clear();

            //End the round
            if (roundCounter == roundLimit)
            {
                Debug.Log("User has reached the round limit");
                
                nextRoundBtnLost.SetActive(false);
                menuButtonLost.SetActive(false);
                toSummaryBtnLost.SetActive(true);
            }

            roundEndDisplay.SetActive(true);

        } else 
		{
			Debug.Log ("win is undefined, restart");
			Application.Quit ();
		}
    }

    public void toSummary()
    {
        loadingPanel.SetActive(true);
        //display an ad
        //adManager.playAd();
        toSummaryBtn.SetActive(false);
        toSummaryBtnLost.SetActive(false);
        //only display the summary panel
        winDisplay.SetActive(false);
        roundEndDisplay.SetActive(false);
        questionDisplay.SetActive(false);
        scoreDisplayText.enabled = false;
        roundNumberText.enabled = false;
        timeRemainingDisplayText.enabled = false;
        timeRemainingHeader.enabled = false;
        summaryDisplay.SetActive(true);
        
        var theTransform = scrollBackgroundSummary.transform as RectTransform;
        theTransform.sizeDelta = new Vector2(680*dataController.getNumberOfRounds(), 1192);
        
        //Display scores for each round
        for (int i = 0; i < dataController.getNumberOfRounds(); i++)
        {
            GameObject instancedScoreDisplayPrefab = GameObject.Instantiate<GameObject>(scoreDisplayPrefab);
            instancedScoreDisplayPrefab.transform.SetParent(GameObject.FindGameObjectWithTag("scoreDisplayContainer").transform, false);

            var script = instancedScoreDisplayPrefab.GetComponent<roundScoreDisplay>();
            script.setScoreText(i);
            script.loadImage(i);
        }

        finalScoreText.text = "Total Score: " + dataController.getTotalScore();

        loadingPanel.SetActive(false);
    }

	public void ReturnToMenu()
	{	
		SceneManager.LoadScene ("MainMenu");
	}

	private void UpdateTimeRemainingDisplay()
	{
		timeRemainingDisplayText.text = Mathf.Round (timeRemaining).ToString();
        if(Mathf.Round(timeRemaining) <= 10)
        {
            timeRemainingDisplayText.color = Color.red;
        } else
        {
            timeRemainingDisplayText.color = Color.white;
        }
	}

    private void preEndRound(bool win)
    {
        isRoundActive = false;
        screenshotManager.TakeScreenshot();
        StartCoroutine(waitUntil(win));
    }

	// Update is called once per frame
	void Update () 
	{
		if (isRoundActive) 
		{	
			if (playerScore == maxScore)
            {
                preEndRound(true);
            }
			
			timeRemaining -= Time.deltaTime;
			UpdateTimeRemainingDisplay ();

			if (timeRemaining <= 0f) 
			{
                preEndRound(false);
            }
		}
	}

    IEnumerator waitUntil(bool win)
    {
        //Debug.Log("Waiting for screenshot to be taken...");
        //Debug.Log("roundCounter = " + roundCounter);
        //Debug.Log("screenshotCounter = " + screenshotManager.returnScreenshotCounter());
        yield return new WaitUntil(() => screenshotManager.returnScreenshotCounter() == roundCounter);
        EndRound(win);
        //Debug.Log("Ending Round");
    }
}
