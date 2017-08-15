using UnityEngine;
using System.Collections;


//ADD TO MAIN CAMERA

public class missions : MonoBehaviour {


	Font font;
	GUIStyle fonty;
	public GUIStyle style;
	GUIStyle currentStyle;

	IEnumerator waitForPatient()
	{
		yield return new WaitForSeconds (0.5f);
		doStuff ();
	}

	void doStuff()
	{
		patientS = new string[100];
		for(int i=1; i<=howManyPacients; i++)
		{
			//			Debug.Log("patient"+i);
			if(GameObject.Find("PatientAndBed ("+i+")"))
			{
				patientS[i-1]=GameObject.Find("PatientAndBed ("+i+")").GetComponentInChildren<PatientStory>().patientName;
			}
		}
		
		randomPacientForMission ();
		

	}

	// Use this for initialization
	void Start () 
	{
//		howManyPacients = GameObject.Find ("Main Camera").GetComponent<spawnPacient> ().howManyPacients;

		font=(Font)Resources.Load("tf2build", typeof(Font));
		fonty = new GUIStyle();
		fonty.font = font;

	//	fonty.border = 

		turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
		activePlayer = turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>();

		StartCoroutine (waitForPatient ());

	}

	public string[] patientS;

	public int[][][] chosenP;
	int[][] doneMission;
	string[][] missionText;
	void randomPacientForMission()
	{
		missionText = new string[100][];
		chosenP = new int[100][][];
		doneMission = new int[100][];
		howManyPacients = 6;
		for(int i=0; i<5; i++)
		{
			missionText[i]=new string[100];
			chosenP[i]=new int[100][];
			doneMission[i] = new int[100];
			for(int i2=0; i2<10; i2++)
			{
				//missionText[i]=new string[100];
				chosenP[i][i2]=new int[100];
				//doneMission[i] = new int[100];
			}
		}
		for(int i=0; i<5; i++)
		{
			int rand = Random.Range (0, howManyPacients);
//			Debug.Log(rand);
			missionText [i][0] = "First, help  pacient " + patientS[rand];
			chosenP [i][0][0] = rand;

			rand = Random.Range (0, howManyPacients);
			chosenP [i][1][0] = rand;

			rand = Random.Range (0, howManyPacients);
			while(rand==chosenP[i][1][0])
				rand = Random.Range (0, howManyPacients);
			chosenP [i][1][1] = rand;
			missionText[i][1] = "First, help pacient " + patientS[chosenP[i][1][0]]+" and then help "+patientS[chosenP [i][1][1]];

			rand = Random.Range (0, howManyPacients);
			chosenP [i][2][0] = rand;

			rand = Random.Range (0, howManyPacients);
			while(rand==chosenP[i][2][0])
				rand = Random.Range (0, howManyPacients);
			chosenP [i][2][1] = rand;

			rand = Random.Range (0, howManyPacients);
			while(rand==chosenP[i][2][0] || rand==chosenP[i][2][1])
				rand = Random.Range (0, howManyPacients);
			chosenP [i][2][2] = rand;
			missionText [i][2] = "First, help pacient " + patientS[chosenP[i][2][0]]+"  then help pacient "+patientS[chosenP [i][2][1]]+" and on third place help pacient "+patientS[chosenP[i][2][2]];
		}
	}

	int howManyPacients = 3;

	private TurnHandler turnHandler;
	private PlayerMovement activePlayer;


	int widthOfMissionBox=330;
	int show=0;
	string isOnButton="";
	int temp=0;

	int start=0;
	IEnumerator wai()
	{
		yield return new WaitForSeconds (0.1f);
		start = 1;
	}

	void OnGUI()
	{
		GUI.BeginGroup( new Rect ( 0, 0, Screen.width, Screen.height));

		GUI.skin.button.wordWrap = true;
		GUI.skin.box.padding = new RectOffset (0, 0, 10, 0);
		GUI.skin.button.fontSize = 25;

		style.fontSize = 40;
		if (GUI.Button(new Rect(Screen.width-100-(show*widthOfMissionBox), 200, 50, 50), new GUIContent("M", "m"),style))
		{
			if(show==0)
				show=1;
			else
				show=0;
		}
		style.fontSize = 15;
		isOnButton = GUI.tooltip;
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
		GUI.color = Color.red;

		if(show==1)
		{
			if(doneMission[PlayerPrefs.GetInt("currentPlayer")][0]!=1)
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 200, 50,50), "<color=black><size=25>1.</size></color>", style);
			else
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 200, 50,50), "<color=black><size=25>+</size></color>", style);
			GUI.Box(new Rect(Screen.width-widthOfMissionBox, 200, widthOfMissionBox,50), "<color=black><size=15>"+missionText[PlayerPrefs.GetInt("currentPlayer")][0]+"</size></color>", style);

			if(doneMission[PlayerPrefs.GetInt("currentPlayer")][1]!=1)
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 250, 50,50), "<color=black><size=25>2.</size></color>", style);
			else
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 250, 50,50), "<color=black><size=25>+</size></color>", style);
			GUI.Box(new Rect(Screen.width-widthOfMissionBox, 250, widthOfMissionBox,50), "<color=black><size=15>"+missionText[PlayerPrefs.GetInt("currentPlayer")][1]+"</size></color>", style);

			if(doneMission[PlayerPrefs.GetInt("currentPlayer")][2]!=1)
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 300, 50,70), "<color=black><size=25>3.</size></color>", style);
			else
				GUI.Box(new Rect(Screen.width-50-widthOfMissionBox, 300, 50,70), "<color=black><size=25>+</size></color>", style);
			GUI.Box(new Rect(Screen.width-widthOfMissionBox, 300, widthOfMissionBox,70), "<color=black><size=15>"+missionText[PlayerPrefs.GetInt("currentPlayer")][2]+"</size></color>", style);
		}
		GUI.EndGroup ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    int activePlayer = PlayerPrefs.GetInt("Player");

        if (PlayerPrefs.GetInt("mission1Player" + PlayerPrefs.GetInt("currentPlayer")) == 1)
	    {
            doneMission[activePlayer][0] = 1;
            Players.GetPlayer(activePlayer).AddHistory(missionText[activePlayer][0] + "\n");
        }
        if (PlayerPrefs.GetInt("mission2Player" + PlayerPrefs.GetInt("currentPlayer")) == 2)
	    {
            doneMission[activePlayer][1] = 1;
            Players.GetPlayer(activePlayer).AddHistory(missionText[activePlayer][1] + "\n");
        }
	    if (PlayerPrefs.GetInt("mission3Player" + PlayerPrefs.GetInt("currentPlayer")) == 3)
	    {
            doneMission[activePlayer][2] = 1;
            Players.GetPlayer(activePlayer).AddHistory(missionText[activePlayer][2] + "\n");
        }
	}
}
