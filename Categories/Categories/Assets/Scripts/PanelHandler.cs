using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour
{
    private DataController dataController;
    private RoundData currentRoundData;
    List<int> unusedQuestions = new List<int>();
    private Question[] questionPool;
    private Text[] panelText;
    public GameObject panel;
    private int questionIndex;
    private int frontSpinCount;
    private int backSpinCount;


    // Start is called before the first frame update
    void Start()
    {
        //Locate the frontPanel
        panelText = GetComponentsInChildren<Text>();

        //Set the spin count of the panel to zero
        frontSpinCount = 0;
        backSpinCount = 0;
        
        //Allow us to access the current round data
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();

        //Load the questions into the questionPool
        questionPool = currentRoundData.questions;

        //Load all the quesiton numbers into our array
        for (int j = 0; j < questionPool.Length; j++)
        {
            unusedQuestions.Add(j);
            //Debug.Log ("Element " + j + " is equal to " + unusedQuestions[j]);
        }
        questionToPanel(0);
        questionToPanel(1);

        //TODO do this by angle of rotation not time
        StartCoroutine(waiterFront(2));
        StartCoroutine(waiterBack(4));
    }

    void questionToPanel(int panelIndex)
    {
        //Set the current question to a random question
        questionIndex = unusedQuestions[Random.Range(0, unusedQuestions.Count)];

        //Load in the question data
        Question questionData = questionPool[questionIndex];

        //Remove the question that has been used from the list
        unusedQuestions.Remove(questionIndex);

        //set the first panel to a category
        panelText[panelIndex].text = questionData.questions;

        if(unusedQuestions.Count < 5)
        {
            //Load all the quesiton numbers back into our list
            Debug.Log("Reloading questions");
            for (int j = 0; j < questionPool.Length; j++)
            {
                unusedQuestions.Add(j);
                //Debug.Log ("Element " + j + " is equal to " + unusedQuestions[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 90, 0, 0));
    }

    IEnumerator waiterFront(int i)
    {
        yield return new WaitForSeconds(i);
        questionToPanel(0);
        StartCoroutine(waiterFront(4));
    }

    IEnumerator waiterBack(int i)
    {
        yield return new WaitForSeconds(i);
        questionToPanel(1);
        StartCoroutine(waiterBack(4));
    }
}
