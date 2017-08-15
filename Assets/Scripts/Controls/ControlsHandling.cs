using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityEditor;

public class ControlsHandling : MonoBehaviour
{
    /*
    public Text ctrlText;
    public Text assignText;
    */

    //public GameObject turnHandler;

    //private TurnHandler turn;


    public int activePlayer;
    public int numberOfPlayers = 1;
    public int activeVSPlayer;
    public int numberOfControllers = 1;
    public int playerToBeAssigned = 0;

    public bool vsMinigameMode;
    public bool assignMode;
    public bool assignVSMode;
    public bool isAssignMenu = false;
    public bool nextPlayerCanBeAssigned = false;

    public bool assignPlayerMode = true;

    public int[] assignedController; //made public so the interface can pull from it

    private string[,] controlName;


    public float getHorizontal, getMenuHorizontal;
    public float getVertical, getMenuVertical;
    public float getHorizontalVS;
    public float getVerticalVS;
    public bool getSelect, getMenuSelect;
    public bool getSelectRelease;
    public bool getSelectVS;
    public bool getCancel, P1Back, getMenuCancel;
    public bool getMenu;
    public bool getItemMenu;
    public bool[] allStart = new bool[4];
    private int activePlayerDisplay;

    public bool menuOpen = false;
    public bool ignoreOtherMenus = false;

    // Use this for initialization
    void Start()
    {
        /*turnHandler = GameObject.Find("Turn Handler");
        turn = turnHandler.GetComponent<TurnHandler>();*/
        playerToBeAssigned = 0;
        assignedController = new int[4];
        controlName = new string[6, 6];
        vsMinigameMode = false;
        assignMode = false;
        assignVSMode = false;
        setControlNames();
        getPlayerPrefs();
    }

    public void savePlayerPrefs()       //this will be called by an interface when the game starts
    {
        PlayerPrefs.SetInt("NumberOfPlayers", numberOfPlayers);
        PlayerPrefs.SetInt("NumberOfControllers", numberOfControllers);
        PlayerPrefs.SetInt("P1", assignedController[0]);
        PlayerPrefs.SetInt("P2", assignedController[1]);
        PlayerPrefs.SetInt("P3", assignedController[2]);
        PlayerPrefs.SetInt("P4", assignedController[3]);
        //note that the currently active player is already saved by the turn handler
    }

    void getPlayerPrefs()
    {
        numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers", 1);
        numberOfControllers = PlayerPrefs.GetInt("NumberOfControllers", 1);
        assignedController[0] = PlayerPrefs.GetInt("P1", 100);
        assignedController[1] = PlayerPrefs.GetInt("P2", 100);
        assignedController[2] = PlayerPrefs.GetInt("P3", 100);
        assignedController[3] = PlayerPrefs.GetInt("P4", 100);
        activePlayer = PlayerPrefs.GetInt("Player", 0);
    }

    public void clearPlayerPrefs()
    {
        PlayerPrefs.SetInt("NumberOfPlayers", 1);
        PlayerPrefs.SetInt("NumberOfControllers", 1);
        PlayerPrefs.SetInt("P1", 100);
        PlayerPrefs.SetInt("P2", 100);
        PlayerPrefs.SetInt("P3", 100);
        PlayerPrefs.SetInt("P4", 100);
        PlayerPrefs.SetInt("Player", 0);
    }

    //this is needed in case someone accidentally added more players than needed
    void getLastCancel()
    {
        if (playerToBeAssigned > 0)
            getCancel = Input.GetButtonUp(controlName[assignedController[playerToBeAssigned - 1], 3]);
    }

    //to confirm players are ready in the menu
    void getAllStart()
    {
        for (int i = 0; i < 4; i++)
        {
            if (assignedController[i] != 100)
            {
                allStart[i] = Input.GetButton(controlName[assignedController[i], 4]);
            }
            else
            {
                allStart[i] = false;
            }
        }
    }

    void getP1Back()
    {
        if (assignedController[0] != 100)
            P1Back = Input.GetButton(controlName[assignedController[0], 3]);
    }

    /*void assignAllControllers()
    {
        assignPlayer();
        if (assignPlayerMode == false)
        {
            assignPlayerMode = true;
            playerToBeAssigned++;
            if (playerToBeAssigned >= numberOfPlayers)
            {
                playerToBeAssigned = 0;
                assignPlayerMode = false;
                assignAllMode = false;
            }
        }
        else
            assignAllMode = true;
    }*/

    void assignPlayer()
    {
        nextPlayerCanBeAssigned = false;
        assignMode = true;
        assignPlayerMode = true;
        assignedController[playerToBeAssigned] = listenAllSubmit();
        if (assignedController[playerToBeAssigned] != 100)
        {
            assignPlayerMode = false;
            if (playerToBeAssigned > 0)
            {
                if (assignedController[playerToBeAssigned] != assignedController[playerToBeAssigned - 1])
                    numberOfControllers++;
            }
        }
        else
        {
            assignPlayerMode = true;
        }
        assignMode = false;
    }

    void setControlNames()
    {
        int i = 0;
        controlName[i, 0] = "Horizontal";
        controlName[i, 1] = "Vertical";
        controlName[i, 2] = "Submit";
        controlName[i, 3] = "Cancel";
        controlName[i, 4] = "Menu";
        controlName[i, 5] = "ItemMenu";
        i++;
        controlName[i, 0] = "Horizontal_P1";
        controlName[i, 1] = "Vertical_P1";
        controlName[i, 2] = "Submit_P1";
        controlName[i, 3] = "Cancel_P1";
        controlName[i, 4] = "Menu_P1";
        controlName[i, 5] = "ItemMenu_P1";
        i++;
        controlName[i, 0] = "Horizontal_P2";
        controlName[i, 1] = "Vertical_P2";
        controlName[i, 2] = "Submit_P2";
        controlName[i, 3] = "Cancel_P2";
        controlName[i, 4] = "Menu_P2";
        controlName[i, 5] = "ItemMenu_P2";
        i++;
        controlName[i, 0] = "Horizontal_P3";
        controlName[i, 1] = "Vertical_P3";
        controlName[i, 2] = "Submit_P3";
        controlName[i, 3] = "Cancel_P3";
        controlName[i, 4] = "Menu_P3";
        controlName[i, 5] = "ItemMenu_P3";
        i++;
        controlName[i, 0] = "Horizontal_P4";
        controlName[i, 1] = "Vertical_P4";
        controlName[i, 2] = "Submit_P4";
        controlName[i, 3] = "Cancel_P4";
        controlName[i, 4] = "Menu_P4";
        controlName[i, 5] = "ItemMenu_P4";
        i++;
        controlName[i, 0] = "Horizontal_KB_VS";
        controlName[i, 1] = "Vertical_KB_VS";
        controlName[i, 2] = "Submit_KB_VS";
    }

    void listenControls()
    {
        if (Input.GetKey(KeyCode.P))
            LoadPerksOverview();
        if (Input.GetKey(KeyCode.O)) //Open the point's interface
            Application.LoadLevel("Points");
        if (Input.GetKey(KeyCode.Backspace) && Application.loadedLevelName == "Points") //Go back to the game
            Application.LoadLevel("First Level");

        if (activePlayer > 3)
            activePlayer = 3;
        if (assignedController[activePlayer] > 5)
            assignedController[activePlayer] = 5;
        getHorizontal = Input.GetAxis(controlName[assignedController[activePlayer], 0]);
        getVertical = Input.GetAxis(controlName[assignedController[activePlayer], 1]);
        getSelect = Input.GetButton(controlName[assignedController[activePlayer], 2]);
        getSelectRelease = Input.GetButtonUp(controlName[assignedController[activePlayer], 2]);
        if (assignedController[activePlayer] != 5)
        {
            getCancel = Input.GetButton(controlName[assignedController[activePlayer], 3]);
            getMenu = Input.GetButton(controlName[assignedController[activePlayer], 4]);
            getItemMenu = Input.GetButtonUp(controlName[assignedController[activePlayer], 5]);
        }
    }

    void listenMenuControls()
    {

        if (activePlayer > 3)
            activePlayer = 3;
        if (assignedController[activePlayer] > 5)
            assignedController[activePlayer] = 5;

        getMenuSelect = Input.GetButtonUp(controlName[assignedController[activePlayer], 2]);
        getMenuCancel = Input.GetButton(controlName[assignedController[activePlayer], 3]);
        getItemMenu = Input.GetButtonUp(controlName[assignedController[activePlayer], 5]);
        getMenuHorizontal = Input.GetAxis(controlName[assignedController[activePlayer], 0]);
        getMenuVertical = Input.GetAxis(controlName[assignedController[activePlayer], 1]);
    }

    void listenVSControls()
    {
        listenControls();

        getHorizontalVS = Input.GetAxis(controlName[activeVSPlayer, 0]);
        getVerticalVS = Input.GetAxis(controlName[activeVSPlayer, 1]);
        getSelectVS = Input.GetButton(controlName[activeVSPlayer, 2]);
    }

    int listenAllSubmit()
    {
        bool undecided = true;
        int controller = 0;
        for (controller = 0; controller <= 5 && undecided; controller++)
        {
            if (controller != activePlayer || (assignMode && controller != 5))
                undecided = !Input.GetButtonUp(controlName[controller, 2]);
        }
        if (undecided)
            return 100;         //while this is set to 100, this function will be called again the next update
        else
            return controller - 1;
    }

    void assignVSPlayer()
    {
        assignMode = false;
        assignVSMode = true;
        if (numberOfControllers > 1)
        {
            activeVSPlayer = listenAllSubmit();
            if (activeVSPlayer != 100)
                assignVSMode = false;
        }
        else
        {
            activeVSPlayer = 5;
            assignVSMode = false;
        }
    }


    void Update()
    {
        activePlayer = PlayerPrefs.GetInt("Player");

        if (isAssignMenu)
        {
            getAllStart();
            getLastCancel();
            getP1Back();
            if (assignPlayerMode)
                assignPlayer();
            else if (playerToBeAssigned < 3)
            {
                nextPlayerCanBeAssigned = true;
                numberOfPlayers++;
            }
        }

        /*if (assignPlayerMode)
        {
            assignPlayer();
        }*/

        if (!isAssignMenu)
        {
            if (!menuOpen)
                listenControls();
            else
                listenMenuControls();
        }
        else if (vsMinigameMode && !isAssignMenu)
        {
            assignVSPlayer();
            listenVSControls();
        }

        if (!menuOpen && (getMenuHorizontal != 0 || getMenuVertical != 0 || getMenuCancel || getMenuSelect))
        {
            getMenuHorizontal = 0;
            getMenuVertical = 0;
            getMenuCancel = false;
            getMenuSelect = false;
        }


        //SetCtrlText();
    }

    /*void SetCtrlText()
    {
        ctrlText.text = "Input:" + InputInterpreter();
    }

    void SetAssignText()
    {
        assignText.text = "";
        if (assignPlayerMode)
        {
            assignText.text = "Player " + (playerToBeAssigned + 1).ToString() + ", please press the Submit button to select your controller.";
        }
        if (assignVSMode)
        {
            assignText.text = "Please press the Submit button to join in on the VS mode.";
        }
    }

    string InputInterpreter()
    {
        string AllInputs = "";
        activePlayerDisplay = activePlayer + 1;
        if (getHorizontal != 0)
            AllInputs += " Horizontal " + activePlayerDisplay.ToString() + ": " + getHorizontal.ToString();
        if (getVertical != 0)
            AllInputs += " Vertical " + activePlayerDisplay.ToString() + ": " + getVertical.ToString();
        if (getHorizontalVS != 0)
            AllInputs += " Horizontal VS: " + getHorizontalVS.ToString();
        if (getVerticalVS != 0)
            AllInputs += " Vertical VS: " + getVerticalVS.ToString();
        if (getSelect)
            AllInputs += " Select " + activePlayerDisplay.ToString();
        if (getSelectVS)
            AllInputs += " Select VS ";
        if (getCancel)
            AllInputs += " Cancel " + activePlayerDisplay.ToString();
        if (getMenu)
            AllInputs += " Menu " + activePlayerDisplay.ToString();
        if (getItemMenu)
            AllInputs += " ItemMenu" + activePlayerDisplay.ToString();

        return AllInputs;
    }*/

    private void LoadPerksOverview()
    {
        Application.LoadLevel("Perks");
    }

    public void SetMenuOpen(bool open)
    {
        menuOpen = open;
    }

    public bool IsMenuOpen()
    {
        return menuOpen;
    }
}
