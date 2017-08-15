using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AssignMenuHandler : MonoBehaviour {

    public GameObject controlsHandler;
    public GameObject[] PlayerCard = new GameObject[4];
    public GameObject[] Prompts = new GameObject[4];
    public GameObject[] ControllerImages = new GameObject[8]; //even/0 = controller, odd = keyboard

    public GameObject BackBar;

    private ControlsHandling controls;

    private bool getCancel, onFinalCard;
    private bool[] allStart = new bool[4];

    public int amountConfirmed = 0;
    private float backProgress = 0f;
    private bool[] playerConfirmed = new bool[4];

    private Animator[] PlayerCardAnim = new Animator[4];
    private TextMesh[] PromptText = new TextMesh[4];
    private FadeText[] PromptFade = new FadeText[4];


    // Use this for initialization
    void Start () {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
        controls.clearPlayerPrefs();
        setImagesInvisible();
        getAnimators();
        getPromptText();
        getPromptFade();
        controls.isAssignMenu = true;
        firstCard();

    }

    void setImagesInvisible()
    {
        for (int i = 0; i < 8; i++)
        {
            ControllerImages[i].GetComponent<Renderer>().enabled = false;
        }
    }

    void getAnimators()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerCardAnim[i] = PlayerCard[i].GetComponent<Animator>();
        }
    }

    void getPromptText()
    {
        for (int i = 0; i < 4; i++)
        {
            PromptText[i] = Prompts[i].GetComponent<TextMesh>();
        }
    }

    void getPromptFade()
    {
        for (int i = 0; i < 4; i++)
        {
            PromptFade[i] = Prompts[i].GetComponent<FadeText>();
        }
    }

    void controlListener()
    {
        getCancel = controls.getCancel;
        allStart = controls.allStart;
    }

    void firstCard()
    {
        controls.playerToBeAssigned = 0;
        PlayerCardAnim[0].Play("PlayerCardRaise");
        PromptText[0].text = "Press A or Enter to join";
        PromptFade[0].active = true;
        controls.assignPlayerMode = true;
    }

    void nextCard()
    {
        controls.playerToBeAssigned++;
        string startEquiv;
        int pToBeAs = controls.playerToBeAssigned;

        if(controls.assignedController[pToBeAs - 1] == 0)
        {
            changeImage(pToBeAs - 1, false);
            startEquiv = "Esc";
        }
        else
        {
            changeImage(pToBeAs - 1, true);
            startEquiv = "Start";
        }

        PlayerCardAnim[pToBeAs - 1].Play("PlayerCardLower");
        PromptText[pToBeAs - 1].text = "Press " + startEquiv + " to confirm";

        PromptText[pToBeAs].text = "Press A or Enter to join";
        PlayerCardAnim[pToBeAs].Play("PlayerCardRaise");
        PromptFade[pToBeAs].active = true;
        controls.assignPlayerMode = true;
    }

    void changeImage(int player, bool controller)
    {
        player *= 2;
        if(controller)
        {
            ControllerImages[player].GetComponent<Renderer>().enabled = true;
            ControllerImages[player + 1].GetComponent<Renderer>().enabled = false;
        }
        else
        {
            ControllerImages[player + 1].GetComponent<Renderer>().enabled = true;
            ControllerImages[player].GetComponent<Renderer>().enabled = false;
        }
    }

    void checkConfirm()
    {
        for(int i = 0; i < 4; i++)
        {
            if(allStart[i] && !playerConfirmed[i])
            {
                playerConfirmed[i] = true;
                confirm(i);
            }
        }
    }

    void confirm(int playerConfirmed)
    {
        amountConfirmed++;
        PromptFade[playerConfirmed].ready = true;
        PromptText[playerConfirmed].text = "Ready";
    }

    void checkCancel()
    {
        if (getCancel && !playerConfirmed[controls.playerToBeAssigned-1] && !playerConfirmed[3])
            prevCard();
    }

    void prevCard()
    {
        onFinalCard = false;
        if (controls.playerToBeAssigned>1)
        {
            controls.playerToBeAssigned -= 2;
            controls.numberOfPlayers -= 2;
            int pToBeAs = controls.playerToBeAssigned;

            PlayerCardAnim[pToBeAs + 2].Play("PlayerCardLower");
            PromptFade[pToBeAs + 2].active = false;

            controls.nextPlayerCanBeAssigned = false;
            nextCard();
        }
        else
        {
            PlayerCardAnim[1].Play("PlayerCardLower");
            PromptFade[1].active = false;

            controls.nextPlayerCanBeAssigned = false;
            firstCard();
        }
    }

    void finalCard()
    {
        controls.numberOfPlayers = 4;
        string startEquiv;
        int pToBeAs = controls.playerToBeAssigned;

        if (controls.assignedController[pToBeAs] == 0)
        {
            changeImage(pToBeAs, false);
            startEquiv = "Esc";
        }
        else
        {
            changeImage(pToBeAs, true);
            startEquiv = "Start";
        }

        PromptText[pToBeAs].text = "Press " + startEquiv + " to confirm";
    }

    void checkBack()
    {
        if (controls.P1Back && backProgress <= 1)
        {
            BackBar.transform.localScale += new Vector3(0.01f, 0, 0);
            backProgress += 0.01f;
        }
        else if (backProgress > 0 && backProgress < 0.9)
        {
            BackBar.transform.localScale -= new Vector3(0.01f, 0, 0);
            backProgress -= 0.01f;
        }    
        else if (backProgress >= 0.9)
            goBack();
    }

    void goBack()
    {
        Application.LoadLevel("menu");
    }
	
	// Update is called once per frame
	void Update () {
        controlListener();

        checkConfirm();

        checkBack();

        if (controls.playerToBeAssigned > 0)
            checkCancel();

        if (controls.nextPlayerCanBeAssigned == true)
        {
            controls.nextPlayerCanBeAssigned = false;
            nextCard();
        }

        if (!controls.assignPlayerMode && !onFinalCard && controls.playerToBeAssigned == 3)
        {
            onFinalCard = true;
            finalCard();
        }

        if((amountConfirmed==controls.playerToBeAssigned || amountConfirmed==4) && amountConfirmed > 0)
        {
            controls.numberOfPlayers = amountConfirmed;
            controls.savePlayerPrefs();
            switch (PlayerPrefs.GetInt("MapNR"))
            {
                case 1:
                    Application.LoadLevel("First Level");
                    break;
                case 2:
                    Application.LoadLevel("Second Level");
                    break;
                case 3:
                    Application.LoadLevel("Third Level");
                    break;
            }
        }
	
	}
}