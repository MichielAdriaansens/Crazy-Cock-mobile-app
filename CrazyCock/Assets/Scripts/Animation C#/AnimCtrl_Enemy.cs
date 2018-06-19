using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class AnimCtrl_Enemy : MonoBehaviour 
{
	Move _PathF;
	Animator _Anim;
	// Use this for initialization
	void Start () 
	{
		_PathF =  this.GetComponent<Move>();
		_Anim = this.GetComponentInChildren<Animator> ();		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_PathF._NavMesh.speed != 0)
		{
			_Anim.SetBool ("Walk", true);
		}
		else
		{
			_Anim.SetBool ("Walk", false);
		}
	}
}
