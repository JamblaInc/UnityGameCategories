using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        string fileName = "Screenshot" + screenshotCounter + ".png";
        //string pathToSave = Application.persistentDataPath + fileName;
#if UNITY_EDITOR
        string pathToSave = "Assets/Resources/Screenshots/" + fileName;
#elif UNITY_ANDROID
        //Test for Android
        string pathToSave = fileName;
#endif
        ScreenCapture.CaptureScreenshot(pathToSave);
        //Debug.Log("Screenshot Taken");
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
