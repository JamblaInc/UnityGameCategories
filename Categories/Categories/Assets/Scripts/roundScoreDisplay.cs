﻿using System.Collections;
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
    private Texture2D textureToLoad;

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
#if !UNITY_EDITOR
        string url = Application.persistentDataPath +"/"+ "Screenshot" + roundNum + ".png";
         var bytes = File.ReadAllBytes( url );
         Texture2D texture = new Texture2D( 996, 2048 );
         texture.LoadImage( bytes );
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite= sp ;

#else

        StartCoroutine(loadTex("Screenshots/Screenshot" + roundNum));
        Debug.Log("sping");
#endif
        //Sprite sp = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f, 0.5f));
        //imageDisplay.sprite = sp;


        /**
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "Screenshot" + roundNum + ".png");
        Texture2D textureToLoad = new Texture2D(996, 2048, TextureFormat.RGB24, false);
        textureToLoad.LoadImage(bytes);
        Sprite sp = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;

#if UNITY_EDITOR
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "Screenshot" + roundNum + ".png");
        Texture2D textureToLoad = new Texture2D(996, 2048, TextureFormat.RGB24, false);
        textureToLoad.LoadImage(bytes);
        Sprite sp = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;

        //loadedSprite = Resources.Load<Sprite>("Screenshots/Screenshot" + roundNum);
        //imageDisplay.sprite = loadedSprite;

        
        //StartCoroutine(loadTex(Application.persistentDataPath + "/Screenshot" + roundNum + ".png"));

        string filePath = Application.streamingAssetsPath + "/Screenshot" + roundNum + ".png";
        WWW www = new WWW(filePath);
        while (!www.isDone) { }
        Texture2D tex;
        tex = www.texture;
        //byte[] byteArray = File.ReadAllBytes(filePath);
        //tex.LoadImage(byteArray);
        Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;
        
#elif UNITY_ANDROID
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Screenshot" + roundNum + ".png");
        Texture2D textureToLoad = new Texture2D(996, 2048, TextureFormat.RGB24, false);
        textureToLoad.LoadImage(bytes);
        Sprite sp = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;
#endif
**/
    }

    IEnumerator loadTex(string filePath)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<Sprite>(filePath);
        while (!resourceRequest.isDone)
        {
            Debug.Log("yielding 0");
            yield return 0;
        }
        //textureToLoad = new Texture2D(996, 2048, TextureFormat.RGB24, false);
        //textureToLoad = (Texture2D)resourceRequest.asset;
        Sprite sp = resourceRequest.asset as Sprite;
        Debug.Log("yield return null");
        //Sprite sp = Sprite.Create(textureToLoad, new Rect(0, 0, textureToLoad.width, textureToLoad.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite = sp;
        yield return null;
    }
}
