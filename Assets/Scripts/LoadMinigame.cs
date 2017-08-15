using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadMinigame : MonoBehaviour
{
    private List<string> minigameList = new List<string>
    {
        "MiniGame2",
        "Minigame3"
    };

    // Use this for initialization
    void Start ()
    {
        ProcessMinigameResults();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadRandomMinigame(int activePlayer, int patientNumber)
    {
        PlayerPrefs.SetInt("recentPatient", patientNumber);

        int random = new System.Random().Next(0, minigameList.Count);
        Application.LoadLevel(minigameList[random]);
    }

    private void ProcessMinigameResults()
    {
        int minigameResults = PlayerPrefs.GetInt("minigameResults"); //getting data from game before mini game
        int patientNumber = PlayerPrefs.GetInt("recentPatient");

        if (minigameResults == 0)
            return;

        if (minigameResults == 1)
        {
            PatientSaved(patientNumber);
        }
        else if (minigameResults == 2)
        {
            PatientNotSaved(patientNumber);
        }

        PlayerPrefs.SetInt("minigameResults", 0);

        //Debug.Log(Players.GetPlayer(PlayerPrefs.GetInt("Player", 0)).GetPoints());
    }

    private void PatientSaved(int patientNumber)
    {
        int points = PlayerPrefs.GetInt("patient" + patientNumber + "difficulty");
        int playerNumber = PlayerPrefs.GetInt("Player");

        //Status 1 means that the patient is saved
        PlayerPrefs.SetInt("patient" + patientNumber + "status", 1);
        Players.GetPlayer(PlayerPrefs.GetInt("Player", 0)).AddPoints(points);
        Players.GetPlayer(playerNumber).AddHistory("You saved Patient " + patientNumber);
    }

    private void PatientNotSaved(int patientNumber)
    {
        int patientLives = PlayerPrefs.GetInt("patient" + patientNumber + "lives");

        if (patientLives > 0)
        {
            PlayerPrefs.SetInt("patient" + patientNumber + "lives", patientLives -1);
        }
        else
        {
            int playerNumber = PlayerPrefs.GetInt("Player");

            //Status 1 means that the patient died
            PlayerPrefs.SetInt("patient" + patientNumber + "status", 1);
            Players.GetPlayer(playerNumber).AddHistory("You killed Patient " + patientNumber);
        }
    }
}
