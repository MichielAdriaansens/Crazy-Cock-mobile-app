using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance;

	void Awake()
	{
		if (instance == null)
		instance = this;
	}

	// Use this for initialization
	public void StartGame () 
	{
		UiManager.instance.Play (); //also plays animation and loads level
		//sound
	}

	void GameOver()
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
