using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class Points : MonoBehaviour {

    public Text title;
    public Text playerPoints;

	// Use this for initialization
	void Start () {
        title.rectTransform.sizeDelta = new Vector2(Screen.width, title.rectTransform.rect.height);
        title.rectTransform.anchoredPosition = new Vector2(-(title.rectTransform.rect.width / 2), -40);
        playerPoints.rectTransform.sizeDelta = new Vector2(Screen.width - 200, Screen.height - 100);
        playerPoints.rectTransform.anchoredPosition = new Vector2(50, -Screen.height + 20);


        SortedDictionary<string, int> listOfPlayers = new SortedDictionary<string, int>();
        int nbPlayer = PlayerPrefs.GetInt("NumberOfPlayers"); ;
        for (int i = 1; i < nbPlayer + 1; i++)
	    {
            listOfPlayers.Add("Player " + i, Players.GetPlayer(i -1).GetPoints());
        }

	    int index = 1;
        foreach (KeyValuePair<string, int> player in listOfPlayers.OrderByDescending(key => key.Value))
        {
            playerPoints.transform.Find("Best" + index).transform.Find("NumberOfPlayer").GetComponent<Text>().text = player.Key.ToString(); ;
            playerPoints.transform.Find("Best" + index).transform.Find("Score").GetComponent<Text>().text = player.Value.ToString();
            index++;
        }

        //Change it by nb of players
        /*foreach (KeyValuePair<string, int> player in listOfPlayers)
        {
            playerPoints.transform.Find("Best" + nbPlayer).transform.Find("NumberOfPlayer").GetComponent<Text>().text = player.Key.ToString(); ;
            playerPoints.transform.Find("Best" + nbPlayer).transform.Find("Score").GetComponent<Text>().text = player.Value.ToString();
            nbPlayer--;
        }*/
    }

    private void OrderByPoints()
    {
        
    }
}
