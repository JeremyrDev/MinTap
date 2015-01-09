using UnityEngine;
using System.Collections;

public class ReadyScript : MonoBehaviour {

	// Use this for initialization
	Vector3 endPosition;
	float smooth = 8;
	int counter=26;
	bool increaseSize = false;
	void Start () {
		endPosition = new Vector3(.5f, .76f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, endPosition, smooth * Time.deltaTime);
		if(increaseSize)
			counter++;
		guiText.fontSize = counter;
		if(transform.position == new Vector3(.5f, .76f, 0)){
			endPosition = new Vector3(.5f, -1.4f, 0);
			smooth = .5f;
			guiText.text = "GO!";
			increaseSize = true;
		}
		if(transform.position == new Vector3(.5f, -1.4f, 0)){
			//endPosition = new Vector3(.5f, -1.4f, 0);
			//smooth = .5f;
			//guiText.text = "GO!";
			increaseSize = false;
			gameObject.active = false;
		}
	}
	void OnMouseDown()
	{
		gameObject.active = false;
	}
}
