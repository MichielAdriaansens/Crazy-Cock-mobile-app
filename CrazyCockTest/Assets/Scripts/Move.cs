using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Move : MonoBehaviour 
{
	[SerializeField]
	Transform _Destination;
	public NavMeshAgent _NavMesh;
	UnitStats _unitStats;


	public bool temCtrl = false;


	// Use this for initialization
	void Start () 
	{
		_NavMesh = this.GetComponent<NavMeshAgent> ();
		_NavMesh.speed = 0;
		_unitStats = this.GetComponent<UnitStats> ();

		if (_NavMesh == null)
		{
			print ("damn boy");
		}
		else
		{
			SetDestination ();
		}
	}

	void SetDestination()
	{
		if (_Destination != null)
		{
			Vector3 targetVector = _Destination.transform.position;
			_NavMesh.SetDestination (targetVector);
		}	
	}

	float Distance()
	{
		Vector3 direction = _Destination.position - this.transform.position;
		return direction.magnitude;
	}

	void MoveUnit()
	{
		_NavMesh.speed = _unitStats.unitSpeed;
	}
	void StopUnit()
	{
		_NavMesh.speed = 0f;
	}

//	IEnumerator GoDestination ()
//	{
//		SetDestination ();
//		yield return new WaitForSeconds (0.01f);
//	}

	// Update is called once per frame
	void Update () 
	{
		if (temCtrl)
		{
			MoveUnit ();
		}
		else
		{
			StopUnit ();
		}
	}
	void LateUpdate()
	{
		if (temCtrl && Distance () < 0.1f)
		{
			temCtrl = false;
		}
	}
}
