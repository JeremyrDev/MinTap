using UnityEngine;
using System.Collections;

public class speedUpScript : MonoBehaviour {

	void OnMouseDown()
	{
		if(Input.GetMouseButton(0))
			Time.timeScale+=1;
		if(Input.GetMouseButton(1))
			Time.timeScale-=1;
	}
}
