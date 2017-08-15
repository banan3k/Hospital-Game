using UnityEngine;
using System.Collections;
//using UnityEditor;

public class menuForPlayer : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture logoTexture;

	public Texture2D buttonNormal;
	public Texture2D buttonHover;

	public GUIStyle StartG;
	public GUIStyle Tut;
	public GUIStyle MapSize;
	public GUIStyle Goal;

	float backgroundWidth = 300;
	float backgroundHeight = 400;
	float logoWidth = 400 * 0.9f;
	float logoHeight = 250 * 0.9f;
	float buttonWidth = 100;
	float buttonHeight = 30;

	public int mapnr = 1;
	string[] goal = {"Save all patients", "Finish with time" };
	int goalNr = 0;

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();

        PlayerPrefs.DeleteAll();
    }

    void OnGUI() {
		//background image
		GUI.DrawTexture(new Rect(Screen.width / 2 - backgroundWidth / 2, Screen.height / 2 - backgroundHeight / 2, backgroundWidth, backgroundHeight), backgroundTexture);

		//logo image
		GUI.DrawTexture(new Rect(Screen.width / 2 - logoWidth / 2, Screen.height / 2 - backgroundHeight / 2 - logoHeight / 2, logoWidth, logoHeight ), logoTexture);

		//tut button
		if(GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - backgroundHeight / 2 + 100, buttonWidth, buttonHeight), "Tutorial", Tut)) {
			Application.LoadLevel("Tutorial");
		}

		//goal button
		if(GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - backgroundHeight / 2 + 170, buttonWidth, buttonHeight), "Goal: " + goal[goalNr], Goal)) {
			goalNr++;
			if(goalNr > 1) {
				goalNr = 0;
			}
		}
		
		//map button
		if(GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - backgroundHeight / 2 + 240, buttonWidth, buttonHeight), "Map: " + mapnr, MapSize)) {
			mapnr++;
			if(mapnr > 3) {
				mapnr = 1;
			}
		}

		//start button
		if(GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2, Screen.height / 2 - backgroundHeight / 2 + 310, buttonWidth, buttonHeight), "Start Game", StartG)) {
			StartButton();
		}
	} 

	public float selected = 4;
	float input;
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.GetAxis ("Vertical"));
		input = Input.GetAxis ("Vertical");
		if (input > 0) {
			selected--;
			input = Input.GetAxis ("Vertical");
			Input.ResetInputAxes();
		}
		if (input < -0.2f) {
			selected++;
			input = Input.GetAxis ("Vertical");
			Input.ResetInputAxes();
		}
		if (selected > 4) {
			selected = 4;
		}
		if (selected < 1) {
			selected = 1;
		}
		selected = (int)selected;

		switch ((int)selected) {
		case 1:
			StartG.normal.background = buttonNormal;
			Tut.normal.background = buttonHover;
			MapSize.normal.background = buttonNormal;
			Goal.normal.background = buttonNormal;
			if (Input.GetButtonUp ("Submit")) {
                    Application.LoadLevel("Tutorial");
			}
			break;
		case 2:
			StartG.normal.background = buttonNormal;
			Tut.normal.background = buttonNormal;
			MapSize.normal.background = buttonNormal;
			Goal.normal.background = buttonHover;
			if (Input.GetButtonUp ("Submit")) {
				goalNr++;
				if(goalNr > 1) {
					goalNr = 0;
				}
			}			
			break;
		case 3:
			StartG.normal.background = buttonNormal;
			Tut.normal.background = buttonNormal;
			MapSize.normal.background = buttonHover;
			Goal.normal.background = buttonNormal;
			if (Input.GetButtonUp ("Submit")) {
                /*
                mapnr++;
				if(mapnr > 3) {
					mapnr = 1;
				}
                */				
			}
			break;
		case 4:
			StartG.normal.background = buttonHover;
			Tut.normal.background = buttonNormal;
			MapSize.normal.background = buttonNormal;
			Goal.normal.background = buttonNormal;
			if (Input.GetButtonUp ("Submit")) {
				StartButton();
			}
			break;
		}
	}

	void StartButton() {
		PlayerPrefs.SetInt ("MapNR", mapnr);
		Application.LoadLevel("AssignControllersMenu");
	}
}
