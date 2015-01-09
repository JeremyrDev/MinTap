using UnityEngine;
using System.Collections;

public class fastForwardScript : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Time.timeScale+=.1f;
		}
		if(Input.GetMouseButtonDown (2))
		{
			Time.timeScale-=.1f;
		}
	}
}
