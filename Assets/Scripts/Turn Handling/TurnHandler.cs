using UnityEngine;
using System.Collections;

public class TurnHandler : MonoBehaviour {

    public int amountOfPlayers = 4;
    public int maxPlayers = 4;         //can't be used to initialize array, but works as reference
    public int activePlayer = 0;
    public int turn = 1;
    public int endOfGame = 30;

    public GameObject[] players = new GameObject[4];
    public GameObject controlsHandler;

    public GameObject TurnDisplay, ActivePlayerDisplay, NextPlayerDisplay, camera, perkSelection;

    private int turnsToLevelUp = 20;
    private Renderer CurrentPlayerMat, NextPlayerMat;
    private ControlsHandling controls;
    private movementAction movementAction;
    private Animator TurnAnimator, ActivePlayerAnimator;
    private TextMesh CurrentPlayerText, NextPlayerText, CurrentTurnText, NextTurnText;

    private bool levelUp = false;

	// Use this for initialization
	void Start () {
        getAllGameObjects();
        getRelevantStaticInformation();
        getPlayerPrefs();
        deSpawnUnneededPlayers();
	    PlacePlayersCorrectly();

        //Enable controls for the current player
        players[activePlayer].GetComponentInChildren<PlayerMovement>().SetCurrentTurn(true);
        //players[activePlayer].GetComponentInChildren<PlayerMovement>().SetCanWalk(true);
        camera.GetComponent<CameraFollow>().SetTarget(players[activePlayer].transform.Find("Player").gameObject);
    }

    void setPlayerColor(Renderer mat, int color)
    {
        switch(color)
        {
            case 0:
                mat.material.color = Color.blue;
                break;
            case 1:
                mat.material.color = Color.yellow;
                break;
            case 2:
                mat.material.color = Color.green;
                break;
            case 3:
                mat.material.color = Color.magenta;
                break;
            default:
                break;
        }
    }

    void getAllGameObjects()
    {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
        TurnDisplay = GameObject.Find("TurnDisplay");
        ActivePlayerDisplay = GameObject.Find("ActivePlayerDisplay");
        NextPlayerDisplay = GameObject.Find("NextPlayerDisplay");
        TurnAnimator = TurnDisplay.GetComponent<Animator>();
        ActivePlayerAnimator = ActivePlayerDisplay.GetComponent<Animator>();
        CurrentPlayerText = GameObject.Find("PlayerText").GetComponent<TextMesh>();
        NextPlayerText = GameObject.Find("NextPlayerText").GetComponent<TextMesh>();
        CurrentTurnText = GameObject.Find("TurnText").GetComponent<TextMesh>();
        NextTurnText = GameObject.Find("NextTurnText").GetComponent<TextMesh>();
        CurrentPlayerMat = ActivePlayerDisplay.GetComponent<Renderer>();
        NextPlayerMat = NextPlayerDisplay.GetComponent<Renderer>();
        camera = GameObject.Find("Main Camera");
        perkSelection = GameObject.Find("PerkGainPanel");
        movementAction = GameObject.Find("Main Camera").GetComponent<movementAction>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void savePlayerPrefs()
    {
        PlayerPrefs.SetInt("Turn", turn);
        PlayerPrefs.SetInt("Player", activePlayer);
        PlayerPrefs.SetInt("currentPlayer", activePlayer);
    }

    void getPlayerPrefs()
    {
        turn = PlayerPrefs.GetInt("Turn" , 1);
        activePlayer = PlayerPrefs.GetInt("Player" , 0);
        amountOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers" , 1);

        CurrentPlayerText.text = "Player " + (activePlayer + 1);
        setPlayerColor(CurrentPlayerMat, activePlayer);
        CurrentTurnText.text = "Turn " + turn;
    }

    public void clearPlayerPrefs()
    {
        PlayerPrefs.SetInt("Turn", 1);
        PlayerPrefs.SetInt("Player", 0);
    }

    void getRelevantStaticInformation()
    {
        amountOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers", 1);
    }

    void disableAllBut(int activePlayer)
    {
        for(int i = 0; i<amountOfPlayers; i++)
        {
            if(i!=activePlayer)
            {
                disableMono(players[i]);
            }
            else if(i==activePlayer)
            {
                enableMono(players[i]);
            }
        }
    }

    void disableMono(GameObject toDisable)
    {
        Component[] allComponents = toDisable.GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour monoComponent in allComponents)
        {
            monoComponent.enabled = false;
        }
    }

    void enableMono(GameObject toEnable)
    {
        Component[] allComponents = toEnable.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour monoComponent in allComponents)
        {
            monoComponent.enabled = true;
        }
    }

    void deSpawnUnneededPlayers()
    {
        for(int i = amountOfPlayers; i<maxPlayers; i++)
        {
            players[i].SetActive(false);
        }
    }

    public void nextPlayer()
    {
        players[activePlayer].GetComponentInChildren<PlayerMovement>().SetCurrentTurn(false);

        if (activePlayer + 1 < amountOfPlayers)
        {
            animatePlayerDisplay(false);
            activePlayer++;
            savePlayerPrefs();

            if (levelUp)
                LevelUp();
        }
        else { 
            nextTurn();
        }

        movementAction.ResetMovementResource();
        camera.GetComponent<CameraFollow>().SetTarget(players[activePlayer].transform.Find("Player").gameObject);
        players[activePlayer].GetComponentInChildren<PlayerMovement>().SetCurrentTurn(true);
    }

    void nextTurn()
    {
        animateTurnDisplay();
        turn++;
        animatePlayerDisplay(true);
        activePlayer = 0;
        savePlayerPrefs();

        if (turn == endOfGame)
        {
            Players.ResetPlayers();
            Application.LoadLevel("EndResult");
        }
        else if (turn % turnsToLevelUp == 0)
        {
            levelUp = true;
            LevelUp();
        }
        else
        {
            levelUp = false;
        }
    }

    void animatePlayerDisplay(bool P1)
    {
        if(!P1)
        {
            setPlayerColor(CurrentPlayerMat, activePlayer);
            setPlayerColor(NextPlayerMat, activePlayer+1);
            CurrentPlayerText.text = "Player " + (activePlayer + 1);
            NextPlayerText.text = "Player " + (activePlayer + 2);
        }
        else
        {
            setPlayerColor(CurrentPlayerMat, activePlayer);
            setPlayerColor(NextPlayerMat, 0);
            CurrentPlayerText.text = "Player " + (activePlayer + 1);
            NextPlayerText.text = "Player " + 1;
        }
        ActivePlayerAnimator.Play("PlayerChange");
    }

    void animateTurnDisplay()
    {
        CurrentTurnText.text = "Turn " + turn;
        NextTurnText.text = "Turn " + (turn + 1);
        TurnAnimator.Play("TurnChange");
    }

    private void LevelUp()
    {
        controls.getMenuSelect = false;
        controls.SetMenuOpen(true);
        controls.ignoreOtherMenus = true;
        perkSelection.GetComponent<PerkSelection>().OpenPanel();
    }

    public GameObject GetActivePlayer()
    {
        return players[activePlayer];
    }

    private void PlacePlayersCorrectly()
    {
        bool repositionCamera = false;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            Vector3 position = Players.GetPlayer(i).GetLocation();
            //Debug.Log(Players.GetPlayer(i).GetLocation());
            //Debug.Log(Players.GetPlayer(i).GetPoints());

            if (!position.Equals(new Vector3(0, 0, 0)))
            {
                PlacePlayerInPositions(i);
                repositionCamera = true;
            }
        }

        if (repositionCamera)
        {
            camera.GetComponent<CameraFollow>().SetRepositionCamera(true);
        }
    }

    private void SavePlayerPositions()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject player = players[i];
            Players.GetPlayer(i).SetLocation(player.transform.Find("Player").position);
        }
    }

    private void PlacePlayerInPositions(int number)
    {
        GameObject player = players[number];
        player.transform.Find("Player").position = Players.GetPlayer(number).GetLocation();
    }

    private void OnDestroy()
    {
        if(turn != endOfGame)
            SavePlayerPositions();
    }
}
