using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {

	public Text answerText;
	private GameConrtoller gameController;

	private AnswerData answerData;

	public bool isSelected;
	public int aCounter;

    public List<string> currentAnswers = new List<string>();

    // Use this for initialization
    void Start () 
	{
		//Access the gamecontroller object
		gameController = FindObjectOfType<GameConrtoller> ();
		aCounter = 0;
	}

	public void SetUp(AnswerData data)
	{
		//Change the colour of the button
		gameObject.GetComponent<Button> ().image.color = Color.white;

		//Ensure button is set to false by default
		isSelected = false;

		//Get the answer data
		answerData = data;

        //Want to access this in gamemanager to stop it restarting at every button.
        //Store the current answers in an array
        currentAnswers.Add(answerData.answerText);
        //Debug.Log("currentAnswers currently is storing: " + currentAnswers[0]);
        Debug.Log("currentAnswers currently is storing: " + currentAnswers[0]);
        Debug.Log("currentAnswers has size: " + currentAnswers.Count);

        //Set the text on the button
        answerText.text = answerData.answerText;
	}

	public int GetAnswerCount()
	{
		return aCounter;
	}

	public void HandleClick()
	{
		//Check if the button has already been pressed
		if (!isSelected) {
			aCounter++;
			//Put the button into a selected state
			isSelected = true;

			//Add the points to the score
			gameController.AddPoints (answerData.isSpecial);

			//Change the colour of the button
			if (answerData.isSpecial) 
			{
				gameObject.GetComponent<Button> ().image.color = Color.yellow;
			} else 
			{
				gameObject.GetComponent<Button> ().image.color = Color.green;
			}
		} else if (isSelected) 
		{
			aCounter--;
			//Put the button into a de-selected state
			isSelected = false;

			//Remove the points from the score
			gameController.RemovePoints (answerData.isSpecial);

			//Change the colour of the button
			gameObject.GetComponent<Button> ().image.color = Color.white;
		}

		//Check if the answer is correct and handle the click in the GameController
		gameController.AnswerButtonClicked (answerData.isSpecial);
		if (aCounter == 7) 
		{
			//allow the user to go to the next round.
		}
	}	
}
