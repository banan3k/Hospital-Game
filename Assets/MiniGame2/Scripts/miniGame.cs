using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class miniGame : MonoBehaviour {

	public Transform card;
	public GameObject myBase;
	public Transform myCamera;
	int A, B;
	GameObject obj;	
	public Vector2 grid;
	int[,] valueOfCard;
	int lastValue;
	Vector3 position;
	public int restOfCard;
	public float time;
	float timeLock;
	int selected;
    public Texture2D[] myTextures;
    private Texture2D tmpTexture;
    public int timeToWin;
    public Text finalText;
    int tmpXCardSelected;
    int tmpYCardSelected;
    int XCardSelected1;
    int YCardSelected1;
    int XCardSelected2;
    int YCardSelected2;

    public GameObject controlsHandler;
    private ControlsHandling controls;
    private float getHorizontal, getVertical;
    private bool getSelect;

    // Use this for initialization
    void Start () {
        AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
        //Get all images in prefabs/images
        myTextures = new Texture2D[System.IO.Directory.GetFiles("Assets/Resources/cardTexture", "*.jpg").Length];
        for (int i = 0; i != System.IO.Directory.GetFiles("Assets/Resources/cardTexture", "*.jpg").Length; i++)
        {
            Texture2D tmpTexture = (Texture2D)Resources.Load(("cardTexture/") +(i + 1).ToString(), typeof(Texture2D));
            myTextures[i] = tmpTexture;
        }

        //Create grid
        valueOfCard = new int[((int)grid.x),((int)grid.y)];
        int myTmpTexture = 0;
        int pair = 0;
        int nbCard = 0;
        time = 0;
        while (nbCard != ((int)grid.x) * ((int)grid.y))
        {
            System.Random random = new System.Random();
            int randomX = random.Next(0, ((int)grid.x));
            int randomY = random.Next(0, ((int)grid.y));
            if (valueOfCard[randomX, randomY] == 0)
            {
                Transform t = Instantiate(card, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 90)) as Transform;
                obj = t.gameObject;
                obj.transform.parent = myBase.transform;
                obj.transform.GetComponent<Renderer>().material.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
                //Change card's texture
                obj.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = myTextures[myTmpTexture];
                pair++;

                t.localPosition = new Vector3(randomX, 0, randomY);
                obj.name = (randomX.ToString("F0") + "x" + randomY.ToString("F0"));
                valueOfCard[randomX, randomY] = myTmpTexture + 1;

                //If there is one pair, new texture
                if (pair == 2)
                {
                    myTmpTexture++;
                    pair = 0;
                }
                nbCard++;
            }
        }
        selected = 0;
        restOfCard = ((int)grid.x) *((int)grid.y);
        lastValue = -1;
        finalText.GetComponent<Text>().text = "Time to win : "+ timeToWin + " seconds";

        tmpXCardSelected = 0;
        tmpYCardSelected = 0;

        GameObject.Find(tmpXCardSelected+"x"+tmpYCardSelected).GetComponent<Renderer>().material.color = new Color(0.8f, 0, 0, 1.0f);
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
        time += Time.deltaTime;
        finalText.GetComponent<Text>().text = "Time to win : " + timeToWin + " seconds\n Time : " + time.ToString("F0") + " seconds";
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
        if (getSelect)
        {
            if (lastValue == -1 && selected == 0 && valueOfCard[tmpXCardSelected, tmpYCardSelected] != -1)
            {
                GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).transform.rotation = Quaternion.Euler(180, 0, 90);
                selected++;
                XCardSelected1 = tmpXCardSelected;
                YCardSelected1 = tmpYCardSelected;
                lastValue = valueOfCard[tmpXCardSelected, tmpYCardSelected];
            }
            else if (selected == 1 && valueOfCard[tmpXCardSelected, tmpYCardSelected] != -1 && ((tmpXCardSelected != XCardSelected1) || (tmpYCardSelected != YCardSelected1)))
            {
                GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).transform.rotation = Quaternion.Euler(180, 0, 90);
                selected++;
                timeLock = (time + 1);
                XCardSelected2 = tmpXCardSelected;
                YCardSelected2 = tmpYCardSelected;
            }
        }
        //If two card are selected
        if (selected == 2)
        {
            if (lastValue != valueOfCard[XCardSelected2, YCardSelected2])
            {
                if (time >= timeLock)
                {
                    GameObject.Find(XCardSelected1 + "x" + YCardSelected1).transform.rotation = Quaternion.Euler(0, 0, 90);
                    GameObject.Find(XCardSelected2 + "x" + YCardSelected2).transform.rotation = Quaternion.Euler(0, 0, 90);
                    lastValue = -1;
                    selected = 0;
                }
            }
            else
            {
                valueOfCard[XCardSelected2, YCardSelected2] = -1;
                valueOfCard[XCardSelected1, YCardSelected1] = -1;
                restOfCard = restOfCard - 2;
                lastValue = -1;
                selected = 0;
            }
        }
        //If no more card
        if (restOfCard == 0)
        {
            int succeded;

            if (time < timeToWin)
            {
                succeded = 1;
                finalText.GetComponent<Text>().text = "You win :)";
            }   
            else
            {
                succeded = 2;
                finalText.GetComponent<Text>().text = "You lose :(";
            }

            PlayerPrefs.SetInt("minigameResults", succeded);
            Application.LoadLevel("First Level");
        }
    }
    
    void setBackColor()
    {
        GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).GetComponent<Renderer>().material.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
    }

    void setCardSelected()
    {
        GameObject.Find(tmpXCardSelected + "x" + tmpYCardSelected).GetComponent<Renderer>().material.color = new Color(0.8f, 0, 0, 1.0f);
    }
}
