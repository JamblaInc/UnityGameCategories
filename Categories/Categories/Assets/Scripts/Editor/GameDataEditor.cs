﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow
{
	public GameData gameData;
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";

	[MenuItem ("Window/Game Data Editor")]
	static void Init()
	{
		GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow (typeof(GameDataEditor));
		window.Show ();
	}

	void OnGUI()
	{
        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (gameData != null) 
		{
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }

            SerializedObject serializedObject = new SerializedObject (this);
			SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

			EditorGUILayout.PropertyField (serializedProperty, true);

			serializedObject.ApplyModifiedProperties ();

			
		}
    }

	private void LoadGameData()
	{
		string filePath = Application.dataPath + gameDataProjectFilePath;

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			gameData = JsonUtility.FromJson<GameData> (dataAsJson);
		} else 
		{
			gameData = new GameData ();
		}
	}

	private void SaveGameData()
	{
		string dataAsJson = JsonUtility.ToJson (gameData);
		string filePath = Application.dataPath + gameDataProjectFilePath;
		File.WriteAllText (filePath, dataAsJson);
	}

}
