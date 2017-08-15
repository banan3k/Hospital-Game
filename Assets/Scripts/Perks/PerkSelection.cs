using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PerkSelection : MonoBehaviour {

    private GameObject firstPerk;
    private GameObject secondPerk;
    private GameObject thirdPerk;

    private ControlsHandling controlsHandler;
    private TurnHandler turnHandler;
    private List<GameObject> selection;
    private Animator panelAnimator, turnAnimator;

    private GameObject currentlySelected;
    private bool controlsEnabled = true;
    private bool openPanel = false;

    public float getHorizontal = 0;
    public bool getSelect = false;

    // Use this for initialization
    void Start () {
        firstPerk = GameObject.Find("FirstPerkContainer");
        secondPerk = GameObject.Find("SecondPerkContainer");
        thirdPerk = GameObject.Find("ThirdPerkContainer");

        controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
        turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
        panelAnimator = GetComponent<Animator>();
        turnAnimator = GameObject.Find("TurnDisplay").GetComponent<Animator>();

        selection = new List<GameObject> {
            firstPerk,
            secondPerk,
            thirdPerk
        };

        currentlySelected = selection[0];
        currentlySelected.transform.Find("Panel").GetComponent<Image>().color = new Color(251, 200, 250, 255);
    }
	
	// Update is called once per frame
	void Update () {
        if(controlsHandler.ignoreOtherMenus)
        {
            listenControls();
            ControlsListener();
        }
	}

    void listenControls()
    {
        getHorizontal = controlsHandler.getMenuHorizontal;
        getSelect = controlsHandler.getMenuSelect;
    }

    private void ControlsListener()
    {
        float direction = getHorizontal;
        GameObject previouslySelected = currentlySelected;

        if (controlsEnabled)
        {
            controlsEnabled = false;

            if (direction != 0f)
            {
                NavigationControls(direction);
                SwitchButtonsHighlighted(currentlySelected, previouslySelected);
            }
            else if (getSelect)
            {
                ActivatePerk();
            }
            
            controlsEnabled = true;
        }
    }

    private void NavigationControls(float direction)
    {
        if (direction < -0.5)
        {
            Input.ResetInputAxes();
            int index = selection.FindIndex(p => p == currentlySelected);
            if (index > 0)
                currentlySelected = selection[index - 1];
            else
                currentlySelected = selection[selection.Count - 1];
        }
        else if (direction > 0.5)
        {
            Input.ResetInputAxes();
            int index = selection.FindIndex(p => p == currentlySelected);
            if (index < selection.Count - 1)
                currentlySelected = selection[index + 1];
            else
                currentlySelected = selection[0];
        }
    }

    private void ActivatePerk()
    {
        PerkProperties.GetActivePerks(turnHandler.activePlayer);
        currentlySelected.GetComponent<Perk>().SetActivated(true);
        PerkProperties.SetActivePerk(turnHandler.activePlayer, currentlySelected.GetComponent<Perk>());
        panelAnimator.Play("ClosePerksGain");
        currentlySelected = selection[0];

        controlsHandler.SetMenuOpen(false);
        controlsHandler.ignoreOtherMenus = false;
    }

    private void SwitchButtonsHighlighted(GameObject toHighlight, GameObject unhighlight)
    {
        unhighlight.transform.Find("Panel").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        toHighlight.transform.Find("Panel").GetComponent<Image>().color = new Color(226, 243, 255, 255);
    }

    private void LoadImage(GameObject o, Perk p)
    {
        Image images = o.transform.Find("Image").GetComponent<Image>();
        images.sprite = Resources.Load<Sprite>(p.GetImage());
    }

    IEnumerator waitPanel()
    {
        yield return new WaitForSeconds(2);
        panelAnimator.Play("OpenPerksGain");
    }

    public void OpenPanel()
    {
        if(turnHandler.activePlayer == 0)
        {
            StartCoroutine(waitPanel());
        }
        else
        {
            panelAnimator.Play("OpenPerksGain");
        }

        List<Perk> list = PerkProperties.GetThreeRandomPerks(turnHandler.activePlayer);

        Destroy(firstPerk.GetComponent<Perk>());
        firstPerk.AddComponent(list[0].GetType());
        LoadImage(firstPerk, list[0]);

        Destroy(secondPerk.GetComponent<Perk>());
        secondPerk.AddComponent(list[1].GetType());
        LoadImage(secondPerk, list[1]);

        Destroy(thirdPerk.GetComponent<Perk>());
        thirdPerk.AddComponent(list[2].GetType());
        LoadImage(thirdPerk, list[2]);
    }
}
