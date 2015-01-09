using UnityEngine;
using System.Collections;

public class objectLerpScript : MonoBehaviour {

	float life = 15f;
	private float speed = 10.0f;
	private float deathTime = 0.0f;
	public Vector3 endPosition;
	public Quaternion originalRotationValue;
	GameObject thePlayer;
	float randomX = 0;
	float randomX2 = 0;
	SceneController sceneScript;
	int randomNumber = 0;
	public TrailRenderer TR;
	string debugInfo;
	public GUIStyle customGuiStyle;
	//For raycasting---
	//RaycastHit hit;
	//float maxRange = 1.1f;
	void Start()
	{
		thePlayer = GameObject.Find("Main Camera");
		sceneScript = thePlayer.GetComponent<SceneController>();
	}
	void OnGUI () 
	{
		//GUI.Label (new Rect (0,0,100,50), debugInfo, customGuiStyle);
		//GUI.Label (new Rect (Screen.width/2,Screen.height - 25,100,50), score.ToString(), customGuiStyle);
		//GUI.Label (new Rect (Screen.width - 150,25,50,100), debugInfo, customGuiStyle);
	}
	void Update () 
	{
		CountDown();
		AutoMove();
		if(transform.position == endPosition)
		{
			TR.enabled = false;
			this.gameObject.SetActiveRecursively(false);

			sceneScript.score -=50f;
			sceneScript.scoreMultiplier = 1;
			sceneScript.cycle = 6;
			sceneScript.multiplierLimit = 2;
			sceneScript.multiplierCounter = 0;
			sceneScript.numberOfLives-=1;
		}
		//per object raycasting to another main object
//		if(Physics.Raycast(transform.position,(player.transform.position-transform.position), out hit))
//			
//		{
//			Debug.DrawRay(transform.position, (player.transform.position-transform.position), Color.green);
//			
//		}    
//		if(Vector3.Distance (player.transform.position, transform.position)<= maxRange)
//		{
//			player.GetComponent<PlayerController>().PlayerDeath();
//		}
	}

	public void Activate()
	{
		float tempWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		TR.enabled = false;
		deathTime = Time.time + life;
		randomX = Random.Range(tempWidth,tempWidth*-1);
		randomX2 = Random.Range(tempWidth,tempWidth*-1);
		
		transform.position = new Vector3(randomX, -6, 0);
		endPosition = new Vector3(randomX2, 5.6f, -.5f);
		debugInfo = randomX.ToString ("f2")+"\n"+randomX2.ToString ("f2")+"\n"+tempWidth;
		TR.time = .1f;
		TR.enabled = true;
	}
	public void Deactivate()
	{
		TR.time = .01f;
		TR.enabled = false;
		this.gameObject.SetActiveRecursively(false);

		//rigidbody.useGravity = true;
		//rigidbody.constraints = RigidbodyConstraints.None;
	}
	private void CountDown()
	{
		if(deathTime<Time.time)
		{
			Deactivate();
		}
	}
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "objectDeath")
		{
			this.gameObject.SetActiveRecursively(false);
			sceneScript.score -=50f;
		}
	}
	private void AutoMove()
	{
		//if(randomNumber == 0){
			//renderer.material = lerpMaterial;
			transform.position = Vector3.Lerp(transform.position, endPosition, (speed/10) * Time.deltaTime);
		//}
		//if(randomNumber == 1){
			//transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);
		//}
	}
	
}
