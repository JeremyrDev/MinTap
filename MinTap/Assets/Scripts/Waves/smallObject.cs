using UnityEngine;
using System.Collections;

public class smallObject : MonoBehaviour {

	float life = 4f;
	private float speed = 10.0f;
	private float deathTime = 0.0f;
	public Vector3 endPosition;
	//public Quaternion originalRotationValue;
	GameObject thePlayer;
	float randomX = 0;
	float randomX2 = 0;
	ArcadeSceneController sceneScript;
	int randomNumber = 0;
	public bool Shrink = false;
	Vector3 scaleTo;
	public bool wasHit = false;
	public Component Halo;
	public bool particleTimerBool = false;
	float particleTimer = 0;
	//public TrailRenderer TR;
	//For raycasting---
	//RaycastHit hit;
	//float maxRange = 1.1f;
	void Start()
	{
		thePlayer = GameObject.Find("Main Camera");
		sceneScript = thePlayer.GetComponent<ArcadeSceneController>();
		scaleTo = transform.localScale;
	}

	void Update () 
	{
		if(sceneScript.endOfGameBool){
			Shrink = true;
			wasHit = true;
		}
		CountDown();
		AutoMove();
		if(transform.position == endPosition && !wasHit)
		{
			this.gameObject.SetActiveRecursively(false);
			//sceneScript.endOfGameBool = true;
			//sceneScript.EndOfGame();
		}
		else if(transform.position == endPosition && wasHit)
		{
			this.gameObject.SetActiveRecursively(false);
		}
		if(Shrink)
		{
			scaleTo = new Vector3(0,0,0);
			Shrink = false;
		}

		transform.localScale = Vector3.Lerp(transform.localScale, scaleTo, 25f*Time.deltaTime);
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
		//TR.enabled = false;
		transform.tag = "Object";
		scaleTo = new Vector3(.075f, .075f, .075f);
		float tempWidth = Camera.main.orthographicSize * Screen.width / Screen.height;

		deathTime = Time.time + life;
		randomX = Random.Range(tempWidth,tempWidth*-1);
		randomX2 = Random.Range(tempWidth,tempWidth*-1);

		transform.position = new Vector3(randomX, -10, 0);
		endPosition = new Vector3(randomX2, 5.6f, -.5f);
		wasHit = false;
	}
	public void Deactivate()
	{
		this.gameObject.SetActiveRecursively(false);
	}
	private void CountDown()
	{
		if(deathTime<Time.time)
		{
			this.gameObject.SetActiveRecursively(false);
//			sceneScript.scoreMultiplier = 1;
//			sceneScript.cycle = 6;
//			sceneScript.multiplierLimit = 2;
//			sceneScript.multiplierCounter = 0;
//			sceneScript.numberOfLives-=1;
		}
	}
//	public void OnTriggerEnter(Collider other)
//	{
//		if(other.tag == "objectDeath")
//		{
//			this.gameObject.SetActiveRecursively(false);
//			sceneScript.scoreMultiplier = 1;
//			sceneScript.cycle = 6;
//			sceneScript.multiplierLimit = 2;
//			sceneScript.multiplierCounter = 0;
//			sceneScript.numberOfLives-=1;
//		}
//	}
	private void AutoMove()
	{
		//if(randomNumber == 0){
			//renderer.material = lerpMaterial;
		//transform.position = Vector3.Lerp(transform.position, endPosition, (speed/10) * Time.deltaTime);
		transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);
		//}
	}
}
