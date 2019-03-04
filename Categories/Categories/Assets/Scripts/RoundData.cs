using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData
{

	public string name;
	public int timeLimitInSeconds;
	public int pointsAddedForSpecialAnswer;
	public int pointsAddedForNormalAnswer;
	public Question[] questions;
}
