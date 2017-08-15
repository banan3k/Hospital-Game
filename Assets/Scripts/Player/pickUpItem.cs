using UnityEngine;
using System.Collections;

//ADD TO MAIN CAMERA


public class pickUpItem : MonoBehaviour {

	// Use this for initialization

	int showItem = 0, showItem2 = 0;


	void useItem1()
	{
		GameObject.Find ("Main Camera").GetComponent<movementAction> ().additionalBonus = 3;
	}

	void useItem2()
	{
		int i = 0;
		while(GameObject.Find("patient"+i))
		{
			int distance=(int)Vector3.Distance (GameObject.Find("patient"+i).transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position);
			if(distance<2 && PlayerPrefs.GetInt("patient"+i)!=-1)
			{
				PlayerPrefs.SetInt("patient"+i, PlayerPrefs.GetInt("patient"+i)+3);
				i=100;
			}
			i++;
		}
	}
	void useItem3()
	{
		int i = 0;
		while(GameObject.Find("patient"+i))
		{
			int distance=(int)Vector3.Distance (GameObject.Find("patient"+i).transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position);
			if(distance<2 && PlayerPrefs.GetInt("patient"+i)!=-1)
			{
				PlayerPrefs.SetInt("isAdditionalAdv", 1);
				i=100;
			}
			i++;
		}
	}

	void useItem4()
	{
		int i = 0;
		while(GameObject.Find("patient"+i))
		{
			int distance=(int)Vector3.Distance (GameObject.Find("patient"+i).transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position);
			if(distance<2 && PlayerPrefs.GetInt("patient"+i)!=-1)
			{
				int isAlife=Random.Range(0,2);
				if(isAlife==0)
					GameObject.Find("Main Camera").GetComponent<LoadingMini>().savePacient(i,PlayerPrefs.GetInt ("currentPlayer"),PlayerPrefs.GetInt("pacientPoints"+i));
				i=100;
			}
			i++;
		}
	}
	void useItem5()
	{
		int i = 0;
		while(GameObject.Find("patient"+i))
		{
			int distance=(int)Vector3.Distance (GameObject.Find("patient"+i).transform.position,GameObject.Find("player").transform.position);
			if(distance<2 && PlayerPrefs.GetInt("patient"+i)==-1)
			{
				PlayerPrefs.SetInt("patient"+i,5);
				PlayerPrefs.SetInt("ressurect"+PlayerPrefs.GetInt("currentPlayer"),PlayerPrefs.GetInt("ressurect"+PlayerPrefs.GetInt("currentPlayer"))+1);
			}
			i++;
		}
	}
	void useItem6()
	{
		int i = 0;
		while(GameObject.Find("patient"+i))
		{
			int distance=(int)Vector3.Distance (GameObject.Find("patient"+i).transform.position,GameObject.Find("player").transform.position);
			if(distance<2 && PlayerPrefs.GetInt("patient"+i)!=-1)
			{

				GameObject.Find("Main Camera").GetComponent<LoadingMini>().savePacient(i,PlayerPrefs.GetInt ("currentPlayer"),PlayerPrefs.GetInt("pacientPoints"+i));
				i=100;
			}
			i++;
		}
	}
	void useItem7()
	{
	//	GameObject.Find("ScriptGuy").GetComponent<charge>().changePowerOfPlayer(PlayerPrefs.GetInt("playerPower"+(PlayerPrefs.GetInt("currentPlayer")+1))+GameObject.Find("ScriptGuy").GetComponent<charge>().power);
		PlayerPrefs.SetInt("stunPlayer",1);
		PlayerPrefs.SetInt ("howManyStune" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyStune" + PlayerPrefs.GetInt("currentPlayer"))+1);

	}
	void useItem8()
	{


				
		GameObject.Find("Main Camera").GetComponent<charge>().changePowerOfPlayer(PlayerPrefs.GetInt("playerPower"+(PlayerPrefs.GetInt("currentPlayer")+1))+GameObject.Find("Main Camera").GetComponent<charge>().power);
		PlayerPrefs.SetInt("playerPower"+(PlayerPrefs.GetInt("currentPlayer")+1),0);


	}

	void useItem9()
	{
		//restart round here
	}

	string[] descriptions;
	int[] possibility;
	int[] bucketOfItems;
	void descriptionOfItem()
	{
		descriptions = new string[100];
		possibility = new int[100];
		bucketOfItems = new int[110];
		descriptions [1] = "Give 3 additional points \nof action";
		descriptions [2] = "Give patient 3 additional \nlifes";
		descriptions [3] = "Give additional adventage \nin mini game";
		descriptions [4] = "Give 50% to save patient";
		descriptions [5] = "Ressurect pacient \n(if possible)";
		descriptions [6] = "Save patient";
		descriptions [7] = "Make next doctorContainer called \nby principle for \none round";
		descriptions [8] = "Steal power from next doctorContainer";
		descriptions [9] = "Give you additional round";
		descriptions [10] = "Do nothing";

		possibility [1] = 15;
		possibility [2] = 15;
		possibility [3] = 15;
		possibility [4] = 10;
		possibility [5] = 10;
		possibility [6] = 5;
		possibility [7] = 10;
		possibility [8] = 10;
		possibility [9] = 5;
		possibility [10] = 5;

		int stack = possibility [1], nextStack = 1;
		for(int i=0; i<100; i++)
		{
			bucketOfItems[i]=nextStack;
			if(i==stack)
			{
				nextStack++;
				stack+=possibility[nextStack];
			}
		}
		while(GameObject.Find("item"))
		{
			int t=Random.Range(0,100);


			if(bucketOfItems[t]==10)
				item =Instantiate(Resources.Load("models/item"+Random.Range(1,9))) as GameObject;
			else
				item = Instantiate(Resources.Load("models/item"+bucketOfItems[t])) as GameObject;
			item.transform.position=GameObject.Find("item").transform.position;
			Destroy(GameObject.Find("item"));
			item.name = "it"+bucketOfItems[t];
			item.transform.localScale = 0.5f*item.transform.localScale;
			item.tag = "item";
			//item.name = "item"+bucketOfItems[t];
			GameObject.Find("item").transform.name+=bucketOfItems[t];
			item.AddComponent<BoxCollider>();
			item.GetComponent<BoxCollider>().size = new Vector3(4,4,4);
//			Debug.Log("rand: "+bucketOfItems[t]);
		}

	}



	void switchingItem(int idI)
	{
		switch(idI)
		{
			case 1:
				useItem1();
				break;
			case 2:
				useItem2();
				break;
			case 3:
				useItem3();
				break;
			case 4:
				useItem4();
				break;
			case 5:
				useItem5();
				break;
			case 6:
				useItem6();
				break;
			case 7:
				useItem7();
				break;
			case 8:
				useItem8();
				break;
			case 9:
				useItem9();
				break;
			case 10:
			//	useItem1();
				break;
		}
	}

	void giveItem(int idItem)
	{
		PlayerPrefs.SetInt("playerBag"+PlayerPrefs.GetInt("currentPlayer"),idItem);
		showItem=1;
		GameObject.Find ("Main Camera").GetComponent<movementAction> ().size -= 2;
		itemText = Resources.Load("textures/item"+idItem) as Texture;
	}
	void useItem()
	{
		int idOfItem = PlayerPrefs.GetInt ("playerBag" + currP);
		PlayerPrefs.SetInt("playerBag"+currP,0);
		Debug.Log ("using! "+idOfItem);
		switchingItem (idOfItem);
		PlayerPrefs.SetInt ("howManyItemsUse" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyItemsUse" + PlayerPrefs.GetInt("currentPlayer"))+1);

	}

	Rect rect;

	int temp=0;
	string isOnButton="";
	void OnGUI() 
	{


		if(showItem==1)
		{
			//GUI.backgroundColor = new Color(GUI.backgroundColor.r,GUI.backgroundColor.g,GUI.backgroundColor.b,0.5f);
		//	guiTexture.color.a
			GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b,1);
			if(showItem2==1)
				GUI.TextField(new Rect (10, 200, 100, 50),descriptions[PlayerPrefs.GetInt("playerBag"+currP)]);
			if(GUI.Button(rect, new GUIContent(itemText, "A")))
			{
				useItem();
				showItem=0;
				showItem2=0;
			}

		//	Debug.Log("dsadas");
		}

		isOnButton = GUI.tooltip;
		//Debug.Log(GUI.tooltip);
		if(isOnButton!="")
		{
			//activePlayer.SetCanWalk(false);

			temp=1;
		}
		else if(temp==1)
		{
			//activePlayer.SetCanWalk(true);
			temp=0;
		}
	}


	
	// Update is called once per frame
	int currP=1;
	Camera camera;
	Texture itemText;
	int getIt=0;
	GameObject item;
	public int howManyItems = 10;

	private TurnHandler turnHandler;
	private PlayerMovement activePlayer;


	void Start () 
	{
		turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
		activePlayer = turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>();

		for(int i=0; i<howManyItems; i++)
		{
			while(getIt==0)
			{
				int rand = Random.Range(-102,85);
				int rand2 = Random.Range(-106,30);
				if(Physics.CheckSphere (new Vector3(rand,2.5f,rand2),2))
				{
					getIt=0;
				}
				else
				{
					int rand3 = Random.Range(0,30);
				//	item = Instantiate(Resources.Load("models/item1")) as GameObject;
					item = new GameObject();
					//Mesh mesh = Resources.Load("/models/item1") as Mesh;
					item.transform.position=new Vector3(rand,1.5f,rand2);
					item.name="item";

					getIt=1;
				}
			}
			getIt=0;
		}

		descriptionOfItem ();
		rect =new Rect(10,100, 100, 100); 

		for(int i=0; i<5; i++)
		{
			PlayerPrefs.SetInt("playerBag"+i,0);
		}
		itemText = Resources.Load("item1") as Texture;
		giveItem (1);
		

	}

	Vector2 mouse;

	int c=1;
	void Update () 
	{
		//for(int i=1; i<10; i++)
		//{
		foreach(GameObject fooObj in GameObject.FindGameObjectsWithTag("item"))
		{
			fooObj.transform.Rotate(0,50*Time.deltaTime,0);
		}
		
		//}
		if(PlayerPrefs.GetInt("currentPlayer")!=currP)
		{
			currP=PlayerPrefs.GetInt("currentPlayer");
			if(PlayerPrefs.GetInt("playerBag"+currP)>0)
				showItem=1;
		}

		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		
		if (rect.Contains (mouse) && showItem == 1) {
			showItem2 = 1;
		} else
			showItem2 = 0;

		if (Input.GetMouseButtonDown(0))
		{ 
		//	Debug.Log(GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
			//	Debug.Log("BBBB"+hit.transform.position);
				int distance=(int)Vector3.Distance (hit.transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position);
				if(hit.transform.name.Length>1 && hit.transform.name.Substring(0,2)=="it")
				{
					if(distance<3)
					{
						Destroy(hit.transform.gameObject);
						giveItem(int.Parse(hit.transform.name.Substring(2,hit.transform.name.Length-2)));
					}

				}
			}
		}

	
	}
}
