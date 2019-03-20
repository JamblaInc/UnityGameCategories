using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    private int screenshotCounter = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeScreenshot_Static(Screen.width, Screen.height);
        }
    }

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if(takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.streamingAssetsPath + "/Screenshots/CameraScreenshot" + screenshotCounter + ".png", byteArray);
            Debug.Log("Saved Screenshot" + screenshotCounter);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;

            //Increment screenshotCounter
            screenshotCounter++;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }

    public int returnScreenshotCount()
    {
        return screenshotCounter;
    }
}
