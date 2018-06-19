using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour 
{
	 

	//singleton
	public static WayPointManager instance;

	[HideInInspector]
	public  GameObject[] _wp;
	[HideInInspector]
	public List<Transform> tempL = new List<Transform> ();

	public List<Transform> RouteUpperL = new List<Transform> (); //set Manually
	public List<Transform> RouteBottomL = new List<Transform> (); //set Manually
	public List<Transform> RouteUpperR = new List<Transform> (); //set Manually
	public List<Transform> RouteBottomR = new List<Transform> (); //set Manually
//	public List<Transform> RouteCentre;
//	public List<Transform> RouteRandom;


	void Awake()
	{
		instance = this;

		_wp = GameObject.FindGameObjectsWithTag ("enemyWP");

		for(int i = 0; i < _wp.Length; i++)
		{
			_wp [i].name = "enemyWP " + i;
			tempL.Add (_wp [i].transform);
		}
	}

}
