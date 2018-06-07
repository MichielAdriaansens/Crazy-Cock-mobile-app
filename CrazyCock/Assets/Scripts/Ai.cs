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

	private bool isTriggered;

	void Start()
	{
		//unitState = UnitState.Patrol;
		_move = GetComponent<Move> ();
		_wpManage = GameObject.Find ("WayPoint_holder").GetComponent<WayPointManager> ();
		//_uStats = GetComponent<UnitStats> ();
	}

	void Chase()
	{
		_move._CurDestination = GameObject.Find ("target").transform;
		_move.SetDestination ();
		_move.moveActive = true;
	}
	void Patrol()
	{
		if (_move.plannedRoute.Count == 0)
		{
			_move.SendMessage ("SetRoute", _wpManage.RouteA);
		}
		_move.FollowRoute ();
	}
	void SwitchState () 
	{
		switch (unitState)
		{
		case UnitState.Idle:
			_move.StopUnit ();
			break;
		case UnitState.Patrol:
			Patrol ();			
			break;
		case UnitState.FLee:
			break;
		case UnitState.Chase:
			Chase ();
			if (_move.onTarget)
			{
				unitState = UnitState.Idle;
			}
			break;
		default:
			break;
		}	
	}

	void Update()
	{
		SwitchState ();
	}
}
