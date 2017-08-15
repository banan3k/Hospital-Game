using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class EndResult : MonoBehaviour {

    public Text playerPoints;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject scrollbar in GameObject.FindGameObjectsWithTag("myScrollbar"))
        {
            scrollbar.GetComponent<Scrollbar>().value = 1;
        }

        List<PlayerProperties> listOfPlayers = new List<PlayerProperties>();
        int nbPlayer = PlayerPrefs.GetInt("NumberOfPlayers"); ;
        for (int i = 1; i < nbPlayer + 1; i++)
        {
            listOfPlayers.Add(Players.GetPlayer(i - 1));
        }

        int index = 1;
        foreach (PlayerProperties playerProperties in listOfPlayers.OrderByDescending(p => p.GetPoints()))
        {
            playerPoints.transform.Find("Best" + index).transform.Find("NumberOfPlayer").GetComponent<Text>().text = "Player " + playerProperties.Number.ToString(); ;
            playerPoints.transform.Find("Best" + index).transform.Find("Score").GetComponent<Text>().text = playerProperties.GetPoints().ToString();
            playerPoints.transform.Find("Best" + index).transform.Find("Details").transform.Find("History").GetComponent<Text>().text = playerProperties.GetHistory();
            index++;
        }
    }

    void Update()
    {
        if (Input.GetButton("Cancel"))
            Application.LoadLevel("menu");
    }
}
