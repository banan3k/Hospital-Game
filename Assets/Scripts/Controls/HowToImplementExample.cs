using UnityEngine;
using System.Collections;

public class HowTo : MonoBehaviour {

    public GameObject controlsHandler;

    private ControlsHandling controls;


    public float getHorizontalTest;

	// Use this for initialization
	void Start () {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
        getHorizontalTest = 0;

	
	}
	
	// Update is called once per frame
	void Update () {
        getHorizontalTest = controls.getHorizontal;
	}
}
