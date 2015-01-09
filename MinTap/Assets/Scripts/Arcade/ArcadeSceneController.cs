using UnityEngine;
using System.Collections;

public class ArcadeSceneController : MonoBehaviour {

	public int numberOfObjectsToCreate = 50;

	public GameObject ObjectToPool;
	GameObject[] objects = null;

	public GameObject TapCircle;

	public int spawnType = 0;
	public int spawnDirection = 0;
	public int spawnCounter = 0;
	public int cycle = 2;
	public int multiplierCounter = 0;
	public int multiplierLimit = 12;
	int newObjectCounter = 0;
	int wave = 0;
	int waveCounter = 0;
	int waveLimit = 5;
	int baseObjectScore = 1;
	int numberOfObjectsToSpawn = 1;
	int spawned = 0;
	int fpsCounter = 0;

	private float timer = 0;
	public float scoreMultiplier = 1;
	public float score = 0;
	float spawnTimer = 0;
	float defaultTimer = 0;
	float timeBetweenSpawn = 1;
	float totalProgressBarValue = 0;
	float speedIncreaseTime = 5;
	float multiplierFontSize = 32;
	float multiplierScaleTimer=0;
	float amountToSpawn = 5;
	float playTimer = 0;
	float fpsTimer = 0;
	float TitleTimer = 0;

	public bool lifeTaken = false;
	bool justStarting = true;
	bool waiting = true;
	bool spawn = false;
	public bool endOfGameBool = false;
	bool increasedMultiplier = false;
	bool increaseCircleSize = false;
	bool stopEndOfGameSpawn = false;

	Color color1 = new Vector4(0.05f, .09f, .15f, 1);
	Color color2 = new Vector4(0.05f, .09f, .15f, 1);
	Color color3 = new Vector4(0.0f, .0f, .0f, 1);

	string multiplierString;
	public string debugInfo;
	string fpsValue;
	
	public GUIStyle scoreTextStyle;
	public GUIStyle multiplierTextStyle;

	public AudioClip waveBell;
	public AudioClip objectDeath;
	public AudioClip lostLife;
	public AudioClip EndOfGameClip;
	public AudioClip multiplier;
	public AudioClip backgroundMusic;

	public Animation endOfGameAnimation;

	public Material tapCircleMaterial;

	public GameObject mouseTrail;
	public TextMesh title;

	Vector3 TitleToVector = new Vector3(.2f, 4.6f, 0);

	float randomX, randomY;

	void Start () 
	{
		objects = new GameObject[numberOfObjectsToCreate];
//		objectsB = new GameObject[numberOfObjectsToCreate];
//		objectsG = new GameObject[numberOfObjectsToCreate];
		camera.backgroundColor = new Vector4(0.05f, .09f, .15f, 1);
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
			timeBetweenSpawn = 0;
			spawnCounter = 0;
			amountToSpawn = 0;
			endOfGameBool = true;
			OpenMenu();
			stopEndOfGameSpawn = true;
			animation.Play("whiteFlash");
		}
	}
	void changeTitleColor()
	{
		TitleTimer+=1*Time.deltaTime;
		//float t2 = Mathf.PingPong(Time.time, 1f) / 1f;
		if(TitleTimer>=.1)
		{
//			if(TitleToVector.x == .1f)
//			{
//				TitleToVector = new Vector3(-.1f, 4.6f, 0);
//			}
//			else
//			{
//				TitleToVector = new Vector3(.1f, 4.6f, 0);
//			}
			randomX = Random.Range(6.15f, -6.15f);
			randomY = Random.Range (5.50f, -5.70f);
			TitleToVector = new Vector3(randomX, randomY,0);
			TitleTimer = 0;
		}
		title.transform.position = Vector3.Lerp(title.transform.position,TitleToVector, 4*Time.deltaTime);
	}
	void OnGUI () 
	{

		GUI.Label (new Rect (0,0,100,50), debugInfo, scoreTextStyle);
		GUI.Label (new Rect (Screen.width/2,0,100,50), score.ToString("n0"), scoreTextStyle);
		GUI.Label (new Rect (Screen.width - 100,0,100,50), multiplierString, multiplierTextStyle);
	}
	void changeBackgroundColor()
	{
		//float t = Mathf.PingPong(Time.time, .5f) / .5f;

		//camera.backgroundColor = Color.Lerp(color1, color2 , t);
		
//		if(t<=0.05f)
//		{
//			//cycle = 0;
//			if(cycle==0)
//				color2 = new Vector4(0.05f, .09f, .15f, 1);
//			if(cycle==1)
//				color2 = Color.blue;
//			if(cycle==2)
//				//color2 = Color.green;
//				color2 = new Vector4(0.05f, .3f, .1f, 1);
//			if(cycle==3)
//				color2 = Color.magenta;
//			if(cycle==4)
//				color2 = Color.black;
//			if(cycle==5)
//				color2 = Color.cyan;
//			if(cycle==6)
//				//color2 = Color.red;
//				color2 = new Vector4(0.3f, .01f, .01f, 1);
//			if(cycle==7)
//				color2 = new Vector4(0.06f, .15f, .35f, 1);
//			if(cycle==8)
//				color2 = Color.yellow;
//			//if(!waiting)
//				//cycle = 0;
//		}
	}
	void Update () 
	{
		changeTitleColor();
		debugInfo = " Wave "+wave.ToString ()+"\n sc: "+spawnCounter+"\n spawned: "+spawned+"\n ml: "+multiplierLimit+"\n ats: "+ amountToSpawn +"\n noots: "+numberOfObjectsToSpawn;
		
		debugInfo += "\n EndOfGame: "+endOfGameBool + "\n FPS: "+fpsValue;

		fpsTimer+=1*Time.deltaTime;
		fpsCounter++;
		if(fpsTimer>=1){
			fpsValue = fpsCounter.ToString();
			fpsCounter = 0;
			fpsTimer = 0;
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

		//debugInfo = "defaultTimer: "+defaultTimer+"\n spawnTimer: "+spawnTimer+"\n"+spawnCounter+"\n "+ amountToSpawn+"\n timebetweenSpawn: "+ timeBetweenSpawn+"\n waiting: "+waiting+"\n wave: "+wave+"\n lives: "+numberOfLives.ToString ("n0");

		if(spawnCounter >= amountToSpawn)
		{
			if(endOfGameBool)
			{
				spawn = false;
			}
			else
			{
				playTimer+=1*Time.deltaTime;
				if(spawn)
				{
					//spawn = false;
					//spawnTimer = 1.5f;
					waiting = true;
					amountToSpawn*=1.05f;
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
				//cycle = 2;
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
				//cycle++;
				if(cycle>8)
					cycle = 0;
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
				//mouseTrail.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y ,0);
				if(Physics.SphereCast(ray.origin, .75f, transform.forward, out hit, 10))
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
						hit.transform.gameObject.GetComponent<smallObject>().Shrink = true;
						hit.transform.gameObject.GetComponent<smallObject>().wasHit = true;
						hit.transform.tag = "Hit";
					}
					else
					{
						scoreMultiplier = 1;
						multiplierLimit = 12;
					}
				} 
			}
			else
			{
				//mouseTrail.transform.position = mouseTrail.transform.position;
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
