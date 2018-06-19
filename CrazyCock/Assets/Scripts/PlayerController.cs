using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	PlayerStats pStats;
	public Transform target;

	public bool isMoving;

	bool goingUp;
	bool goingDown;
	bool goingLeft;
	bool goingRight;


	// Use this for initialization
	void Start () 
	{
		pStats = GetComponent<PlayerStats> ();
	}

	void Move()
	{
		if (target != null)
		{
			Vector3 direction = target.position - this.transform.position;

			if (direction.magnitude > pStats.stoppingDist)
			{
				this.transform.Translate (direction.normalized * pStats.playaSpeed * Time.deltaTime, Space.World);
				//transform.LookAt (target.position);
				transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * pStats.rotSpeed);
				isMoving = true;
			}
			else
			{
				isMoving = false;
			}
		}
	}

	void DetectWP(Vector3 dir)
	{

		RaycastHit hit;
		Ray front = new Ray (this.transform.position, dir);
		if (Physics.Raycast (front, out hit, 1.5f, 1 << 8))
		{
		//	print (hit.transform.name);
			target = hit.transform;
		}

		Debug.DrawRay (this.transform.position, dir * 1.5f, Color.red,8f);

	} 

	void AutoMove()
	{
		if (goingUp)
		{
			DetectWP (Vector3.forward);
		} else if (goingDown)
		{
			DetectWP (Vector3.back);
		} else if (goingLeft)
		{
			DetectWP (Vector3.left);
		} else if (goingRight)
		{
			DetectWP (Vector3.right);
		}
	}



	void PickUpItem()
	{

	}

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("w"))
		{
			goingUp = true;
			goingDown = false;
			goingLeft = false;
			goingRight = false;
		}
		if (Input.GetKeyDown("s"))
		{
			goingDown = true;
			goingUp = false;
			goingLeft = false;
			goingRight = false;
		}
		if (Input.GetKeyDown("a"))
		{
			goingLeft = true;
			goingUp = false;
			goingDown = false;
			goingRight = false;
		}
		if (Input.GetKeyDown("d"))
		{
			goingUp = false;
			goingDown = false;
			goingLeft = false;
			goingRight = true;
		}
		//find better way then Update
		AutoMove();

	}
	void LateUpdate()
	{
		Move ();
	}
}
