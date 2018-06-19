using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour 
{
	public int pointValue;
	public float speedBuff;

	// Use this for initialization
	void Start () 
	{
		if (transform.name == "EggItem")
		{
			pointValue = 1;
			speedBuff = 0f;
		}
		else if (transform.name == "NRGItem")
		{
			pointValue = 25;
			speedBuff = 0.5f;
		}
	}

	public void DestroyObj()
	{
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
