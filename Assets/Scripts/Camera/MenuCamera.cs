using UnityEngine;
using System.Collections;

public class MenuCamera : MonoBehaviour {

	public GUIStyle style;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (-150, 50, -100);
		style.normal.textColor = Color.black;
	}

	bool forward = false;
	bool back = false;
	bool left = false;
	bool right = false;

	// Update is called once per frame
	void Update () {
		if (transform.position.x < 100 && transform.position.z <= -100) {
			right = true;
		} else {
			right = false;
		}

		if (transform.position.x >= 100 && transform.position.z < 40) {
			forward = true;
		} else {
			forward = false;
		}

		if (transform.position.z >= 40 && transform.position.x > -150) {
			left = true;
		} else {
			left = false;
		}

		if (transform.position.x <= -150 && transform.position.z > -100) {
			back = true;
		} else {
			back = false;
		}

		if (forward) {
			transform.position += Vector3.forward * Time.deltaTime * 30;
			transform.RotateAround (transform.position, Vector3.up, -38.5f * Time.deltaTime);
		}
		if (back) {
			transform.position += Vector3.back * Time.deltaTime * 30;
			transform.RotateAround (transform.position, Vector3.up, -38.5f * Time.deltaTime);
		}
		if (left) {
			transform.position += Vector3.left * Time.deltaTime * 30;
		} 
		if (right) {
			transform.position += Vector3.right * Time.deltaTime * 30;
		}

	}
	void OnGUI() {
		GUI.Label (new Rect (10, Screen.height - 20, 100, 20), "Music: http://www.bensound.com", style);
	}
}
