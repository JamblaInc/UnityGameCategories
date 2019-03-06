using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Text currentTimeDisplay;
    public int currentRoundTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeDisplay = GetComponent<Text>();
        currentRoundTime = 10;
        currentTimeDisplay.text = currentRoundTime + "s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void timeDisplayUpdater(float newTime)
    {
        currentTimeDisplay.text = newTime + "s";
        //currentRoundTime = Mathf.RoundToInt(newTime);
    }
}
