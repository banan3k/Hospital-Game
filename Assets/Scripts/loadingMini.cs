using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class LoadingMini : MonoBehaviour {

	//call function loadMiniGame from 'Script Guy' with number of the game (from 0) and number of pacient that we curing
	//for example: GameObject.Find ("ScriptGuy").GetComponent<loadingMini> ().loadMiniGame (0, 2);	

	//the kill or save pacient function is calling automatic after load saved scene. There is space for NextRound() and EndGame() function.

	//Mini game should end with this two lines :
	//PlayerPrefs.SetInt("recentResault",resault); where resault is 1 (win) or 2 (lose) - 0 is for new game
	//Application.LoadLevel ("testN"); load level that is saved in this script
	int idPatient=0;

    private List<string> minigameList = new List<string>
    {
        "MiniGame2",
        "Minigame3"
    };

	void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{ 
			
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				int distance=(int)Vector3.Distance (hit.transform.position,GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position);

				if(hit.transform.name.Length>7 && hit.transform.name.Substring(0,7)=="patient")
				{
					idPatient = int.Parse(hit.transform.name.Substring(7,hit.transform.name.Length-7));
					if(PlayerPrefs.GetInt("patient"+idPatient)!=-1)
					{

						if(distance<2)
						{
							loadMiniGame(0,idPatient);
						}
					}
				}
			}
		}
		if(Input.GetMouseButton(1))
			GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.position = new Vector3 (0,50, 0);*/
	}

	void Start()
	{
		if(PlayerPrefs.GetInt("currentPlayer")==0)
		{
			PlayerPrefs.SetInt("currentPlayer",1);
		}

		loadDataToGame ();
	}

	void loadNewSceneToGame() //this function is about adding saved scane to game build, that can be load after mini game
	{
		#if UNITY_EDITOR
		EditorBuildSettingsScene[] newBuild = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length +1]; 
		EditorBuildSettingsScene saveScene = new EditorBuildSettingsScene("Assets/testN.unity", true);
		System.Array.Copy(EditorBuildSettings.scenes, newBuild, EditorBuildSettings.scenes.Length); 
		newBuild[EditorBuildSettings.scenes.Length] = saveScene;
		EditorBuildSettings.scenes = newBuild;
		#endif

	}
	public void saveData(int idPacient)
	{
		PlayerPrefs.SetInt("recentPacient",idPacient);

		//PlayerPrefs.SetInt("currentPlayer",numberOfPlayer);
		//PlayerPrefs.SetInt("points",1); //example to save data on computer. This is only temp value, deleted on exit application !!
	}
	public void loadMiniGame(int number, int idPacient)
	{
		loadNewSceneToGame();
		saveData(idPacient);	
		#if UNITY_EDITOR
		EditorApplication.SaveScene("Assets/testN.unity"); //saving positioning of all objects in scene on new file
		#endif

        LoadRandomMinigame();
	}

    private void LoadRandomMinigame()
    {
        int random = new System.Random().Next(0, minigameList.Count);
        Application.LoadLevel(minigameList[random]);
    }

	public void savePacient(int pacientId, int playerId, int points)
	{
		PlayerPrefs.SetInt ("pacient" + pacientId, -1);

		if (PlayerPrefs.GetInt ("firstSave") == 0)
			PlayerPrefs.SetInt ("firstSave",1);

		int pacientLives = PlayerPrefs.GetInt ("pacient"+pacientId);
		if (pacientLives == 1)
			PlayerPrefs.SetInt ("saveOnLastLife" + playerId, 1);

		int playerScore = PlayerPrefs.GetInt ("score" + playerId);
		playerScore += points;
		PlayerPrefs.SetInt ("score" + playerId, playerScore);


		PlayerPrefs.SetInt ("howManySave" + playerId, PlayerPrefs.GetInt ("howManySave" + playerId)+1);
			
		int isAnybodyAlive = 0;
		for(int i=1; i<=PlayerPrefs.GetInt ("howManyPacients"); i++)
		{
			if(PlayerPrefs.GetInt ("pacient"+pacientId)!=-1)
				isAnybodyAlive=1;
		}
		if(isAnybodyAlive==0)
		{
			//EndGame();
		}
		else
		{
			//NextRound();
		}
	}
	void killPacient(int id)
	{
		int pacientLives = PlayerPrefs.GetInt ("pacient"+id)-1;

		PlayerPrefs.SetInt ("howManyLose" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyLose" + PlayerPrefs.GetInt("currentPlayer"))+1);
		int isAnybodyAlive = 0;
		if(pacientLives==0)
		{
			PlayerPrefs.SetInt("pacient"+id,-1);
			PlayerPrefs.SetInt ("howManyKill" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyKill" + PlayerPrefs.GetInt("currentPlayer"))+1);
			if (PlayerPrefs.GetInt ("firstKill") == 0)
				PlayerPrefs.SetInt ("firstKill",1);
		}
		else
		{
			PlayerPrefs.SetInt("pacient"+id,pacientLives);
		}
		for(int i=1; i<=PlayerPrefs.GetInt ("howManyPacients"); i++)
		{
			if(PlayerPrefs.GetInt ("pacient"+id)!=-1)
				isAnybodyAlive=1;
		}
		if(isAnybodyAlive==0)
		{
			//EndGame();
		}
		else
		{
			//NextRound();
		}

	}
	public void loadDataToGame()
	{
		int recentScore = PlayerPrefs.GetInt ("recentResault"); //getting data from game before mini game
		if (recentScore > 0) // checking if this is start game or load after mini game
		{
			//Debug.Log ("recent score "+recentScore);
			PlayerPrefs.SetInt("isAdditionalAdv", 0);
			if(recentScore==1)
			{
				int pacientTemp=PlayerPrefs.GetInt ("recentPacient");
				savePacient(pacientTemp, PlayerPrefs.GetInt ("currentPlayer"), PlayerPrefs.GetInt ("pacient"+pacientTemp+"Points"));
			}
			else if(recentScore==2)
			{
				killPacient(PlayerPrefs.GetInt ("recentPacient"));
			}
		}
	}
	void OnApplicationQuit() 
	{
		//PlayerPrefs.DeleteAll();
	}


}
