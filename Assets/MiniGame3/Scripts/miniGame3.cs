using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class miniGame3 : MonoBehaviour {
	public Transform card;
	public GameObject myBase;
	public Transform myCamera;
	GameObject obj;	
	public Vector2 grid;
	int[,] valueOfCard;
	int lastValue;
	Vector3 position;
	Transform cardSelected;
    public Text finalText;
    int endOfGame;
    public int difficulty;
    int tmpXCardSelected;
    int tmpYCardSelected;

    public GameObject controlsHandler;
    private ControlsHandling controls;
    private float getHorizontal, getVertical;
    private bool getSelect;

    // Use this for initialization
    void Start () {
        endOfGame = 0;
        //Create grid
        valueOfCard = new int[((int)grid.x),((int)grid.y)];
        int nbCard = ((int)grid.x) * ((int)grid.y);
        System.Random random = new System.Random();
        int randomX = random.Next(0, ((int)grid.x));
        int randomY = random.Next(0, ((int)grid.y));
        float red = UnityEngine.Random.value;
        float green = UnityEngine.Random.value;
        float blue = UnityEngine.Random.value;
        Color myColor = new Color(red, green, blue, 1.0f);
		SetDifficulty ();
        Color myColorToFind = new Color(red + (float)((12 - difficulty) * 0.01), green + (float)((12 - difficulty) * 0.01), blue + (float)((12 - difficulty) * 0.01), 1.0f);
        for (int x = 0; x != ((int)grid.x); x++)
        {
            for (int y = 0; y != ((int)grid.y); y++)
            {
                Transform t = Instantiate(card, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as Transform;
                obj = t.gameObject;
                obj.transform.parent = myBase.transform;
                if (x == randomX && y == randomY)
                {
                    obj.GetComponent<Renderer>().material.color = myColorToFind;
                    obj.transform.GetChild(0).GetComponent<Renderer>().material.color = myColorToFind;
                    valueOfCard[x, y] = 1;
                }
                else
                {
                    obj.GetComponent<Renderer>().material.color = myColor;
                    obj.transform.GetChild(0).GetComponent<Renderer>().material.color = myColor;
                    valueOfCard[x, y] = 0;
                }
                t.localPosition = new Vector3(x, 0, y);
                obj.name = (x.ToString("F0") + "x" + y.ToString("F0"));
                
            }
        }
        finalText.GetComponent<Text>().text = "You have to find the different coloured card !";
        tmpXCardSelected = 0;
        tmpYCardSelected = 0;
        setCardSelected();

        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
    }

    void listenControls()
    {
        getHorizontal = controls.getHorizontal;
        getVertical = controls.getVertical;
        getSelect = controls.getSelect;
    }

    // Update is called once per frame
    void Update()
    {
        listenControls();
        if (getVertical > 0.3 && tmpXCardSelected > 0)
        {
            Input.ResetInputAxes();
            setBackColor();
            tmpXCardSelected--;
            setCardSelected();
        }
        else if (getVertical < -0.3 && tmpXCardSelected < (int)grid.x - 1)
        {
            Input.ResetInputAxes();
            setBackColor();
            tmpXCardSelected++;
            setCardSelected();
        }
        else if (getHorizontal < -0.3 && tmpYCardSelected > 0)
        {
            Input.ResetInputAxes();
            setBackColor();
            tmpYCardSelected--;
            setCardSelected();
        }
        else if (getHorizontal > 0.3 && tmpYCardSelected < (int)grid.y - 1)
        {
            Input.ResetInputAxes();
            setBackColor();
            tmpYCardSelected++;
            setCardSelected();
        }
        else if (getSelect && endOfGame == 0)
        {
            int succeded;

            if (valueOfCard[tmpXCardSelected, tmpYCardSelected] == 1)
            {
                finalText.GetComponent<Text>().text = "You win ! :)";
                succeded = 1;
            }
            else
            {
                succeded = 2;
                finalText.GetComponent<Text>().text = "You loose ! :(";
            }

            PlayerPrefs.SetInt("minigameResults", succeded);
            Application.LoadLevel("First Level");

            endOfGame = 1;
        }
    }

	void SetDifficulty() {
		difficulty = PlayerPrefs.GetInt ( "difficulty", difficulty) * 2;
	}

    void setBackColor()
    {
        GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).GetComponent<Renderer>().material.color = GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    void setCardSelected()
    {
        GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1.0f);
    }
}
