using UnityEngine;
using System.Collections;

public class EndTurn : MonoBehaviour {

    private ControlsHandling controlsHandler;
    private TurnHandler turnHandler;
    private PlayerMovement activePlayer;

	// Use this for initialization
	void Start () {
        controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
        turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
        activePlayer = turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseEnter()
    {
        //activePlayer.SetCanWalk(false);
    }

    public void OnMouseExit()
    {
        //activePlayer.SetCanWalk(true);
    }

    public void OnMouseClick()
    {
        if (activePlayer.PlayerReachedTarget() && !controlsHandler.IsMenuOpen())
        {
            turnHandler.nextPlayer();
            activePlayer = turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>();
        }
    }
}
