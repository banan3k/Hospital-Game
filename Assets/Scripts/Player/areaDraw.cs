using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

//ADD TO MAIN CAMERA. THIS SCRIPT NEED TO CONNECT WITH COLLIDER SPHERE IN SCENE, THAT HAS UI TEXT COMPONENT. TO SPHERE, ADD SCRIPT CALLED "checkName";
// additional, to script that change turn, exacly in function that is changing turn, you need to add this lines :

/*
	PlayerPrefs.SetInt ("currentPlayer", PlayerPrefs.GetInt ("currentPlayer") + 1);
	if (PlayerPrefs.GetInt ("currentPlayer") > 4)
		PlayerPrefs.SetInt ("currentPlayer", 1);
 */

public class areaDraw : MonoBehaviour {

	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;
	float multiplyScale=10;
	// Use this for initialization

	int whatCheckNumber=0;
	int[] numbersOfHit;

	int howManyOnWall = 0;

	IEnumerator waitToCheck(Vector3 vec, int numberOfWally)
	{


		//Debug.Log ("AAAA"+counter);
		numbersOfHit [whatCheckNumber] = numberOfWally+1;
		whatCheckNumber++;
		yield return new WaitForSeconds (whatCheckNumber*0.05f);
		//Debug.Log ("bbb");
		GameObject.Find ("checkNameArea").transform.position = vec;

		yield return new WaitForSeconds (0.05f);
		GameObject.Find ("checkNameArea").transform.position = new Vector3 (10, 20, 10);
		Text ktoreDotknal = GameObject.Find ("checkNameArea").transform.GetComponent<Text> ();
		ktoreDotknal.text += "\n";
//		Debug.Log (ktoreDotknal.text);
		string temp = ktoreDotknal.text;

	}


	int howManyPuw=0;
	int whereStart=0;
	int[] whereCantStart;

	int thereIsCDoor=0;


	void checkSpawn()
	{
		RaycastHit hit;
		whereCantStart = new int[5] {0,0,0,0,0};
		//whereStart [0] = 0;
		Vector3 p1 = transform.position;
		p1.z += 1;
		if (Physics.SphereCast(p1, 1, transform.right, out hit, 13)) 
		{
			whereCantStart[0]=1;
		}
		if (Physics.SphereCast(p1, 1, transform.forward, out hit, 13)) 
		{
			whereCantStart[1]=1;
		}
		if (Physics.SphereCast(p1, 1, transform.right*-1, out hit, 13)) 
		{
			whereCantStart[2]=1;
		}
		if (Physics.SphereCast(p1, 1, transform.forward*-1, out hit, 13)) 
		{
			whereCantStart[3]=1;
		}

		//float distanceToObstacle = 0;

		if (Physics.SphereCast(p1, 1, transform.right, out hit, 10)) 
		{
			whereStart=1;
			if (Physics.SphereCast(p1, 1, transform.forward, out hit, 10)) 
			{
				whereStart=2;
				if (Physics.SphereCast(p1, 1, transform.right*-1, out hit, 5)) 
				{
					whereStart=3;
					if (Physics.SphereCast(p1, 1, transform.forward*-1, out hit, 5)) 
					{
						whereStart=4;
					}
				}
			}
			//distanceToObstacle = hit.distance;

		}
//		Debug.Log("where start: "+whereStart);
	}

	IEnumerator destroyWaste()
	{
		yield return new WaitForSeconds (0.5f);

		int i=0, i2=0, i3=0;
		
//		Debug.Log ("q "+whatQuater [0]);

		Text ktoreDotknal = GameObject.Find ("checkNameArea").transform.GetComponent<Text> ();
		string temp = ktoreDotknal.text;
		string[] toCheckArr = temp.Split ('\n');
		string[] doubleCheck;
		string[] whereCheck=new string[100];
		for(i=0; i<toCheckArr.Length; i++)
		{
			doubleCheck=toCheckArr[i].Split('-');
			toCheckArr[i]=doubleCheck[0];
			if(doubleCheck.Length>1)
				whereCheck[i]=doubleCheck[1];
			//Debug.Log(toCheckArr[i]);
		}
		int numberOfHit = 0;
		i3 = 0;

		int theIsPair = 0;

		int i5 = 0;
		while(whatQuater[i5]<=whereStart && whereStart>0)
		{
		//	lineRendererAll[i5].SetVertexCount(0);
			i5++;
		}
		for(i=0; i<howManyOnWall; i++)
		{
			//Debug.Log("what: "+whatQuater[i]+" vs "+whereCantStart[3]+" vs "+i);
			if(whatQuater[i]==1 && whereCantStart[0]==1)
			{
				lineRendererAll[i].SetVertexCount(0);
				howManyPoints[i]=0;
			}
			if(whatQuater[i]==2 && whereCantStart[1]==1)
			{
				lineRendererAll[i].SetVertexCount(0);
				howManyPoints[i]=0;
			}
			if(whatQuater[i]==3 && whereCantStart[2]==1)
			{
				lineRendererAll[i].SetVertexCount(0);
				howManyPoints[i]=0;
			}
			if(whatQuater[i]==4 && whereCantStart[3]==1)
			{
				lineRendererAll[i].SetVertexCount(0);
				howManyPoints[i]=0;
			//	Debug.Log("A");
			}
		}

		if (whereStart > 0) {
			lineRendererAll [numbersOfHit [howManyOnWall - 1]].SetVertexCount (0);
			howManyPoints[numbersOfHit [howManyOnWall - 1]]=0;
		}
		for(i=i5;/*(int)(size/whereStart)*/ i<toCheckArr.Length-1; i++)
		{
			numberOfHit=i;


			for(i2=i+1; i2<toCheckArr.Length; i2++)
			{
			//	Debug.Log ("aaa "+toCheckArr[i]+" vs "+toCheckArr[i2]);
				if(toCheckArr[i]==toCheckArr[i2] || ((whereCheck[i]!="") && toCheckArr[i]==whereCheck[i2]) || ((whereCheck[i]!="") && whereCheck[i]==toCheckArr[i2]))
				{
					Debug.Log(numbersOfHit[numberOfHit]+" and "+numbersOfHit[numberOfHit+1]+" do usuniecia miedzy "+i+" a "+i2+ " poniewarz "+toCheckArr[i]+" (ew."+whereCheck[i] +") vs "+toCheckArr[i2]);
					theIsPair=1;
					for(i3=0; i3<i2-i; i3++)
					{
						Debug.Log(numberOfHit+" and "+numbersOfHit[numberOfHit]);
						if(whereCantStart[0]==1)
						{
						//	numbersOfHit[numberOfHit]=0;
					//		lineRendererAll[numberOfHit].SetVertexCount(0);
						}
						//if(whereStart==2)
						///{
						//	numbersOfHit[1]=0;
						//	lineRendererAll[1].SetVertexCount(0);
						//}
						//if(numberOfHit>0)

						if(howManyPoints[numbersOfHit[numberOfHit]]==2 && numbersOfHit[numberOfHit]!=howManyOnWall)
						{
							lineRendererAll[numbersOfHit[numberOfHit]].SetVertexCount(0);
							howManyPoints[numbersOfHit[numberOfHit]]=0;
					//		howManyPoints
						/*	lineRendererAll[numbersOfHit[numberOfHit+1]].SetVertexCount(0);
							if(whereCantStart[0]==1)
								lineRendererAll[numbersOfHit[numberOfHit-1]].SetVertexCount(0);*/
							Debug.Log(howManyPoints[numbersOfHit[numberOfHit]]);
						}

						howManyPoints[numbersOfHit[numberOfHit]]=0;
						lineRendererAll[numbersOfHit[numberOfHit]].SetVertexCount(0);
						numberOfHit++;
					}

					i2=toCheckArr.Length;
					i++;//tutaj zmiana
					
				}

			}
			int temp2=0;

			if(theIsPair==0)
			{

				Debug.Log (numbersOfHit[0]+" kf "+numberOfHit);
				for(i2=toCheckArr.Length-1; i2>=0; i2--)
				{
					if(i!=i2 && toCheckArr[i]==toCheckArr[i2] || ((whereCheck[i]!="") && toCheckArr[i]==whereCheck[i2]) || ((whereCheck[i]!="") && whereCheck[i]==toCheckArr[i2]))
					{
						Debug.Log("ratuj "+toCheckArr[i]+" vs "+toCheckArr[i2]);
						temp2=1;
					}
				}
		//		Debug.Log(numbersOfHit[numberOfHit-1]+" there is no pair "+toCheckArr[i-1]);
				//for(int i=0
				if(temp2!=1 && numberOfHit!=whereStart)
				{
					if(numberOfHit>0)
					{
						lineRendererAll[numbersOfHit[numberOfHit-1]].SetVertexCount(0);
						howManyPoints[numbersOfHit[numberOfHit-1]]=0;
					}
					else
					{
						lineRendererAll[numbersOfHit[0]].SetVertexCount(0);
						howManyPoints[numbersOfHit[0]]=0;
					}
				}
				temp2=0;

			}
			theIsPair=0;
		}

		checkIfIsOkey ();

	}

	int howManyChecked=1;
	void chackCollDoor()
	{
		RaycastHit hit;
		int i = 1;

		Vector3 p1 = GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position;	
		//while(GameObject.Find("door"+i))
	//	{
		p1 = GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position;	
		
			if (Physics.SphereCast(p1, 1, GameObject.Find("door"+howManyChecked).transform.position, out hit, 5)) 
			{
				thereIsCDoor=1;
			Debug.Log("hadfsdfsffffffffffffffffffffffffffffffffff"+" vs "+GameObject.Find("door"+howManyChecked).transform.name);
			if(whatSideX[howManyChecked]==1)
				whatSideX[howManyChecked]=2;
			if(whatSideX[howManyChecked]==2)
				whatSideX[howManyChecked]=1;
			}
	//	howManyChecked++;
		//}
		howManyChecked++;

	}

	float theta_scale = 0.1f;  
	int[] howManyPoints = new int[1000];
	LineRenderer[] lineRendererAll = new LineRenderer[1000];
	LineRenderer[] lineRendererAll2 = new LineRenderer[1000];
	IEnumerator destroyWasteD()
	{
		yield return new WaitForSeconds (0.5f);
		int i = 0;
	//	Debug.Log (whatQuater[1]);
		chackCollDoor ();


		for(i=0; i<=howManyOnWall; i++)
		{
//			Debug.Log (whatSideX[howManyChecked-1]+" vs "+whatQuater[i]+" vs "+i);
			if(whatSideDoor[howManyChecked-1]==2)
			{
				if(whatSideX[howManyChecked-1]==1 && (whatQuater[i]==4 || whatQuater[i]==1))
				{
					lineRendererAll[i].SetVertexCount(0);
					howManyPoints[i]=0;
				}
				if(whatSideX[howManyChecked-1]==2 && (whatQuater[i]==2 || whatQuater[i]==3))
				{
					lineRendererAll[i].SetVertexCount(0);
					howManyPoints[i]=0;
	//				Debug.Log(i);
				}
			}
			if(whatSideDoor[howManyChecked-1]==1)
			{
				if(whatSideY[howManyChecked-1]==1 && (whatQuater[i]==2 || whatQuater[i]==1))
				{
					lineRendererAll[i].SetVertexCount(0);
					howManyPoints[i]=0;
				}
				if(whatSideY[howManyChecked-1]==2 && (whatQuater[i]==4 || whatQuater[i]==3))
				{
					lineRendererAll[i].SetVertexCount(0);
					howManyPoints[i]=0;
					//				Debug.Log(i);
				}
			}

		}

	}
	int repaint=0;
	void checkIfIsOkey()
	{
		int i=0;
		for(i=0; i<howManyOnWall; i++)
		{
			if(howManyPoints[i]>1)
			{
//				Debug.Log("jest!");
				repaint=1;
			}
		}
		if(repaint==0)
		{
	//		drawCircle(GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")), 5, 1,0,1);
		}
		repaint = 0;
	}

	IEnumerator waitForDoors(GameObject A, int i)
	{
		yield return new WaitForSeconds (i*1.0f);
		drawCircle (A,(int)((13-differenceX)/1.5f),0,1,1);
	}
	int[] whatSideX, whatSideY;
	int differenceX, differenceZ;
	int[] whatSideDoor;
	void checkDoors()
	{
		int i = 1;
		while(GameObject.Find("door"+i))
		{
			Debug.Log("kolejne drzwi "+i);
			if(GameObject.Find("door"+i).transform.GetComponent<MeshRenderer>().receiveShadows==true)
			{
				Debug.Log("horizontal");
				whatSideDoor[i]=1;
			}
			else
			{
				Debug.Log("vertical");
				whatSideDoor[i]=2;
			}


			Vector3 posDoor = GameObject.Find("door"+i).transform.position;
			differenceX = (int)(posDoor.x - this.transform.position.x);
			differenceZ = (int)(posDoor.z - this.transform.position.z);

			if (differenceX < 0) 
			{
				differenceX *= -1;
				whatSideX[i] = 1;
				Debug.Log (differenceX + " vs" + differenceZ);
			} else
				whatSideX[i] = 2;
			if (differenceZ < 0) {
				differenceZ *= -1;
				whatSideY[i] = 1;
			} else
				whatSideY[i] = 2;

			if(differenceX<13 && differenceZ<13)
				StartCoroutine(waitForDoors (GameObject.Find("door"+i),i));

			i++;
		}

	}

	void drawCircle(GameObject ToWhat, int r, int mode, int isDoor, int color)
	{
		Text ktoreDotknal = GameObject.Find("checkNameArea").transform.GetComponent<Text> ();
		ktoreDotknal.text = "";
		numbersOfHit = new int[100];
		whatQuater = new int[100];

		howManyOnWall = 0;
		//int r = 2;
		float x = -1.5f, y = -1.5f;
		multiplyScale = r;
		//if(recentScore==0)
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		if(color==1)
			lineRenderer.SetColors(Color.green, Color.red);
		else
			lineRenderer.SetColors(Color.green, Color.green);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(size+1);
		lineRenderer.useWorldSpace = false;
		lineRenderer.material = GameObject.Find("Player1").transform.GetChild(1).GetComponent<MeshRenderer> ().material;

		if(color==1)
			lineRenderer.material.color = Color.red;
		else
			lineRenderer.material.color = Color.green;
	//	lineRenderer.material.color = Color.red;
		int i = 0;
		lineRenderer.enabled = false;
		
		GameObject[] newPartOfLine = new GameObject[100];
		int i3 = 0;
		for(i3=0; i3<100; i3++)
		{
			whatQuater[i3]=0;
		}
		for(i3=0; i3<100; i3++)
		{
			newPartOfLine[i3]= new GameObject ();
			newPartOfLine[i3].transform.position=ToWhat.transform.position;
			newPartOfLine[i3].transform.parent = ToWhat.transform;
			lineRendererAll[i3]= newPartOfLine[i3].AddComponent<LineRenderer>();
			lineRendererAll[i3].SetVertexCount(0);
			lineRendererAll[i3].SetWidth (0.2f,0.2f);
			lineRendererAll[i3].material = lineRenderer.material;
			lineRendererAll[i3].useWorldSpace = false;
		}
		lineRendererAll[0].SetVertexCount(1);
		Vector3 pos = new Vector3 (x, 0.1f, y);
		lineRendererAll[0].SetPosition(0, pos);
		
		
		
		
		
		
		
		int isOnWall = 0;
		int i2=0;
		int scalarValue = size/4;
		
		
		int vertexNumber = 0;
		int counter = 0;
		
		
		float lastPositionX = 0, lastPositionY=0;
		
		
		i = 0;
		
		for(float theta = 0f; theta <=0.1f+ (2*Mathf.PI); theta += theta_scale)
		{
			
			i++;
			//	Debug.Log(i);

			x = (3) * Mathf.Cos(theta);
			y = (3) * Mathf.Sin(theta);
			
			//	Debug.Log(x+" : "+y);
			// Set the position of this point
			Vector3 pos2 = new Vector3(x, y, 1);
			
			Vector3 spawnPos = new Vector3(x*multiplyScale,1.5f,y*multiplyScale);
			spawnPos.x+=ToWhat.transform.position.x;
			spawnPos.z+=ToWhat.transform.position.z;
			
			howManyPoints[howManyOnWall]=vertexNumber;					

			//if(whatQuater[howManyOnWall]==0)
			//{
			if(theta<(0.1f+ (2*Mathf.PI))/4)
			{
				whatQuater[howManyOnWall]=1;
			}
			else if(theta<(0.1f+ (2*Mathf.PI))/2)
				{
				whatQuater[howManyOnWall]=2;

					whatQuater[howManyOnWall]=2;
			//		Debug.Log("cccccccccccccccccccccccccccccccccccc"+i+" vs "+howManyOnWall);
				}
			else if(theta<(int)((0.1f+ (2*Mathf.PI))/1.33f))
				{
				whatQuater[howManyOnWall]=3;
			//		Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb"+i+" vs "+howManyOnWall);
				}
			else
				{
					whatQuater[howManyOnWall]=4;
				//	Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+i+" vs "+howManyOnWall);
				}
		//	}
			if (Physics.CheckSphere (spawnPos, 1) && counter>3) 
			{
			//	Debug.Log("hit");
		//		StartCoroutine(waitToCheck(spawnPos, howManyOnWall));
				pos = new Vector3(x*multiplyScale, 0.1f, y*multiplyScale);
				lineRendererAll[howManyOnWall].SetPosition(counter, pos);
				



				howManyOnWall++;
				vertexNumber=0;
				
				
				
				counter=0;
				
			} else 
			{
//				Debug.Log("nothing");
				
			}
			//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			//cube.transform.position = spawnPos;
			
			
			
			
			
			vertexNumber++;
			
			//	Debug.Log(howManyOnWall);
			
			lineRendererAll[howManyOnWall].SetVertexCount(vertexNumber+1);
			pos = new Vector3(x*multiplyScale, 0.1f, y*multiplyScale);
			lineRendererAll[howManyOnWall].SetPosition(counter, pos);
			
			counter++;
			
		}
		//	Debug.Log (howManyOnWall);
		//whatQuater[howManyOnWall-1]=4;
		lineRendererAll [howManyOnWall].SetVertexCount (vertexNumber);
		if(mode==0)
		{
			if(isDoor==0)
				StartCoroutine (destroyWaste ());
			if(isDoor==1)
				StartCoroutine (destroyWasteD ());
		}
		for(i=0; i<=howManyOnWall; i++)
		{
			newPartOfLine[i].transform.parent=null;
		}
		if(howManyOnWall>0)
		{
			for(i=howManyOnWall+1; i<100; i++)
			{
				Destroy(newPartOfLine[i]);
			}
		}
	}
	LineRenderer lineRenderer;
	int[] whatQuater;
	public int size = 5;


	void Update () 
	{
	
		/*if(GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetComponent<movementAction>().stop==1) //example of checking if action points are out
		{
			Debug.Log("break action");
		}*/


		
	}
	IEnumerator test(GameObject gameOb)
	{
		yield return new WaitForSeconds (0.5f);
		drawCircle (gameOb,(int)(size/1.5f),1,0,2);
	}
	int recentScore;
	void Start () 
	{
		whatSideX = new int[100];
		whatSideY = new int[100];
		whatSideDoor = new int[100];


		lineRenderer = gameObject.AddComponent<LineRenderer>();

		if (PlayerPrefs.GetInt ("currentPlayer") == 0)
			PlayerPrefs.SetInt ("currentPlayer", 1);




		recentScore = PlayerPrefs.GetInt ("recentResault"); //getting data from game before mini game
		if (recentScore > 0) // checking if this is start game or load after mini game
		{
		//	GameObject cube = new GameObject();
		//	cube.transform.position = new Vector3(PlayerPrefs.GetFloat("recentPlayerPositionX"),PlayerPrefs.GetFloat("recentPlayerPositionY"),PlayerPrefs.GetFloat("recentPlayerPositionZ"));
		//	drawCircle (cube,size,0,0,1);
		//	Destroy(cube);
		//	StartCoroutine (test (cube));
		}
		else
		{
			checkDoors ();
			checkSpawn ();
//			Debug.Log(PlayerPrefs.GetInt("currentPlayer"));
			drawCircle (GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")),size,0,0,1);
			StartCoroutine (test (GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer"))));
		}
		
	

	
	}



}
