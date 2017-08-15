using UnityEngine;
using System.Collections;

//ADD TO MAIN CAMERA

public class battery : MonoBehaviour {

	// Use this for initialization

	Font font;
	public int howManyBattery=5;
	int getIt=0;
	GameObject batteryItem;

	void Start () 
	{
		for(int i=0; i<howManyBattery; i++)
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
					batteryItem = Instantiate(Resources.Load("models/battery")) as GameObject;
					//Mesh mesh = Resources.Load("/models/item1") as Mesh;
					batteryItem.transform.position=new Vector3(rand,1.5f,rand2);
					batteryItem.transform.localScale = 0.65f*batteryItem.transform.localScale;
					batteryItem.name="battery";
					batteryItem.tag="battery";
					batteryItem.AddComponent<BoxCollider>();
					batteryItem.GetComponent<BoxCollider>().size = new Vector3(4,4,4);
					getIt=1;
				}
			}
			getIt=0;
		}


		//drawBattery ();
		//PlayerPrefs.SetInt ("currentPlayer", 1);

		batteryFont = new GUIStyle();
		batteryFont.fontSize=25;
		font=(Font)Resources.Load("tf2build", typeof(Font));
		batteryFont.font = font;
		savePlayerPositionOnStart ();


	}

	void savePlayerPositionOnStart()
	{
//		Debug.Log (PlayerPrefs.GetInt ("currentPlayer"));
		PlayerPrefs.SetFloat ("recentPlayerPositionX", GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position.x);
		PlayerPrefs.SetFloat ("recentPlayerPositionY", GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position.y);
		PlayerPrefs.SetFloat ("recentPlayerPositionZ", GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position.z);
	}

	void addBattery(int idPlayer)
	{
		PlayerPrefs.SetInt ("batteryPlayer" + idPlayer, 1);
		PlayerPrefs.SetInt("howManyBattery"+idPlayer,PlayerPrefs.GetInt("howManyBattery"+idPlayer)+1);
	}
	bool checkIfIsBattery(int idPlayer)
	{
		if (PlayerPrefs.GetInt ("batteryPlayer" + idPlayer) > 0)
			return true;
		else
			return false;
	}

	GUIStyle batteryFont;
	void OnGUI() 
	{
        GUI.Label(new Rect(20, 100, 100, 20), PlayerPrefs.GetInt ("batteryPlayer" + PlayerPrefs.GetInt("currentPlayer")) + " battery", batteryFont);
	}
	// Update is called once per frame
	void Update () 
	{
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("battery"))
		{
			fooObj.transform.Rotate(0,50*Time.deltaTime,0);
		}

		if (Input.GetMouseButtonDown(0))
		{ 
			
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				int distance=(int)Vector3.Distance (hit.transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position);
				if(hit.transform.name=="battery")
				{
					if(distance<2)
					{
						addBattery(PlayerPrefs.GetInt("currentPlayer"));
						Destroy(hit.transform.gameObject);
					}

				}

			}

		}
	}
}
