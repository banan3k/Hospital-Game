using UnityEngine;
using System.Collections;

//ADD TO MAIN CEMERA. THIS SCRIPT NEEDS GAMEOBJECT CALLED "beds", that has gameobjects "bedX" where X is not important. Every bed is a location, that can we can spawn doctor there.

public class spawnPacient : MonoBehaviour {

	public int howManyPacients=10;
	int[] takenBads;
	int temp=0, num=0;

	void spawn(int number)
	{
		num++;
		//Debug.Log (number);
		Vector3 pos = GameObject.Find ("beds").transform.GetChild (number).transform.position;
		GameObject pacient = GameObject.CreatePrimitive (PrimitiveType.Cube);
		pacient.name = "patient" + num;
		pacient.transform.position = new Vector3(pos.x+1,3,pos.z);
		pacient.AddComponent<BoxCollider>();
		pacient.AddComponent<PatientStory> ();
		pacient.GetComponent<BoxCollider>().size = new Vector3(4,4,4);
	}

	// Use this for initialization
	void Start () 
	{
		takenBads = new int[100];
		for(int i=0; i<howManyPacients; i++)
		{
			while(temp==0)
			{
				int rand = Random.Range(0,30);
				if(takenBads[rand]!=1)
				{
					takenBads[rand]=1;
					spawn (rand);
					temp=1;
				}
			}
			temp=0;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
