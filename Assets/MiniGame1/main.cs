using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class main : MonoBehaviour {

	// Use this for initialization
	Camera camera;

	int difficulty=4;

	int[][] plane = new int[2][];
	int[][] colorsOfFight=new int[2][];
	Mesh arrowMesh;

	void Start () 
	{
		//Debug.Log (GameObject.Find ("score3").GetComponent<RectTransform> ().localScale);

		GameObject.Find ("score3").GetComponent<RectTransform> ().localScale = new Vector3 (0, 0, 0);
		GameObject.Find ("score3").GetComponent<Image> ().enabled = true;

		arrowMesh = GameObject.Find ("character1").transform.GetChild (0).GetComponent<MeshFilter> ().mesh;
		for(int i=1; i<=5; i++)
			GameObject.Find ("character"+i).transform.GetChild(0).GetComponent<MeshFilter> ().mesh = null;

	//	int a = 5 / 2;
		//Debug.Log (a);
		
		lives [0] = 5;
		lives [1] = 5;
		points [0] = 0;
		points [1] = 0;

		plane[0] = new int[6];
		plane[1] = new int[6];

		colorsOfFight[0] = new int[6];
		colorsOfFight[1] = new int[6];

		int[] positionToChoose = new int[5] {1,2,3,4,5};

		Material colorForAddEnemy=Resources.Load("green", typeof(Material)) as Material;

	//	Debug.Log(Random.Range(0,4));
		int countDifficulty = int.Parse((difficulty / 2).ToString());
		GameObject.Find ("characterE5").GetComponent<Renderer> ().material = colorForAddEnemy;
		colorsOfFight[1][5] = 3;


	/*	int additionalColor = Random.Range (0, 4);
		if(additionalColor==0)
		{
			colorForAddEnemy = Resources.Load("green", typeof(Material)) as Material;
			colorsOfFight[1][5]=3;
		}
		else if(additionalColor==1)
		{
			colorForAddEnemy = Resources.Load("red", typeof(Material)) as Material;
			colorsOfFight[1][5]=2;
		}
		else if(additionalColor==2)
		{
			colorForAddEnemy = Resources.Load("yellow", typeof(Material)) as Material;
			colorsOfFight[1][5]=4;
		}
		else if(additionalColor==3)
		{
			colorForAddEnemy = Resources.Load("blue", typeof(Material)) as Material;
			colorsOfFight[1][5]=1;
		}
		GameObject.Find ("characterE5").GetComponent<Renderer> ().material = colorForAddEnemy;

*/
		colorsOfFight [1] [1] = 1;
		colorsOfFight [1] [2] = 2;
		colorsOfFight [1] [3] = 3;
		colorsOfFight [1] [4] = 4;
		colorsOfFight [1] [5] = 1;
		Debug.Log (countDifficulty);
		for (int i=0; i<countDifficulty; i++) 
		{	
			int additionalColor = Random.Range (0, 4);
			if(additionalColor==0)
			{
				colorForAddEnemy = Resources.Load("green", typeof(Material)) as Material;
					colorsOfFight[1][5-i]=3;
			}
			else if(additionalColor==1)
			{
				colorForAddEnemy = Resources.Load("red", typeof(Material)) as Material;
					colorsOfFight[1][5-i]=2;
			}
			else if(additionalColor==2)
			{
				colorForAddEnemy = Resources.Load("yellow", typeof(Material)) as Material;
				colorsOfFight[1][5-i]=4;
			}
			else if(additionalColor==3)
			{
				colorForAddEnemy = Resources.Load("blue", typeof(Material)) as Material;
				colorsOfFight[1][5-i]=1;
			}
			GameObject.Find ("characterE"+(5-i)).GetComponent<Renderer> ().material = colorForAddEnemy;
		}



		colorsOfFight [0] [1] = 1;
		colorsOfFight [0] [2] = 3;
		colorsOfFight [0] [3] = 2;
		colorsOfFight [0] [4] = 4;
		colorsOfFight [0] [5] = 4;



		Vector3[] positionStartEnemy = new Vector3[10];
		for (int i=0; i<GameObject.Find("AllModelsEnemy").transform.childCount; i++)
			positionStartEnemy [i] = GameObject.Find ("AllModelsEnemy").transform.GetChild(i).transform.position;

		int ranTemp = 0;
		for(int i=0; i<5; i++)
		{
			ranTemp=Random.Range(0,5-i);
			plane[1][i]=positionToChoose[ranTemp];
			for(int i2=ranTemp; i2<4-i; i2++)
				positionToChoose[i2]=positionToChoose[i2+1];



		}

		for(int i=0; i<GameObject.Find("AllModelsEnemy").transform.childCount; i++)
		{
			GameObject.Find("AllModelsEnemy").transform.GetChild(plane[1][i]-1).transform.position = new Vector3(GameObject.Find("platform"+(i+1)+"B").transform.position.x,GameObject.Find("platform"+(i+1)+"B").transform.position.y+15,GameObject.Find("platform"+(i+1)+"B").transform.position.z);
		//	Debug.Log(plane[1][i]+" vs "+i);
		}


		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 16.0f / 9.0f;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
		camera = GetComponent<Camera>();
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
		//GameObject.Find ("Canvas").GetComponent<RectTransform> ().rect.size = new Vector2 (0, 0);

		//Debug.Log (Screen.width+" vs "+GameObject.Find("score1").transform.localPosition.x);


		alreadyTakenPlatform = new GameObject[10];
		positionStart = new Vector3[10];
		rotationStart = new Quaternion[10];
		breckets = new int[10];
		characterTookPlatform = new int[10] {0,0,0,0,0,0,0,0,0,0};
		for (int i=0; i<GameObject.Find("AllModels").transform.childCount; i++) 
		{
			positionStart [i] = GameObject.Find ("AllModels").transform.GetChild (i).transform.position;
			rotationStart[i]= GameObject.Find ("AllModels").transform.GetChild (i).transform.rotation;
		}
		startRotation=GameObject.Find ("AllModels").transform.GetChild(0).transform.rotation;


	}


	GameObject toMoveCharacter;
	int moveAlready=0;
	GameObject[] alreadyTakenPlatform;
	int[] characterTookPlatform;
	int[] breckets;
	Vector3[] positionStart;
	Quaternion[] rotationStart;
	Quaternion startRotation;
	void movingCharacter()
	{



		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		if (usedKey==1)
		{
			Debug.Log ("key "+keyCharacter.transform.name);
			hit = keyCharacter;
			usedKey=0;
		}

		if(hit.transform.name=="character5")
		{
//			Debug.Log(GameObject.Find ("character5").GetComponent<Renderer> ().material.name);
			string textureName=GameObject.Find ("character5").GetComponent<Renderer> ().material.name;
			Material materialToUse = Resources.Load("blue", typeof(Material)) as Material;
			if(textureName=="yellow (Instance)")
			{
			//	Debug.Log("A");
				materialToUse = Resources.Load("blue", typeof(Material)) as Material;
				colorsOfFight [0] [5] = 1;
			}
			else if(textureName=="blue (Instance)")
			{
			//	Debug.Log("B");
				materialToUse = Resources.Load("red", typeof(Material)) as Material;
				colorsOfFight [0] [5] = 2;
			}
			else if(textureName=="red (Instance)")
			{
			//	Debug.Log("C");
				materialToUse = Resources.Load("green", typeof(Material)) as Material;
				colorsOfFight [0] [5] = 3;
			}
			else if(textureName=="green (Instance)")
			{
			//	Debug.Log("D");
				materialToUse = Resources.Load("yellow", typeof(Material)) as Material;
				colorsOfFight [0] [5] = 4;
			}
			else
			{
			//	Debug.Log("E");
				materialToUse = Resources.Load("blue", typeof(Material)) as Material;
				colorsOfFight [0] [5] = 1;
			}
			//Resources.Load("blue", typeof(Material)) as Material;
			GameObject.Find ("character5").GetComponent<Renderer> ().material = materialToUse;

		}




		if(hit.transform.name[0]=='c')
		{
			if(moveAlready==0)
			{
				toMoveCharacter = hit.transform.gameObject;
				moveAlready=1;
//				Debug.Log("AAAA");
				for(int i=1; i<=GameObject.Find ("AllModels").transform.childCount; i++)
					GameObject.Find ("AllModels").transform.GetChild(i-1).transform.GetChild(0).GetComponent<MeshFilter> ().mesh = null;
			//		GameObject.Find ("character"+i).transform.GetChild(0).GetComponent<MeshFilter> ().mesh = null;
				toMoveCharacter.transform.GetChild(0).GetComponent<MeshFilter> ().mesh = arrowMesh;
			}
			else
			{

				int numberTochange=int.Parse(hit.transform.gameObject.name[hit.transform.gameObject.name.Length-1].ToString());
				int numberOfCharacter=int.Parse(toMoveCharacter.name[toMoveCharacter.name.Length-1].ToString());
				int temp=characterTookPlatform[numberTochange];
				if(alreadyTakenPlatform[temp]!=null)
				{
					//if(characterTookPlatform[numberOfCharacter]==0)
				//	Debug.Log("mam cie!");

					Vector3 tempPosition = alreadyTakenPlatform[temp].transform.position;
					alreadyTakenPlatform[temp].transform.position=new Vector3(toMoveCharacter.transform.position.x,alreadyTakenPlatform[temp].transform.position.y,alreadyTakenPlatform[temp].transform.position.z);
					//alreadyTakenPlatform[temp].transform.rotation=toMoveCharacter.transform.rotation;
					characterTookPlatform[int.Parse(alreadyTakenPlatform[temp].transform.name[alreadyTakenPlatform[temp].transform.name.Length-1].ToString ())]=characterTookPlatform[numberOfCharacter];
					//Debug.Log(int.Parse(alreadyTakenPlatform[temp].transform.name[alreadyTakenPlatform[temp].transform.name.Length-1].ToString ())+" a "+characterTookPlatform[numberOfCharacter]);
					alreadyTakenPlatform[characterTookPlatform[numberOfCharacter]]=alreadyTakenPlatform[temp];	

					breckets[numberOfCharacter]=1;
					//Debug.Log(hit.transform.c);
					toMoveCharacter.transform.position=new Vector3(tempPosition.x,toMoveCharacter.transform.position.y,toMoveCharacter.transform.position.z);
					if(numberOfCharacter==1 || numberOfCharacter==5)
						toMoveCharacter.transform.position=new Vector3(tempPosition.x,toMoveCharacter.transform.position.y,toMoveCharacter.transform.position.z);
					
				//	toMoveCharacter.transform.rotation=new Quaternion(0,180,0,0);
				/*	if(numberOfCharacter==4)
						toMoveCharacter.transform.Rotate(0,90,0);
					if(numberOfCharacter==3)
						toMoveCharacter.transform.Rotate(0,45,0);*/
					moveAlready=0;
					characterTookPlatform[numberOfCharacter]=temp;
					alreadyTakenPlatform[temp]=toMoveCharacter;
					//Debug.Log("Aaaa");
				}
//				Debug.Log("temp "+temp);

			}

		}
		else if(hit.transform.name[0]=='p' && moveAlready==1 && bannedPosition[int.Parse(hit.transform.name[hit.transform.name.Length-2].ToString())-1]!=1)
		{

			for(int i=1; i<=GameObject.Find ("AllModels").transform.childCount; i++)
				GameObject.Find ("AllModels").transform.GetChild(i-1).transform.GetChild(0).GetComponent<MeshFilter> ().mesh = null;
	//			GameObject.Find ("character"+i).transform.GetChild(i).GetComponent<MeshFilter> ().mesh = null;

			int temp=int.Parse ((hit.transform.name[hit.transform.name.Length-2]).ToString ());
			int numberOfCharacter=int.Parse(toMoveCharacter.name[toMoveCharacter.name.Length-1].ToString());


			if(beginning==1 && alreadyTakenPlatform[temp]==null && numberOfCharacter>0 && numberOfCharacter<6)
			{
			//	Debug.Log("AAAAA "+characterTookPlatform[numberOfCharacter]);

				plane[0][characterTookPlatform[numberOfCharacter]-1]=0;
			}
				
//			Debug.Log("prawie cie! "+temp);
			if(alreadyTakenPlatform[temp]!=null)
			{
				//if(characterTookPlatform[numberOfCharacter]==0)
				Debug.Log("mam cie!");
				//alreadyTakenPlatform[temp].transform.position=toMoveCharacter.transform.position;
				alreadyTakenPlatform[temp].transform.position=new Vector3(toMoveCharacter.transform.position.x,alreadyTakenPlatform[temp].transform.position.y,alreadyTakenPlatform[temp].transform.position.z);

			//	alreadyTakenPlatform[temp].transform.rotation=toMoveCharacter.transform.rotation;
				characterTookPlatform[int.Parse(alreadyTakenPlatform[temp].transform.name[alreadyTakenPlatform[temp].transform.name.Length-1].ToString ())]=characterTookPlatform[numberOfCharacter];
				//Debug.Log(int.Parse(alreadyTakenPlatform[temp].transform.name[alreadyTakenPlatform[temp].transform.name.Length-1].ToString ())+" a "+characterTookPlatform[numberOfCharacter]);
				alreadyTakenPlatform[characterTookPlatform[numberOfCharacter]]=alreadyTakenPlatform[temp];	
				//Debug.Log("Aaaa");
			}
			else if(characterTookPlatform[numberOfCharacter]!=0)
			{
				//characterTookPlatform[numberOfCharacter]=temp;
				alreadyTakenPlatform[characterTookPlatform[numberOfCharacter]]=null;
			//	Debug.Log("b "+characterTookPlatform[numberOfCharacter]);
			}


		//	Debug.Log(characterTookPlatform[numberOfCharacter]);

			breckets[numberOfCharacter]=1;
			//Debug.Log(hit.transform.c);
			toMoveCharacter.transform.position=new Vector3(hit.transform.position.x,hit.transform.position.y+10,hit.transform.position.z-5);
			if(numberOfCharacter==1 || numberOfCharacter==5)
				toMoveCharacter.transform.position=new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z-1);

			toMoveCharacter.transform.rotation=new Quaternion(0,180,0,0);
			if(numberOfCharacter==4)
				toMoveCharacter.transform.Rotate(0,90,0);
			if(numberOfCharacter==3)
				toMoveCharacter.transform.Rotate(0,45,0);
			moveAlready=0;
			characterTookPlatform[numberOfCharacter]=temp;
			alreadyTakenPlatform[temp]=toMoveCharacter;
		//	Debug.Log("zaznaczam "+temp);
			toMoveCharacter=null;

		}
		else if(hit.transform.name=="start")
		{
			int confrontationStart=0;
			for(int i=1; i<6; i++)
			{
//				Debug.Log(breckets[i]);
				if(breckets[i]==0)
					confrontationStart=1;
			}
			if(confrontationStart==0)
			{
				startBattle();
			}
		}

	}

	int calculateBattle(int a, int b)
	{

//		Debug.Log (colorsOfFight[0][a]+" vs "+colorsOfFight[1][b]+" vs "+a+" "+b);
		if(a==0)
		{
			if(b!=0)
				return 4;
			else
				return 3;
		}
		else if(b==0)
		{
			return 5;
		}
		if(colorsOfFight[0][a]==1)
		{
			if(colorsOfFight[1][b]==1)
			{
				return 3;
			}
			if(colorsOfFight[1][b]==2)
			{
				return 1;
			}
			if(colorsOfFight[1][b]==3)
			{
				return 0;
			}
			if(colorsOfFight[1][b]==4)
			{
				return 2;
			}
		}
		if(colorsOfFight[0][a]==2)
		{
			if(colorsOfFight[1][b]==2)
			{
				return 3;
			}
			if(colorsOfFight[1][b]==1)
			{
				return 2;
			}
			if(colorsOfFight[1][b]==3)
			{
				return 1;
			}
			if(colorsOfFight[1][b]==4)
			{
				return 0;
			}
		}
		if(colorsOfFight[0][a]==3)
		{
			if(colorsOfFight[1][b]==3)
			{
				return 3;
			}
			if(colorsOfFight[1][b]==1)
			{
				return 0;
			}
			if(colorsOfFight[1][b]==2)
			{
				return 2;
			}
			if(colorsOfFight[1][b]==4)
			{
				return 1;
			}
		}
		if(colorsOfFight[0][a]==4)
		{
			if(colorsOfFight[1][b]==4)
			{
				return 3;
			}
			if(colorsOfFight[1][b]==1)
			{
				return 1;
			}
			if(colorsOfFight[1][b]==2)
			{
				return 0;
			}
			if(colorsOfFight[1][b]==3)
			{
				return 2;
			}
		}
		return 10;
	}


	int[] lives = new int[2];
	int[] points = new int[2];
	int prepeareRestart=0;
	int beginning=0;


	IEnumerator waitForShuffle()
	{
		yield return new WaitForSeconds (2);
		shuffleEnemy ();
	}

	int[] bannedPosition = new int[6];
	void shuffleEnemy()
	{
		int[] positionToChoose = new int[5];
		int i3 = 0;
		for(int i=0; i<5; i++)
		{

			if(bannedPosition[i]!=1)
			{
//				Debug.Log("bann "+bannedPosition[i]+" vs "+i3+" = "+(i+1));
				positionToChoose[i3]=i+1;
				i3++;
			}
			plane[1][i]=0;
		}
		int ranTemp = 0;
		for(int i=0; i<5; i++)
		{
//			Debug.Log("do losu: "+positionToChoose[i]);
		}
		for(int i=0; i<GameObject.Find("AllModelsEnemy").transform.childCount; i++)
		{
			ranTemp=Random.Range(0,i3-i);
			if(GameObject.Find("AllModelsEnemy").transform.GetChild(i).name=="characterE5")
			{
				int newColorEnemy=Random.Range(0,4);
				Material matTemp=Resources.Load("blue", typeof(Material)) as Material;
				if(newColorEnemy==0)
				{
					matTemp = Resources.Load("blue", typeof(Material)) as Material;
					colorsOfFight[1][5]=1;
				}
				if(newColorEnemy==1)
				{
					matTemp = Resources.Load("red", typeof(Material)) as Material;
					colorsOfFight[1][5]=2;
				}
				if(newColorEnemy==2)
				{
					matTemp = Resources.Load("green", typeof(Material)) as Material;
					colorsOfFight[1][5]=3;
				}
				else
				{
					matTemp = Resources.Load("yellow", typeof(Material)) as Material;
					colorsOfFight[1][5]=4;
				}
				GameObject.Find ("characterE5").GetComponent<Renderer> ().material = matTemp;	

			
			//Resources.Load("blue", typeof(Material)) as Material;


			}
//			Debug.Log(ranTemp+" po "+positionToChoose[ranTemp]+" vs "+i3+" ko "+i);

			plane[1][positionToChoose[ranTemp]-1]=int.Parse(GameObject.Find("AllModelsEnemy").transform.GetChild(i).name[GameObject.Find("AllModelsEnemy").transform.GetChild(i).name.Length-1].ToString());
			GameObject.Find("AllModelsEnemy").transform.GetChild(i).transform.position = new Vector3(GameObject.Find("platform"+(positionToChoose[ranTemp])+"B").transform.position.x,GameObject.Find("platform"+(positionToChoose[ranTemp])+"B").transform.position.y+15,GameObject.Find("platform"+(positionToChoose[ranTemp])+"B").transform.position.z);

			for(int i2=ranTemp; i2<i3-i-1; i2++)
				positionToChoose[i2]=positionToChoose[i2+1];
			
			
			
		}
		for(int i=0; i<5; i++)
		{
//			Debug.Log(positionToChoose[i]+" uolo "+plane[1][i]);
		}
		for(int i=0; i<GameObject.Find("AllModelsEnemy").transform.childCount; i++)
		{
			//GameObject.Find("AllModelsEnemy").transform.GetChild(plane[1][i]-1).transform.position = new Vector3(GameObject.Find("platform"+(i+1)+"B").transform.position.x,GameObject.Find("platform"+(i+1)+"B").transform.position.y+15,GameObject.Find("platform"+(i+1)+"B").transform.position.z);
			//	Debug.Log(plane[1][i]+" vs "+i);
		}

	}


	IEnumerator animationOfBattle(int mode, int position)
	{

		Debug.Log (position);
		GameObject playerCharacter = GameObject.Find ("character" + plane[0][position]);
		GameObject enemCharacter = GameObject.Find ("characterE" + plane[1][position]);

		position++;


		GameObject playerPlatform = GameObject.Find ("platform" + position + "A");
		GameObject enemPlatform = GameObject.Find ("platform" + position + "B");

		yield return new WaitForSeconds(2+((position-1)*1));


		if(mode==0)
		{
			while(playerPlatform.transform.position.z<297)
			{
				playerPlatform.transform.position=new Vector3	(playerPlatform.transform.position.x,playerPlatform.transform.position.y,playerPlatform.transform.position.z+0.5f);
				playerCharacter.transform.position=new Vector3	(playerCharacter.transform.position.x,playerCharacter.transform.position.y,playerCharacter.transform.position.z+0.5f);

				enemPlatform.transform.position=new Vector3	(enemPlatform.transform.position.x,enemPlatform.transform.position.y,enemPlatform.transform.position.z-0.5f);
				enemCharacter.transform.position=new Vector3(enemCharacter.transform.position.x,enemCharacter.transform.position.y,enemCharacter.transform.position.z-0.5f);

				yield return new WaitForSeconds(0.02f);
			}
		}

		if(mode==1)
		{
			int temp=0;
			while(playerPlatform.transform.position.z<297)
			{
				playerPlatform.transform.position=new Vector3	(playerPlatform.transform.position.x,playerPlatform.transform.position.y,playerPlatform.transform.position.z+0.5f);
				playerCharacter.transform.position=new Vector3	(playerCharacter.transform.position.x,playerCharacter.transform.position.y,playerCharacter.transform.position.z+0.5f);

				temp++;
				if(temp==15)
				{
					Destroy(enemCharacter);
				}

				yield return new WaitForSeconds(0.02f);
			}
			yield return new WaitForSeconds(0.5f);
			Destroy(playerCharacter);
			while(playerPlatform.transform.position.z>260)
			{
				playerPlatform.transform.position=new Vector3	(playerPlatform.transform.position.x,playerPlatform.transform.position.y,playerPlatform.transform.position.z-0.5f);
				
				yield return new WaitForSeconds(0.02f);
			}
		}
		if(mode==2)
		{
			int temp=0;
			while(enemPlatform.transform.position.z>260)
			{
				enemPlatform.transform.position=new Vector3	(enemPlatform.transform.position.x,enemPlatform.transform.position.y,enemPlatform.transform.position.z-0.5f);
				enemCharacter.transform.position=new Vector3	(enemCharacter.transform.position.x,enemCharacter.transform.position.y,enemCharacter.transform.position.z-0.5f);
				
				temp++;
				if(temp==15)
				{
					Destroy(playerCharacter);
				}
				
				yield return new WaitForSeconds(0.02f);
			}
			yield return new WaitForSeconds(0.5f);
			Destroy(enemCharacter);
			while(enemPlatform.transform.position.z<297)
			{
				enemPlatform.transform.position=new Vector3	(enemPlatform.transform.position.x,enemPlatform.transform.position.y,enemPlatform.transform.position.z+0.5f);
				
				yield return new WaitForSeconds(0.02f);
			}
		}
		if(mode==3 && playerCharacter)
		{
			while(playerPlatform.transform.position.z<278)
			{
				playerPlatform.transform.position=new Vector3	(playerPlatform.transform.position.x,playerPlatform.transform.position.y,playerPlatform.transform.position.z+0.5f);
				playerCharacter.transform.position=new Vector3	(playerCharacter.transform.position.x,playerCharacter.transform.position.y,playerCharacter.transform.position.z+0.5f);
				
				enemPlatform.transform.position=new Vector3	(enemPlatform.transform.position.x,enemPlatform.transform.position.y,enemPlatform.transform.position.z-0.5f);
				enemCharacter.transform.position=new Vector3(enemCharacter.transform.position.x,enemCharacter.transform.position.y,enemCharacter.transform.position.z-0.5f);
				
				yield return new WaitForSeconds(0.02f);
			}
			while(playerPlatform.transform.position.z>260)
			{

				playerPlatform.transform.position=new Vector3	(playerPlatform.transform.position.x,playerPlatform.transform.position.y,playerPlatform.transform.position.z-0.5f);
//				Debug.Log("ee "+playerCharacter.name);
				playerCharacter.transform.position=new Vector3(playerCharacter.transform.position.x,playerCharacter.transform.position.y,playerCharacter.transform.position.z-0.5f);
				
				enemPlatform.transform.position=new Vector3	(enemPlatform.transform.position.x,enemPlatform.transform.position.y,enemPlatform.transform.position.z+0.5f);
				enemCharacter.transform.position=new Vector3(enemCharacter.transform.position.x,enemCharacter.transform.position.y,enemCharacter.transform.position.z+0.5f);
				
				yield return new WaitForSeconds(0.02f);
			}
		}
		if(position==4)
			canStartOver = 0;
	}

	void startBattle()
	{
		for(int i=1; i<=GameObject.Find ("AllModels").transform.childCount; i++)
			GameObject.Find ("AllModels").transform.GetChild(i-1).transform.GetChild(0).GetComponent<MeshFilter> ().mesh = null;

		int score = 0;
		//if(beginning==0)
		//{
			for(int i=1; i<=5; i++)
			{
			//	Debug.Log(characterTookPlatform[i]);
				if(characterTookPlatform[i]>0)
					plane[0][characterTookPlatform[i]-1]=i;


			//	Debug.Log(colorsOfFight[0][i]+" vs "+colorsOfFight[1][i]);
			}
	//	}

		for(int i=0; i<5; i++)
		{
//			Debug.Log(plane[0][i]+" vs "+plane[1][i]);

			score=calculateBattle (plane[0][i],plane[1][i]);
			if(score==0)
			{
	//			Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = exchange");
				points[0]++;
				points[1]++;

				bannedPosition[i]=1;

				lives[0]--;
				lives[1]--;

				StartCoroutine(animationOfBattle(0,i));

				GameObject.Find("characterE"+plane[1][i]).transform.parent=null;

			//	GameObject.Find("character"+plane[0][i]).transform.position = new Vector3(GameObject.Find("character"+plane[0][i]).transform.position.x,GameObject.Find("character"+plane[0][i]).transform.position.y+10,GameObject.Find("character"+plane[0][i]).transform.position.z);
			//	GameObject.Find("characterE"+plane[1][i]).transform.position = new Vector3(GameObject.Find("characterE"+plane[1][i]).transform.position.x,GameObject.Find("characterE"+plane[1][i]).transform.position.y+10,GameObject.Find("characterE"+plane[1][i]).transform.position.z);
				GameObject.Find("character"+plane[0][i]).GetComponent<BoxCollider>().enabled=false;
				GameObject.Find("characterE"+plane[1][i]).GetComponent<BoxCollider>().enabled=false;
				//Destroy(GameObject.Find("characterE"+plane[1][i]));
				characterTookPlatform[plane[0][i]]=0;
				plane[0][i]=0;
				plane[1][i]=0;
				alreadyTakenPlatform[i]=null;
			}
			if(score==1)
			{
//				Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = win");
				points[0]++;
				lives[1]--;
				lives[0]--;

				StartCoroutine(animationOfBattle(1,i));

				//Destroy(GameObject.Find("characterE"+plane[1][i]));
				//Destroy(GameObject.Find("character"+plane[0][i]));
				characterTookPlatform[plane[0][i]]=0;
				//GameObject.Find("character"+plane[0][i]).transform.position = new Vector3(GameObject.Find("character"+plane[0][i]).transform.position.x,GameObject.Find("character"+plane[0][i]).transform.position.y+10,GameObject.Find("character"+plane[0][i]).transform.position.z);
				//GameObject.Find("character"+plane[0][i]).GetComponent<BoxCollider>().enabled=false;
				plane[0][i]=0;
				plane[1][i]=0;

			}
			if(score==2)
			{
		//		Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = lose");
				points[1]++;
				lives[0]--;
				lives[1]--;

				StartCoroutine(animationOfBattle(2,i));
			//	Destroy(GameObject.Find("character"+plane[0][i]));
			//	Destroy(GameObject.Find("characterE"+plane[1][i]));
				//GameObject.Find("characterE"+plane[1][i]).transform.position = new Vector3(GameObject.Find("characterE"+plane[1][i]).transform.position.x,GameObject.Find("characterE"+plane[1][i]).transform.position.y+10,GameObject.Find("characterE"+plane[1][i]).transform.position.z);
				//GameObject.Find("characterE"+plane[1][i]).GetComponent<BoxCollider>().enabled=false;

				characterTookPlatform[plane[0][i]]=0;
				alreadyTakenPlatform[i]=null;
				plane[0][i]=0;
				plane[1][i]=0;
			}
			if(score==3)
			{
	//			Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = tie");
				StartCoroutine(animationOfBattle(3,i));
			}
			if(score==4)
			{
	//			Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = nodoby");
				StartCoroutine(animationOfBattle(2,i));
				points[1]++;
				lives[1]--;
			}
			if(score==5)
			{
//				Debug.Log(plane[0][i]+" vs "+plane[1][i]+" = nodoby");
				StartCoroutine(animationOfBattle(1,i));
				points[0]++;
				lives[0]--;
			}
		}	
		StartCoroutine(pushWall());
		Debug.Log (points [0] + " vs " + points [1]);
		Debug.Log (lives [0] + " vs " + lives [1]);

		GameObject.Find ("score1").GetComponent<Text> ().text = points [0].ToString();
		GameObject.Find ("score2").GetComponent<Text> ().text = points [1].ToString();

		GameObject.Find ("score3").GetComponent<RectTransform> ().localScale = new Vector3 (1,1,1);

		Sprite scoreImg=Resources.Load("win", typeof(Sprite)) as Sprite;;
		if(lives[0]==0 || lives[1]==0)
		{
			if(points[0]>points[1])
			{
				Debug.Log("Win");
				scoreImg = Resources.Load("win", typeof(Sprite)) as Sprite;

			}
			else if(points[0]<points[1])
			{
				Debug.Log("Lose");
				scoreImg = Resources.Load("lose", typeof(Sprite)) as Sprite;
			}
			else
			{
				if(difficulty%2==1)
				{
					Debug.Log("Win");
					scoreImg = Resources.Load("win", typeof(Sprite)) as Sprite;
				}
				else
				{
					Debug.Log("Lose");
					scoreImg = Resources.Load("lose", typeof(Sprite)) as Sprite;
				}
			}
		}
		else
		{
			Debug.Log("Restart");
			scoreImg = Resources.Load("draw", typeof(Sprite)) as Sprite;
			//GameObject.Find ("score3").GetComponent<Image> ().sprite = scoreImg;

			prepeareRestart=1;
		}
		StartCoroutine(dissapearScore(scoreImg));




	}
	IEnumerator dissapearScore(Sprite toSet) 
	{

		GameObject.Find ("score3").GetComponent<Image> ().sprite = toSet;

		GameObject.Find ("score3").GetComponent<RectTransform> ().localScale = new Vector3 (0,0,0);
		yield return new WaitForSeconds (7);
		GameObject.Find ("score3").GetComponent<RectTransform> ().localScale = new Vector3 (1,1,1);
		yield return new WaitForSeconds (2);
		GameObject.Find ("score3").GetComponent<RectTransform> ().localScale = new Vector3 (0,0,0);
	}

	IEnumerator pushWall() 
	{
		canStartOver = 1;
		Transform theWall = GameObject.Find ("TheWall").transform;
		int angle = 0;
		while(theWall.position.y>-45)
		{
			yield return new WaitForSeconds(0.001f);
			theWall.position = new Vector3 (-12, theWall.position.y-0.5f, 277);
			/*for (int i=0; i<GameObject.Find("AllModels").transform.childCount; i++)
			{

				if(GameObject.Find ("AllModels").transform.GetChild(i).transform.rotation.y>0)
					GameObject.Find ("AllModels").transform.GetChild(i).transform.Rotate(0,3,0,0);
			}*/
			for (int i=0; i<GameObject.Find("AllModels").transform.childCount; i++)
			{
				
				//if(GameObject.Find ("AllModels").transform.GetChild(1).transform.rotation.eulerAngles.y>0)
				if(angle<60)
					GameObject.Find ("AllModels").transform.GetChild(i).transform.Rotate(0,3,0,0);
			}
			angle++;
		//	Debug.Log(GameObject.Find ("AllModels").transform.GetChild(1).transform.rotation.eulerAngles.y);
		}

	}

	int canStartOver=0;
	IEnumerator pushWall2() 
	{
		Transform theWall = GameObject.Find ("TheWall").transform;
		int angle = 0;
		while(theWall.position.y<10)
		{
			yield return new WaitForSeconds(0.001f);
			theWall.position = new Vector3 (-12, theWall.position.y+0.5f, 277);
			for (int i=0; i<GameObject.Find("AllModels").transform.childCount; i++)
			{
				//if(GameObject.Find ("AllModels").transform.GetChild(i).transform.localRotation.eulerAngles.y<270)
				if(angle<60)
					GameObject.Find ("AllModels").transform.GetChild(i).transform.Rotate(0,3,0,0);

			}
			angle++;
//			Debug.Log(GameObject.Find ("AllModels").transform.GetChild(0).transform.rotation.eulerAngles.y);
			//			Debug.Log("WaitAndPrint " + Time.time);
		}
		canStartOver = 0;
	}

	void sendCharacterBack()
	{
		Ray ray = camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		int i=0;
		Physics.Raycast (ray, out hit);
		if(hit.transform.name[0]=='c')
		{
			if(hit.transform.position.z>200)
			{
			//	int temp=int.Parse ((hit.transform.name[hit.transform.name.Length-2]).ToString ());
				int numberOfCharacter=int.Parse(hit.transform.name[hit.transform.name.Length-1].ToString());

				Debug.Log(int.Parse(hit.transform.name[hit.transform.name.Length-1].ToString())-1);
				hit.transform.position=positionStart[numberOfCharacter-1];
				hit.transform.rotation=rotationStart[numberOfCharacter-1];

				alreadyTakenPlatform[characterTookPlatform[numberOfCharacter]]=null;
				characterTookPlatform[numberOfCharacter]=0;
			}
		/*	while(breckets[i]!=1 && i<6)
				i++;


			if(hit.transform.position.z>200)
			{
				Debug.Log(hit.transform.position.z);

				//int temp=int.Parse ((hit.transform.name[hit.transform.name.Length-2]).ToString ());
				int numberOfCharacter=int.Parse(hit.transform.name[hit.transform.name.Length-1].ToString());

				hit.transform.position=positionStart[i-1];
			//	if(numberOfCharacter==5)
			//		hit.transform.position=new Vector3positionStart[i-1];
				hit.transform.rotation=startRotation;

				alreadyTakenPlatform[characterTookPlatform[numberOfCharacter]]=null;
				characterTookPlatform[numberOfCharacter]=0;


				breckets[i]=0;
			}
			*/
		}

	}
	RaycastHit keyCharacter;
	int usedKey=0;
	// Update is called once per frame
	void Update () 
	{
		if(canStartOver==0)
		{
			if(prepeareRestart==0)
			{
				if(Input.GetKeyUp(KeyCode.G))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("character1").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("character1").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.F))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("character2").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("character2").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.D))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("character3").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("character3").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.S))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("character4").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("character4").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.A))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("character5").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("character5").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}

				if(Input.GetKeyUp(KeyCode.Alpha1))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("platform5A").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("platform5A").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.Alpha2))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("platform4A").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("platform4A").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.Alpha3))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("platform3A").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("platform3A").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.Alpha4))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("platform2A").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("platform2A").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}
				if(Input.GetKeyUp(KeyCode.Alpha5))
				{
					Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("platform1A").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("platform1A").transform.position).y+10,0));
					Physics.Raycast (ray, out keyCharacter);
					usedKey=1;
					movingCharacter();
				}

				if(Input.GetKey(KeyCode.Return))
				{
						Ray ray = camera.ScreenPointToRay (new Vector3(camera.WorldToScreenPoint (GameObject.Find("start").transform.position).x+10,camera.WorldToScreenPoint (GameObject.Find("start").transform.position).y+10,0));
						Physics.Raycast (ray, out keyCharacter);
						usedKey=1;
						movingCharacter();
				}
			}
			if(Input.GetKey(KeyCode.Return) && prepeareRestart!=0 && GameObject.Find("TheWall").transform.position.y<-45)
			{
				prepeareRestart=0;
				beginning=1;
				StartCoroutine(pushWall2());
				StartCoroutine(waitForShuffle());
				//shuffleEnemy();
			}
			if(Input.GetMouseButtonDown(0))
			{
				if(prepeareRestart==0 /*&& canStartOver==0*/)
					movingCharacter();
				else if(GameObject.Find("TheWall").transform.position.y<-45)
				{
					prepeareRestart=0;
					beginning=1;
					StartCoroutine(pushWall2());
					StartCoroutine(waitForShuffle());
					//shuffleEnemy();
				}

			}
			if(Input.GetMouseButtonDown(1))
			{

				sendCharacterBack();
			}
		}
		if(Input.GetKey(KeyCode.Escape))
		{
//			Debug.Log("A");
			Application.Quit();
		}
	}
}
