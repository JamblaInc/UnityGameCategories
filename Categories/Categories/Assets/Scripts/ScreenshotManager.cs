﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    private int screenshotCounter = 0;

    public void TakeScreenshot()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        string fileName = "/Screenshots/Screenshot" + screenshotCounter + ".png";
        string pathToSave = Application.streamingAssetsPath + fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        Debug.Log("Screenshot Taken");
        screenshotCounter++;
        yield return new WaitForEndOfFrame();
    }

    public int returnScreenshotCounter()
    {
        return screenshotCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}