using UnityEngine;
using System.Collections;

public class SceneControllerMenu : MonoBehaviour {

	public GameObject ObjectToPool;
	GameObject[] objects = null;
	public int numberOfObjectsToCreate = 50;

	public int cycle = 7;


	private float timer = 0;


	bool spawn = false;


	Color color1 = new Vector4(0.06f, .15f, .35f, 1);
	Color color2 = new Vector4(0.06f, .15f, .35f, 1);
	Color color3 = new Vector4(0.0f, .0f, .2f, 1);

	string multiplierString;
	public string debugInfo;
	
	public GUIStyle scoreTextStyle;

	public Material tapCircleMaterial;

	void Start () 
	{
		objects = new GameObject[numberOfObjectsToCreate];

		InstantiateObjects();
	}


	void OnGUI () 
	{
		GUI.Label (new Rect (0,0,100,50), debugInfo, scoreTextStyle);
	}
	void changeBackgroundColor()
	{
		float t = Mathf.PingPong(Time.time, .5f) / .5f;
		camera.backgroundColor = Color.Lerp(color1, color2 , t);
		
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
		}
	}
	void Update () 
	{
//		if(lifeTaken)
//		{
//			lifeTakenCounter+=1*Time.deltaTime;
//			if(lifeTakenCounter>=1)
//			{
//				lifeTaken = false;
//				lifeTakenCounter = 0;
//				cycle = 0;
//			}
//			else
//			{
//				cycle = 6;
//			}
//		}
		changeBackgroundColor();
//		if(increasedMultiplier)
//		{
//			multiplierScaleTimer+=1*Time.deltaTime;
//			if(multiplierScaleTimer >=.05f)
//			{
//				if(multiplierTextStyle.fontSize > 32)
//				{
//					multiplierTextStyle.fontSize -=1;
//				}
//
//				if(multiplierTextStyle.fontSize ==32)
//					increasedMultiplier = false;
//				multiplierScaleTimer = 0;
//			}
//		}
//		if(scoreMultiplier>1)
//		{
//			multiplierString = "X" + scoreMultiplier;
//		}
//		else
//		{
//			multiplierString = " ";
//		}
//		if(numberOfLives<=0)
//		{
//			EndOfGame();
//		}

		//debugInfo = "defaultTimer: "+defaultTimer+"\n spawnTimer: "+spawnTimer+"\n"+spawnCounter+"\n "+ amountToSpawn+"\n timebetweenSpawn: "+ timeBetweenSpawn+"\n waiting: "+waiting+"\n wave: "+wave+"\n lives: "+numberOfLives.ToString ("n0");
		//debugInfo = " Wave "+wave.ToString ()+"\n Lives: "+ numberOfLives+ "\n cycle: "+cycle;
//		if(spawnCounter >= amountToSpawn)
//		{
//			if(endOfGameBool)
//			{
//				spawn = false;
//				if(spawnCounter>145)
//				{
//					newObject1 = false;
//					newObject2 = false;
//					newObject3 = false;
//				}
//			}
//			else
//			{
//				playTimer+=1*Time.deltaTime;
//				if(spawn)
//				{
//					spawn = false;
//					spawnTimer = 1.5f;
//					waiting = true;
//					amountToSpawn*=1.5f;
//				}
//				else
//				{
//					spawn = true;
//				}
//				if(justStarting)
//				{
//					justStarting = false;
//				}
//			}
//		}
//		if(waiting)
//		{
//			if(cycle !=6)
//				cycle = 2;
//			defaultTimer+=1*Time.deltaTime;
//			if(defaultTimer >= spawnTimer)
//			{
//				spawn = true;
//				waiting = false;
//				defaultTimer = 0;
//				spawnCounter = 0;
//				audio.PlayOneShot(waveBell);
//				wave++;
//				timeBetweenSpawn-=.1f;
//				if(wave>=waveLimit)
//				{
//					waveLimit+=5;
//					timeBetweenSpawn = 1;
//					if(!newObject2)
//						newObject2 = true;
//					if(newObject2 && !newObject3)
//						newObject3 = true;
//				}
//			}
		//-----------------------------Spawn----------------------------------------------------

		timer+=1*Time.deltaTime;
		if(timer>=1)
		{
			ActivateObject();
			timer = 0;
		}
	}
	//----------------------------------------------------------------------------------------------------------
	private void InstantiateObjects()
	{
		for(int i = 0; i < numberOfObjectsToCreate; i++)
		{
			objects[i] = Instantiate(ObjectToPool) as GameObject;
			objects[i].SetActiveRecursively(false);
		}
	}

	//-------------------------------------------------------------------------------------
	private void ActivateObject()
	{
		for(int i = 0; i < numberOfObjectsToCreate; i++)
		{
			if(objects[i].active==false)
			{
				objects[i].SetActiveRecursively(true);
				objects[i].GetComponent<smallObject>().Activate();	
				return;
			}
		}
	}
}
