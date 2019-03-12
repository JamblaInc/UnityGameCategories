using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text currentTimeDisplay;
    public Text currentNumberOfRoundsDisplay;
    private DataController dataController;
    public Slider roundTimeSlider;
    public Slider currentNumberOfRoundsSlider;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
                
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
    
}
