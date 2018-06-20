using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour 
{
	Move _move;
	WayPointManager _wpManage;
	UnitStats _uStats;
	AnimCtrl_Enemy _anim;

	GameObject _player;
	Vector3 playerDist;
	public enum UnitState {Idle, Patrol, Chase, FLee};
	public UnitState unitState;

	private bool isTriggered = false;
	public bool gotPlayer;

	void Start()
	{
		_move = GetComponent<Move> ();
		_uStats = GetComponent<UnitStats> ();
		_wpManage = GameObject.Find ("_WayPoint_holder").GetComponent<WayPointManager> ();
		_player = GameObject.FindGameObjectWithTag ("PlayerCC");
		_anim = GetComponent<AnimCtrl_Enemy> ();

	}

	void Chase()
	{
		_move.onRoute = false;
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


	void DistanceFromPlayer()
	{

			playerDist = transform.position - _player.transform.position;
			if (playerDist.magnitude < _uStats.AttackRange)
			{
				//LookatPlayer
				Vector3 newRot = _player.transform.position - this.transform.position;
				transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (newRot), Time.deltaTime * 10);

			if (!_player.GetComponent<PlayerStats> ().NRGized && !gotPlayer)
			{
				_anim.Attack ();
				KillPlayer ();

				gotPlayer = true;
			}
		}
	}

	void KillPlayer()
	{
		unitState = UnitState.Idle;

		_player.GetComponent<PlayerController> ().SendMessage ("Dead", true);
		_player.GetComponent<AnimCtrl_Playa> ().PlayDeath(transform.position);

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

		DistanceFromPlayer ();
	}
}
