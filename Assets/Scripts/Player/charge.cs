using UnityEngine;
using System.Collections;

//ADD TO MAIN CAMERA

public class charge : MonoBehaviour {


	//attached this to ScriptGuy 

	public int maxOfPower = 100;
	public int power=100;
	public int costOfUse=20;
	public int costOfUseHandy=20;
	public float scaleOfWidth=1;

	GameObject chargeLook;

	float height=1.0f;

	// Use this for initialization
	void Start () 
	{
		if(!GameObject.Find("charge"))
		{
//			Debug.Log("there is no charge yet");
			chargeLook = GameObject.CreatePrimitive(PrimitiveType.Cube);
			chargeLook.name="charge";
			chargeLook.transform.rotation= new Quaternion(0,0,0,0);
			chargeLook.transform.parent=GameObject.Find("Main Camera").transform;
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width*0.025f, (float)Screen.height*0.9f, 10));

			chargeLook.transform.localScale=new Vector3(1,height,1.55f);
			chargeLook.transform.position=worldPos;

			Texture2D text = new Texture2D(2, 2, TextureFormat.ARGB32, false);
			text.SetPixel(0, 0, Color.red);
			text.SetPixel(1, 0, Color.red);
			text.SetPixel(0, 1, Color.red);
			text.SetPixel(1, 1, Color.red);
			text.Apply();
			
			// connect texture to material of GameObject this script is attached to
			chargeLook.GetComponent<MeshRenderer>().material.mainTexture = text;

			for(int i=0; i<5; i++)
			PlayerPrefs.SetInt("playerPower"+i,100);

			//testing
			/*changePowerOfPlayer(50);
			changePowerOfPlayer(100);
			changePowerOfPlayer(20); */
		//	chargeLook.transform.LookAt(GameObject.Find("Main Camera").transform);
		}

	}

	public void changePowerOfPlayer(int whatPower)
	{
		power = whatPower;
		drawCharge ();
	}
	int isDoingDrawing=0;
	IEnumerator drawing(float whatSize)
	{
		isDoingDrawing = 1;
		if(chargeLook.transform.localScale.x>whatSize)
		{
			while(chargeLook.transform.localScale.x>whatSize)
			{
				yield return new WaitForSeconds (0.01f);
				chargeLook.transform.localScale=new Vector3(chargeLook.transform.localScale.x-0.025f,height,0.05f);
				chargeLook.transform.position=new Vector3(chargeLook.transform.position.x-0.0125f,chargeLook.transform.position.y,chargeLook.transform.position.z);
			}
			isDoingDrawing = 0;
		}
		else
		{
			while(chargeLook.transform.localScale.x<whatSize)
			{
				yield return new WaitForSeconds (0.01f);
				chargeLook.transform.localScale=new Vector3(chargeLook.transform.localScale.x+0.025f,height,0.05f);
				chargeLook.transform.position=new Vector3(chargeLook.transform.position.x+0.0125f,chargeLook.transform.position.y,chargeLook.transform.position.z);
			}
			isDoingDrawing = 0;

		}

		//chargeLook.transform.LookAt(GameObject.Find("Main Camera").transform);
	}
	IEnumerator queDraw(float whatSize)
	{

		if(isDoingDrawing==0)
			StartCoroutine(drawing (whatSize));
		else
		{
			while(isDoingDrawing==1)
				yield return new WaitForSeconds (1);
			StartCoroutine(drawing (whatSize));
		}
	}

	public void drawCharge()
	{
		float whatSize = ((float)power / 100)*scaleOfWidth;
	//	Debug.Log (whatSize+" vs "+scaleOfWidth);
		StartCoroutine(queDraw (whatSize));
	}
	public bool useChargeCannon(int mode /*0 for normal, 1 for handy one*/)
	{
		if((power>=costOfUse && mode==0) || (power>=costOfUseHandy && mode==1))
		{
			if(mode==0)
				power-=costOfUse;
			else
				power-=costOfUseHandy;
			drawCharge();
			return true;
		}
		else
			return false;
	}
	public void getCharge(int howMuch)
	{
		power += howMuch;
		if (power > maxOfPower)
			power = maxOfPower;
		drawCharge();
	}

	// Update is called once per frame
	void Update () 
	{
		chargeLook.transform.localRotation= new Quaternion(0,0,0,0);
	}
}
