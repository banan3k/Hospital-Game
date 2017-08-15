using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    private GameObject controlsHandler, turnHandler, personalCannonSpawn;

    private ControlsHandling controls;

    private SpawnCannon spawnCannon;

    private TurnHandler turn;

    public GameObject[] buttons = new GameObject[3];

    private bool getItemMenu, getSelect, getCancel;

    public bool menuOpen = false;

    private float getHorizontal;

    public int selectedButton = 0;

    private bool forceEndMode = false;


    // Use this for initialization
    void Start () {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();

        turnHandler = GameObject.Find("Turn Handler");
        turn = turnHandler.GetComponent<TurnHandler>();

        personalCannonSpawn = GameObject.Find("personalCannon");
        spawnCannon = personalCannonSpawn.GetComponent<SpawnCannon>();

    }

    void controlListener()
    {
        if(!forceEndMode)
        {
            getItemMenu = controls.getItemMenu;
            getCancel = controls.getMenuCancel;
            getHorizontal = controls.getMenuHorizontal;
        }

        getSelect = controls.getMenuSelect;
    }

    void openMenu()
    {
        menuOpen ^= true;
        controls.menuOpen = menuOpen;
    }

    void selectButton()
    {
        if (getHorizontal > 0.5)
        {
            Input.ResetInputAxes();
            selectedButton++;
            selectedButton %= 3;
        }
        else if (getHorizontal < -0.5)
        {
            Input.ResetInputAxes();
            selectedButton--;
            if (selectedButton < 0)
                selectedButton = 2;
        }
    }

    void handleButton()
    {
        switch (selectedButton)
        {
            case 0:
                //use item
                buttons[0].GetComponent<Animator>().SetBool("IsSelected", true);
                buttons[1].GetComponent<Animator>().SetBool("IsSelected", false);
                buttons[2].GetComponent<Animator>().SetBool("IsSelected", false);
                break;
            case 1:
                buttons[0].GetComponent<Animator>().SetBool("IsSelected", false);
                buttons[1].GetComponent<Animator>().SetBool("IsSelected", true);
                buttons[2].GetComponent<Animator>().SetBool("IsSelected", false);
                break;
            case 2:
                buttons[0].GetComponent<Animator>().SetBool("IsSelected", false);
                buttons[1].GetComponent<Animator>().SetBool("IsSelected", false);
                buttons[2].GetComponent<Animator>().SetBool("IsSelected", true);
                break;
            default:
                break;
        }
    }

    void hideAllButtons()
    {
        buttons[0].GetComponent<Animator>().SetBool("IsSelected", false);
        buttons[1].GetComponent<Animator>().SetBool("IsSelected", false);
        buttons[2].GetComponent<Animator>().SetBool("IsSelected", false);
    }

    void pushSelect()
    {
        switch(selectedButton)
        {
            case 0:
                //use item
                buttons[0].GetComponent<Animator>().Play("Select");
                break;
            case 1:
                spawnCannon.spawnC(turn.GetActivePlayer());
                buttons[1].GetComponent<Animator>().Play("Select");
                break;
            case 2:
                turn.nextPlayer();
                buttons[2].GetComponent<Animator>().Play("Select");
                break;
            default:
                break;
        }
        cancelMenu();
        forceEndMode = false;
    }

    void cancelMenu()
    {
        menuOpen = false;
        if(!controls.ignoreOtherMenus)
            controls.menuOpen = menuOpen;
    }
	
	// Update is called once per frame
	void Update () {
        controlListener();
        if (getItemMenu && !controls.ignoreOtherMenus)
            openMenu();
        else if(controls.ignoreOtherMenus)
            menuOpen = false;

        if(menuOpen)
        {
            if (getCancel)
                cancelMenu();
            selectButton();
            handleButton();

            if (getSelect)
                pushSelect();
        }
        else
        {
            hideAllButtons();
        }
	
	}

    public void forceEndTurn()
    {
        forceEndMode = true;
        selectedButton = 2;
        openMenu();
    }
}
