using UnityEngine;
using System.Collections;

//ADD TO MAIN CAMERA

public class movementAction : MonoBehaviour {

	public int scalar=1;
	public int additionalBonus = 0;
    public int size = 5;
    public int stop = 0;

    private TurnHandler turnHandler;
    private PlayerMovement activePlayer;
    private GUIStyle batteryFont;
    private Vector3 distanceStart;
    private Vector3 beforePosition;
    private Vector3 lastPosition;
    private float DistanceTravelled;
    private int breakDistance = 1;
    private int howManyPoinstPerRound = 5;

    void Start () 
	{
		turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
		activePlayer = turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>();

        batteryFont = new GUIStyle();
		batteryFont.fontSize=45;

		lastPosition = GetActivePlayerPosition();
		//size = GameObject.Find("player").GetComponent<
	}
	void OnGUI() 
	{
		//GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b,0.5f);
		GUI.Label(new Rect(Screen.width/2, 20, 100, 20), Players.GetPlayer(turnHandler.activePlayer).GetMovementResouce().ToString(), batteryFont);
	}
	void resetActionPoints(int toWhat)
	{
		size = toWhat;
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name.Substring(0,4) == "door")
		{
			PlayerPrefs.SetInt("howManyDoors"+PlayerPrefs.GetInt("currentPlayer"),PlayerPrefs.GetInt("howManyDoors"+PlayerPrefs.GetInt("currentPlayer"))+1);
			
		}
		if(col.gameObject.name.Substring(0,4) == "wall")
		{
			PlayerPrefs.SetInt("howManyOnWall"+PlayerPrefs.GetInt("currentPlayer"),PlayerPrefs.GetInt("howManyOnWall"+PlayerPrefs.GetInt("currentPlayer"))+1);
				
		}
	}

	// Update is called once per frame
	void Update ()
	{
	    int activePlayer = turnHandler.activePlayer;

		if (additionalBonus != 0)
		{
		    Players.GetPlayer(activePlayer).AddMovementResource(additionalBonus);
			additionalBonus = 0;
		}
		/*if(GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position!=beforePosition)
		{
			
			//Debug.Log("AAA");
			beforePosition=GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position;
			movementResource= Vector3.Distance(distanceStart,GameObject.Find ("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position);
			
			if(movementResource>breakDistance)
			{
				breakDistance++;
				PlayerPrefs.SetInt ("howManyWalk" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyWalk" + PlayerPrefs.GetInt("currentPlayer"))+1);

				Debug.Log (movementResource);
			}
			
		}
		else if(beforePosition!=distanceStart)
		{
			distanceStart=GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position;
			Debug.Log("ola boga");
		}
		if(breakDistance>=size*3+1 && stop==0)
		{
			Debug.Log("STOP");
			stop=1;
		}*/

        DistanceTravelled += Vector3.Distance(GetActivePlayerPosition(), lastPosition);

        Debug.Log("DistanceTravelled: " + DistanceTravelled + " - breakDistance: " + breakDistance);

        if (DistanceTravelled>breakDistance && Players.GetPlayer(activePlayer).GetMovementResouce() > 0)
		{
			breakDistance+=scalar;
            Players.GetPlayer(activePlayer).SubstractMovementResouce();
			PlayerPrefs.SetInt ("howManyWalk" + PlayerPrefs.GetInt("currentPlayer"), PlayerPrefs.GetInt ("howManyWalk" + PlayerPrefs.GetInt("currentPlayer"))+1);
			
		//	Debug.Log (DistanceTravelled);
		}
		else if(Players.GetPlayer(activePlayer).GetMovementResouce() == 0)
		{
            ReachedEdge();
		}

		lastPosition = GameObject.Find("Player"+PlayerPrefs.GetInt("currentPlayer")).transform.GetChild(0).position;

	}

    private Vector3 GetActivePlayerPosition()
    {
        int playerNumber = PlayerPrefs.GetInt("Player");
        return turnHandler.GetActivePlayer().transform.Find("Player").transform.position;
    }

    private void ReachedEdge()
    {
        turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>().StopWalking();
        Players.GetPlayer(turnHandler.activePlayer).SetMovementResource(0);
    }

    public void ResetMovementResource()
    {
        Players.GetPlayer(turnHandler.activePlayer).SetMovementResource(9);
        lastPosition = GetActivePlayerPosition();
        DistanceTravelled = 0;
        breakDistance = 0;
    }
}
