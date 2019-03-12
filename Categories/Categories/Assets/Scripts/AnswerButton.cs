using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {

	public Text answerText;
	private GameConrtoller gameController;
	public AnswerData answerData;

	public bool isSelected;
	public int aCounter;

    public Sprite specialButtonSprite;
    public Sprite normalButtonSprite;

    // Use this for initialization
    void Start () 
	{
		//Access the gamecontroller object
		gameController = FindObjectOfType<GameConrtoller> ();
		aCounter = 0;
    }

	public void SetUp(AnswerData data)
	{
        //Reset the colour & image of the button
        gameObject.GetComponent<Button>().image.color = Color.white;
        gameObject.GetComponent<Button>().image.sprite = normalButtonSprite;

		//Ensure button is set to false by default
		isSelected = false;

		//Get the answer data
		answerData = data;

        //Set the text on the button
        answerText.text = answerData.answerText;
	}

    public string returnAnswer()
    {
        return answerData.answerText;
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

            //Check that the current answer is in the currentAnswer list, if so, delete
            if(gameController.currentAnswers.Contains(answerData.answerText))
            {
                gameController.currentAnswers.Remove(answerData.answerText);
            }

            //Print the entire list
            for (int i = 0; i < gameController.currentAnswers.Count; i++)
            {
                Debug.Log(gameController.currentAnswers[i]);
            }

            //Change the colour of the button
            if (answerData.isSpecial) 
			{
                gameObject.GetComponent<Button>().image.sprite = specialButtonSprite;
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

            //Check that the current answer is in the currentAnswer list, if not, add
            if (!gameController.currentAnswers.Contains(answerData.answerText))
            {
                gameController.currentAnswers.Add(answerData.answerText);
            }

            //Print the entire list
            for(int i = 0; i < gameController.currentAnswers.Count; i++)
            {
                Debug.Log(gameController.currentAnswers[i]);
            }

            //Reset the colour & image of the button
            gameObject.GetComponent<Button>().image.color = Color.white;
            gameObject.GetComponent<Button>().image.sprite = normalButtonSprite;
        }

		//Check if the answer is correct and handle the click in the GameController
		gameController.AnswerButtonClicked (answerData.isSpecial);
		if (aCounter == 7) 
		{
			//allow the user to go to the next round.
		}
	}	
}
