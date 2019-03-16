using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text currentTimeDisplay;
    public Text currentNumberOfRoundsDisplay;
    public Text currentRoundsHead;
    public Text currentTimeHead;
    public Text screenHeader;
    private DataController dataController;
    public Slider roundTimeSlider;
    public Slider currentNumberOfRoundsSlider;
    public Button muteButton;
    public Button helpButton;
    public Button startButton;
    public GameObject helpPanel;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();

        //Hide help panel
        helpPanel.SetActive(false);
        
        //Get and display current round time
        roundTimeSlider.value = dataController.getCurrentRoundTime();
        currentTimeDisplay.text = dataController.getCurrentRoundTime() + "s";

        //Get and display current number of rounds
        currentNumberOfRoundsSlider.value = dataController.getNumberOfRounds();
        currentNumberOfRoundsDisplay.text = dataController.getNumberOfRounds ().ToString();
       
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeDisplay.text = dataController.getCurrentRoundTime() + "s";
        dataController.changeRoundTime(roundTimeSlider.value);

        currentNumberOfRoundsDisplay.text = dataController.getNumberOfRounds().ToString();
        dataController.changeNumberOfRounds(currentNumberOfRoundsSlider.value);

    }

    public void displayHelp()
    {
        muteButton.enabled = false;
        startButton.enabled = false;
        helpButton.enabled = false;
        helpPanel.gameObject.SetActive(true);
    }
    
    public void closeHelp()
    {
        muteButton.enabled = true;
        startButton.enabled = true;
        helpButton.enabled = true;
        helpPanel.gameObject.SetActive(false);
    }
}
