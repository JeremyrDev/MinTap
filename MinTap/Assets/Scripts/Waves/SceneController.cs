using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public int numberOfObjectsToCreate = 50;

	public GameObject ObjectToPool;
	GameObject[] objects = null;

//	public GameObject ObjectBToPool;
//	GameObject[] objectsB = null;
//
//	public GameObject ObjectGToPool;
//	GameObject[] objectsG = null;

	public GameObject threeObjectPool;
	GameObject[] threeObject = null;
	public int numberOfThreeToCreate = 50;

	public GameObject TapCircle;

	public int spawnType = 0;
	public int spawnDirection = 0;
	public int spawnCounter = 0;
	public int cycle = 7;
	public int numberOfLives = 3;
	public int multiplierCounter = 0;
	public int multiplierLimit = 12;
	int newObjectCounter = 0;
	int wave = 0;
	int waveCounter = 0;
	int waveLimit = 5;
	int baseObjectScore = 1;
	int numberOfObjectsToSpawn = 2;
	int spawned = 0;

	private float timer = 0;
	public float scoreMultiplier = 1;
	public float score = 0;
	float spawnTimer = 1;
	float defaultTimer = 0;
	float timeBetweenSpawn = 1;
	float totalProgressBarValue = 0;
	float speedIncreaseTime = 5;
	float multiplierFontSize = 32;
	float multiplierScaleTimer=0;
	float amountToSpawn = 5;
	float playTimer = 0;
	float lifeTakenCounter = 0;

	public bool lifeTaken = false;
	bool justStarting = true;
	bool waiting = true;
	bool spawn = false;
	bool startNewObject = false;
	bool newObject1 = true;
	bool newObject2 = false;
	bool newObject3 = false;
	bool showNotification = true; 
	public bool endOfGameBool = false;
	bool increasedMultiplier = false;
	bool increaseCircleSize = false;
	bool stopEndOfGameSpawn = false;

	Color color1 = new Vector4(0.05f, .09f, .15f, 1);
	Color color2 = new Vector4(0.05f, .09f, .15f, 1);
	Color color3 = new Vector4(0.0f, .0f, .0f, 1);

	string multiplierString;
	public string debugInfo;

	public GUITexture menuBackground;

	public GUIText notificationText;
	
	public GUIStyle scoreTextStyle;
	public GUIStyle multiplierTextStyle;

	public AudioClip waveBell;
	public AudioClip objectDeath;
	public AudioClip lostLife;
	public AudioClip EndOfGameClip;
	public AudioClip multiplier;
	public AudioClip backgroundMusic;

	public Material tapCircleMaterial;

	void Start () 
	{
		objects = new GameObject[numberOfObjectsToCreate];
//		objectsB = new GameObject[numberOfObjectsToCreate];
//		objectsG = new GameObject[numberOfObjectsToCreate];

		InstantiateObjects();
	}
	public void OpenMenu()
	{
		if(endOfGameBool)
		{

		}
		else
		{

		}
	}
	public void EndOfGame()
	{
		if(!stopEndOfGameSpawn)
		{
			newObject1 = true;
			newObject2 = true;
			newObject3 = true;
			timeBetweenSpawn = 0;
			spawnCounter = 0;
			amountToSpawn = 150;
			endOfGameBool = true;
			OpenMenu();
			stopEndOfGameSpawn = true;
		}
	}

	void OnGUI () 
	{
		GUI.Label (new Rect (0,0,100,50), debugInfo, scoreTextStyle);
		GUI.Label (new Rect (Screen.width/2,0,100,50), score.ToString("n0"), scoreTextStyle);
		GUI.Label (new Rect (Screen.width - 100,0,100,50), multiplierString, multiplierTextStyle);
	}
	void changeBackgroundColor()
	{
		float t = Mathf.PingPong(Time.time, .5f) / .5f;
		//float t = Mathf.InverseLerp (0, 1, Time.time);
		
		//camera.backgroundColor = Color.Lerp (color1, color2, t);

		if(endOfGameBool && stopEndOfGameSpawn)
		{
			camera.backgroundColor = Color.Lerp(color1, color3 , t);
			color1 = new Vector4(0.15f, .05f, .05f, 1);
		}
		else
		{
			camera.backgroundColor = Color.Lerp(color1, color2 , t);
		}
		
		if(t<=0.05f)
		{
			//cycle = 0;
			if(cycle==0)
				color2 = new Vector4(0.05f, .09f, .15f, 1);
			if(cycle==1)
				color2 = Color.blue;
			if(cycle==2)
				//color2 = Color.green;
				color2 = new Vector4(0.05f, .3f, .1f, 1);
			if(cycle==3)
				color2 = Color.magenta;
			if(cycle==4)
				color2 = Color.black;
			if(cycle==5)
				color2 = Color.cyan;
			if(cycle==6)
				//color2 = Color.red;
				color2 = new Vector4(0.3f, .01f, .01f, 1);
			if(cycle==7)
				color2 = new Vector4(0.06f, .15f, .35f, 1);
			if(cycle==8)
				color2 = Color.yellow;
			if(!waiting)
				cycle = 0;
		}
	}
	void Update () 
	{
		if(lifeTaken)
		{
			lifeTakenCounter+=1*Time.deltaTime;
			if(lifeTakenCounter>=1)
			{
				lifeTaken = false;
				lifeTakenCounter = 0;
				cycle = 0;
			}
			else
			{
				cycle = 6;
			}
		}
		changeBackgroundColor();
		if(increasedMultiplier)
		{
			multiplierScaleTimer+=1*Time.deltaTime;
			if(multiplierScaleTimer >=.02f)
			{
				if(multiplierTextStyle.fontSize > 32)
				{
					multiplierTextStyle.fontSize -=1;
				}

				if(multiplierTextStyle.fontSize ==32)
					increasedMultiplier = false;
				multiplierScaleTimer = 0;
			}
		}
		if(scoreMultiplier>1)
		{
			multiplierString = "X" + scoreMultiplier;
		}
		else
		{
			multiplierString = " ";
		}
		if(numberOfLives<=0)
		{
			EndOfGame();
		}

		//debugInfo = "defaultTimer: "+defaultTimer+"\n spawnTimer: "+spawnTimer+"\n"+spawnCounter+"\n "+ amountToSpawn+"\n timebetweenSpawn: "+ timeBetweenSpawn+"\n waiting: "+waiting+"\n wave: "+wave+"\n lives: "+numberOfLives.ToString ("n0");
		debugInfo = " Wave "+wave.ToString ()+"\n Lives: "+ numberOfLives+"\n sc: "+spawnCounter+"\n spawned: "+spawned+"\n ml: "+multiplierLimit+"\n ats: "+ amountToSpawn +"\n noots: "+numberOfObjectsToSpawn;
		if(spawnCounter >= amountToSpawn)
		{
			if(endOfGameBool)
			{
				spawn = false;
				if(spawnCounter>50)
				{
					newObject1 = false;
					newObject2 = false;
					newObject3 = false;
				}
			}
			else
			{
				playTimer+=1*Time.deltaTime;
				if(spawn)
				{
					spawn = false;
					spawnTimer = 1.5f;
					waiting = true;
					amountToSpawn*=1.5f;
				}
				else
				{
					spawn = true;
				}
				if(justStarting)
				{
					justStarting = false;
				}
			}
		}
		if(waiting)
		{
			if(cycle !=6)
				cycle = 2;
			defaultTimer+=1*Time.deltaTime;
			if(defaultTimer >= spawnTimer)
			{
				spawn = true;
				waiting = false;
				defaultTimer = 0;
				spawnCounter = 0;
				spawned = 0;
				audio.PlayOneShot(waveBell);
				wave++;
				timeBetweenSpawn-=.1f;
				if(wave>=waveLimit)
				{
					waveLimit+=5;
					timeBetweenSpawn = 1;
					numberOfObjectsToSpawn++;
				}
			}
		}
		//-----------------------------Spawn----------------------------------------------------
		if(spawn)
		{
			timer+=1*Time.deltaTime;
			if(timer>=timeBetweenSpawn)
			{
				for(int i = 0; i < numberOfObjectsToSpawn; i++)
					ActivateObject();
				timer = 0;
			}
		}
		//-----------------------------Get Mouse Input----------------------------------------------------
		if(!endOfGameBool){
			if (Input.GetMouseButton(0)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Debug.DrawRay(ray.origin, Vector3.forward);

				if(Physics.SphereCast(ray.origin, .75f,transform.forward, out hit, 10))
				{
					if(hit.transform.tag == "Object")
					{
						multiplierCounter++;
						if(multiplierCounter>multiplierLimit)
						{
							cycle = 0;
							multiplierLimit*=2;
							scoreMultiplier*=2;
							increasedMultiplier = true;
							multiplierTextStyle.fontSize = 60;
							multiplierCounter = 0;
						}
						score+=baseObjectScore*scoreMultiplier;
						audio.PlayOneShot(objectDeath);
						//hit.transform.parent.gameObject.GetComponent<smallObject>().TR.enabled = false;
						hit.transform.gameObject.GetComponent<smallObject>().Deactivate();

					}
				} 
			}
		}
	}
	//----------------------------------------------------------------------------------------------------------
	private void InstantiateObjects()
	{
		for(int i = 0; i < numberOfObjectsToCreate; i++)
		{
			objects[i] = Instantiate(ObjectToPool) as GameObject;
			objects[i].SetActiveRecursively(false);

//			objectsB[i] = Instantiate(ObjectBToPool) as GameObject;
//			objectsB[i].SetActiveRecursively(false);
//			objectsG[i] = Instantiate(ObjectGToPool) as GameObject;
//			objectsG[i].SetActiveRecursively(false);
		}
	}

	//-------------------------------------------------------------------------------------
	private void ActivateObject()
	{			
		//spawned = 0;
		//if(spawned <= numberOfObjectsToSpawn){
			for(int i = 0; i < numberOfObjectsToCreate; i++)
			{
				if(objects[i].active==false)
				{
					//if(newObject1){
					objects[i].SetActiveRecursively(true);
					objects[i].GetComponent<smallObject>().Activate();
					//}
//					if(newObject3){
//						objectsB[i].SetActiveRecursively(true);
//						objectsB[i].GetComponent<smallObject>().Activate();
//						spawnCounter++;
//					}
//					if(newObject2){
//						objectsG[i].SetActiveRecursively(true);
//						objectsG[i].GetComponent<smallObject>().Activate();
//						spawnCounter++;
//					}
					spawnCounter++;
					//spawned++;
					return;


				}
			//ActivateObject();
			//return;
		}
	}
}
