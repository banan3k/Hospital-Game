using UnityEngine;
using System.Collections;

public class SpawnCannon : MonoBehaviour {

    public GameObject sharedCannon;

    private GameObject currentPlayer;
	
	public void spawnC (GameObject doctor)
    {
        sharedCannon.transform.position = doctor.transform.Find("Player").gameObject.GetComponent<NavMeshAgent>().transform.position;

        FireCannonball fireCannonball = sharedCannon.GetComponent<FireCannonball>();


        fireCannonball.SetDoctor(doctor);
        fireCannonball.resetCannonball();
        fireCannonball.attachDoctor();
        fireCannonball.animatorSettings();

        //reduce charge
    }
}
