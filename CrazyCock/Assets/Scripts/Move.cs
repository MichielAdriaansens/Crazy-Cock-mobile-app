using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Move : WayPointManager
{
	
	public Transform _CurDestination;

	UnitStats _unitStats;
	Ai _ai;

	public NavMeshAgent _NavMesh;

	public bool moveActive = false;
	[HideInInspector]
	public bool patrolPause;
	[HideInInspector]
	public bool onRoute;
	[HideInInspector]
	public int nextDest = 0;

	public List<Transform> plannedRoute;


	// Use this for initialization
	void Awake () 
	{
		plannedRoute = new List<Transform> ();
		//Get required Components this script interacts with
		_NavMesh = this.GetComponent<NavMeshAgent> ();
		_NavMesh.speed = 0;
		_unitStats = this.GetComponent<UnitStats> ();
		_ai = this.GetComponent<Ai> ();

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
		} else
		{
			moveActive = false;
		}
	}

	float Distance() //Calculate destination between target ennehh poppetje.. returns float value daarvan. handig
	{	if (_CurDestination != null)
		{
			Vector3 direction = _CurDestination.position - this.transform.position;
			return direction.magnitude;
		} else
		{
			return 0f;
		}
	}

	void DestinationReached(bool bingo) //functie runs when bestemming is reached
	{
		StopUnit ();



		if (onRoute)
		{
			onRoute = false;
			nextDest++;
			if (nextDest > plannedRoute.Count - 1)
			{
				nextDest = 0;
			}
		} 
		else
		{
			_ai.SendMessage ("CheckOnTarget", true);

			_CurDestination = null;
		}
	}
		
	#region SetFollowRoutePauses
	void SetRoute(List<Transform> routeInput)
	{
		foreach (Transform path in routeInput)
		{
			plannedRoute.Add (path);
		}

	}

	public void FollowRoute()
	{
		
		if (!onRoute)
		{
			// coroutine voor randon pause inbetween moving from reached target to the next
			StartCoroutine (HoldUpNibba ());
			onRoute = true;
		} 

	}

	IEnumerator HoldUpNibba()
	{
		_CurDestination = plannedRoute [nextDest].transform;

		float rng;
		//change the seed number of the random range to make it more rng
		Random.InitState(System.DateTime.Now.Millisecond);
		rng = Random.Range (0,3);
		if (rng == 0 && _unitStats.Performance < 7 || _unitStats.Performance < 3)
		{
			patrolPause = true;
		} else
		{
			patrolPause = false;
		}

		if (patrolPause)
		{
			rng = Random.Range (0, 30) / 10f;
		} else
		{
			 rng = 0; 
		}

		print ("wp " + _CurDestination.name);

		yield return new WaitForSeconds(rng);

		//FollowRoute Code

		SetDestination ();	//Niet vergeten als je moet lopen
		moveActive = true;
	}
	#endregion

	void MoveUnit() //set de speed of the nav mesh agent so the unit starts moving
	{
		
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
	