using UnityEngine;
using System.Collections;

public class FireCannonball : MonoBehaviour {

    public float firePower;
    public GameObject cannonball;
    public GameObject smoke;
    public GameObject fire;
    public GameObject doctor;
    public GameObject doctorContainer;
    public GameObject playerTarget;
    public Camera uiCamera;
    public CameraFollow camera;
    public CannonCamera cannonCamera;

    private CannonControls cannonControls;
    private Vector3 firingDirection;
    private Vector3 cbInitPos;
    private Rigidbody ballRB;
    private Animator doctorAnimator;

    private bool doctorIsFired = false;
    private bool doctorMoving = false;
    private bool doctorInCannon = false;
    private bool doctorHasCollided = false;

    public bool isPersonal = false;

	// Use this for initialization
	void Start () {
        cannonball = transform.Find("Cannonball").gameObject;
        ballRB = cannonball.GetComponent<Rigidbody>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        cannonCamera = GameObject.Find("Main Camera").GetComponent<CannonCamera>();
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        cannonControls = GetComponent<CannonControls>();
        ballRB.useGravity = false;
        cbInitPos = cannonball.transform.position;
        firePower = 0;
        smoke.SetActive(false);
        fire.SetActive(false);

        if (isPersonal)
        {
            transform.localScale = new Vector3(0, 0, 0);
            transform.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
        }

        //animatorSettings();
        //attachDoctor();
    }

    // Update is called once per frame
    void Update()
    {
        if (doctorInCannon)
            doctorCannonUpdate();
        if (doctorIsFired)
        {
            doctorStopCheck();
            if (doctorHasCollided && doctorMoving)
            {
                doctorCollidedUpdate();
            }
            else if (doctorMoving)
                doctorUpdate();
        }
    }

    public void animatorSettings()
    {
        doctorAnimator = doctor.GetComponentInChildren<Animator>();
        doctorAnimator.SetBool("IsWalking", false);
        doctorAnimator.SetBool("IsBall", false);
        doctorAnimator.SetBool("IsStandingUp", false);
    }

    public void attachDoctor()
    {
        doctorHasCollided = false;
        doctorInCannon = true;

        firingDirection = GetComponent<CannonPosToAnim>().getBarrelEndPoint();
        doctor.transform.position = cannonball.transform.position;
        doctor.transform.rotation = Quaternion.LookRotation(firingDirection);
        doctor.transform.rotation *= Quaternion.Euler(90, 0, 0);
        doctor.GetComponentInChildren<PlayerMovement>().enabled = false;
        doctor.GetComponentInChildren<NavMeshAgent>().enabled = false;
        playerTarget.GetComponentInChildren<TargetPicker>().enabled = false;
        doctorContainer.transform.Find("PlayerTarget").GetChild(1).GetComponent<Renderer>().enabled = false;
        camera.SetTarget(doctor);
        cannonCamera.SetCannon(this.gameObject);
        camera.enabled = false;
        cannonCamera.enabled = true;
    }

    public void hasCollided()
    {
        if(doctorIsFired)
        {
            doctorHasCollided = true;
            doctorAnimator.SetBool("IsBall", true);
        }
    }

    void doctorCannonUpdate()
    {
        firingDirection = GetComponent<CannonPosToAnim>().getBarrelEndPoint();
        doctor.transform.position = cannonball.transform.position;
        doctor.transform.rotation = Quaternion.LookRotation(firingDirection);
        doctor.transform.rotation *= Quaternion.Euler(90, 0, 0);
    }

    void doctorUpdate()
    {
        firingDirection = GetComponent<CannonPosToAnim>().getBarrelEndPoint();
        doctor.transform.position = cannonball.transform.position;
    }

    void doctorCollidedUpdate()
    {
        firingDirection = GetComponent<CannonPosToAnim>().getBarrelEndPoint();
        doctor.transform.position = cannonball.transform.position;
        doctor.transform.rotation = cannonball.transform.rotation;
    }

    void doctorStopCheck()
    {
        if(ballRB.velocity.magnitude < 1 && doctorHasCollided)
        {
            doctorMoving = false;
            doctor.transform.rotation = Quaternion.Slerp(doctor.transform.rotation, Quaternion.Euler(0, doctor.transform.eulerAngles.y, 0), Time.deltaTime * 3.0f);
        }
        if (!doctorMoving && Quaternion.Angle(Quaternion.Euler(0, doctor.transform.eulerAngles.y, 0), doctor.transform.rotation) <= 2)
        {
            doctor.transform.rotation = Quaternion.Euler(0, doctor.transform.eulerAngles.y, 0);
            unattachDoctor();
        }
    }

    void unattachDoctor()
    {
        doctorIsFired = false;
        doctorAnimator.SetBool("IsBall", false);
        doctorAnimator.Play("StandingUp");
        doctor.GetComponentInChildren<PlayerMovement>().enabled = true;
        doctor.GetComponentInChildren<NavMeshAgent>().enabled = true;
        playerTarget.GetComponentInChildren<TargetPicker>().enabled = true;
        doctorContainer.transform.Find("PlayerTarget").GetChild(1).GetComponent<Renderer>().enabled = true;
        playerTarget.transform.position = doctor.transform.position;
        playerTarget.GetComponent<TargetPicker>().goTo();
        camera.SetTarget(playerTarget);
        camera.SetDoctorInFlight(false);
        uiCamera.enabled = true;

        resetCannonball();
        EmptyDoctor();
        cannonCamera.EmptyCannon();
        cannonControls.ResetCannon();
        doctorHasCollided = false;

        if (isPersonal)
        {
            transform.localScale = new Vector3(0, 0, 0);
        }

        GameObject.Find("MenuHandler").GetComponent<MenuHandler>().forceEndTurn();
    }

    public void fireCannonball()
    {
        firingDirection = GetComponent<CannonPosToAnim>().getBarrelEndPoint();
        doctorInCannon = false;
        ballRB.useGravity = true;
        ballRB.AddForce(firingDirection * firePower);
        smoke.SetActive(true);
        fire.SetActive(true);
        doctorIsFired = true;
        doctorMoving = true;
        StartCoroutine("stopSmokingFire");

        camera.SetDoctorInFlight(true);
        uiCamera.enabled = false;
        camera.enabled = true;
        cannonCamera.enabled = false;
    }

    IEnumerator stopSmokingFire()
    {
        yield return new WaitForSeconds(5);
        smoke.SetActive(false);
        fire.SetActive(false);
    }

    public void resetCannonball()
    {
        //make sure to unattach player beforehand
        ballRB.useGravity = false;
        ballRB.velocity = new Vector3(0f, 0f, 0f);
        ballRB.angularVelocity = new Vector3(0f, 0f, 0f);
        cannonball.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        cannonball.transform.position = cbInitPos;
        animatorSettings();
    }

    public void SetDoctor(GameObject doctorContainer)
    {
        this.doctorContainer = doctorContainer;
        this.doctor = doctorContainer.transform.Find("Player").gameObject;
        this.playerTarget = doctorContainer.transform.Find("PlayerTarget").gameObject;

        if(isPersonal)
        {
            transform.localScale = new Vector3(1.5f, 1, 1.5f);
            cbInitPos = cannonball.transform.position;
            smoke.SetActive(true);
            StartCoroutine("stopSmokingFire");
        }
    }

    public bool HasDoctor()
    {
        return doctor != null;
    }

    private void EmptyDoctor()
    {
        this.doctorContainer = null;
        this.doctor = null;
        this.playerTarget = null;
    }
}
