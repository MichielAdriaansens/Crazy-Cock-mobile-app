using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour 
{
	 

	//singleton
	public static WayPointManager instance;

	public  GameObject[] _wp;
	public Transform[] RouteA;

	void Awake()
	{
		instance = this;

		_wp = GameObject.FindGameObjectsWithTag ("enemyWP");
		System.Array.Reverse (_wp);
		for(int i = 0; i < _wp.Length; i++)
		{
			_wp [i].name = "enemyWP " + i;
		}
	}

}
