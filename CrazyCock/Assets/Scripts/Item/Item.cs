using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour 
{
	public int pointValue;
	public float speedBuff;
	public float buffduration;

	// Use this for initialization
	void Start () 
	{
		if (transform.name == "EggItem")
		{
			pointValue = 1;
			speedBuff = 0f;
			buffduration = 0;
		}
		else if (transform.name == "NRGItem")
		{
			pointValue = 25;
			speedBuff = 1f;
			buffduration = 6f;
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
