using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour 
{
	Move _move;
	WayPointManager _wpManage;
//	UnitStats _uStats;

	public enum UnitState {Idle, Patrol, Chase, FLee};
	public UnitState unitState;

	private bool isTriggered = false;

	void Start()
	{
		//unitState = UnitState.Patrol;
		_move = GetComponent<Move> ();
		_wpManage = GameObject.Find ("WayPoint_holder").GetComponent<WayPointManager> ();
		//_uStats = GetComponent<UnitStats> ();
	}

	void Chase()
	{
		_move.onRoute = false;
	//	_move._CurDestination = GameObject.Find ("target").transform;	//Debug target must be Player
		_move.SetDestination ();
		_move.moveActive = true;
	}
	void Patrol()
	{
		if (_move.plannedRoute.Count == 0)
		{
			_move.SendMessage ("SetRoute", _wpManage.tempL);	//set wich route unit will follow 
		}
		_move.FollowRoute ();
	}
	void SwitchState () 
	{
		switch (unitState)
		{
		case UnitState.Idle:
			#region OnceInUpdate
			if (!isTriggered)
			{
				_move.onRoute = false;
				isTriggered = true;
			}
			#endregion
			_move.StopUnit ();
			break;
		case UnitState.Patrol:
	
			Patrol ();			
			break;
		case UnitState.FLee:
			#region OnceInUpdate
			if (!isTriggered)
			{
				_move.onRoute = false;
				isTriggered = true;
			}
			#endregion
			break;
		case UnitState.Chase:
			#region OnceInUpdate
			if (!isTriggered)
			{
				_move.onRoute = false;
				isTriggered = true;
			}
			#endregion
			Chase ();
			break;
		default:
			break;
		}	
	}
	void CheckOnTarget(bool bingo)
	{
		unitState = UnitState.Idle;

		return;
	}
		
	void Update()
	{
		SwitchState ();
	}
}
