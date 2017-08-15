using UnityEngine;
using System.Collections;

public class LookupObjects : MonoBehaviour {

    public float distance = 10f;
    public GameObject[] patientList;
    public GameObject[] cannonList;
    public GameObject selectedPatientContainer;
    public GameObject selectedCannon;
    
    private TurnHandler turnHandler;

    private ControlsHandling controlsHandler;

    // Use this for initialization
    void Start()
    {
        patientList = GameObject.FindGameObjectsWithTag("Patient");
        cannonList = GameObject.FindGameObjectsWithTag("Cannon");
        turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();

        controlsHandler = GameObject.Find("ControlsHandler").GetComponent<ControlsHandling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controlsHandler.getSelectRelease)
            return;

        GameObject doctor = turnHandler.GetActivePlayer();

        CheckIfPatientIsInReach(doctor);
        CheckIfCannonIsInReach(doctor);
    }

    private void CheckIfPatientIsInReach(GameObject doctorContainer)
    {
        float shortestDistance = int.MaxValue;
        GameObject doctor = doctorContainer.transform.Find("Player").gameObject;

        foreach (GameObject patient in patientList)
        {
            float patientDistance = Vector3.Distance(patient.transform.position, doctor.transform.position);

            if (!(patientDistance < shortestDistance))
                continue;

            shortestDistance = patientDistance;
            selectedPatientContainer = patient;
        }

        PatientStory patientStory = selectedPatientContainer.GetComponentInChildren<PatientStory>();
        int patientStatus = PlayerPrefs.GetInt("patient" + patientStory.GetPatientNumber() + "status");
        if (Vector3.Distance(selectedPatientContainer.transform.position, doctor.transform.position) < 6 && patientStatus == 0)
        {
            selectedPatientContainer.GetComponentInChildren<PatientStory>().OpenPatientChart();
        }   
    }

    private void CheckIfCannonIsInReach(GameObject doctorContainer)
    {
        float shortestDistance = int.MaxValue;
        GameObject doctor = doctorContainer.transform.Find("Player").gameObject;

        foreach (GameObject cannon in cannonList)
        {
            float cannonDistance = Vector3.Distance(cannon.transform.position, doctor.transform.position);

            if (!(cannonDistance < shortestDistance))
                continue;

            shortestDistance = cannonDistance;
            selectedCannon = cannon;
        }

        if (Vector3.Distance(selectedCannon.transform.position, doctor.transform.position) < 3)
        {
            FireCannonball cannonball = selectedCannon.GetComponent<FireCannonball>();
            cannonball.SetDoctor(doctorContainer);
            cannonball.attachDoctor();
            cannonball.animatorSettings();
        }
    }
}
