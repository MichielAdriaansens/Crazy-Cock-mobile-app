using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour 
{
	//points given to player on Death
	public int pointValue = 100;

	//How accurate unit behaves
	public int Performance = 10;

	//Basic movement speed for patrolling
	public float BaseSpeed = 1.2f;

	//Add when chasing or fleeing
	public float BonusSpeed = 0.3f; 

	//how close unit must be to destination to register ..when destination is reached
	public float stoppingDist = 0.5f;

	//Ramge for Attacking Player
	public float AttackRange = 0.5f;

}
