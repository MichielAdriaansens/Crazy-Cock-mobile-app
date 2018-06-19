using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl_Playa : MonoBehaviour 
{
	PlayerStats pStats;
	PlayerController playerCtrl;
	Animator _anim;

	// Use this for initialization
	void Start () 
	{
		playerCtrl = GetComponent<PlayerController> ();	
		_anim = GetComponentInChildren<Animator> ();
		pStats = GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerCtrl.isMoving)
		{
			_anim.SetBool ("Walk", true);
		} else
		{
			_anim.SetBool ("Walk", false);
		}

		if (pStats.NRGized)
		{
			_anim.SetBool ("CrazyMode", true);
		} else
		{
			_anim.SetBool ("CrazyMode", false);
		}
	}
}
