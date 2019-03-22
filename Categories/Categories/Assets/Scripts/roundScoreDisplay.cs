using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundScoreDisplay : MonoBehaviour
{
    private DataController dataController;
    private GameConrtoller gameConrtoller;
    public Image imageDisplay;
    public Text scoreText;
    Sprite sprite;
    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting");
        gameConrtoller = FindObjectOfType<GameConrtoller>();
        dataController = FindObjectOfType<DataController>();

        imageDisplay = GetComponent<Image>();
        scoreText = GetComponentInChildren<Text>();
        setScoreText(gameConrtoller.returnDisplayCounter());
        loadImage(gameConrtoller.returnDisplayCounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScoreText(int roundNum)
    {
        scoreText.text = dataController.returnRoundScores(roundNum).ToString();
    }

    public void loadImage(int roundNum)
    {
        sprite = Resources.Load<Sprite>("Screenshots/Screenshot" + roundNum);
        imageDisplay.sprite = sprite;
        Debug.Log("roundNum = " + roundNum);
        gameConrtoller.setIsFinished(true);
    }
}
