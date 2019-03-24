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
    public Sprite loadedSprite;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Starting");
        gameConrtoller = FindObjectOfType<GameConrtoller>();
        dataController = FindObjectOfType<DataController>();

        imageDisplay = GetComponent<Image>();
        //scoreText = GetComponentInChildren<Text>();
    }
    
    public void setScoreText(int roundNum)
    {
        scoreText.text = dataController.returnRoundScores(roundNum).ToString();
        Debug.Log("Setting score to" + dataController.returnRoundScores(roundNum).ToString());
    }

    public void loadImage(int roundNum)
    {
        Debug.Log("Setting image " + roundNum);
        loadedSprite = Resources.Load<Sprite>("Screenshots/Screenshot" + roundNum);
        imageDisplay.sprite = loadedSprite;
        //Debug.Log("roundNum = " + roundNum);
    }
}
