using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PerkOverviewHandler : MonoBehaviour {

    public GameObject PerkPrefab;
    public int columnCount = 12;
    public List<PerkCategory> PerkCategories;
    public List<GameObject> PerksGameObjects;
    public GameObject ActivePerk = null;
    public GameObject perkName;
    public GameObject perkClass;
    public GameObject selectedPerkImage;
    public int currentPlayer;

    private RectTransform rowRectTransform;
    private RectTransform containerRectTransform;
    private float width;
    private float ratio;
    private float height; 
    private float startPos = 0f;
    private bool controlsEnabled = false;
    private Animator confirmPanelAnimator;
    private bool confirmPanelOpen = false;
    private bool activatePerk = false;

    //Variables for Confirm Panel
    private GameObject yesButton;
    private GameObject noButton;
    private GameObject activeButton;

    private ControlsHandling controlsHandler;

    private float getHorizontal;
    private bool getSelect, getCancel;

    // Use this for initialization
    void Start () {
        controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
        controlsHandler.SetMenuOpen(true);

        PerkCategories = new List<PerkCategory>{
            new PerkCategory("Movement"),
            new PerkCategory("Medicine"),
            new PerkCategory("Cannon")
        };

        currentPlayer = PlayerPrefs.GetInt("Player");
        List<Perk> PerksList = PerkProperties.GetAllPerks();
        PerkCategories = PerkProperties.GetAllPerkCategories();
        PerkProperties.GetActivePerks(currentPlayer);

        selectedPerkImage = GameObject.Find("PerkSelected");
        perkName = GameObject.Find("PerkName");
        perkClass = GameObject.Find("PerkClass");

        //Set variables for Confirm Panel
        confirmPanelAnimator = GameObject.Find("ConfirmPanel").GetComponent<Animator>();
        yesButton = GameObject.Find("YesButton");
        noButton = GameObject.Find("NoButton");
        activeButton = noButton;

        //Get the necessary RectTransforms
        rowRectTransform = PerkPrefab.GetComponent<RectTransform>();
        containerRectTransform = gameObject.GetComponent<RectTransform>();

        //Calculate the dimension of a perk by the container dimensions
        width = containerRectTransform.rect.width / (columnCount * PerkCategories.Count);
        ratio = width / rowRectTransform.rect.width;
        height = rowRectTransform.rect.height * ratio;
        
        //Render all Perks per Category
        foreach (PerkCategory cat in PerkCategories)
        {
            CreatePerkGroupOverview(cat, PerksList.FindAll(p => p.category.name == cat.name));
            startPos += width * (columnCount + 0.3f);
        }
    }

    private void Update()
    {
        listenControls();
        ControlListener();
    }

    void listenControls()
    {
        getHorizontal = controlsHandler.getMenuHorizontal;
        getSelect = controlsHandler.getMenuSelect;
        getCancel = controlsHandler.getMenuCancel;
    }

    private void CreatePerkGroupOverview(PerkCategory cat, List<Perk> perks)
    {
        int j = 0;
        for (int i = 0; i < perks.Count; i++)
        {
            if (i % columnCount == 0)
                j++;

            //Create a new Perk from the PerkPrefab
            GameObject newPerk = Instantiate(PerkPrefab) as GameObject;
            newPerk.name = perks[i].GetName();
            newPerk.transform.SetParent(gameObject.transform);

            //Load the Image
            Image[] images = newPerk.GetComponents<Image>();
            images[0].sprite = Resources.Load<Sprite>(perks[i].GetImage());
            
            //Add the Specific Perk script to the object
            newPerk.AddComponent(perks[i].GetType());
            
            if (PerkProperties.HasPerkActivated(currentPlayer, newPerk.GetComponent<Perk>()))
            {
                newPerk.GetComponent<Perk>().SetActivated(true);
                SetOriginalColor(newPerk);
            }

            //Highlight the first Perk
            if (ActivePerk == null)
            {
                ActivePerk = newPerk;
                HighlightPerk(ActivePerk);
            }

            //Move the new Perk to the desired location
            RectTransform rectTransform = newPerk.GetComponent<RectTransform>();
            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount) + width * 0.1f + startPos;
            float y = containerRectTransform.rect.height / 2 - height * j + height * 0.1f;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width - width * 0.1f;
            y = rectTransform.offsetMin.y + height - height * 0.1f;
            rectTransform.offsetMax = new Vector2(x, y);

            PerksGameObjects.Add(newPerk);
        }
    }

    public void ControlListener()
    {
        //Events won't keep firing if someone keeps a key pressed in.
        if (controlsEnabled)
        {
            controlsEnabled = false;
            float direction = getHorizontal;

            //Handle Confirm controls
            if(confirmPanelOpen)
                ConfirmControlsListiner(direction);

            //Handle Perk Overview controls
            else
            {
                if (getSelect && !confirmPanelOpen && !ActivePerk.GetComponent<Perk>().isActivated)
                    OpenConfirmPanel();
                
                else if (getCancel)
                    Application.LoadLevel("First Level");

                else if (direction != 0f)
                    NavigationControls(direction);
            }
                
        }

        controlsEnabled = true;
    }

    private void NavigationControls(float direction)
    {
        //Unhighlight the previously selected Perk
        SetOriginalColor(ActivePerk); 

        int index = GetPerkIndex(direction);
        if (direction > 0.1f && index < PerksGameObjects.Count)
            ActivePerk = PerksGameObjects[index];
        else if (direction < -0.1f && index >= 0)
            ActivePerk = PerksGameObjects[index];

        //Highlight the new Perk
        HighlightPerk(ActivePerk);
    }

    private void ConfirmControlsListiner(float direction)
    {
        //Navigate between the Yes and No buttons
        if (direction != 0)
            ConfirmNavigation();

        //Handle the Submit event
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //If the Yes button was submitted, activate the selected Perk
            if (activeButton == yesButton)
            {
                ActivePerk.GetComponent<Perk>().SetActivated(true);
                PerkProperties.SetActivePerk(currentPlayer, ActivePerk.GetComponent<Perk>());
                SetOriginalColor(ActivePerk);
            }

            CloseDialogue();
        }
    }

    private void ConfirmNavigation()
    {
        activatePerk = !activatePerk;
        if (activeButton != yesButton)
            SwitchButtonsHighlighted(yesButton, noButton);
        else
            SwitchButtonsHighlighted(noButton, yesButton);
    }

    private void OpenConfirmPanel()
    {
        confirmPanelAnimator.Play("OpenPerksConfirm");
        confirmPanelOpen = true;

    }

    private void SetOriginalColor(GameObject perk)
    {
        Color originalColor = PerkPrefab.GetComponent<Image>().color;
        if (perk.GetComponent<Perk>().IsActivated())
            originalColor.a = 1f;

        perk.GetComponent<Image>().color = originalColor;
    }

    private void HighlightPerk(GameObject perk)
    {
        ActivePerk.GetComponent<Image>().color = Color.blue;
        selectedPerkImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(ActivePerk.GetComponent<Perk>().GetImage());
        perkName.GetComponent<Text>().text = ActivePerk.GetComponent<Perk>().GetName();
        perkClass.GetComponent<Text>().text = ActivePerk.GetComponent<Perk>().GetCategory();
    }

    private void SwitchButtonsHighlighted(GameObject toHighlight, GameObject unhighlight)
    {
        activeButton = toHighlight;

        toHighlight.GetComponent<Image>().color = new Color(226, 243, 255, 255);
        toHighlight.GetComponentInChildren<Text>().color = new Color(0, 0, 255, 255);

        unhighlight.GetComponent<Image>().color = new Color(0, 0, 255, 0);
        unhighlight.GetComponentInChildren<Text>().color = new Color(226, 243, 255);
    }

    private int GetPerkIndex(float direction)
    {
        return PerksGameObjects.FindIndex(p => p == ActivePerk) + (int) direction;
    }

    public void CloseDialogue()
    {
        confirmPanelAnimator.Play("ClosePerksConfirm");
        confirmPanelOpen = false;
        activeButton = noButton;
        SwitchButtonsHighlighted(noButton, yesButton);
    }
}
