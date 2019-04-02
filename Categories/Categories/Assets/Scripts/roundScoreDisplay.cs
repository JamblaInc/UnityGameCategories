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
        //Texture2D texture = new Texture2D( 996, 2048 );
        Texture2D texture = new Texture2D( 670, 1200 );
        texture.LoadImage( bytes );
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imageDisplay.sprite= sp ;
#else
        StartCoroutine(loadTex("Screenshots/Screenshot" + roundNum));
#endif
    }

    IEnumerator loadTex(string filePath)
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<Sprite>(filePath);
        while (!resourceRequest.isDone)
        {
            Debug.Log("yielding 0");
            yield return 0;
        }

        Sprite sp = resourceRequest.asset as Sprite;
        Debug.Log("yield return null");

        imageDisplay.sprite = sp;
        yield return null;
    }
}
