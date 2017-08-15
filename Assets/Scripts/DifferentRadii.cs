using UnityEngine;
using System.Collections;

public class DifferentRadii : MonoBehaviour {

	public GameObject Player;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	public bool BetweenRadii(Vector3 center, float radius, Vector3 other) {
		float dist = Vector3.Distance(center, other);
		if (dist <= radius) {
			return true;
		} else {
			return false;
		}
	}
}
