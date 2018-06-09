using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EggWiggle : MonoBehaviour {

	int Rng;

	// Use this for initialization
	void Start ()
	{
		Random.InitState(System.DateTime.Now.Millisecond);
		Rng = Random.Range (0, 5);

		if (Rng != 0)
		{
			Destroy (this.GetComponent<EggWiggle> ());
		} 
		else
		{
				
		}
	}
		
	void GenerateWiggle()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
