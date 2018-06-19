using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour 
{
	public static UiManager instance;

	public bool playTriggered;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void Play()
	{
		playTriggered = true;
		StartCoroutine (IntroAnim ());
	}

	IEnumerator IntroAnim()
	{
		yield return new WaitForSeconds (0.7f);
		SceneManager.LoadScene ("CC_Lvl1");
	}
}
