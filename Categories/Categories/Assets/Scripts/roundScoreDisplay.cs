using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class roundScoreDisplay : MonoBehaviour
{
    private DataController dataController;
    private GameConrtoller gameConrtoller;
    public Image imageDisplay;
    public Text scoreText;
    public Sprite file;
    public Sprite loadedSprite;
    public Texture2D tex;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Starting");
        gameConrtoller = FindObjectOfType<GameConrtoller>();
        dataController = FindObjectOfType<DataController>();

        imageDisplay = GetComponent<Image>();
        //scoreText = GetComponentInChildren<Text>();
    }
    
    public void setScoreText(int roundNum)
    {
        scoreText.text = "Round " + (roundNum+1) + ": " + dataController.returnRoundScores(roundNum).ToString();
        //Debug.Log("Setting score to" + dataController.returnRoundScores(roundNum).ToString());
    }

    public void loadImage(int roundNum)
    {
#if UNITY_EDITOR
        Texture2D texture = GetScreenshotImage(Application.persistentDataPath + "Screenshot" + roundNum + ".png");
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //loadedSprite = Resources.Load<Sprite>("Screenshots/Screenshot" + roundNum);
        imageDisplay.sprite = sp;
#elif UNITY_ANDROID
        StartCoroutine(loadTex(Application.persistentDataPath + "Screenshot" + roundNum + ".png"));
#endif
    }

    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(996, 2048, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    IEnumerator loadTex(string filePath)
    {
        //tex = new Texture2D(996, 2048, TextureFormat.RGB24, false);
        //WWW www = new WWW(filePath);
        //yield return www;
        //tex = www.texture;
        byte[] byteArray = File.ReadAllBytes(filePath);
        tex.LoadImage(byteArray);
        Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;
        if(1 == 0)
        {
            yield return 1;
        }
    }
}
