using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	void OnMouseDown()
	{
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
