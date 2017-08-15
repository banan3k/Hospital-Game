using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

//add this to sphere with collider and ui text. this script help areadraw.cs

public class checkName : MonoBehaviour {

	// Use this for initialization

	//add to sphere with collider and name it "checkNameArea"
	Text ktoreDotknal;
	void Start () 
	{
		ktoreDotknal = this.transform.GetComponent<Text> ();
		ktoreDotknal.text = "";
	}

	int firstNo=0;
	void OnTriggerEnter(Collider other)
	{

//		Debug.Log ("stick to : "+other.transform.name);
		//if(other.transform.name!="checkNameArea" && other.transform.name!="checkNameArea2" && firstNo>0)
		if(other.transform.name[0]=='C')
			ktoreDotknal.text += other.transform.name+"-";

		firstNo++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
