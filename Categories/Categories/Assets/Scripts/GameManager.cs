/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Question[] categories;
	private static List<Question> unusedCategories;

	public Answers[] answers;	
	private static List<Answers> unusedAnswers;


	private Question currentCategory;

	[SerializeField]
	private Text categoryText, answerText1, answerText2, answerText3, answerText4, answerText5, answerText6, answerText7;

	void Start()
	{
		if (unusedCategories == null || unusedCategories.Count == 0) 
		{
			unusedCategories = categories.ToList<Question>();
		}

		SetCurrentCategory();
	}

	void SetCurrentCategory()
	{
		int randomCategoryIndex = Random.Range (0, unusedCategories.Count);
		currentCategory = unusedCategories [randomCategoryIndex];

		categoryText.text = currentCategory.category;
		answerText1.text = currentCategory.answers [0];
		answerText2.text = currentCategory.answers [1];
		answerText3.text = currentCategory.answers [2];
		answerText4.text = currentCategory.answers [3];
		answerText5.text = currentCategory.answers [4];
		answerText6.text = currentCategory.answers [5];
		answerText7.text = currentCategory.answers [6];

		unusedCategories.RemoveAt (randomCategoryIndex);

	}


	public void UserSelectAnswer (Image buttonColour)
	{
		Color myWhite = new Color(255, 255, 255);
		Color myBlue = new Color(0, 219, 219);

		buttonColour.color = myBlue;
	}
}
**/