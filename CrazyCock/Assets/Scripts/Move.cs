using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Move : WayPointManager
{
	
	public Transform _CurDestination;
	UnitStats _unitStats;

	public NavMeshAgent _NavMesh;


	public bool moveActive = false;
	public bool onTarget;
	public bool onRoute;

	public List<Transform> plannedRoute;

	public int nextDest = 0;
	// Use this for initialization
	void Start () 
	{
		plannedRoute = new List<Transform> ();
		//Get required Components this script interacts with
		_NavMesh = this.GetComponent<NavMeshAgent> ();
		_NavMesh.speed = 0;
		_unitStats = this.GetComponent<UnitStats> ();

		//warn ya dumbass for forgetting to assign navmesh agent component on unit
		if (_NavMesh == null) 
		{
			print ("damn boy, give " + this.transform.name + " navMeshAgentPlx");
		}
			
	}

	public void SetDestination() //get vector 3 location of destination Transform
	{
		if (_CurDestination != null)
		{
			Vector3 targetVector = _CurDestination.transform.position;
			_NavMesh.SetDestination (targetVector);
			Distance ();
		}	
	}

	float Distance() //Calculate destination between target ennehh poppetje.. returns float value daarvan. handig
	{
		Vector3 direction = _CurDestination.position - this.transform.position;
		return direction.magnitude;
	}

	void DestinationReached(bool bingo) //functie runs when bestemming is reached
	{
		print ("i'm here now!");
		StopUnit ();
		onTarget = true;
		_CurDestination = null;

		if (onRoute)
		{
			onRoute = false;
			nextDest++;
			if (nextDest > plannedRoute.Count -1)
			{
				nextDest = 0;
			}
		}
	}
		

	void SetRoute(Transform[] routeInput)
	{
		foreach (Transform path in routeInput)
		{
			plannedRoute.Add (path);
		}

	}

	public void FollowRoute()
	{
		// coroutine voor randon timer
		if (!onRoute)
		{
			_CurDestination = plannedRoute [nextDest];
			SetDestination ();	//Niet vergeten als je moet lopen
			moveActive = true;

			onRoute = true;
		}
	}

	IEnumerator HoldUpNibba()
	{
		
		yield return null;


	}

	void MoveUnit() //set de speed of the nav mesh agent so the unit starts moving
	{
		onTarget = false;
		if (_CurDestination == null)
		{
			moveActive = false;
			return;
		}
		
		_NavMesh.speed = _unitStats.BaseSpeed;
	}
	public void StopUnit() //unit stops drops and opens up shop ya'll
	{
		_NavMesh.speed = 0f;
	}
		



	// Update is called once per frame
	void Update () 
	{
		if (moveActive)
		{
			MoveUnit ();
		}
	}
	void LateUpdate()
	{
		if (moveActive && Distance () < _unitStats.stoppingDist)
		{
			moveActive = false;
			SendMessage ("DestinationReached", true);
		}
	}
}
	