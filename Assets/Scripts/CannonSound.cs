using UnityEngine;
using System.Collections;

public class CannonSound : MonoBehaviour {
	
	public AudioClip shootSound1;
	public AudioClip shootSound2;
	public AudioClip bounceSound1;
	public AudioClip turningSound1;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	void Awake () {
		source = GetComponent<AudioSource>();
	}	

	void Update() {
		/*if(Input.GetKeyUp(KeyCode.Space)) {
			PlaySound(1);
		}
		if(Input.GetKeyUp(KeyCode.W)) {
			PlaySound(2);
		}
		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
			PlaySound(3);
		}*/
	}
	//1 = cannonshot, 2 = bouncing off walls, 3 = turning the cannon
	public void PlaySound(int a) {
		float vol;
		switch (a) {
			case 1 :
				vol = Random.Range (volLowRange, volHighRange);
				float choose = Random.Range (0.0f, 1.0f);
				if (choose > 0.5f) {
					source.PlayOneShot (shootSound1, vol);
				} else {
					source.PlayOneShot (shootSound2, vol);
				}
			break;
			case 2 :
				vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (bounceSound1, vol);
			break;
			case 3 :
				vol = Random.Range (volLowRange, volHighRange);
				source.PlayOneShot (turningSound1, vol);
			break;
		}
	}
}
