using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level_Manager : MonoBehaviour
{
	public static Level_Manager instance;
	public bool playerDied;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	GameObject[] Items;
	public int EggCounter = 0;
	public bool levelWon; 
	//GameObject[] Eggs;

	// Use this for initialization
	void Start () 
	{
		Items = GameObject.FindGameObjectsWithTag ("Item");
		foreach(GameObject item in Items)
		{
			if (item.GetComponent<Item> ().ItemId == 0)
			{
				EggCounter ++;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (EggCounter == 0)
		{
			levelWon = true;
			UiManager.instance.ifWin (levelWon);
		}
		if(playerDied)
		{
			UiManager.instance.ifLose (playerDied);
		}
	}
}
