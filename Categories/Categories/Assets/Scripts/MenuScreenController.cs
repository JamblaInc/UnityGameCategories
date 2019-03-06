using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene ("MainGameScene");
	}

	public void LoadInstructions()
	{
		SceneManager.LoadScene ("About");
	}

	public void LoadSettings()
	{
		SceneManager.LoadScene ("Settings");
	}

	public void ReturnToMenu()
	{	
		SceneManager.LoadScene ("MainMenu");
	}
}
