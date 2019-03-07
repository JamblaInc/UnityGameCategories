using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Text currentTimeDisplay;
    private DataController dataController;
    public Slider roundTimeSlider;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
                
        roundTimeSlider.value = dataController.getCurrentRoundTime();
        currentTimeDisplay.text = dataController.getCurrentRoundTime() + "s";
       
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeDisplay.text = dataController.getCurrentRoundTime() + "s";
        dataController.changeRoundTime(roundTimeSlider.value);

    }
    
}
