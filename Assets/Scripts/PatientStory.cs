using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PatientStory : MonoBehaviour {

	//to make this visible, make the bool show true when clicking on a patient

	public string patientName;
	public string disease;
	public int age;
	public string backgroundStory;
	public int difficulty;

    public int patientNumber = 1;
    private int lives;
    private bool saved;
    private bool dead;

    private ControlsHandling controlsHandler;
    private LoadMinigame loadingMini;
    private Animator animator;
    private GameObject patientInfo;
    private GameObject backgroundInfo;
    private GameObject saveButton;
    private GameObject closeButton;
    private GameObject activeButton;
    private bool patientChartOpen = false;

    private string[] _names = {
		"Alyssa Villagomez",
		"Ardella Howes",
		"Daniele Bacchus",
		"Elinor Filippone",
		"Sherri Corney",
		"Jamee Huss",
		"Daphine Mccrady",
		"Tory Lamantia",
		"Gwendolyn Kyer",
		"Shizuko Risinger",
		"Vivian Salomon",
		"Brandon Wanke",
		"Kathern Mattinson",
		"Evia Boshears",
		"Kaila Gillam",
		"Odelia Quinlan",
		"Chase Kozak",
		"Jamaal Kohlmeier",
		"Victoria Christopherso",
		"Francesco Gauger"
	};

	private string[] _diseases = {
		"Headache",
		"Toothache",
		"Dizziness",
		"Alcohol poisoning",
		"Overwork",
		"Insomnia",
		"Saw UFO",
		"Age",
		"Obesity",
		"Fever",
		"Runny nose",
		"Stupidity",
		"Laziness",
		"Hallucinations",
		"Fear beetles",
		"Too much party",
		"Fracture",
		"Wound",
		"Bitten by a wolf",
		"Faking",
		"Amnesia",
		"Alzheimer",
		"Vaccine",
		"Stomach ache",
		"Dandruff",
		"Surfeit",
		"Shopaholic",
		"Shot",
		"Football fight fans",
		"Constipation",
		"Broken heart",
		"Suicide attempt",
		"Meat addiction",
		"Trauma",
		"Depression",
		"Fall from a horse",
		"Taking part in mafia war",
		"Fatigue",
		"Blindness"
	};

	private string[] _background = {
		"From a small town, came to the city and tried to jump a fence. Did not jump high enough.",
		"A real city person, very dull but rich.",
		"Was born in a wealthy family in a developing capital. He lived out of trouble until he was about 18 years old, but at that point things changed.",
		"Studied a lot and was among the most popular people. With the help of great friends, succeeded in a extraordinary world. But with capability and eagerness, there's nothing to can be stopped from accomplishing all goals. Could quickly become a true inspiration for many."
	};

	void Start () {
		GenerateStory();
		style.normal.textColor = Color.black;
		style.wordWrap = true;

	    controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
	    loadingMini = GameObject.Find("Turn Handler").GetComponent<LoadMinigame>();
	    animator = GameObject.Find("PatientChart").GetComponent<Animator>();
        patientInfo = GameObject.Find("PatientInfo");
        backgroundInfo = GameObject.Find("BackgroundInfo");
        saveButton = GameObject.Find("SaveButton");
        closeButton = GameObject.Find("CloseButton");
	    activeButton = closeButton;

	    ResetPanel();
    }

	void GenerateStory()
	{
	    patientName = _names [Random.Range (0, _names.Length)];
		disease = _diseases [Random.Range (0, _diseases.Length)];
		age = Random.Range (0, 100);
		backgroundStory = _background [Random.Range (0, _background.Length)];

        if(PlayerPrefs.HasKey("patient" + patientNumber + "status"))
            return;

        //Status 1 means that the patient is still sick
        PlayerPrefs.SetInt("patient" + patientNumber + "status", 0);

        difficulty = Random.Range(1, 4);
        PlayerPrefs.SetInt("patient" + patientNumber + "difficulty", difficulty);

        lives = Random.Range(1, 8);
        PlayerPrefs.SetInt("patient" + patientNumber + "lives", lives);
    }

    private void SwitchButtonsHighlighted(GameObject toHighlight, GameObject unhighlight)
    {
        unhighlight.transform.Find("Panel").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        toHighlight.transform.Find("Panel").GetComponent<Image>().color = new Color(226, 243, 255, 255);

        activeButton = toHighlight;
    }

    public Texture backgroundT;
	public bool show = false;
	public GUIStyle style;

    private void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        bool selected = controlsHandler.getMenuSelect;

        if(!patientChartOpen)
            return;

        if (direction != 0f)
        {
            SwitchButtons();
            Input.ResetInputAxes();
        }
        else if (selected)
        {
            StartCoroutine(OnSubmit());
        }
    }

    private void SwitchButtons()
    {
        if(activeButton.Equals(saveButton))
            SwitchButtonsHighlighted(closeButton, saveButton);
        else
            SwitchButtonsHighlighted(saveButton, closeButton);
    }

    private IEnumerator OnSubmit()
    {
        ClosePatientChart();

        yield return new WaitForSeconds(1);

        ResetPanel();

        //Debug.Log(activeButton.name);
        //Debug.Log(activeButton.Equals(saveButton));

        if (activeButton.Equals(saveButton))
            loadingMini.LoadRandomMinigame(PlayerPrefs.GetInt("Player"), patientNumber);
        else
            activeButton = closeButton;
    }

    public void OpenPatientChart()
    {
        animator.Play("PatientChartOpen");
        patientInfo.GetComponent<Text>().text = "Age: " + age + "\nName: " + patientName + "\nAilment: " + disease;
        backgroundInfo.GetComponent<Text>().text = backgroundStory;

        patientChartOpen = true;
        controlsHandler.SetMenuOpen(true);
    }

    public void ClosePatientChart()
    {
        animator.Play("PatientChartClose");
        patientChartOpen = false;
        controlsHandler.SetMenuOpen(false);
    }

    private void ResetPanel()
    {
        saveButton.transform.Find("Panel").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        closeButton.transform.Find("Panel").GetComponent<Image>().color = new Color(226, 243, 255, 255);
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public int GetPatientNumber()
    {
        return patientNumber;
    }

    public int GetLives()
    {
        return lives;
    }

    public void SubstractLife()
    {
        if(lives > 0)
            lives--;
    }

    public bool IsSaved()
    {
        return saved;
    }

    public void SetIsSaved(bool isSaved)
    {
        saved = isSaved;
    }

    public bool IsDead()
    {
        return dead;
    }

    public void SetIsDead(bool isDead)
    {
        this.dead = isDead;
    }
}
