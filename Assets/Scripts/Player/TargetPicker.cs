using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TargetPicker : MonoBehaviour
{

    public float surfaceOffset = 1.5f;
    public GameObject setTargetOn;

    public float movementSpeed = 6f;

    private PlayerMovement player;

    public CameraFollow temp;

    public GameObject eye;
    private Image eyeRenderer;

    private GameObject transformClone;

    private ControlsHandling controlsHandler;

    private float getHorizontal, getVertical;
    private bool getSelect;

    private Vector3 movement;

    private float blueRadius, redRadius;

    private bool panning = false;

    private void Start()
    {
        controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
        player = setTargetOn.GetComponent<PlayerMovement>();
        changeColour(Color.green);
        eyeRenderer = eye.GetComponent<Image>();
        panMode(false);

        transformClone = new GameObject("Position");

        transform.position = setTargetOn.transform.position;

        //temp
        blueRadius = 12f;
        redRadius = 21f;
        //temp
    }

    void listenControls()
    {
        getSelect = controlsHandler.getSelect;
        getHorizontal = controlsHandler.getHorizontal;
        getVertical = controlsHandler.getVertical;
    }

    void Update()
    {
        if(player.HasCurrentTurn())
        {
            listenControls();
            moveCursor();
            checkRadius();
        }
        else
        {
            transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        }
    }

    void checkRadius()
    {
        //get radii
        float distance = Vector3.Distance(transform.position, setTargetOn.transform.position);

        if(distance > redRadius)
        {
            panMode(true);
        }
        else if(distance > blueRadius)
        {
            panMode(false);
            outerRadius();
        }
        else
        {
            panMode(false);
            innerRadius();
        }
    }

    void moveCursor()
    {
        movement.Set(getHorizontal, 0f, getVertical);
        movement = movement.normalized * movementSpeed * Time.deltaTime;

        transform.position = transform.position + movement;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!getSelect && !player.HasCurrentTurn() && !player.CanWalk())
        {

        }
        else if (getSelect && !panning && player.HasCurrentTurn())
        {
            goTo();
        }
        else if(getSelect && panning && player.HasCurrentTurn())
        {
            transform.position = setTargetOn.transform.position;
        }
        else if(player.HasCurrentTurn())
        {
            noClick();
        }
    }

    void noClick()
    {
        if(player.PlayerReachedTarget())
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

            temp = cam.GetComponent<CameraFollow>();
            temp.SetTarget(gameObject);
        }
        temp.clicked = false;
    }

    public void goTo()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        GetComponent<Animator>().Play("Select");

        transformClone.transform.position = transform.position;

        setTargetOn.SendMessage("SetTarget", transformClone.transform);

        temp = cam.GetComponent<CameraFollow>();
        temp.SetTarget(setTargetOn);
        temp.clicked = true;
    }

    void changeColour(Color col)
    {
        transform.GetChild(1).GetComponent<Renderer>().material.color = col;
    }

    void innerRadius()
    {
        changeColour(Color.green);
    }

    void outerRadius()
    {
        changeColour(Color.red);
    }

    void panMode(bool on)
    {
        if (on)
        {
            eyeRenderer.enabled = true;
            transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            movementSpeed = 12f;
            panning = true;
        }
        else
        {
            eyeRenderer.enabled = false;
            transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            movementSpeed = 6f;
            panning = false;
        }
    }
}
