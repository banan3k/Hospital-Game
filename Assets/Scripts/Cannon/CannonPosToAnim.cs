using UnityEngine;
using System.Collections;

public class CannonPosToAnim : MonoBehaviour
{

    public GameObject controlsHandler;

    private ControlsHandling controls;

    public float rotationSpeedBarrelHorizontal;
    public float rotationSpeedBarrelVertical;
    public float rotationSpeedWheels;
    public float barrelResetSpeed;
    public float firePower;
    public float firePowerEffect;
    public float shakeIntensity;

    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject barrel;
    public GameObject barrelEnd;

    public bool canVertical;
    public bool canHorizontal;
    public bool charging;
    public bool firing;
    public bool fired;
    public bool isColliding;


    public float getHorizontal;
    public float getVertical;
    public bool collideOnce;
    public bool checkInitialPos;
    public Quaternion barrelInitialRot;
    public Vector3 barrelInitialPos;

    // Use this for initialization
    void Start()
    {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
        collideOnce = true;
        checkInitialPos = true;
        rotationSpeedBarrelHorizontal = 10;
        rotationSpeedBarrelVertical = 8;
        rotationSpeedWheels = 15;
        barrelResetSpeed = 1;
        firePowerEffect = 0.01f;
    }

    Vector3 getPosWheels()
    {
        Vector3 posWheels = (wheel1.transform.position - wheel2.transform.position) * 0.5f + wheel2.transform.position;
        return posWheels;
    }

    Vector3 getRotBarrel()
    {
        Vector3 rotBarrel = wheel1.transform.position - wheel2.transform.position;
        return rotBarrel;
    }

    public Vector3 getBarrelEndPoint()
    {
        Vector3 barrelEndPoint = barrelEnd.transform.position - barrel.transform.position;
        return barrelEndPoint;
    }

    void rotateHorizontal()
    {
        transform.RotateAround(getPosWheels(), Vector3.up, getHorizontal * Time.deltaTime * rotationSpeedBarrelHorizontal);
        wheel1.transform.Rotate(getHorizontal * Time.deltaTime * rotationSpeedWheels, 0, 0);
        wheel2.transform.Rotate(-getHorizontal * Time.deltaTime * rotationSpeedWheels, 0, 0);
    }

    void rotateVertical()
    {
        barrel.transform.RotateAround(getPosWheels(), getRotBarrel(), getVertical * Time.deltaTime * rotationSpeedBarrelHorizontal);
    }

    void shakeAnim()
    {
        barrel.transform.rotation = barrelInitialRot;
        float ranX = Random.Range(-1f, 1f) * firePower * shakeIntensity;
        float ranY = Random.Range(-1f, 1f) * firePower * shakeIntensity;
        float ranZ = Random.Range(-1f, 1f) * firePower * shakeIntensity;
        barrel.transform.Rotate(ranX, ranY, ranZ);
    }

    void fireAnim()
    {
        Vector3 backfire = barrel.transform.position - barrelEnd.transform.position;
        barrel.transform.Translate(backfire * firePower * firePowerEffect, Space.World);
    }

    void resetBarrel()
    {
        float step = barrelResetSpeed * Time.deltaTime;
        barrel.transform.position = Vector3.MoveTowards(barrel.transform.position, barrelInitialPos, step);
        barrel.transform.rotation = barrelInitialRot;
        if (barrel.transform.position == barrelInitialPos)
            firing = false;
    }

    void Update()
    {
        if (canHorizontal)
            rotateHorizontal();
        if (canVertical)
            rotateVertical();
        if (charging)
        {
            if (checkInitialPos)
            {
                barrelInitialRot = barrel.transform.rotation;
                barrelInitialPos = barrel.transform.position;
                checkInitialPos = false;
            }
            shakeAnim();
        }
        if (firing)
        {
            if(!fired)
            {
                charging = false;
                checkInitialPos = true;
                fireAnim();
                firePower = 0;
                fired = false;
            }
            resetBarrel();
        }
    }

    //best for physics
    void FixedUpdate()
    {
        if (isColliding && collideOnce)
        {
            getHorizontal *= -5;
            getVertical *= -5;
            collideOnce = false;
        }
        else if (!isColliding)
        {
            collideOnce = true;
        }
    }

    void registerControls()
    {
        getHorizontal = controls.getHorizontal;
        getVertical = controls.getVertical;
    }

    // LateUpdate is called at the very end
    void LateUpdate()
    {
        if (!isColliding)
        {
            registerControls();
        }
    }

    public float GetCannonPower()
    {
        return firePower;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
}
