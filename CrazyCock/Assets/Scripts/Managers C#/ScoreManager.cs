using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;


	int highScore;
	int score = 0;

	void Awake()
	{	
		if(instance == null)
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.SetInt ("score", score);
	}

	void NewhighScore()
	{
		if (PlayerPrefs.HasKey ("highscore"))
		{
			if (score > PlayerPrefs.GetInt("highscore"))
			{
				PlayerPrefs.SetInt ("highscore", score);
			}
		}
		else
		{
			PlayerPrefs.SetInt ("highscore", score);
		}

	}

	void getScore(int Eggs)
	{
		PlayerPrefs.SetInt ("score", score + Eggs);
	}
}
